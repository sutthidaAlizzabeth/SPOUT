using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Android.Content;
using Android.Speech;

public class SpeechToText : MonoBehaviour {

	public InputField field;
	public Button btnRecord;
	public Sprite imgMicrophone;
	public Sprite imgStop;
	private bool isRecording;

	// Use this for initialization
	void Start () {
		field.text = "Show Text Here!";
		//set started value of isRecording;
		isRecording = false;
	}
	
	//Use this when click microphone button
	public void OnSpeech(string str){
		//set text in InputField
		field.text = str;

		isRecording = !isRecording;
		if (isRecording) {
			var voiceIntent = new Intent (RecognizerIntent.ActionRecognizeSpeech);

			//change source image of button to stop recording button
			btnRecord.image.overrideSprite = imgStop;
		} else {
			//change source image of button to microphone button
			btnRecord.image.overrideSprite = imgMicrophone;
		}
	}
}
