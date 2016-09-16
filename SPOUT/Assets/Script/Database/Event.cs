using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class Event{
	public string id;
	public string theme_id;
	public string level;

	static public Dictionary<int,Event> genEventList(){
		Dictionary<int, Event> eventList = new Dictionary<int, Event> ();
		Event e = null;
		int num = 0;
		string json = "";

		//read json string
		using (StreamReader r = new StreamReader("Assets/Script/JSON/events.js")){
			json = r.ReadToEnd ();
		}

		// prepare string for transforming to object
		json = json.Remove(json.Length - 2,2).Remove (0, 12);

		//split string object to arraylist
		do{
			if(json.IndexOf("{") == 0){
				num = json.IndexOf("}") + 1;
				e = JsonUtility.FromJson<Event>(json.Substring(0, num));
				eventList.Add(int.Parse(e.id),e);
				json = json.Remove(0, num);
			}
			else{
				json = json.Remove(0, json.IndexOf("{"));
				num = json.IndexOf("}") + 1;
				e = JsonUtility.FromJson<Event>(json.Substring(0, num));
				eventList.Add(int.Parse(e.id),e);
				json = json.Remove(0, num);
			}

			//check next string object
			num = json.IndexOf("{");
		}
		while(num != -1);

		return eventList;
	}

}

