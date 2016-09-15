using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for change scene
	public void Change (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
}
