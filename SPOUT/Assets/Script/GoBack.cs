using UnityEngine;
using UnityEngine.SceneManagement;
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
				Application.Quit();
			} else {
				//load scene
				SceneManager.LoadScene (sceneName);
			}
		}
	}
}
