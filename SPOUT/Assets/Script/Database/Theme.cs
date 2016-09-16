using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class Theme{
	public string id;
	public string name;
	public string image;

	static public Dictionary<int,Theme> genThemeList(){
		//prepare variable
		Dictionary<int, Theme> themeList = new Dictionary<int, Theme>();
		Theme t = null;
		int num = 0;
		string json = "";

		//read json string
		using (StreamReader r = new StreamReader("Assets/Script/JSON/themes.js")){
			json = r.ReadToEnd ();
		}

		// prepare string for transforming to object
		json = json.Remove(json.Length - 2,2).Remove (0, 12);

		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				t = JsonUtility.FromJson<Theme>(json.Substring(0, num));
				themeList.Add(int.Parse(t.id),t);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				t = JsonUtility.FromJson<Theme>(json.Substring(0, num));
				themeList.Add(int.Parse(t.id),t);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return themeList;
	}

}
