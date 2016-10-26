﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TextToSpeech: MonoBehaviour {

	private const string TAG = "[TextToSpeechDemo]: ";

	private Text dialog;
	private string message;

	public Text statusText;
	public Text ttsDataActivityStatusText;
	public Text localeText;
	public Slider localeSlider;

	public Text pitchText;
	public Slider pitchSlider;

	public Text speechRateText;
	public Slider speechRateSlider;

	public Text volumeText;
	public Slider volumeSlider;

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
		//dialog = GameObject.Find ("dialog").GetComponent (typeof(Text)) as Text;
		CheckTTSDataActivity();
		UpdateSettingsValue();
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

	private void UpdateSettingsValue(){
		UpdateSpeechLocaleSetting();
		UpdatePitchSetting();
		UpdateSpeechRateSetting();
		UpdateVolumeSetting();
	}

	private void WaitingMode(){
		UpdateStatus("Waiting...");
	}

	private void UpdateStatus(string status){
		if(statusText!=null){
			statusText.text = String.Format("Status: {0}",status);	
		}
	}

	private void UpdateTTSDataActivityStatus(string status){
		if(ttsDataActivityStatusText!=null){
			ttsDataActivityStatusText.text = String.Format("TTS Data Activity Status: {0}",status);
		}
	}

	private void UpdateLocale(SpeechLocale locale){
		if(localeText!=null){
			localeText.text = String.Format("Locale: {0}",locale);
			textToSpeechPlugin.SetLocale(locale);
		}
	}

	private void UpdatePitch(float pitch){
		if(pitchText!=null){
			pitchText.text = String.Format("Pitch: {0}",pitch);
			textToSpeechPlugin.SetPitch(pitch);
		}
	}

	private void UpdateSpeechRate(float speechRate){
		if(speechRateText!=null){
			speechRateText.text = String.Format("Speech Rate: {0}",speechRate);
			textToSpeechPlugin.SetSpeechRate(speechRate);
		}
	}

	private void UpdateVolume(int volume){
		if(volumeText!=null){
			volumeText.text = String.Format("Volume: {0}",volume);
			textToSpeechPlugin.IncreaseMusicVolumeByValue(volume);
		}
	}

	public void readDialog(){
		message = dialog.text;
		SpeakOut ();
	}

	public void SpeakOut(){
		textToSpeechPlugin.SetSpeechRate (0.8f);
		textToSpeechPlugin.SetLocale (0);
		textToSpeechPlugin.SetPitch (1.2f);
		UpdateVolume (15);
		string utteranceId  = "test-utteranceId";

		if(textToSpeechPlugin.isInitialized()){
			UpdateStatus("Trying to speak...");
			Debug.Log(TAG + "SpeakOut whatToSay: " + "I love you" + " utteranceId " + utteranceId);
			textToSpeechPlugin.SpeakOut("I love you",utteranceId);	
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

			SpeakOut();
		}else{
			Debug.Log(TAG + "locale not set," + ttsLocaleCountry.ToString() + "locale is  notavailable");
		}
	}

	private void OnDestroy(){
		//call this of your not going to used TextToSpeech Service anymore
		textToSpeechPlugin.ShutDownTextToSpeechService();
	}

	public void OnLocaleSliderChange(){
		Debug.Log(TAG + "OnLocaleSliderChange");
		UpdateSpeechLocaleSetting();
	}

	private void UpdateSpeechLocaleSetting(){
		if(localeSlider!=null){
			SpeechLocale locale = (SpeechLocale)localeSlider.value;
			UpdateLocale(locale);
		}
	}

	private void UpdatePitchSetting(){
		if(pitchSlider!=null){
			float pitch = pitchSlider.value;
			UpdatePitch(pitch);
		}
	}

	public void OnPitchSliderChange(){
		Debug.Log(TAG + "OnPitchSliderChange");
		UpdatePitchSetting();
	}

	public void OnSpeechRateSliderChange(){
		Debug.Log(TAG + "OnSpeechRateSliderChange");
		UpdateSpeechRateSetting();
	}

	private void UpdateSpeechRateSetting(){
		if(speechRateSlider!=null){
			float speechRate = speechRateSlider.value;
			UpdateSpeechRate(speechRate);
		}
	}

	public void OnVolumeSliderChange(){
		Debug.Log(TAG + "OnLocaleSliderChange");
		UpdateVolumeSetting();
	}

	private void UpdateVolumeSetting(){
		if(volumeSlider!=null){
			int volume = (int)volumeSlider.value;
			UpdateVolume(volume);
		}
	}

	private void OnInit(int status){
		Debug.Log(TAG + "OnInit status: " + status);

		if(status == 1){
			UpdateStatus("init speech service successful!");

			//get available locale on android device
			//textToSpeechPlugin.GetAvailableLocale();

			UpdateLocale(SpeechLocale.US);
			UpdatePitch(1f);
			UpdateSpeechRate(1f);

			CancelInvoke("WaitingMode");
			Invoke("WaitingMode",waitingInterval);
		}else{
			UpdateStatus("init speech service failed!");

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
		UpdateStatus("Start Speech...");
		Debug.Log(TAG + "OnStartSpeech utteranceId: " + utteranceId);

		if(IsSpeaking()){
			UpdateStatus("speaking...");
		}
	}

	private void OnEndSpeech(string utteranceId){
		UpdateStatus("Done Speech...");
		Debug.Log(TAG + "OnDoneSpeech utteranceId: " + utteranceId);

		CancelInvoke("WaitingMode");
		Invoke("WaitingMode",waitingInterval);
	}

	private void OnErrorSpeech(string utteranceId){
		UpdateStatus("Error Speech...");

		CancelInvoke("WaitingMode");
		Invoke("WaitingMode",waitingInterval);

		Debug.Log(TAG + "OnErrorSpeech utteranceId: " + utteranceId);
	}
}