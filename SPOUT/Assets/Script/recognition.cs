using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class recognition : MonoBehaviour {

	public InputField field;

	// Use this for initialization
	public void Start () {
		field.text = "Alizzabeth";

		AndroidJavaClass s = new AndroidJavaClass ("android.speech.RecognizerIntent");
		AndroidJavaObject intent = new AndroidJavaObject ("android.content.Intent", s.GetStatic<string>("ACTION_RECOGNIZE_SPEECH"));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
