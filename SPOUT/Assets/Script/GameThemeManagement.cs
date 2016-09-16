using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameThemeManagement : MonoBehaviour {

	Button btn_theme; //description object in Unity (game_theme scene)
	Text description; //description object in Unity (game_theme scene)
	//all theme objects
	static public Dictionary<int,Theme> themeList = Theme.genThemeList ();
	//choosed theme object
	static public Theme theme = themeList[1];

	// Use this for initialization
	//this method was call first
	void Start () {
		//get btn_theme object in Unity (game_theme scene)
		btn_theme = GameObject.Find("btn_theme").GetComponent(typeof(Button)) as Button;
		//description object in Unity (game_theme scene)
		description = GameObject.Find("description").GetComponent(typeof(Text)) as Text;

		//set content of btn_theme and theme description
		//at the first, use index 1 (first theme id)
		setThemeButton (int.Parse(theme.getId()));
	}
		
	
	// when touch btn_go button
	public void go () {
		//get theme id of current theme object
		int index = int.Parse(theme.getId());

		//set the next index of list_img_theme
		//if current index is the last index, then set index = 1 (return to the first image)
		//else increase index
		if(index == themeList.Count){
			index = 1;
		}else{
			index = index + 1;
		}

		//set content of btn_theme and theme description
		setThemeButton (index);
	}

	public void back(){
		//get theme id of current theme object
		int index = int.Parse(theme.getId());

		//set the next index of list_img_theme
		//if current index is the first index, then set index = the last index (return to the last image)
		//else decrease index
		if(index == 1){
			index = themeList.Count;
		}else{
			index = index - 1;
		}

		//set content of btn_theme and theme description
		setThemeButton (index);
	}

	private void setThemeButton(int index){
		//set sprite (image) into btn_theme
		btn_theme.image.overrideSprite = Resources.Load ("g_theme/" + themeList[index].getImage(), typeof(Sprite)) as Sprite;
		//set image name into "description"
		description.text = themeList[index].getName();
		//prepare "theme" global variable for sending to other scenes
		theme = themeList[index];
	}

	//user click at the choosed theme, then scene change to game_level
	public void goToLevel(){
		SceneManager.LoadScene ("game_level");
	}

}
