using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class test : MonoBehaviour {
	public GameObject prefabButton;
	public RectTransform ParentPanel;

	// Use this for initialization
	void Start () {
		for (int i = 1; i <= 5; i++) {
			GameObject goButton = (GameObject)Instantiate(prefabButton);
			goButton.transform.SetParent(ParentPanel, false);
			goButton.transform.localScale = new Vector3 (1,1,1);
			goButton.transform.position = new Vector3 (350, 850-(i*100), 0);
			Button tempButton = goButton.GetComponent<Button>();
		}

	}
}
