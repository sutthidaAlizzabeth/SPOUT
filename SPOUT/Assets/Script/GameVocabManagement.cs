using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameVocabManagement : MonoBehaviour {
	static public Dictionary<int,Knowledge> vocabList;
	private Dictionary<int,Text> btnTextArray;
	public GameObject prefabButton;
	public GameObject prefabSpeaker;
	public RectTransform ParentPanel;
//	public Text btn_vocab_1_text;
//	public Text btn_vocab_2_text;
//	public Text btn_vocab_3_text;
//	public Text btn_vocab_4_text;
//	public Button btn_vocab_1;
//	public Button btn_vocab_2;
//	public Button btn_vocab_3;
//	public Button btn_vocab_4;
//	public Button btn_speaker_1;
//	public Button btn_speaker_2;
//	public Button btn_speaker_3;
//	public Button btn_speaker_4;
//	private Dictionary<int,Text> btn_text;
//	private Dictionary<int,Button> btn_speaker;

	// Use this for initialization
	void Start () {
		//get all vocab in this event
		getVocab ();

		generateButton ();

//		//get text of vocab components
//		btn_vocab_1_text.text = vocabList [1].content + " (" + vocabList[1].type+")";
//		btn_vocab_2_text.text = vocabList [2].content + " (" + vocabList[2].type+")";
//		btn_vocab_3_text.text = vocabList [3].content + " (" + vocabList[3].type+")";
//		btn_vocab_4_text.text = vocabList [4].content + " (" + vocabList[4].type+")";
//
//		//collect text in vocab button
//		btn_text = new Dictionary<int, Text>();
//		btn_text.Add (1,btn_vocab_1_text);
//		btn_text.Add (2,btn_vocab_2_text);
//		btn_text.Add (3,btn_vocab_3_text);
//		btn_text.Add (4,btn_vocab_4_text);
//
//		//collect speaker button
//		btn_speaker = new Dictionary<int, Button>();
//		btn_speaker.Add (1, btn_speaker_1);
//		btn_speaker.Add (2, btn_speaker_2);
//		btn_speaker.Add (3, btn_speaker_3);
//		btn_speaker.Add (4, btn_speaker_4);
//
//		//add click event into button
//		btn_vocab_1.onClick.AddListener (() => clickVocab(vocabList[1],btn_vocab_1_text));
//		btn_vocab_2.onClick.AddListener (() => clickVocab(vocabList[2],btn_vocab_2_text));
//		btn_vocab_3.onClick.AddListener (() => clickVocab(vocabList[3],btn_vocab_3_text));
//		btn_vocab_4.onClick.AddListener (() => clickVocab(vocabList[4],btn_vocab_4_text));
	}

	//when user click on vocab button
//	public void clickVocab(Knowledge k, Text t){
//		//ถ้าบนปุ่มแสดงคำศัพท์อยู่
//		if (t.text.Equals (k.content+" ("+k.type+")")) {
//			foreach(int id in btn_text.Keys){
//				if (!btn_text[id].text.Equals (k.content+" ("+k.type+")")) {
//					btn_text[id].text = vocabList[id].content + " ("+vocabList[id].type+")";
//					btn_speaker [id].image.color = Color.white;
//					btn_speaker [id].enabled = true;
//				}
//			}
//			t.text = k.meaning;
//		} else {
//			t.text = k.content + " ("+k.type+")";
//		}
//	}

	private void generateButton(){
		//set amount of button 
		int amount = vocabList.Count;

		btnTextArray = new Dictionary<int, Text> ();
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
			btn_vocab_text.text = vocabList [i].content + " (" + vocabList[i].type+")";

			//add button into array
			btnTextArray.Add(i,btn_vocab_text);

			//add function into button (onclick)
			int index = i;
			string content = btn_vocab_text.text;
			btn_vocab.onClick.AddListener (() => clickContent(index,content));
		}
	}

	//when user click on vocab button
	public void clickContent(int index, string content){
		if (content.Contains(btnTextArray [index].text)) {
			btnTextArray [index].text = vocabList[index].meaning;
			foreach(int key in btnTextArray.Keys){
				if (!key.Equals (index)) {
					if (vocabList[key].type.Length > 1) {
						btnTextArray[key].text = vocabList [key].content + " (" + vocabList[key].type+")";
					} else {
						btnTextArray [key].text = vocabList [key].content;
					}
				}

			}
		} else {
			btnTextArray [index].text = content;
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

	private void getVocab(){
		int num = 0;
		//prepare data set and variable
		Dictionary<int, Knowledge> allKnowledgeList = ConnectDatabase.genKnowledgeList();//Knowledge.genKnowledgeList ();
		Dictionary<string,Vocabulary> allVocabList = ConnectDatabase.genVocabularyList();//Vocabulary.genVocabularyList ();
		ArrayList knowledgeKeyList = new ArrayList ();
		vocabList = new Dictionary<int, Knowledge> ();

		//get all knowledge_id in selected event (level object)
		foreach(string key in allVocabList.Keys){
			if (allVocabList [key].event_id == GameLevelManagement.level.id) {
				knowledgeKeyList.Add (allVocabList [key].knowledge_id);
			}
		}

		//get all knowledge object that knowledge_id is in knowledgeKeyList
		foreach(int key in allKnowledgeList.Keys){
			if (knowledgeKeyList.Contains (allKnowledgeList [key].id)) {
				vocabList.Add (num++, allKnowledgeList[key]);
			}
		}
			
	}
}
