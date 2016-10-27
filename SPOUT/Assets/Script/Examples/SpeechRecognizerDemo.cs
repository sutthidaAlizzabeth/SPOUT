using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class SpeechRecognizerDemo : MonoBehaviour {

	private const string TAG = "[SpeechRecognizerDemo]: ";

	private SpeechPlugin speechPlugin;	
	private bool hasInit = false;
	public Text resultText;
	public Text partialResultText;
	public Text statusText;

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
			textToSpeechPlugin.OnGetLocaleCountry+=OnGetLocaleCountry;
			textToSpeechPlugin.OnChangeLocale+=OnSetLocale;
			textToSpeechPlugin.OnStartSpeech+=OnStartSpeech;
			textToSpeechPlugin.OnEndSpeech+=OnEndSpeech;
			textToSpeechPlugin.OnErrorSpeech+=OnErrorSpeech;	
		}
	}

	private void RemoveTextToSpeechPluginListener(){
		if(textToSpeechPlugin!=null){
			//remove text to speech listener
			textToSpeechPlugin.OnInit-=OnInit;
			textToSpeechPlugin.OnGetLocaleCountry-=OnGetLocaleCountry;
			textToSpeechPlugin.OnChangeLocale-=OnSetLocale;
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

	public void StartListening(){
		bool isSupported = speechPlugin.CheckSpeechRecognizerSupport();

		if(isSupported){
			//number of possible results
			//Note: sometimes even you put 5 numberOfResults, there's a chance that it will be only 3 or 2
			//it is not constant.

			// enable beep
			speechPlugin.EnableBeep(true);

			// enable offline
			//speechPlugin.EnableOffline(true);

			// enable partial Results
			speechPlugin.EnablePartialResult(true);
			
			int numberOfResults = 5;
			speechPlugin.StartListening(numberOfResults);
			
			//by activating this, the Speech Recognizer will start and you can start Speaking or saying something 
			//speech listener will stop automatically especially when you stop speaking or when you are speaking 
			//for a long time
		}else{
			Debug.Log(TAG + "Speech Recognizer not supported by this Android device ");
		}
	}

	public void StartListeningNoBeep(){
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
			speechPlugin.StartListening(numberOfResults);
			///speechPlugin.StartListeningNoBeep(numberOfResults,true);
			
			//by activating this, the Speech Recognizer will start and you can start Speaking or saying something 
			//speech listener will stop automatically especially when you stop speaking or when you are speaking 
			//for a long time
		}else{
			Debug.Log(TAG + "Speech Recognizer not supported by this Android device ");
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

	private void OnDestroy(){
		RemoveSpeechPluginListener();
		RemoveTextToSpeechPluginListener();

		speechPlugin.StopListening();
		speechPlugin.DestroySpeechController();

		//call this of your not going to used TextToSpeech Service anymore
		textToSpeechPlugin.ShutDownTextToSpeechService();
	}

	private void UpdateStatus(string status){
		if(statusText!=null){
			statusText.text = String.Format("Status: {0}",status);	
		}
	}

	//SpeechRecognizer Events
	private void onReadyForSpeech(string data){
		if(speechPlugin!=null){
			//Disables modal
			speechPlugin.EnableModal(false);	
		}

		UpdateStatus(data.ToString());
	}

	private void onBeginningOfSpeech(string data){
		UpdateStatus(data.ToString());
	}

	private void onEndOfSpeech(string data){
		UpdateStatus(data.ToString());
	}

	private void onError(int data){
		SpeechRecognizerError error = (SpeechRecognizerError)data;
		UpdateStatus(error.ToString());
		//statusText.text =  String.Format("Status: {0}",error.ToString());

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
				Debug.Log( TAG + "partial possibleResults " + possibleResults );
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
		}
	}

	private void OnGetLocaleCountry(string localeCountry){
		Debug.Log( TAG + "OnGetLocaleCountry localeCountry: " + localeCountry);
	}
	
	private void OnSetLocale(int status){
		Debug.Log(TAG + "OnSetLocale status: " + status);
		if(status == 1){
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