using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class MainManagement : MonoBehaviour {


	void Start(){
		Canvas exitCanvas = GameObject.Find ("exit").GetComponent (typeof(Canvas)) as Canvas;
		exitCanvas.enabled = false;
	}

	public void goToGame(){
		Dictionary<int,Theme> themeList = Theme.genThemeList ();
		GameThemeManagement.theme = themeList [1];
		SceneManager.LoadScene ("game_theme");
	}
}
