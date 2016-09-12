using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameThemeControl : MonoBehaviour {

	public Button btn_back; //btn_back object in Unity (game_theme scene)
	public Button btn_go; //btn_go object in Unity (game_theme scene)
	public Button btn_theme; //btn_theme object in Unity (game_theme scene)
	public Text page; //page object in Unity (game_theme scene)
	public Text description; //description object in Unity (game_theme scene)
	private Sprite theme; //contain an image of theme
	private int img_theme_size; //size of img_theme string array
	//all names of theme images
	private string[] img_theme = {
		"hospital",
		"school"
	};

	// Use this for initialization
	//this method was call first
	void Start () {
		//set size of "img_theme" string array into img_theme_size variable
		setImgThemeSize ();

		//set content of page, btn_theme, and theme description
		//at the first, use index 0
		setThemeButton (0);
	}
	
	// when touch btn_go button
	public void go () {
		//description content is one of "img_theme" string array
		//get content of "description" from getDescriptionName()
		//find index of "description" in "img_theme" string array
		int index = Array.IndexOf (img_theme, getDescriptionName());

		//set the next index of img_theme
		//if current index is the last index, then set index = 0 (return to the first image)
		//else increase index
		if(index == img_theme_size - 1){
			index = 0;
		}else{
			index = index + 1;
		}

		//set content of page, btn_theme, and theme description
		setThemeButton (index);
	}

	public void back(){
		//description content is one of "img_theme" string array
		//get content of "description" from getDescriptionName()
		//find index of "description" in "img_theme" string array
		int index = Array.IndexOf (img_theme, getDescriptionName());

		//set the next index of img_theme
		//if current index is the first index, then set index = the last index (return to the last image)
		//else increase index
		if(index == 0){
			index = img_theme_size - 1;
		}else{
			index = index - 1;
		}

		//set content of page, btn_theme, and theme description
		setThemeButton (index);
	}

	private void setThemeButton(int indexOfButton){
		//load sprite (image) from Resource/Texture/theme
		theme = Resources.Load ("theme/" + img_theme [indexOfButton], typeof(Sprite)) as Sprite;
		//set sprite (image) into btn_theme
		btn_theme.image.overrideSprite = theme;
		//set image name into "description"
		description.text = img_theme [indexOfButton];
		//set number of page (pattern x/total)
		page.text = (indexOfButton + 1) + "/" + img_theme_size;
	}

	//get content of "dexcription"
	private string getDescriptionName(){
		return description.text;
	}

	//get size of "img_theme" array into img_theme_size variable;
	private void setImgThemeSize(){
		img_theme_size = img_theme.Length;
	}
}
