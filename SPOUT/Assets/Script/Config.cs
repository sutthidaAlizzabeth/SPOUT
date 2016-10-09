using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class Config : MonoBehaviour {

	public void configGameThemeWarning(){
		Toggle toggle = GameObject.Find ("Toggle").GetComponent (typeof(Toggle)) as Toggle;
		Setting setting = new Setting ("themeWarning", toggle.isOn.ToString());
		string json = JsonUtility.ToJson (setting);
		File.WriteAllText (Application.persistentDataPath+"/themeWarning.json",json);
	}
}
