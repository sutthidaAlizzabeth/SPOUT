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
		isRecording = !isRecording;
		if (isRecording) {
			// create the intent and start the activity
			var voiceIntent = new Intent (RecognizerIntent.ActionRecognizeSpeech);
			voiceIntent.PutExtra (RecognizerIntent.ExtraLanguageModel, "en-US");

			// put a message on the modal dialog
			voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Allizabeth");

			// if there is more then 1.5s of silence, consider the speech over
			voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
			voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
			voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
			voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

			//result
			var matches = voiceIntent.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
			if(matches.Count != 0){
				string textInput = field.text + matches [0];

				// limit the output to 500 character
				if (textInput.Length > 500) {
					textInput = textInput.Substring (0, 500);
				}

				//set content in "field"
				field.text = textInput;
			}
			else{
				field.text = "No speech was recognised";
			}

			//change source image of button to stop recording button
			btnRecord.image.overrideSprite = imgStop;
		} else {
			//change source image of button to microphone button
			btnRecord.image.overrideSprite = imgMicrophone;
		}
	}
}
