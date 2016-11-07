using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizManagement: MonoBehaviour {

	private const string TAG = "[TextToSpeechDemo]: ";

	private Canvas canvas_main;
	private Canvas popup_correct;
	private Canvas popup_wrong;
	private Image panel;
	private Text dialog;
	private Dictionary<int, Dialog> allThemeDialog; //all dialog in this theme
	private int countDialog;// allThemeDialog.Count
	private string randomDialog; //get from allThemeDialog, and will show in scene
	private int countQuiz; //number of questions that user test
	private int randomKey; //random key
	private string message;
	static private int quizeId;

	private SpeechPlugin speechPlugin;
	private TextToSpeechPlugin textToSpeechPlugin;
	private float waitingInterval = 2f;

	private void Awake(){
		speechPlugin = SpeechPlugin.GetInstance();
		speechPlugin.SetDebug(0);

		textToSpeechPlugin = TextToSpeechPlugin.GetInstance();
		textToSpeechPlugin.SetDebug(0);
		textToSpeechPlugin.Initialize();

		textToSpeechPlugin.OnInit+=OnInit;
		textToSpeechPlugin.OnChangeLocale+=OnSetLocale;
		textToSpeechPlugin.OnStartSpeech+=OnStartSpeech;
		textToSpeechPlugin.OnEndSpeech+=OnEndSpeech;
		textToSpeechPlugin.OnErrorSpeech+=OnErrorSpeech;
	}

	// Use this for initialization
	void Start (){
		CheckTTSDataActivity();

		//get component from unity (game_quiz scene)
		canvas_main = GameObject.Find ("Canvas").GetComponent (typeof(Canvas)) as Canvas;
		popup_correct = GameObject.Find ("popup_correct").GetComponent (typeof(Canvas)) as Canvas;
		popup_wrong = GameObject.Find ("popup_wrong").GetComponent (typeof(Canvas)) as Canvas;
		panel = GameObject.Find ("Panel").GetComponent (typeof(Image)) as Image;
		dialog = GameObject.Find ("dialog").GetComponent (typeof(Text)) as Text;

		//get all dialog in this theme
		genAllThemeDialog ();

		//count amount of all dialog in allThemeDialog
		countDialog = allThemeDialog.Count;

		//start quiz with first question (เริ่มที่ข้อ 1) and random dialog to be a question
		countQuiz = 1;
		textToSpeechPlugin.RegisterBroadcastEvent();
		textToSpeechPlugin.SetSpeechRate (0.8f);
		textToSpeechPlugin.SetLocale (SpeechLocale.US);
		textToSpeechPlugin.SetPitch (1.0f);
		UpdateVolume (15);
		randomQuizDialog ();
	}

	private void OnApplicationPause(bool val){
		//for text to speech events
		if(textToSpeechPlugin!=null){
			if(textToSpeechPlugin.isInitialized()){
				if(val){
					textToSpeechPlugin.UnRegisterBroadcastEvent();
				}else{
					textToSpeechPlugin.RegisterBroadcastEvent();
				}
			}
		}
	}

	public void RemoveListener(){
		textToSpeechPlugin.UnRegisterBroadcastEvent();
	}

	//random a dialog from allThemeDialog (all dialog in choosed theme) to be a question
	public void randomQuizDialog(){
		//random only 10 questions for a round of quiz
		if (countQuiz <= 10) {
			//close check canvas before randoming next question
			closeCheckBox ();

			//randomimg a key of dialog until it is exist key
			do {
				randomKey = UnityEngine.Random.Range (1, countDialog);
			} while(!allThemeDialog.ContainsKey (randomKey));

			countQuiz++;

			//			GameObject canvas_main = new GameObject ("canvas_main");
			//			canvas_main.transform.SetParent (this.transform);
			//
			//			Text test1 = canvas_main.gameObject.AddComponent<Text> ();
			//			test1.text = "Yahhhhh!!!";

			//show question (dialog) and then remove it
			randomDialog = allThemeDialog [randomKey].dialog;
			allThemeDialog.Remove (randomKey);
			dialog.text = randomDialog;

		} else {
			//if user finish 10 dialogs testing, load a result scene 
			//OnDestroy();

			textToSpeechPlugin.UnRegisterBroadcastEvent();
			//textToSpeechPlugin.ShutDownTextToSpeechService ();

			SceneManager.LoadScene ("game_quiz_total");
		}
	}

	//check canvase will show after user finish using microphone
	public void showCheckBox(){
		popup_correct.enabled = true;
		panel.enabled = true;
	}

	//close check canvas when user click "next" button on check canvas
	private void closeCheckBox(){
		popup_wrong.enabled = false;
		popup_correct.enabled = false;
		panel.enabled = false;
	}

	//generate all dialog in this theme
	private void genAllThemeDialog(){
		allThemeDialog = new Dictionary<int, Dialog> ();

		//generate all dailog from database
		Dictionary<int,Dialog> allDialogList = new Dictionary<int, Dialog> ();
		allDialogList = ConnectDatabase.genDialogList ();//Dialog.genDialogList ();

		//count variable will be a key of allThemeDialog
		int count = 1;
		//get all event keys in this theme and match them with event_id in all dialog
		foreach (int event_id in GameLevelManagement.levelList.Keys) {
			foreach (int id in allDialogList.Keys) {
				if (allDialogList [id].event_id.Equals (event_id) && allDialogList[id].quiz.Equals(1)) {
					allThemeDialog.Add (count, allDialogList [id]);
					count++;
				}
			}
		}

	}

	private void UpdateVolume(int volume){
		textToSpeechPlugin.IncreaseMusicVolumeByValue(volume);
	}

	public void readQuiz(){
		if (quizeId == null) {
			quizeId = 1;
		}
		dialog = GameObject.Find ("dialog").GetComponent (typeof(Text)) as Text;
		//Text topic = GameObject.Find ("topic").GetComponent (typeof(Text)) as Text;

		if (textToSpeechPlugin.CheckTTSDataActivity() && textToSpeechPlugin.isInitialized()) {
			message = dialog.text;
			textToSpeechPlugin.SpeakOut(message,"q"+quizeId.ToString());
			quizeId++;
		} else {
			dialog.text = "it can not use TToTT";
		}

	}


	//checks if speaking
	public bool IsSpeaking(){
		return textToSpeechPlugin.IsSpeaking();
	}

	private void CheckTTSDataActivity(){
		if(textToSpeechPlugin!=null){
			if(textToSpeechPlugin.CheckTTSDataActivity()){
				//dialog.text = "can use";
			}else{
				//dialog.text = "can not use";
			}
		}
	}

	public void SpeakUsingAvailableLocaleOnDevice(){

		//on this example we will use spain locale
		TTSLocaleCountry ttsLocaleCountry = TTSLocaleCountry.SPAIN;

		//check if available
		bool isLanguageAvailanble =  textToSpeechPlugin.CheckLocale(ttsLocaleCountry);

		if(isLanguageAvailanble){
			string countryISO2Alpha = textToSpeechPlugin.GetCountryISO2Alpha(ttsLocaleCountry);

			//set spain language
			textToSpeechPlugin.SetLocaleByCountry(countryISO2Alpha);
			Debug.Log(TAG + "locale set," + ttsLocaleCountry.ToString() + "locale is available");

			//SpeakOut();
		}else{
			Debug.Log(TAG + "locale not set," + ttsLocaleCountry.ToString() + "locale is  notavailable");
		}
	}

	private void OnDestroy(){
		//call this of your not going to used TextToSpeech Service anymore
		textToSpeechPlugin.ShutDownTextToSpeechService();
	}

	private void OnInit(int status){
		Debug.Log(TAG + "OnInit status: " + status);

		if(status == 1){

			//get available locale on android device
			//textToSpeechPlugin.GetAvailableLocale();
			textToSpeechPlugin.SetLocale(SpeechLocale.US);
			textToSpeechPlugin.SetPitch(1f);
			textToSpeechPlugin.SetSpeechRate(1f);

			CancelInvoke("WaitingMode");
			Invoke("WaitingMode",waitingInterval);
		}else{

			CancelInvoke("WaitingMode");
			Invoke("WaitingMode",waitingInterval);
		}
	}

	private void OnSetLocale(int status){
		Debug.Log(TAG + "OnSetLocale status: " + status);
		if(status == 1){
			//float pitch = Random.Range(0.1f,2f);
			//textToSpeechPlugin.SetPitch(pitch);
		}
	}

	private void OnStartSpeech(string utteranceId){
		Debug.Log(TAG + "OnStartSpeech utteranceId: " + utteranceId);
	}

	private void OnEndSpeech(string utteranceId){
		Debug.Log(TAG + "OnDoneSpeech utteranceId: " + utteranceId);

		CancelInvoke("WaitingMode");
		Invoke("WaitingMode",waitingInterval);
	}

	private void OnErrorSpeech(string utteranceId){

		CancelInvoke("WaitingMode");
		Invoke("WaitingMode",waitingInterval);

		Debug.Log(TAG + "OnErrorSpeech utteranceId: " + utteranceId);
	}
}