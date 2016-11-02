using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class KnCategoryManagement : MonoBehaviour {
	private Text btn_cate_1_text;
	private Text btn_cate_2_text;
	private Text btn_cate_3_text;
	private Dictionary<int,Category> categoryList;
	static public int categoryId;

	// Use this for initialization
	void Start () {
		getCategory ();
		btn_cate_1_text = GameObject.Find ("btn_cate_1_text").GetComponent (typeof(Text)) as Text;
		btn_cate_2_text = GameObject.Find ("btn_cate_2_text").GetComponent (typeof(Text)) as Text;
		btn_cate_1_text.text = categoryList [1].topic_thai;
		btn_cate_2_text.text = categoryList [2].topic_thai;
	}

	public void gotoKnContent(Text btn){
		foreach(int key in categoryList.Keys){
			if(categoryList[key].topic_thai.Equals(btn.text)){
				categoryId = key+1;
			}
		}
		SceneManager.LoadScene ("kn_content");
	}
	
	private void getCategory(){
		Dictionary<int, Category> allCategoryList = new Dictionary<int, Category> ();
		allCategoryList = ConnectDatabase.genCategoryList ();

		int count = 1;
		categoryList = new Dictionary<int, Category> ();
		foreach(int key in allCategoryList.Keys){
			if (!key.Equals (1)) {
				categoryList.Add (count++,allCategoryList[key]);
			}
		}
	}
}
