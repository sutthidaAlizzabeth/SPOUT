using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Character{
	public int id;
	public string sex;
	public string type;

	static public Dictionary<string,Character> genCharacterList(){
		Dictionary<string, Character> characterList = new Dictionary<string, Character> ();
		Character c = null;
		int num = 0;

		//transform json string to string
		TextAsset jsonText = Resources.Load ("JSON/characters", typeof(TextAsset)) as TextAsset;
		string json = jsonText.text.ToString ();

		// prepare string for transforming to object
		json = json.Remove(json.Length - 2,2).Remove (0, 12);

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

}
