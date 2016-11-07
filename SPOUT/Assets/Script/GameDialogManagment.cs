using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameDialogManagment : MonoBehaviour {
	private const string TAG = "[TextToSpeechDemo]: ";

	private SpriteRenderer npc;
	private SpriteRenderer user;
	private Text dialog;
	private Text subitle;
	private Button btn_next;
	private Text btn_next_text;
	private Button btn_speak;
	private Button btn_stop;
	private Button btn_close;
	private Canvas Canvas_exit;
	private Canvas Canvas_option;
	private Canvas Canvas_tryAgain;
	private Canvas Canvas_end;
	private Image panel;
	private int index;
	private Dictionary<int,Dialog> dialogList;
	static private int gameId;

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
	void Start () {
		CheckTTSDataActivity();

		//set initail characters
		npc = GameObject.Find ("npc").GetComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		npc.sprite = GameCharacterManagement.selectedNpc.image.overrideSprite;
		user = GameObject.Find ("user").GetComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		user.sprite = GameCharacterManagement.selectedUser.image.overrideSprite;

		//get component
		dialog = GameObject.Find("dialog").GetComponent(typeof(Text)) as Text;
		subitle = GameObject.Find("subtitle").GetComponent (typeof(Text)) as Text;
		btn_next = GameObject.Find ("btn_next").GetComponent (typeof(Button)) as Button;
		btn_next_text = GameObject.Find ("btn_next_text").GetComponent (typeof(Text)) as Text;
		btn_speak = GameObject.Find ("btn_speak").GetComponent (typeof(Button)) as Button;
		btn_stop = GameObject.Find ("btn_stop").GetComponent (typeof(Button)) as Button;
		btn_close = GameObject.Find ("btn_close").GetComponent (typeof(Button)) as Button;
		panel = GameObject.Find ("Panel").GetComponent (typeof(Image)) as Image;
		panel.enabled = false;

		//get all conversation of this event
		dialogList = new Dictionary<int, Dialog>();
		getConversation ();

		textToSpeechPlugin.RegisterBroadcastEvent();
		textToSpeechPlugin.SetSpeechRate (0.8f);
		textToSpeechPlugin.SetLocale (SpeechLocale.US);
		textToSpeechPlugin.SetPitch (1.0f);
		UpdateVolume (15);


		//at default, don't show  exit popup
		Canvas_exit = GameObject.Find("exit").GetComponent(typeof(Canvas)) as Canvas;
		Canvas_exit.enabled = false;

		//at default, don't show  option popup
		Canvas_option = GameObject.Find("Canvas_option").GetComponent(typeof(Canvas)) as Canvas;
		Canvas_option.enabled = false;

		Canvas_tryAgain = GameObject.Find ("tryAgain").GetComponent (typeof(Canvas)) as Canvas;
		Canvas_tryAgain.enabled = false;

		Canvas_end = GameObject.Find ("end").GetComponent (typeof(Canvas)) as Canvas;
		Canvas_end.enabled = false;

		index = 1;
		next ();
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

	private void UpdateVolume(int volume){
		textToSpeechPlugin.IncreaseMusicVolumeByValue(volume);
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

	public void readDialog(){
		if (textToSpeechPlugin.CheckTTSDataActivity() && textToSpeechPlugin.isInitialized()) {
//			string message = dialog.text;
//			textToSpeechPlugin.SpeakOut(message,"d"+gameId.ToString());
//			gameId++;
			dialog.text = "Success";
		} else {
			dialog.text = "it can not use TToTT";
		}
	}

	public void mike(){
		unUseNextBtn ();
		unUseSpeakBtn ();
		useStopBtn ();
	}

	public void stopMike(){
		useNextBtn ();
		unUseSpeakBtn ();
		unUseStopBtn ();
	}

	public void next(){
		if (index > dialogList.Count) {
			panel.enabled = true;
			Canvas_end.enabled = true;
		} else {
			if (dialogList[index].person == 1) {
				useNextBtn ();
				unUseSpeakBtn ();
				unUseStopBtn ();
				useNpc ();
				dialog.color = Color.black;

				if (npc.sprite.name.Contains ("1") || npc.sprite.name.Contains ("2")) {
					dialog.text = dialogList [index].dialog;
					subitle.text = dialogList [index].meaning.Replace("ค่ะ","ครับ");
					subitle.text = subitle.text.Replace("คะ","ครับ");
					subitle.text = subitle.text.Replace ("ฉัน","ผม");
				} else {
					dialog.text = dialogList [index].dialog;
					subitle.text = dialogList [index].meaning;
				}

				index++;
			} else {
				unUseNextBtn ();
				useSpeakBtn ();
				unUseStopBtn ();
				useUser ();
				dialog.color = Color.black;

				if (user.sprite.name.Contains ("1") || user.sprite.name.Contains ("2")) {
					dialog.text = dialogList [index].dialog;
					subitle.text = dialogList [index].meaning.Replace("ค่ะ","ครับ");
					subitle.text = subitle.text.Replace("คะ","ครับ");
					subitle.text = subitle.text.Replace ("ฉัน","ผม");
				} else {
					dialog.text = dialogList [index].dialog;
					subitle.text = dialogList [index].meaning;
				}

				index++;
			}
		}
	}

	private void useNextBtn(){
		btn_next.image.color = Color.white;
		btn_next.enabled = true;
//		btn_next.interactable = true;
		btn_next_text.enabled = true;
	}

	private void unUseNextBtn(){
		btn_next.image.color = Color.clear;
//		btn_next.interactable = false;
		btn_next.enabled = false;
		btn_next_text.enabled = false;
	}

	private void useSpeakBtn(){
		btn_speak.image.color = Color.white;
//		btn_speak.interactable = true;
		btn_speak.enabled = true;
	}

	private void unUseSpeakBtn(){
		btn_speak.image.color = Color.clear;
//		btn_speak.interactable = false;
		btn_speak.enabled = false;
	}

	private void useStopBtn(){
		btn_stop.image.color = Color.white;
//		btn_stop.interactable = true;
		btn_stop.enabled = true;
	}

	private void unUseStopBtn(){
		btn_stop.image.color = Color.clear;
//		btn_stop.interactable = false;
		btn_stop.enabled = false;
	}

	private void useNpc(){
		npc.enabled = true;
		user.enabled = false;
	}

	private void useUser(){
		npc.enabled = false;
		user.enabled = true;
	}
		
	private void getConversation(){
		Dictionary<int,Dialog> allDialogList = new Dictionary<int, Dialog> ();
		allDialogList = ConnectDatabase.genDialogList ();
		int count = 1;

		foreach(int key in allDialogList.Keys){
			if (allDialogList [key].event_id.Equals (GameLevelManagement.level.id)) {
				dialogList.Add (count, allDialogList [key]);
				count++;
			}
		}
	}
}