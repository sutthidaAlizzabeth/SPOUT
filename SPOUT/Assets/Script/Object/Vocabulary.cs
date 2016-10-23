using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Vocabulary{
	public int event_id;
	public int knowledge_id;

	static public Dictionary<string,Vocabulary> genVocabularyList(){
		//prepare variable
		Dictionary<string, Vocabulary> vocabularyList = new Dictionary<string, Vocabulary>();
		Vocabulary v = null;
		int num = 0;

		//transform json string to string
		TextAsset jsonText = Resources.Load ("JSON/vocabularies", typeof(TextAsset)) as TextAsset;
		string json = jsonText.text.ToString ();

		// prepare string for transforming to object
		json = json.Remove(json.Length - 2,2).Remove (0, 12);

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
}
