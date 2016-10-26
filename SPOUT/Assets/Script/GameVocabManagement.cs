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
	public Button btn_speaker_1;
	public Button btn_speaker_2;
	public Button btn_speaker_3;
	public Button btn_speaker_4;
	private Dictionary<int,Text> btn_text;
	private Dictionary<int,Button> btn_speaker;

	// Use this for initialization
	void Start () {
		//get all vocab in this event
		getVocab ();

		//set content of topic
		topic = GameObject.Find ("topic").GetComponent (typeof(Text)) as Text;
		topic.text = "คำศัพท์น่ารู้";

		//get text of vocab components
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

		//collect speaker button
		btn_speaker = new Dictionary<int, Button>();
		btn_speaker.Add (1, btn_speaker_1);
		btn_speaker.Add (2, btn_speaker_2);
		btn_speaker.Add (3, btn_speaker_3);
		btn_speaker.Add (4, btn_speaker_4);

		//add click event into button
		btn_vocab_1.onClick.AddListener (() => clickVocab(vocabList[1],btn_vocab_1_text));
		btn_vocab_2.onClick.AddListener (() => clickVocab(vocabList[2],btn_vocab_2_text));
		btn_vocab_3.onClick.AddListener (() => clickVocab(vocabList[3],btn_vocab_3_text));
		btn_vocab_4.onClick.AddListener (() => clickVocab(vocabList[4],btn_vocab_4_text));
	}

	//when user click on vocab button
	public void clickVocab(Knowledge k, Text t){
		//ถ้าบนปุ่มแสดงคำศัพท์อยู่
		if (t.text.Equals (k.content+" ("+k.type+")")) {
			foreach(int id in btn_text.Keys){
				if (!btn_text[id].text.Equals (k.content+" ("+k.type+")")) {
					btn_text[id].text = vocabList[id].content + " ("+vocabList[id].type+")";
					btn_speaker [id].image.color = Color.white;
					btn_speaker [id].enabled = true;
				}
			}
			t.text = k.meaning;
		} else {
			t.text = k.content + " ("+k.type+")";
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
