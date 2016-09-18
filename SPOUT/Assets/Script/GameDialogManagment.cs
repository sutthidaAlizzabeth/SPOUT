using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameDialogManagment : MonoBehaviour {

	private SpriteRenderer npc;
	private SpriteRenderer user;

	// Use this for initialization
	void Start () {
		//set initail characters
		npc = GameObject.Find ("npc").GetComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		npc.sprite = GameCharacterManagement.selectedNpc.image.overrideSprite;
		user = GameObject.Find ("user").GetComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		user.sprite = GameCharacterManagement.selectedUser.image.overrideSprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
