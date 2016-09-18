using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialog {
	public string id;
	public int person;
	public string event_id;
	public int sequence;
	public string background_name;
	public string meaning;
	public string dialog;
	public int choice;

	static public Dictionary<string,Dialog> genDialogList(){
		Dictionary<string, Dialog> dialogList = new Dictionary<string, Dialog> ();
		Dialog d = null;
		int num = 0;

		//transform json string to string
		TextAsset jsonText = Resources.Load ("JSON/dialog", typeof(TextAsset)) as TextAsset;
		string json = jsonText.text.ToString ();

		// prepare string for transforming to object
		json = json.Remove(json.Length - 2,2).Remove (0, 12);

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

}
