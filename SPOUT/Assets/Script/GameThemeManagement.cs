using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameThemeManagement : MonoBehaviour {

	public Button btn_theme;
	public Text description;
	public Text text_page;

	//all theme objects
	static public Dictionary<int,Theme> themeList;
	//choosed theme object
	static public Theme theme;
	public Canvas warnningCanvas;
	public Image panel;


	// Use this for initialization
	//this method was call first
	void Start () {
		themeList = new Dictionary<int, Theme> ();
		themeList = ConnectDatabase.genThemeList ();//Theme.genThemeList ();

		if (theme == null) {
			theme = themeList[1];
		}

		//set image of btn_theme, description and text_page
		setThemeButton (theme.id);
		setThemePage (theme.id);

		//at first, warning popup don't show
		warnningCanvas.enabled = false;
		panel.enabled = false;
	}
		

	// when touch btn_go button
	public void go () {
		//get theme id of current theme object
		int index = theme.id;

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
		int index = theme.id;

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
		description.text = themeList[index].name_th;
		//prepare "theme" global variable for sending to other scenes
		theme = themeList[index];
	}

	private void setThemePage(int number){
		text_page.text = number + "/" + themeList.Count;
	}

	//user click at the choosed theme, then scene change to game_level
	public void goToLevel(){
		if (theme.warning == 1) {
			//get data from themeWarning.json in Asset floder
			//if this file exist, app will run in try side
			//is this file don't exist, app will run in catch side (because of error)
			try{
				string msg = File.ReadAllText (Application.persistentDataPath+"/themeWarning.json");
				if (msg != null) {
					Setting setting = JsonUtility.FromJson<Setting>(msg);
					//"false" means show popup
					if(setting.value == "false"){
						warnningCanvas.enabled = true;
						panel.enabled = true;
					}
					else{
						SceneManager.LoadScene ("game_level");
					}
				}
			}
			catch{
				warnningCanvas.enabled = true;
				panel.enabled = true;
			}
		} else {
			SceneManager.LoadScene ("game_level");
		}

	}

}