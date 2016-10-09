using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameQuizManagement : MonoBehaviour {
	private Canvas popup_correct;
	private Canvas popup_wrong;
	private Image panel;
	private Text dialog;
	private Dictionary<string, Dialog> allThemeDialog; //all dialog in this theme
	private int countDialog;// allThemeDialog.Count
	private string randomDialog; //get from allThemeDialog, and will show in scene
	private int countQuiz; //number of questions that user test
	private int randomKey; //random key

	// Use this for initialization
	void Start () {
		//get component from unity (game_quiz scene)
		popup_correct = GameObject.Find ("popup_correct").GetComponent (typeof(Canvas)) as Canvas;
		popup_wrong = GameObject.Find ("popup_wrong").GetComponent (typeof(Canvas)) as Canvas;
		panel = GameObject.Find ("Panel").GetComponent (typeof(Image)) as Image;
		dialog = GameObject.Find ("dialog").GetComponent (typeof(Text)) as Text;

		//close check canvas as a default

		//get all dialog in this theme
		genAllThemeDialog ();

		countQuiz = 1;
		randomQuizDialog ();

	}

	public void showCheckBox(){
		popup_correct.enabled = true;
		panel.enabled = true;
	}

	private void closeCheckBox(){
		popup_wrong.enabled = false;
		popup_correct.enabled = false;
		panel.enabled = false;
	}

	public void randomQuizDialog(){
		closeCheckBox ();
		countDialog = allThemeDialog.Count;
		if (countQuiz <= 10) {
			do {
				randomKey = Random.Range (1, countDialog);
			} while(!allThemeDialog.ContainsKey (randomKey.ToString ()));
			countQuiz++;
			randomDialog = allThemeDialog [randomKey.ToString ()].dialog;
			allThemeDialog.Remove (randomKey.ToString ());
			dialog.text = randomDialog;
		} else {
			SceneManager.LoadScene ("game_quiz_total");
		}
	}
	
 	//generate all dialog in this theme
	private void genAllThemeDialog(){
		allThemeDialog = new Dictionary<string, Dialog> ();

		//generate all dailog from database
		Dictionary<string,Dialog> allDialogList = new Dictionary<string, Dialog> ();
		allDialogList = Dialog.genDialogList ();

		int count = 1;
		foreach (int event_id in GameLevelManagement.levelList.Keys) {
			foreach (string id in allDialogList.Keys) {
				if (allDialogList [id].event_id.Equals (event_id.ToString())) {
					allThemeDialog.Add (count.ToString (), allDialogList [id]);
					count++;
				}
			}
		}
	
	}
}
