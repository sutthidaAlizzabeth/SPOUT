using UnityEngine;
using System.Collections;

public class BasicPopupManagement : MonoBehaviour {
	public Canvas popupCanvas;

	void Start(){
		popupCanvas.enabled = false;
	}
	
	// Update is called once per frame
	public void ShowPopup () {
		popupCanvas.enabled = true;
	}
}
