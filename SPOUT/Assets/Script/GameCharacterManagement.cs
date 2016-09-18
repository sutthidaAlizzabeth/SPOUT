using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameCharacterManagement : MonoBehaviour {

	private Button npc;
	private Button user;
	private Button selectedCharacter;
	static public Button selectedNpc;
	static public Button selectedUser;
	private Button selectedNpcIcon;
	private Button selectedUserIcon;
	private Dictionary<int,Button> btn_icon;

	// Use this for initialization
	void Start () {
		//get character button components
		npc = GameObject.Find ("npc").GetComponent (typeof(Button)) as Button;
		user = GameObject.Find ("user").GetComponent (typeof(Button)) as Button;


		//set default sprite to character button
		npc.image.overrideSprite = Resources.Load ("g_character/char_1", typeof(Sprite)) as Sprite;
		user.image.overrideSprite = Resources.Load ("g_character/char_3", typeof(Sprite)) as Sprite;

		//set selected NPC and user
		selectedNpc = npc;
		selectedUser = user;

		//set color of button
		//setColorCharacter(selected button, unselected button)
		setColorCharacter (npc, user);

		//get all icon components
		btn_icon = new Dictionary<int, Button>();
		btn_icon.Add (1,GameObject.Find ("btn_char_1").GetComponent (typeof(Button)) as Button);
		btn_icon.Add (2,GameObject.Find ("btn_char_2").GetComponent (typeof(Button)) as Button);
		btn_icon.Add (3,GameObject.Find ("btn_char_3").GetComponent (typeof(Button)) as Button);
		btn_icon.Add (4,GameObject.Find ("btn_char_4").GetComponent (typeof(Button)) as Button);

		//select default npc and user icon
		selectedNpcIcon = btn_icon [1];
		selectedUserIcon = btn_icon [2];
		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon1(){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[1].image.sprite.name.Substring(5), typeof(Sprite)) as Sprite;

		if (selectedCharacter.name.Equals ("npc")) {
			selectedNpc = selectedCharacter;
			selectedNpcIcon = btn_icon [1];
		} else {
			selectedUser = selectedCharacter;
			selectedUserIcon = btn_icon [1];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon2(){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[2].image.sprite.name.Substring(5), typeof(Sprite)) as Sprite;

		if (selectedCharacter.name.Equals ("npc")) {
			selectedNpc = selectedCharacter;
			selectedNpcIcon = btn_icon [2];
		} else {
			selectedUser = selectedCharacter;
			selectedUserIcon = btn_icon [2];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon3(){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[3].image.sprite.name.Substring(5), typeof(Sprite)) as Sprite;

		if (selectedCharacter.name.Equals ("npc")) {
			selectedNpc = selectedCharacter;
			selectedNpcIcon = btn_icon [3];
		} else {
			selectedUser = selectedCharacter;
			selectedUserIcon = btn_icon [3];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon4(){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[4].image.sprite.name.Substring(5), typeof(Sprite)) as Sprite;

		if (selectedCharacter.name.Equals ("npc")) {
			selectedNpc = selectedCharacter;
			selectedNpcIcon = btn_icon [4];
		} else {
			selectedUser = selectedCharacter;
			selectedUserIcon = btn_icon [4];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	//switch character from npc to user or user to npc
	public void chooseCharacter(string selected){
		if (selected.Equals ("npc")) {
			setColorCharacter (npc,user);
		} else {
			setColorCharacter (user,npc);
		}
	}

	//selected button's color is white
	//unselected button's color is grey
	private void setColorCharacter(Button selected, Button unselected){
		selected.image.color = Color.white;
		unselected.image.color = Color.grey;
		selectedCharacter = selected;
	}

	private void setInteractiveIcon(Button pncIcon, Button userIcon){
		foreach(int id in btn_icon.Keys){
			if (!btn_icon [id].Equals (pncIcon) && !btn_icon [id].Equals (userIcon)){
					btn_icon [id].interactable = true;
			} else {
				btn_icon [id].interactable = false;
			}
		}
	}
}
