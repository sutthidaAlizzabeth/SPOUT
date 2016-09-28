using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainManagement : MonoBehaviour {

	public void goToGame(){
		Dictionary<int,Theme> themeList = Theme.genThemeList ();
		GameThemeManagement.theme = themeList [1];
		SceneManager.LoadScene ("game_theme");
	}
}
