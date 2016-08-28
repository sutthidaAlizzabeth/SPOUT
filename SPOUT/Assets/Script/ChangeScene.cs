using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public string sceneName;
	private SpriteRenderer spriteRenderer;

	//Use this for initialization
	public void Start (){
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Use this for change scene
	public void Update () {
		if(Input.GetMouseButtonDown(0)){
			Application.LoadLevel(sceneName);
		}
	}

}
