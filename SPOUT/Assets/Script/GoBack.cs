using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {
 	

	// Use this for back to pre scene
	public void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("sc_main");
		}
	}
}
