using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ShowLogo : MonoBehaviour {

	public Image img;

	void Start(){
		img.CrossFadeAlpha (0, 3.0f, false);
		//img.CrossFadeColor (Color.black, 1.0f, false, true);
		SceneManager.LoadScene ("game_theme");
	}
}
