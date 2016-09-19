using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Popup : MonoBehaviour {
	private Canvas canvas;
	private Canvas popupCanvas;
	private Image panel;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas").GetComponent (typeof(Canvas)) as Canvas;
		popupCanvas = GameObject.Find ("popup").GetComponent (typeof(Canvas)) as Canvas;
		panel = GameObject.Find ("Panel").GetComponent (typeof(Image)) as Image;
		canvas.enabled = true;
		popupCanvas.enabled = false;
		panel.enabled = false;
	}
	
	public void showPopup(){
		popupCanvas.enabled = true;
		panel.enabled = true;
	}

	public void closePopup(){
		popupCanvas.enabled = false;
		panel.enabled = false;
	}

	public void closeApplication(){
		Application.Quit();
	}
}
