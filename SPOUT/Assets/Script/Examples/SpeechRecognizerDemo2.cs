using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class SpeechRecognizerDemo2 : MonoBehaviour {
	
	private const string TAG = "[SpeechRecognizerDemo]: ";
	
	private SpeechPlugin speechPlugin;	
	private bool hasInit = false;
	public Text resultText;
	public Text partialResultText;
	public Text statusText;

	//for Speech Recognizer to recognize your voice based on your country
	//hardcoded extra locale this value is depends on your mobile device if its available it will work
	//if not it will not work
	//public string currentExtraLocale = "ja-JP";
	public SpeechExtraLocale currentExtraLocale = SpeechExtraLocale.JP;

	public Text speechExtraLocaleText;
	public Slider speechExtaLocaleSlider;
	
	private TextToSpeechPlugin textToSpeechPlugin;
	
	// Use this for initialization
	void Start (){		
		speechPlugin = SpeechPlugin.GetInstance();
		speechPlugin.SetDebug(0);
		speechPlugin.Init();

		AddSpeechPluginListener();		

		
		textToSpeechPlugin = TextToSpeechPlugin.GetInstance();
		textToSpeechPlugin.SetDebug(0);
		textToSpeechPlugin.Initialize();

		AddTextToSpeechPluginListener();
	}

	private void OnEnable(){
		AddSpeechPluginListener();
		AddTextToSpeechPluginListener();
	}

	private void OnDisable(){
		RemoveSpeechPluginListener();
		RemoveTextToSpeechPluginListener();
	}

	private void AddSpeechPluginListener(){
		if(speechPlugin!=null){
			//add speech recognizer listener
			speechPlugin.onReadyForSpeech+=onReadyForSpeech;
			speechPlugin.onBeginningOfSpeech+=onBeginningOfSpeech;
			speechPlugin.onEndOfSpeech+=onEndOfSpeech;
			speechPlugin.onError+=onError;
			speechPlugin.onResults+=onResults;
			speechPlugin.onPartialResults+=onPartialResults;
		}
	}

	private void RemoveSpeechPluginListener(){
		if(speechPlugin!=null){
			//remove speech recognizer listener
			speechPlugin.onReadyForSpeech-=onReadyForSpeech;
			speechPlugin.onBeginningOfSpeech-=onBeginningOfSpeech;
			speechPlugin.onEndOfSpeech-=onEndOfSpeech;
			speechPlugin.onError-=onError;
			speechPlugin.onResults-=onResults;
			speechPlugin.onPartialResults-=onPartialResults;
		}
	}

	private void AddTextToSpeechPluginListener(){
		if(textToSpeechPlugin!=null){
			//add text to speech listener
			textToSpeechPlugin.OnInit+=OnInit;
			textToSpeechPlugin.OnStartSpeech+=OnStartSpeech;
			textToSpeechPlugin.OnEndSpeech+=OnEndSpeech;
			textToSpeechPlugin.OnErrorSpeech+=OnErrorSpeech;	
		}
	}

	private void RemoveTextToSpeechPluginListener(){
		if(textToSpeechPlugin!=null){
			//remove text to speech listener
			textToSpeechPlugin.OnInit-=OnInit;
			textToSpeechPlugin.OnStartSpeech-=OnStartSpeech;
			textToSpeechPlugin.OnEndSpeech-=OnEndSpeech;
			textToSpeechPlugin.OnErrorSpeech-=OnErrorSpeech;	
		}
	}
	
	private void OnApplicationPause(bool val){
		//for text to speech events
		if(textToSpeechPlugin!=null){
			if(hasInit){
				if(val){
					RemoveSpeechPluginListener();
					RemoveTextToSpeechPluginListener();
					textToSpeechPlugin.UnRegisterBroadcastEvent();
				}else{
					AddSpeechPluginListener();
					AddTextToSpeechPluginListener();
					textToSpeechPlugin.RegisterBroadcastEvent();
				}
			}
		}
	}

	//this is for debug or test purpose only to log available extra locale on adb using  "adb logcat -s Unity" comand on command prompt or terminal
	public void CheckSpeechRecognizerExtraLanguage(){
		string[] extraLanguageAvailable =  speechPlugin.GetExtraLanguage();
		foreach(string extraLocale in extraLanguageAvailable){
			Debug.Log( TAG + extraLocale );
		}
	}
	
	public void StartListeningWithExtraLanguage(){
		bool isSupported = speechPlugin.CheckSpeechRecognizerSupport();
		
		if(isSupported){
			//number of possible results
			//Note: sometimes even you put 5 numberOfResults, there's a chance that it will be only 3 or 2
			//it is not constant.

			// disable beep
			speechPlugin.EnableBeep(false);

			// enable offline
			//speechPlugin.EnableOffline(true);

			// enable partial Results
			speechPlugin.EnablePartialResult(true);
			
			int numberOfResults = 5;
			
			//if you are planning to change speech recognizer language and you wanted to use text to speech too
			//you must set the locale or extra locale, in our example we use japanese to compliment the 
			//speech recognizer japanese language
			string selectedExtraLocale = currentExtraLocale.GetDescription();

			SpeechLocale speechLocale = SpeechLocaleHelper.GetSpeechLocale(currentExtraLocale);
			if(speechLocale!=SpeechLocale.NONE){
				textToSpeechPlugin.SetLocale(speechLocale);
			}else{
				TTSLocaleCountry ttsLocaleCountry = SpeechLocaleHelper.GetTTSExtraLocale(currentExtraLocale);
				SetLocaleByCountry(ttsLocaleCountry);
			}
			
			//in this sample we use japanese because this languages are always depends on your mobile devices 
			//it may work or not work you can have a lot of extra language or very few
			
			//this is supposed to be how to do this but there's bug on google search and they don't fix it based on my 
			//research
			//speechPlugin.StartListeningWithExtraLanguage(numberOfResults,"ja-JP");
			
			//so we you this with hack fix instead
			//you can see that we use "ja" here for japanese language based on the above reference "ja-JP"
			speechPlugin.StartListeningWithHackExtraLanguage(numberOfResults,currentExtraLocale.GetDescription());
			
			//by activating this, the Speech Recognizer will start and you can start Speaking or saying something 
			//speech listener will stop automatically especially when you stop speaking or when you are speaking 
			//for a long time
		}else{
			Debug.Log("Speech Recognizer not supported by this Android device ");
		}
	}

	private void SetLocaleByCountry(TTSLocaleCountry ttsLocale){
		bool isLanguageAvailanble =  textToSpeechPlugin.CheckLocale(ttsLocale);
		if(isLanguageAvailanble){
			string countryISO2Alpha = textToSpeechPlugin.GetCountryISO2Alpha(ttsLocale);
			textToSpeechPlugin.SetLocaleByCountry(countryISO2Alpha);
		}else{
			//default to us if ttslocale is not available
			//some language will not be play for ex. if speech is set to japan but tts is not japanese it will not speak
			textToSpeechPlugin.SetLocale(SpeechLocale.US);
		}
	}
	
	//cancel speech
	public void CancelSpeech(){
		if(speechPlugin!=null){
			bool isSupported = speechPlugin.CheckSpeechRecognizerSupport();
			
			if(isSupported){			
				speechPlugin.Cancel();
			}
		}
		
		Debug.Log( TAG + " call CancelSpeech..  ");
	}
	
	public void StopListening(){
		if(speechPlugin!=null){
			speechPlugin.StopListening();
		}
		Debug.Log( TAG + " StopListening...  ");
	}
	
	public void StopCancel(){
		if(speechPlugin!=null){
			speechPlugin.StopCancel();
		}
		Debug.Log( TAG + " StopCancel...  ");
	}

	public void OnSpeechExtraLocaleSliderChange(){
		Debug.Log("[TextToSpeechDemo] OnExtraLocaleSliderChange");
		if(speechExtaLocaleSlider!=null){

			//update current extra locale here
			currentExtraLocale = (SpeechExtraLocale)speechExtaLocaleSlider.value;

			//update the status to notify user
			UpdateSpeechExtraLocale(currentExtraLocale);
		}
	}

	private void UpdateSpeechExtraLocale(SpeechExtraLocale ttsLocaleCountry){
		if(speechExtraLocaleText!=null){
			speechExtraLocaleText.text = String.Format("Extra Locale: {0}",ttsLocaleCountry);
		}
	}

	private void UpdateStatus(string status){
		if(statusText!=null){
			statusText.text = String.Format("Status: {0}",status);	
		}
	}

	public void NextExtraLocale(){
		if(speechExtaLocaleSlider!=null){
			if(speechExtaLocaleSlider.value < speechExtaLocaleSlider.maxValue){
				speechExtaLocaleSlider.value++;
			}
		}
	}
	
	public void PrevExtraLocale(){
		if(speechExtaLocaleSlider!=null){
			if(speechExtaLocaleSlider.value > 1){
				speechExtaLocaleSlider.value--;
			}
		}
	}
	
	private void OnDestroy(){
		RemoveSpeechPluginListener();
		RemoveTextToSpeechPluginListener();

		speechPlugin.StopListening();
		speechPlugin.DestroySpeechController();
		
		//call this of your not going to used TextToSpeech Service anymore
		textToSpeechPlugin.ShutDownTextToSpeechService();
	}
	
	//SpeechRecognizer Events
	private void onReadyForSpeech(string data){
		
		if(speechPlugin!=null){
			//Disables modal
			speechPlugin.EnableModal(false);	
		}

		if(statusText!=null){
			statusText.text =  String.Format("Status: {0}",data.ToString()); 
		}
	}
	
	private void onBeginningOfSpeech(string data){
		if(statusText!=null){
			statusText.text =  String.Format("Status: {0}",data.ToString()); 
		}
	}
	
	private void onEndOfSpeech(string data){
		if(statusText!=null){
			statusText.text =  String.Format("Status: {0}",data.ToString()); 
		}
	}
	
	private void onError(int data){
		if(statusText!=null){
			
			SpeechRecognizerError error = (SpeechRecognizerError)data;
			statusText.text =  String.Format("Status: {0}",error.ToString());
			//statusText.text =  String.Format("Status: {0}",data.ToString());
		}
		
		if(resultText!=null){
			resultText.text =  "Result: Waiting for result...";
		}
	}
	
	private void onResults(string data){
		if(resultText!=null){
			string[] results =  data.Split(',');
			Debug.Log( TAG + " result length " + results.Length);
			
			//when you set morethan 1 results index zero is always the closest to the words the you said
			//but it's not always the case so if you are not happy with index zero result you can always 
			//check the other index
			
			//sample on checking other results
			foreach( string possibleResults in results ){
				Debug.Log( TAG + " possibleResults " + possibleResults );
			}
			
			//sample showing the nearest result
			string whatToSay  = results.GetValue(0).ToString();
			string utteranceId  = "test-utteranceId";
			resultText.text =  string.Format("Result: {0}",whatToSay); 
			
			//check if Text to speech has initialized
			if(textToSpeechPlugin.hasInitialized()){
				//Text To Speech Sample Usage
				textToSpeechPlugin.SpeakOut(whatToSay,utteranceId);
			}
		}
	}

	private void onPartialResults( string data ){
		if(partialResultText!=null){
			string[] results =  data.Split(',');
			Debug.Log( TAG + " partial result length " + results.Length);

			//when you set morethan 1 results index zero is always the closest to the words the you said
			//but it's not always the case so if you are not happy with index zero result you can always 
			//check the other index

			//sample on checking other results
			foreach( string possibleResults in results ){
				Debug.Log( TAG + " partial possibleResults " + possibleResults );
			}

			//sample showing the nearest result
			string whatToSay  = results.GetValue(0).ToString();
			string utteranceId  = "test-utteranceId";
			partialResultText.text =  string.Format("Partial Result: {0}",whatToSay); 

			//check if Text to speech has initialized
			/*if(textToSpeechPlugin.hasInitialized()){
				//Text To Speech Sample Usage
				textToSpeechPlugin.SpeakOut(whatToSay,utteranceId);
			}*/
		}
	}
	//SpeechRecognizer Events
	
	
	//TextToSpeech Events
	private void OnInit(int status){
		Debug.Log(TAG + "OnInit status: " + status);
		
		if(status == 1){
			hasInit = true;
			textToSpeechPlugin.SetLocale(SpeechLocale.US);
			textToSpeechPlugin.SetPitch(1f);
		}
	}	

	
	private void OnStartSpeech(string utteranceId){
		Debug.Log(TAG + "OnStartSpeech utteranceId: " + utteranceId);
	}
	
	private void OnEndSpeech(string utteranceId){
		Debug.Log(TAG + "OnEndSpeech utteranceId: " + utteranceId);
	}
	
	private void OnErrorSpeech(string utteranceId){
		Debug.Log(TAG + "OnErrorSpeech utteranceId: " + utteranceId);
	}
	//TextToSpeech Events
}