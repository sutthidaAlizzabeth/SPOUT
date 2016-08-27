using UnityEngine;
using System.Collections;

public class BackToMain : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("main_menu");
		}
	}
}
