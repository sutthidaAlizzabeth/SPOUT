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
		/*Setting setting = new Setting ("themeWarning", "false");
		string json = JsonUtility.ToJson (setting);
		Text msg = GameObject.Find ("Text").GetComponent (typeof(Text)) as Text;
		msg.text = json;
		string file = "/language.json";
		File.WriteAllText (Application.persistentDataPath+"/themeWarning.json",json);
		string tts = File.ReadAllText (Application.persistentDataPath + "/themeWarning.json");
		msg.text = Application.persistentDataPath;*/
	}

	public void goToGame(){
		Dictionary<int,Theme> themeList = Theme.genThemeList ();
		GameThemeManagement.theme = themeList [1];
		SceneManager.LoadScene ("game_theme");
	}
}
