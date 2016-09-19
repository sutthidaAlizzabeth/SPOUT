using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameLevelManagement : MonoBehaviour {

	private Text thm_name;
	private Button btn_easy;
	private Button btn_normal;
	private Button btn_hard;

	static public Dictionary<int,Event> levelList = new Dictionary<int, Event> ();
	static public Event level;

	// Use this for initialization
	void Start () {
		//get game objects from Unity (game_level scene)
		thm_name = GameObject.Find ("thm_name").GetComponent (typeof(Text)) as Text;
		btn_easy = GameObject.Find("btn_easy").GetComponent(typeof(Button)) as Button;
		btn_normal = GameObject.Find("btn_normal").GetComponent(typeof(Button)) as Button;
		btn_hard = GameObject.Find("btn_hard").GetComponent(typeof(Button)) as Button;

		//set default button isn't interactive
		btn_easy.interactable = false;
		btn_normal.interactable = false;
		btn_hard.interactable = false;

		levelList.Clear ();

		//find all levels of choosed theme
		findLevel (GameThemeManagement.theme.id);

		if (levelList.Count != 0) {
			//set choosed theme to thm_name
			thm_name.text = GameThemeManagement.theme.name;

			//set interaction of button
			foreach (int id in levelList.Keys) {
				if (levelList [id].level.Equals ("easy")) {
					btn_easy.interactable = true;
				} else if (levelList [id].level.Equals ("normal")) {
					btn_normal.interactable = true;
				} else {
					btn_hard.interactable = true;
				}
			}

		} else {
			thm_name.text = "No Level";
		}
	
	}

	public void easy(){
		foreach(int id in levelList.Keys){
			if(levelList[id].level.Equals("easy")){
				level = levelList [id];
			}
		}
		goToVocab ();
	}

	public void normal(){
		foreach(int id in levelList.Keys){
			if(levelList[id].level.Equals("normal")){
				level = levelList [id];
			}
		}
		goToVocab ();
	}

	public void hard(){
		foreach(int id in levelList.Keys){
			if(levelList[id].level.Equals("hard")){
				level = levelList [id];
			}
		}
		goToVocab ();
	}

	//find all level of choosed theme
	public Dictionary<int,Event> findLevel(string themeId){
		//get all level of all theme
		Dictionary<int,Event> eventList = Event.genEventList ();

		if (eventList.Count != 0) {
			foreach(int id in eventList.Keys){
				//filter only levels of choosed theme
				if(eventList[id].theme_id == GameThemeManagement.theme.id){
					levelList.Add (id, eventList [id]);
				}
			}
		}

		return levelList;
	}

	private void goToVocab(){
		SceneManager.LoadScene ("game_vocab");
	}
}
