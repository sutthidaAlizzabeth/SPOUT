using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowLogo : MonoBehaviour {

	public Image img;

	void Start(){
		img.CrossFadeAlpha (0, 3.0f, false);
		//img.CrossFadeColor (Color.black, 1.0f, false, true);
		Application.LoadLevel("game_theme");
	}
}
