using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KnContentManagement : MonoBehaviour {
	private Dictionary<int,Knowledge> allKnList; //all content in selected category
	private Dictionary<int,Knowledge> searchList; //all content from search result
	private Dictionary<int,Text> btnTextArray;
	private Dictionary<int,Button> speakerArray;
	public GameObject prefabButton;
	public GameObject prefabSpeaker;
	public RectTransform ParentPanel;

	// Use this for initialization
	void Start () {
		allKnList = new Dictionary<int, Knowledge> ();
		searchList = new Dictionary<int, Knowledge> ();

		//get all knowledge content
		genKnowledgeContent ();

		//at first user don't search,so get all content
		searchList = allKnList;

		//generate content button and speaker button
		generateButton ();
	}

	private void generateButton(){
		//set amount of button 
		int amount = searchList.Count;;
//		if (searchList.Count < 4) {
//			amount = searchList.Count;
//		}

		btnTextArray = new Dictionary<int, Text> ();
		Button btn_vocab;
		Text btn_vocab_text;

		speakerArray = new Dictionary<int, Button> ();
		Button btn_speaker;

		//generate button
		for(int i=0 ; i<amount ; i++){
			//vocab button
			GameObject btn = (GameObject)Instantiate(prefabButton);
			btn.transform.SetParent(ParentPanel, false);
			btn.transform.localScale = new Vector3 (1,1,1);
			btn.transform.position = new Vector3 (370, 720-(i*110), 0);
			btn_vocab = btn.GetComponent<Button>();

			//set text on vacab button
			btn_vocab_text = btn_vocab.GetComponentInChildren<Text> ();
			if (searchList [i].type.Length > 1) {
				btn_vocab_text.text = searchList [i].content + " (" + searchList[i].type+")";
			} else {
				btn_vocab_text.text = searchList [i].content;
			}

			//add button into array
			btnTextArray.Add(i,btn_vocab.GetComponentInChildren<Text> ());

			//add function into button (onclick)
			int index = i;
			string content = btn_vocab_text.text;
			btn_vocab.onClick.AddListener (() => clickContent(index,content));


//			//speaker on vocab button
//			GameObject speaker = (GameObject)Instantiate(prefabSpeaker);
//			speaker.transform.SetParent (ParentPanel, false);
//			speaker.transform.localScale = new Vector3 (1,1,1);
//			speaker.transform.position = new Vector3 (200, 715-(i*110), 0);
//			btn_speaker = speaker.GetComponent<Button> ();
//			//add btn_speaker into array
//			speakerArray.Add(i,btn_speaker);
		}
	}

	//when user click on vocab button
	public void clickContent(int index, string content){
		//topic.text = btnTextArray [index].text;
		if (content.Contains(btnTextArray [index].text)) {
			btnTextArray [index].text = searchList [index].meaning;
			foreach(int key in btnTextArray.Keys){
				if (!key.Equals (index)) {
					if (searchList [key].type.Length > 1) {
						btnTextArray[key].text = searchList [key].content + " (" + searchList[key].type+")";
					} else {
						btnTextArray [key].text = searchList [key].content;
					}
				}
			}
		} else {
			btnTextArray [index].text = content;
		}
	}
	
	private void genKnowledgeContent(){
		Dictionary<int,Knowledge> allKnowledge = new Dictionary<int, Knowledge> ();
		allKnowledge = ConnectDatabase.genKnowledgeList ();

		int index = 0;
		foreach(int key in allKnowledge.Keys){
			if (allKnowledge [key].category_id.Equals (KnCategoryManagement.categoryId)) {
				allKnList.Add (index++,allKnowledge[key]);
			}
		}
	}
}
