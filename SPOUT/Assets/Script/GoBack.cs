using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {
 	
	public string sceneName;

	// Use this for back to pre scene
	public void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel(sceneName);
		}
	}
}
