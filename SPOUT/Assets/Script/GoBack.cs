using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;

public class GoBack : MonoBehaviour {

	//this parameter derive from each scene
	public string sceneName;

	// Update is called once per frame
	void Update () {
		//when user touch "back" button on mobile
		if (Input.GetKey (KeyCode.Escape)) {
			//if sceneName equals "quit"
			//user touch "back" button in sc_main
			//user want to close app
			if (String.Compare (sceneName, "quit") == 0) {
				Canvas popupCanvas = GameObject.Find ("popup").GetComponent (typeof(Canvas)) as Canvas;
				Image panel = GameObject.Find ("Panel").GetComponent (typeof(Image)) as Image;
				popupCanvas.enabled = true;
				panel.enabled = true;
			} else {
				//load scene
				SceneManager.LoadScene (sceneName);
			}
		}
	}
}
