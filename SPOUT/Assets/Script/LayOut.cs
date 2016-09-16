using UnityEngine;
using System.Collections;

public class LayOut : MonoBehaviour {
	public string orientation = "";
	// Use this for initialization
	void Start () {
		if (orientation == "portrait") {
			Screen.orientation = ScreenOrientation.Portrait;
		}else if(orientation == "landscape"){
			Screen.orientation = ScreenOrientation.Landscape;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
