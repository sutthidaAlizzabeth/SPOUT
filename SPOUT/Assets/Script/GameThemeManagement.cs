using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections;

public class GameThemeManagement : MonoBehaviour {

	Button btn_theme;
	Text description; //description object in Unity (game_theme scene)
	private Sprite img_theme; //contain an image of theme
	private int list_img_theme_size; //size of list_img_theme string array
	//all names of theme images
	static private string[] list_img_theme = {
		"thm_flirting",
		"thm_school"
	};
	static public string theme = list_img_theme[0].Substring(4);

	// Use this for initialization
	//this method was call first
	void Start () {
		//get btn_theme object in Unity (game_theme scene)
		btn_theme = GameObject.Find("btn_theme").GetComponent(typeof(Button)) as Button;
		//description object in Unity (game_theme scene)
		description = GameObject.Find("description").GetComponent(typeof(Text)) as Text;

		//set size of "list_img_theme" string array into list_img_theme_size variable
		setImgThemeSize ();

		//set content of btn_theme and theme description
		//at the first, use index 0
		setThemeButton (Array.IndexOf (list_img_theme, "thm_"+theme));
	}
		
	
	// when touch btn_go button
	public void go () {
		//description content is one of "list_img_theme" string array
		//get content of "description" from getDescriptionName()
		//find index of "description" in "list_img_theme" string array
		int index = Array.IndexOf (list_img_theme, "thm_"+getDescriptionName());

		//set the next index of list_img_theme
		//if current index is the last index, then set index = 0 (return to the first image)
		//else increase index
		if(index == list_img_theme_size - 1){
			index = 0;
		}else{
			index = index + 1;
		}

		//set content of btn_theme and theme description
		setThemeButton (index);
	}

	public void back(){
		//description content is one of "list_img_theme" string array
		//get content of "description" from getDescriptionName()
		//find index of "description" in "list_img_theme" string array
		int index = Array.IndexOf (list_img_theme, "thm_"+getDescriptionName());

		//set the next index of list_img_theme
		//if current index is the first index, then set index = the last index (return to the last image)
		//else increase index
		if(index == 0){
			index = list_img_theme_size - 1;
		}else{
			index = index - 1;
		}

		//set content of btn_theme and theme description
		setThemeButton (index);
	}

	private void setThemeButton(int indexOfButton){
		ArrayList obj = new ArrayList ();
		int num = 0;
		string json = "";
		using (StreamReader r = new StreamReader("Assets/Script/JSON/themes.js")){
			json = r.ReadToEnd ();
			json = json.Remove(json.Length - 2,2).Remove (0, 12).Replace(" ", String.Empty);
			do{
				if(json.IndexOf("{") == 0){
					num = json.IndexOf("}") + 1;
					obj.Add(json.Substring(0,num));
					json = json.Remove(0, num);
				}
				else{
					num = json.IndexOf("{");
					json = json.Remove(0, num);
					num = json.IndexOf("}") + 1;
					obj.Add(json.Substring(0,num));
					json = json.Remove(0, num);
				}
				num = json.IndexOf("{");
			}
			while(num != -1);
		}

		Theme t = JsonUtility.FromJson<Theme> (obj [0].ToString());


		//load sprite (image) from Resource/g_theme
		img_theme = Resources.Load ("g_theme/" + list_img_theme [indexOfButton], typeof(Sprite)) as Sprite;
		//set sprite (image) into btn_theme
		btn_theme.image.overrideSprite = img_theme;
		//set image name into "description"
		description.text = obj[1].ToString(); //list_img_theme [indexOfButton].Substring(4);
		//prepare "theme" global variable for sending to other scenes
		theme = getDescriptionName ();
	}

	//user click at the choosed theme, then scene change to game_level
	public void goToLevel(){
		SceneManager.LoadScene ("game_level");
	}

	//get content of "dexcription"
	public string getDescriptionName(){
		return description.text;
	}

	//get size of "list_img_theme" array into list_img_theme_size variable;
	private void setImgThemeSize(){
		list_img_theme_size = list_img_theme.Length;
	}

	//retrieve data from JSON file (themes.js)
	private void setThemeObj(){
		using (StreamReader r = new StreamReader("Assets/Script/JSON/themes.js")){
			string json = r.ReadToEnd ();
			json = json.Remove(json.Length - 2,2).Remove (0, 12).Replace(" ", String.Empty);
			string[] theme = json.Split (',');
		}
	}
}
