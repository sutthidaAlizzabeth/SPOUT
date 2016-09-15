using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameLevelManagement : MonoBehaviour {

	private Text thm_name;
	static public string level;

	// Use this for initialization
	void Start () {
		//get 'thm_name' object (Text) in Unity (game_level scene)
		thm_name = GameObject.Find ("thm_name").GetComponent (typeof(Text)) as Text;
		thm_name.text = GameThemeManagement.theme;
	}

	public void levelEasy(){
		level = "easy";
		goToVocab ();
	}
	
	public void levelNormal(){
		level = "normal";
		goToVocab ();
	}

	public void levelHard(){
		level = "hard";
		goToVocab ();
	}

	private void goToVocab(){
		SceneManager.LoadScene ("game_vocab");
	}
}
