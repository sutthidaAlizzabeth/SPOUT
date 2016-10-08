﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameDialogManagment : MonoBehaviour {
	private SpriteRenderer npc;
	private SpriteRenderer user;
	private Text eng;
	private Text th;
	private Text btn_next_text;
	private Button btn_next;
	private Button btn_speak;
	private int index;
	private Dictionary<string,Dialog> dialogList;

	// Use this for initialization
	void Start () {
		//set initail characters
		npc = GameObject.Find ("npc").GetComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		npc.sprite = GameCharacterManagement.selectedNpc.image.overrideSprite;
		user = GameObject.Find ("user").GetComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		user.sprite = GameCharacterManagement.selectedUser.image.overrideSprite;

		//get component
		eng = GameObject.Find("dialog").GetComponent(typeof(Text)) as Text;
		th = GameObject.Find ("subtitle").GetComponent (typeof(Text)) as Text;
		btn_next = GameObject.Find ("btn_next").GetComponent (typeof(Button)) as Button;
		btn_speak = GameObject.Find ("btn_speak").GetComponent (typeof(Button)) as Button;
		btn_next_text = GameObject.Find ("btn_next_text").GetComponent (typeof(Text)) as Text;

		//get all conversation of this event
		dialogList = new Dictionary<string, Dialog>();
		getConversation ();

		index = 1;
		next ();

		//at default, don't show  exit popup
		Canvas exitCanvas = GameObject.Find ("exit").GetComponent (typeof(Canvas)) as Canvas;
		exitCanvas.enabled = false;
	}

	public void speak(){
		btn_speak.interactable = false;
		btn_speak.image.color = Color.clear;
		btn_next.image.color = Color.white;
		btn_next.interactable = true;
		btn_next_text.enabled = true;
	}

	public void next(){
		if (dialogList[index.ToString()].person == 1) {
			btn_speak.image.color = Color.clear;
			btn_speak.interactable = false;
			btn_next.enabled = true;
			btn_next.interactable = true;
			btn_next_text.enabled = true;
			npc.enabled = true;
			user.enabled = false;

			if (npc.sprite.name.Contains ("1") || npc.sprite.name.Contains ("2")) {
				eng.text = dialogList [index.ToString()].dialog;
				th.text = dialogList [index.ToString()].meaning.Replace("ค่ะ","ครับ");
				th.text = th.text.Replace("คะ","ครับ");
				th.text = th.text.Replace ("ฉัน","ผม");
			} else {
				eng.text = dialogList [index.ToString()].dialog;
				th.text = dialogList [index.ToString()].meaning;
			}

			index++;
		} else {
			btn_speak.image.color = Color.white;
			btn_speak.interactable = true;
			btn_next.image.color = Color.clear;
			btn_next.interactable = false;
			btn_next_text.enabled = false;
			npc.enabled = false;
			user.enabled = true;

			if (user.sprite.name.Contains ("1") || user.sprite.name.Contains ("2")) {
				eng.text = dialogList [index.ToString()].dialog;
				th.text = dialogList [index.ToString()].meaning.Replace("ค่ะ","ครับ");
				th.text = th.text.Replace("คะ","ครับ");
				th.text = th.text.Replace ("ฉัน","ผม");
			} else {
				eng.text = dialogList [index.ToString()].dialog;
				th.text = dialogList [index.ToString()].meaning;
			}

			index++;
		}
	}

	private void getConversation(){
		Dictionary<string,Dialog> allDialogList = new Dictionary<string, Dialog> ();
		allDialogList = Dialog.genDialogList ();
		int count = 1;

		foreach(string key in allDialogList.Keys){
			if (allDialogList [key].event_id.Equals (GameLevelManagement.level.id)) {
				dialogList.Add (count.ToString(), allDialogList [key]);
				count++;
			}
		}
	}
}
