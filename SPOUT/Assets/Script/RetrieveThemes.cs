using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class RetrieveThemes : MonoBehaviour {

	static public string jsonTheme;
	private Text test;

	// Use this for initialization
	void Start () {
		test = GameObject.Find ("test").GetComponent (typeof(Text)) as Text;
		StartCoroutine(GetThemes());
	}

	IEnumerator GetThemes()
	{
		//  run the php file
		WWW webRequest = new WWW("http://localhost/spout/RetrieveData.php");

		// wait for the feedback of the command
		yield return webRequest;


		if (webRequest.error != null) {
			print ("There was an error getting the high score: " + webRequest.error);
		} else {
			// this is a GUIText that will display the themes.
			jsonTheme = webRequest.text.Substring (1, webRequest.text.IndexOf ("]")-1);
			Dictionary<int,Theme> dic = genThemeList ();
			test.text = dic [1].name;

		}
		//Text msg = GameObject.Find("Text").GetComponent(typeof(Text)) as Text ;
		//msg.text = webRequest.text;
	}


	static public Dictionary<int,Theme> genThemeList(){
		//prepare variable
		Dictionary<int, Theme> themeList = new Dictionary<int, Theme>();
		Theme t = null;
		int num = 0;


		//split string object to arraylist
		do{
			if(jsonTheme.IndexOf("{") == 0){
				num = jsonTheme.IndexOf("}") + 1;
				t = JsonUtility.FromJson<Theme>(jsonTheme.Substring(0, num));
				themeList.Add(int.Parse(t.id),t);
				jsonTheme = jsonTheme.Remove(0, num);
			}
			else{
				jsonTheme = jsonTheme.Remove(0, jsonTheme.IndexOf("{"));
				num = jsonTheme.IndexOf("}") + 1;
				t = JsonUtility.FromJson<Theme>(jsonTheme.Substring(0, num));
				themeList.Add(int.Parse(t.id),t);
				jsonTheme = jsonTheme.Remove(0, num);
			}

			//check next string object
			num = jsonTheme.IndexOf("{");
		}
		while(num != -1);

		return themeList;
	}
}

