using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ConnectDatabase : MonoBehaviour {

	static public string jsonThemes;
	static public string jsonVocabularies;
	static public string jsonEvents;
	static public string jsonKnowledge;
	static public string jsonDialog;
	static public string jsonCharacters;

	// Use this for initialization
	void Start () {
		StartCoroutine(GetThemes());
	}

	IEnumerator GetThemes()
	{
		//  run the php file
		WWW webRequest = new WWW("http://spout.hosting.itkmutt19.in.th/RetrieveData.php");//localhost ("http://localhost/spout/RetrieveData.php");

		// wait for the feedback of the command
		yield return webRequest;

		if (webRequest.error != null) {
			print ("There was an error getting the high score: " + webRequest.error);
		} else {
			// this is a GUIText that will display the themes.
			string temp = webRequest.text;
			jsonThemes = temp.Substring (temp.IndexOf ("[")+1, temp.IndexOf ("]")-1);
			temp = temp.Remove (0, temp.IndexOf ("]") + 1);
			jsonVocabularies = temp.Substring (temp.IndexOf ("[")+1, temp.IndexOf ("]")-4);
			temp = temp.Remove (0, temp.IndexOf("]")+1);
			jsonEvents = temp.Substring (temp.IndexOf ("[")+1, temp.IndexOf ("]")-4);
			temp = temp.Remove (0, temp.IndexOf("]")+1);
			jsonKnowledge = temp.Substring (temp.IndexOf ("[")+1, temp.IndexOf ("]")-4);
			temp = temp.Remove (0, temp.IndexOf("]")+1);
			jsonDialog = temp.Substring (temp.IndexOf ("[")+1, temp.IndexOf ("]")-4);
			temp = temp.Remove (0, temp.IndexOf("]")+1);
			jsonCharacters = temp.Substring (temp.IndexOf ("[")+1, temp.IndexOf ("]")-4);

		}
	}


	static public Dictionary<int,Theme> genThemeList(){
		string json = jsonThemes;
		//prepare variable
		Dictionary<int, Theme> themeList = new Dictionary<int, Theme>();
		Theme t = null;
		int num = 0;


		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				t = JsonUtility.FromJson<Theme>(json.Substring(0, num));
				themeList.Add(t.id,t);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				t = JsonUtility.FromJson<Theme>(json.Substring(0, num));
				themeList.Add(t.id,t);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return themeList;
	}

	static public Dictionary<string,Vocabulary> genVocabularyList(){
		string json = jsonVocabularies;
		//prepare variable
		Dictionary<string, Vocabulary> vocabularyList = new Dictionary<string, Vocabulary>();
		Vocabulary v = null;
		int num = 0;

		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				v = JsonUtility.FromJson<Vocabulary>(json.Substring(0, num));
				vocabularyList.Add(v.event_id+"-"+v.knowledge_id,v);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				v = JsonUtility.FromJson<Vocabulary>(json.Substring(0, num));
				vocabularyList.Add(v.event_id+"-"+v.knowledge_id,v);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return vocabularyList;
	}

	static public Dictionary<int,Event> genEventList(){
		string json = jsonEvents;
		Dictionary<int, Event> eventList = new Dictionary<int, Event> ();
		Event e = null;
		int num = 0;

		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				e = JsonUtility.FromJson<Event>(json.Substring(0, num));
				eventList.Add(e.id,e);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				e = JsonUtility.FromJson<Event>(json.Substring(0, num));
				eventList.Add(e.id,e);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return eventList;
	}

	static public Dictionary<int,Knowledge> genKnowledgeList(){
		string json = jsonKnowledge;
		//prepare variable
		Dictionary<int, Knowledge> knowledgeList = new Dictionary<int, Knowledge>();
		Knowledge k = null;
		int num = 0;

		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				k = JsonUtility.FromJson<Knowledge>(json.Substring(0, num));
				knowledgeList.Add(k.id,k);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				k = JsonUtility.FromJson<Knowledge>(json.Substring(0, num));
				knowledgeList.Add(k.id,k);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return knowledgeList;
	}

	static public Dictionary<int,Dialog> genDialogList(){
		string json = jsonDialog;
		Dictionary<int, Dialog> dialogList = new Dictionary<int, Dialog> ();
		Dialog d = new Dialog();
		int num = 0;

		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				d = JsonUtility.FromJson<Dialog>(json.Substring(0, num));
				dialogList.Add(d.id,d);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				d = JsonUtility.FromJson<Dialog>(json.Substring(0, num));
				dialogList.Add(d.id,d);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return dialogList;
	}

	static public Dictionary<string,Character> genCharacterList(){
		string json = jsonCharacters;
		Dictionary<string, Character> characterList = new Dictionary<string, Character> ();
		Character c = null;
		int num = 0;

		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				c = JsonUtility.FromJson<Character>(json.Substring(0, num));
				characterList.Add(c.type+"_"+c.id,c);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				c = JsonUtility.FromJson<Character>(json.Substring(0, num));
				characterList.Add(c.type+"_"+c.id,c);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return characterList;
	}

	public void goToMainmenu(){
		SceneManager.LoadScene ("main");
	}
}

