using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechToText : MonoBehaviour {

	public InputField field;

	// Use this for initialization
	void Start () {
		field.text = "Show Text Here!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSpeech(string str){
		field.text = str;
	}
}
