using UnityEngine;
using System;
using System.Collections;

public class SceneManagement : MonoBehaviour {

	//this parameter derive from each scene
	public string sceneName;
	
	// Update is called once per frame
	void Update () {
		//when user touch "back" button
		if (Input.GetKey (KeyCode.Escape)) {
			//if sceneName equals "quit"
			//user touch "back" button in sc_main
			//user want to close app
			if (String.Compare (sceneName, "quit") == 0) {
				Application.Quit();
			} else {
				//load scene
				Application.LoadLevel(sceneName);
			}
		}
	}

	// Use this for change scene
	public void Change (string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
