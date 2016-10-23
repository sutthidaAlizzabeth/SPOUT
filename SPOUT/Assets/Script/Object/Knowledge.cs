using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Knowledge{
	public string topic_en;
	public string topic_th;
	public string image_name;
	public string content;
	public string meaning;
	public string type;
	public string pronunciation;
	public int id;
	public string example;
	public string ex_meaning;

	static public Dictionary<int,Knowledge> genKnowledgeList(){
		//prepare variable
		Dictionary<int, Knowledge> knowledgeList = new Dictionary<int, Knowledge>();
		Knowledge k = null;
		int num = 0;

		//transform json string to string
		TextAsset jsonText = Resources.Load ("JSON/knowledge", typeof(TextAsset)) as TextAsset;
		string json = jsonText.text.ToString ();

		// prepare string for transforming to object
		json = json.Remove(json.Length - 2,2).Remove (0, 12);

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

}
