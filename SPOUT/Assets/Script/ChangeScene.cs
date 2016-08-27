using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for change scene
	public void Change (string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
