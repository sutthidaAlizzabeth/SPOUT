using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCharacterManagement : MonoBehaviour {

	private Button npc;
	private Button user;

	void Awake(){
		npc = GameObject.Find ("npc").GetComponent (typeof(Button)) as Button;
		user = GameObject.Find ("user").GetComponent (typeof(Button)) as Button;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	public void chooseCharacter(){
	}

	public void switchCharacterToUser(){
	}
}
