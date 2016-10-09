using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameQuizManagement : MonoBehaviour {
	private Canvas popup_correct;
	private Canvas popup_wrong;
	private Image panel;
	private Text dialog;
	private Dictionary<string, Dialog> allThemeDialog;
	private int countDialog;
	private string randomDialog;

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
		countDialog = allThemeDialog.Count;

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
		randomDialog = allThemeDialog[Random.Range(1,countDialog).ToString()].dialog;
		dialog.text = randomDialog;
	}
	
 	//generate all dialog in this theme
	private void genAllThemeDialog(){
		allThemeDialog = new Dictionary<string, Dialog> ();

		//generate all dailog from database
		Dictionary<string,Dialog> allDialogList = new Dictionary<string, Dialog> ();
		allDialogList = Dialog.genDialogList ();

		int count = 1;
		foreach (int event_id in GameLevelManagement.levelList.Keys) {
			foreach (string key in allDialogList.Keys) {
				if (allDialogList [key].event_id.Equals (event_id.ToString())) {
					allThemeDialog.Add (count.ToString (), allDialogList [key]);
					count++;
				}
			}
		}
	
	}
}
