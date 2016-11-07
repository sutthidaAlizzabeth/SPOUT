using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameQuizeeeManagement : MonoBehaviour {
	private Canvas canvas_main;
	private Canvas popup_correct;
	private Canvas popup_wrong;
	private Image panel;
	private Text dialog;
	private Dictionary<int, Dialog> allThemeDialog; //all dialog in this theme
	private int countDialog;// allThemeDialog.Count
	private string randomDialog; //get from allThemeDialog, and will show in scene
	private int countQuiz; //number of questions that user test
	private int randomKey; //random key

	// Use this for initialization
	void Start () {
		//get component from unity (game_quiz scene)
		canvas_main = GameObject.Find ("Canvas").GetComponent (typeof(Canvas)) as Canvas;
		popup_correct = GameObject.Find ("popup_correct").GetComponent (typeof(Canvas)) as Canvas;
		popup_wrong = GameObject.Find ("popup_wrong").GetComponent (typeof(Canvas)) as Canvas;
		panel = GameObject.Find ("Panel").GetComponent (typeof(Image)) as Image;
		dialog = GameObject.Find ("dialog").GetComponent (typeof(Text)) as Text;

		//get all dialog in this theme
		genAllThemeDialog ();

		//count amount of all dialog in allThemeDialog
		countDialog = allThemeDialog.Count;

		//start quiz with first question (เริ่มที่ข้อ 1) and random dialog to be a question
		countQuiz = 1;
		randomQuizDialog ();
//
//		Text topic = GameObject.Find ("topic").GetComponent (typeof(Text)) as Text;
//		topic.text = "Alizzabeth";

	}

	//check canvase will show after user finish using microphone
	public void showCheckBox(){
		popup_correct.enabled = true;
		panel.enabled = true;
	}

	//close check canvas when user click "next" button on check canvas
	private void closeCheckBox(){
		popup_wrong.enabled = false;
		popup_correct.enabled = false;
		panel.enabled = false;
	}

	//random a dialog from allThemeDialog (all dialog in choosed theme) to be a question
	public void randomQuizDialog(){
		//random only 10 questions for a round of quiz
		if (countQuiz <= 10) {
			//close check canvas before randoming next question
			closeCheckBox ();

			//randomimg a key of dialog until it is exist key
			do {
				randomKey = Random.Range (1, countDialog);
			} while(!allThemeDialog.ContainsKey (randomKey));
				
			countQuiz++;

//			GameObject canvas_main = new GameObject ("canvas_main");
//			canvas_main.transform.SetParent (this.transform);
//
//			Text test1 = canvas_main.gameObject.AddComponent<Text> ();
//			test1.text = "Yahhhhh!!!";

			//show question (dialog) and then remove it
			randomDialog = allThemeDialog [randomKey].dialog;
			allThemeDialog.Remove (randomKey);
			dialog.text = randomDialog;

		} else {
			//if user finish 10 dialogs testing, load a result scene 
			TextToSpeech t = new TextToSpeech();
			t.OnDestroy();
			SceneManager.LoadScene ("game_quiz_total");
		}
	}
	
 	//generate all dialog in this theme
	private void genAllThemeDialog(){
		allThemeDialog = new Dictionary<int, Dialog> ();

		//generate all dailog from database
		Dictionary<int,Dialog> allDialogList = new Dictionary<int, Dialog> ();
		allDialogList = ConnectDatabase.genDialogList ();//Dialog.genDialogList ();

		//count variable will be a key of allThemeDialog
		int count = 1;
		//get all event keys in this theme and match them with event_id in all dialog
		foreach (int event_id in GameLevelManagement.levelList.Keys) {
			foreach (int id in allDialogList.Keys) {
				if (allDialogList [id].event_id.Equals (event_id) && allDialogList[id].quiz.Equals(1)) {
					allThemeDialog.Add (count, allDialogList [id]);
					count++;
				}
			}
		}
	
	}
}
