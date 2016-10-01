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
	private Dictionary<int,Character> icon_image;
	private Dictionary<int,Character> char_image;

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


		//set sprite to btn_icon
		getCharacter ();
		int count = 1;
		foreach (int i in icon_image.Keys) {
			btn_icon [count].image.overrideSprite = Resources.Load ("g_character/icon_" + icon_image [i].id, typeof(Sprite)) as Sprite;
			if (count == 4) {
				break;
			} else {
				count++;
			}
		}

		//select default npc and user icon
		selectedNpcIcon = btn_icon [1];
		selectedUserIcon = btn_icon [3];
		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void go(){
		string name = btn_icon [2].image.overrideSprite.name;
		int id = int.Parse(name.Substring (name.IndexOf("_")+1));
		int npcIconId = 0;
		int userIconId = 0;

		for(int i = 1 ; i <= 4 ; i++){
			btn_icon [i].image.overrideSprite = Resources.Load ("g_character/icon_" + icon_image [id].id, typeof(Sprite)) as Sprite;

			if (btn_icon [i].image.overrideSprite.name == selectedNpcIcon.image.overrideSprite.name) {
				npcIconId = i;
			}

			if(btn_icon[i].image.overrideSprite.name == selectedUserIcon.image.overrideSprite.name){
				userIconId = i;
			}

			if (id == icon_image.Count) {
				id = 1;
			} else {
				id++;
			}
		}

		if ((npcIconId) == 1) {
			selectedNpcIcon = btn_icon [4];
		} else {
			selectedNpcIcon = btn_icon [npcIconId-1];
		}

		if ((userIconId) == 1) {
			selectedUserIcon = btn_icon [4];
		} else {
			selectedUserIcon = btn_icon [userIconId-1];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void back(){
		string name = btn_icon [3].image.overrideSprite.name;
		int id = int.Parse(name.Substring (name.IndexOf("_")+1));
		int npcIconId = 0;
		int userIconId = 0;

		for(int i = 4 ; i >= 1 ; i--){

			btn_icon [i].image.overrideSprite = Resources.Load ("g_character/icon_" + icon_image [id].id, typeof(Sprite)) as Sprite;

			if (btn_icon [i].image.overrideSprite.name == selectedNpcIcon.image.overrideSprite.name) {
				npcIconId = i;
			}

			if(btn_icon[i].image.overrideSprite.name == selectedUserIcon.image.overrideSprite.name){
				userIconId = i;
			}

			if (id == 1) { 
				id = icon_image.Count;
			} else {
				id--;
			}

		}

		if ((npcIconId) == 4) {
			selectedNpcIcon = btn_icon [1];
		} else {
			selectedNpcIcon = btn_icon [npcIconId+1];
		}

		if ((userIconId) == 4) {
			selectedUserIcon = btn_icon [1];
		} else {
			selectedUserIcon = btn_icon [userIconId+1];
		}
			

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon1(Button btn){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[1].image.overrideSprite.name.Substring(5), typeof(Sprite)) as Sprite;

		if (selectedCharacter.name.Equals ("npc")) {
			selectedNpc = selectedCharacter;
			selectedNpcIcon = btn_icon [1];
		} else {
			selectedUser = selectedCharacter;
			selectedUserIcon = btn_icon [1];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon2(Button btn){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[2].image.overrideSprite.name.Substring(5), typeof(Sprite)) as Sprite;

		if (selectedCharacter.name.Equals ("npc")) {
			selectedNpc = selectedCharacter;
			selectedNpcIcon = btn_icon [2];
		} else {
			selectedUser = selectedCharacter;
			selectedUserIcon = btn_icon [2];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon3(Button btn){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[3].image.overrideSprite.name.Substring(5), typeof(Sprite)) as Sprite;

		if (selectedCharacter.name.Equals ("npc")) {
			selectedNpc = selectedCharacter;
			selectedNpcIcon = btn_icon [3];
		} else {
			selectedUser = selectedCharacter;
			selectedUserIcon = btn_icon [3];
		}

		setInteractiveIcon (selectedNpcIcon, selectedUserIcon);
	}

	public void chooseIcon4(Button btn){
		selectedCharacter.image.overrideSprite = Resources.Load ("g_character/char_" + btn_icon[4].image.overrideSprite.name.Substring(5), typeof(Sprite)) as Sprite;

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
		}
		if(selected.Equals ("user")){
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

	//set interactive of btn_icon
	private void setInteractiveIcon(Button pncIcon, Button userIcon){
		foreach(int id in btn_icon.Keys){
			if (!btn_icon [id].image.overrideSprite.name.Equals (pncIcon.image.overrideSprite.name) && !btn_icon [id].image.overrideSprite.name.Equals (userIcon.image.overrideSprite.name)){
					btn_icon [id].interactable = true;
			} else {
				btn_icon [id].interactable = false;
			}
		}
	}
		

	private void getCharacter(){
		int id;
		char_image = new Dictionary<int, Character> ();
		icon_image = new Dictionary<int, Character> ();
		Dictionary<string,Character> characterList = Character.genCharacterList ();
		foreach(string key in characterList.Keys){
			id = int.Parse(characterList [key].id);
			if (key.Contains ("char")){
				char_image.Add (id, characterList [key]);
			} else {
				icon_image.Add (id, characterList [key]);
			}
		}
	}
}
