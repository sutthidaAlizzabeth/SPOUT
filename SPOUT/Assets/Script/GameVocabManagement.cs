using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameVocabManagement : MonoBehaviour {
	static public Dictionary<int,Knowledge> vocabList;
	public Text topic;
	public Text btn_vocab_1_text;
	public Text btn_vocab_2_text;
	public Text btn_vocab_3_text;
	public Text btn_vocab_4_text;
	public Button btn_vocab_1;
	public Button btn_vocab_2;
	public Button btn_vocab_3;
	public Button btn_vocab_4;
	private Dictionary<int,Text> btn_text;

	// Use this for initialization
	void Start () {
		//get all vocab in this event
		getVocab ();

		//set content of topic
		topic = GameObject.Find ("topic").GetComponent (typeof(Text)) as Text;
		topic.text = "คำศัพท์น่ารู้";

		//get text of vocab components
//		btn_vocab_1_text = GameObject.Find ("btn_vocab_1_text").GetComponent (typeof(Text)) as Text;
//		btn_vocab_2_text = GameObject.Find ("btn_vocab_2_text").GetComponent (typeof(Text)) as Text;
//		btn_vocab_3_text = GameObject.Find ("btn_vocab_3_text").GetComponent (typeof(Text)) as Text;
//		btn_vocab_4_text = GameObject.Find ("btn_vocab_4_text").GetComponent (typeof(Text)) as Text;
		btn_vocab_1_text.text = vocabList [1].content + " (" + vocabList[1].type+")";
		btn_vocab_2_text.text = vocabList [2].content + " (" + vocabList[2].type+")";
		btn_vocab_3_text.text = vocabList [3].content + " (" + vocabList[3].type+")";
		btn_vocab_4_text.text = vocabList [4].content + " (" + vocabList[4].type+")";

		//collect text in vocab button
		btn_text = new Dictionary<int, Text>();
		btn_text.Add (1,btn_vocab_1_text);
		btn_text.Add (2,btn_vocab_2_text);
		btn_text.Add (3,btn_vocab_3_text);
		btn_text.Add (4,btn_vocab_4_text);

		//get button components
//		btn_vocab_1 = GameObject.Find("btn_vocab_1").GetComponent(typeof(Button)) as Button;
//		btn_vocab_2 = GameObject.Find("btn_vocab_2").GetComponent(typeof(Button)) as Button;
//		btn_vocab_3 = GameObject.Find("btn_vocab_3").GetComponent(typeof(Button)) as Button;
//		btn_vocab_4 = GameObject.Find("btn_vocab_4").GetComponent(typeof(Button)) as Button;

		//add click event into button
		btn_vocab_1.onClick.AddListener (() => clickVocab(vocabList[1],btn_vocab_1_text));
		btn_vocab_2.onClick.AddListener (() => clickVocab(vocabList[2],btn_vocab_2_text));
		btn_vocab_3.onClick.AddListener (() => clickVocab(vocabList[3],btn_vocab_3_text));
		btn_vocab_4.onClick.AddListener (() => clickVocab(vocabList[4],btn_vocab_4_text));
	}

	public void clickVocab(Knowledge k, Text t){
		if (t.text.Equals (k.content+" ("+k.type+")")) {
			foreach(int id in btn_text.Keys){
				if (!btn_text[id].text.Equals (k.content+" ("+k.type+")")) {
					btn_text[id].text = vocabList[id].content + " ("+vocabList[id].type+")";
				}
			}
			t.text = k.meaning;
		} else {
			t.text = k.content + " ("+k.type+")";
		}
	}

	private void getVocab(){
		int num = 1;
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
