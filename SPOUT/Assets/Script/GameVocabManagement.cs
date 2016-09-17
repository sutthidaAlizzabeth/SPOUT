using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameVocabManagement : MonoBehaviour {
	static public ArrayList vocabList;
	private Text topic;

	// Use this for initialization
	void Start () {
		getVocab ();
		topic = GameObject.Find ("topic").GetComponent (typeof(Text)) as Text;
		topic.text = vocabList.Count.ToString();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void getVocab(){
		//prepare data set and variable
		Dictionary<int, Knowledge> allKnowledgeList = Knowledge.genKnowledgeList ();
		Dictionary<string,Vocabulary> allVocabList = Vocabulary.genVocabularyList ();
		ArrayList knowledgeKeyList = new ArrayList ();
		vocabList = new ArrayList ();

		//get all knowledge_id in selected event (level object)
		foreach(string key in allVocabList.Keys){
			if (allVocabList [key].event_id == GameLevelManagement.level.id) {
				knowledgeKeyList.Add (allVocabList [key].knowledge_id);
			}
		}

		//get all knowledge object that knowledge_id is in knowledgeKeyList
		foreach(int key in allKnowledgeList.Keys){
			if (knowledgeKeyList.Contains (allKnowledgeList [key].id)) {
				vocabList.Add (allKnowledgeList[key]);
			}
		}
			
	}
}
