﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameThemeManagement : MonoBehaviour {

	private Button btn_theme;
	private Text description;
	private Text text_page;

	//all theme objects
	static public Dictionary<int,Theme> themeList;
	//choosed theme object
	static public Theme theme;


	// Use this for initialization
	//this method was call first
	void Start () {
		themeList = new Dictionary<int, Theme> ();
		themeList = Theme.genThemeList ();

		if (theme == null) {
			theme = themeList[1];
		}
			

		//get game objects from Unity (game_theme scene)
		btn_theme = GameObject.Find("btn_theme").GetComponent(typeof(Button)) as Button;
		description = GameObject.Find("description").GetComponent(typeof(Text)) as Text;
		text_page = GameObject.Find ("text_page").GetComponent (typeof(Text)) as Text;

		setThemeButton (int.Parse(theme.id));
		setThemePage (int.Parse(theme.id));
	}
		

	// when touch btn_go button
	public void go () {
		//get theme id of current theme object
		int index = int.Parse(theme.id);

		//set the next index of list_img_theme
		//if current index is the last index, then set index = 1 (return to the first image)
		//else increase index
		if(index == themeList.Count){
			index = 1;
		}else{
			index = index + 1;
		}

		//set content of btn_theme, theme description and page
		setThemePage(index);
		setThemeButton (index);
	}

	public void back(){
		//get theme id of current theme object
		int index = int.Parse(theme.id);

		//set the next index of list_img_theme
		//if current index is the first index, then set index = the last index (return to the last image)
		//else decrease index
		if(index == 1){
			index = themeList.Count;
		}else{
			index = index - 1;
		}

		//set content of btn_theme theme description and page
		setThemeButton (index);
		setThemePage (index);
	}

	private void setThemeButton(int index){
		//set sprite (image) into btn_theme
		btn_theme.image.overrideSprite = Resources.Load ("g_theme/" + themeList[index].image, typeof(Sprite)) as Sprite;
		//set image name into "description"
		description.text = themeList[index].name;
		//prepare "theme" global variable for sending to other scenes
		theme = themeList[index];
	}

	private void setThemePage(int number){
		text_page.text = number + "/" + themeList.Count;
	}

	//user click at the choosed theme, then scene change to game_level
	public void goToLevel(){
		SceneManager.LoadScene ("game_level");
	}

}