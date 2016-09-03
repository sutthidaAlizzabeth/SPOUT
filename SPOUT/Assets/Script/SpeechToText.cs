using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechToText : MonoBehaviour {

	public InputField field;
	public Button microphone;
	public Sprite btnImg;

	// Use this for initialization
	void Start () {
		field.text = "Show Text Here!";
		//microphone = gameObject.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSpeech(string str){
		field.text = str;
		microphone.image.overrideSprite = btnImg;
		//microphone.image.sprite = btnImg;
	}
}
