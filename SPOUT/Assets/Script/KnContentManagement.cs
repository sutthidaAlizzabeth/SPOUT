using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KnContentManagement : MonoBehaviour {
	private Dictionary<int,Knowledge> allKnList; //all content in selected category
	private Dictionary<int,Button> btnArray;
	private Dictionary<int,Button> speakerArray;
//	static public Dictionary<int,Knowledge> searchList;
	public GameObject prefabButton;
	public GameObject prefabSpeaker;
	public RectTransform ParentPanel;

	private Text topic;

	// Use this for initialization
	void Start () {
		
		allKnList = new Dictionary<int, Knowledge> ();
//		searchList = new Dictionary<int, Knowledge> ();
		btnArray = new Dictionary<int, Button> ();
		topic = GameObject.Find ("topic").GetComponent (typeof(Text)) as Text;

		//get all knowledge content
		if(allKnList.Count <= 0){
			genKnowledgeContent ();
		}


		//at first user don't search,so get all content
//		searchList = allKnList;

		//generate content button and speaker button
		generateButton ();
	}

	private void generateButton(){
		//set amount of button 
		int amount = allKnList.Count;
		btnArray.Clear ();
		Button btn_vocab;
		Text btn_vocab_text;

		//generate button
		for(int i=0 ; i<amount ; i++){
			//vocab button
			GameObject btn = (GameObject)Instantiate(prefabButton);
			btn.transform.SetParent(ParentPanel, false);
			btn.transform.localScale = new Vector3 (1,1,1);
			//btn.transform.position = new Vector3 (370, 720-(i*110), 0);

			btn_vocab = btn.GetComponent<Button>();

			//set text on vacab button
			btn_vocab_text = btn_vocab.GetComponentInChildren<Text> ();
			if (allKnList [i].type.Length > 1) {
				btn_vocab_text.text = allKnList [i].content + " (" + allKnList[i].type+")";
			} else {
				btn_vocab_text.text = allKnList[i].content;
			}

			//add button into array
			btnArray.Add(i,btn_vocab);

			//add function into button (onclick)
			int index = i;
			string content = btn_vocab_text.text;
			btn_vocab.onClick.AddListener (() => clickContent(index,content));

		}
	}

	//when user click on vocab button
	public void clickContent(int index, string content){
		if (content.Contains(btnArray [index].GetComponentInChildren<Text>().text)) {
			btnArray [index].GetComponentInChildren<Text>().text = allKnList[index].meaning;
			foreach(int key in btnArray.Keys){
				if (!key.Equals (index)) {
					if (allKnList[key].type.Length > 1) {
						btnArray[key].GetComponentInChildren<Text>().text = allKnList[key].content + " (" + allKnList[key].type+")";
					} else {
						btnArray [key].GetComponentInChildren<Text>().text = allKnList[key].content;
					}
				}

			}
		} else {
			btnArray [index].GetComponentInChildren<Text>().text = content;
		}
	}

	public void searchContent(){
		InputField box_search = GameObject.Find ("box_search").GetComponent (typeof(InputField)) as InputField;
		string search = box_search.text;

		Dictionary<int, Knowledge> temp = new Dictionary<int, Knowledge> ();
		allKnList.Clear ();
		genKnowledgeContent ();
		if(search != null || search != ""){
			int index = 0;
			foreach(int key in allKnList.Keys){
				if (allKnList [key].content.Contains (box_search.text)) {
					temp.Add (index++,allKnList[key]);
				}
			}
			destroyBtn ();
			allKnList = temp;
			generateButton ();
			//SceneManager.LoadScene ("kn_content");
		}

	}

	public void destroyBtn(){
		int childs = ParentPanel.childCount;
		for(int i = childs-1 ; i>=0 ; i--){
			GameObject.Destroy (ParentPanel.GetChild(i).gameObject);
		}

	}

	public void speakerEnable(Button speaker){
		if (!speaker.enabled) {
			speaker.image.color = Color.white;
			speaker.enabled = true;
		} else {
			speaker.image.color = Color.clear;
			speaker.enabled = false;
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

		//KnCategoryManagement.searchList = allKnList;
	}
}
