using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLevelManagement : MonoBehaviour {

	public Text topic;

	// Use this for initialization
	void Start () {
		topic.text = GameThemeManagement.theme;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
