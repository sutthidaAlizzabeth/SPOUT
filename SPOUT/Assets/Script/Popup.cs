using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Popup : MonoBehaviour {
	private Canvas canvas;
	private Canvas settingCanvas;
	private Image panel;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas").GetComponent (typeof(Canvas)) as Canvas;
		settingCanvas = GameObject.Find ("setting").GetComponent (typeof(Canvas)) as Canvas;
		panel = GameObject.Find ("Panel").GetComponent (typeof(Image)) as Image;
		canvas.enabled = true;
		settingCanvas.enabled = false;
		panel.enabled = false;

	}
	
	public void showSettingPopup(){
		settingCanvas.enabled = true;
		panel.enabled = true;

	}

	public void closeSettingPopup(){
		settingCanvas.enabled = false;
		panel.enabled = false;
	}

	public void showExitPopup(){
		Canvas exitCanvas = GameObject.Find ("exit").GetComponent (typeof(Canvas)) as Canvas;
		exitCanvas.enabled = true;
		settingCanvas.enabled = false;
		panel.enabled = true;
	}

	public void closeExitPopup(){
		Canvas exitCanvas = GameObject.Find ("exit").GetComponent (typeof(Canvas)) as Canvas;
		exitCanvas.enabled = false;
		settingCanvas.enabled = false;
		panel.enabled = false;
	}

	public void closeApplication(){
		Application.Quit();
	}

	public void closeWarningPopup(){
		Canvas warnningCanvas = GameObject.Find ("warning").GetComponent (typeof(Canvas)) as Canvas;
		warnningCanvas.enabled = false;
		settingCanvas.enabled = false;
		panel.enabled = false;
	}

	public void showWarningPopup(){
		Canvas warnningCanvas = GameObject.Find ("warning").GetComponent (typeof(Canvas)) as Canvas;
		warnningCanvas.enabled = true;
	}
}
