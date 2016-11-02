using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KnContentManagement : MonoBehaviour {
	static public Dictionary<int,Knowledge> knList;
	private Text btn_vocab_1_text;

	// Use this for initialization
	void Start () {
		genKnowledgeContent ();
		btn_vocab_1_text = GameObject.Find ("btn_vocab_1_text").GetComponent (typeof(Text)) as Text;
		btn_vocab_1_text.text = knList [1].content;
	}
	
	private void genKnowledgeContent(){
		Dictionary<int,Knowledge> allKnowledge = new Dictionary<int, Knowledge> ();
		allKnowledge = ConnectDatabase.genKnowledgeList ();
		knList = new Dictionary<int, Knowledge> ();

		int count = 1;
		foreach(int key in allKnowledge.Keys){
			if (allKnowledge [key].category_id.Equals (KnCategoryManagement.categoryId)) {
				knList.Add (count++,allKnowledge[key]);
			}
		}
	}
}
