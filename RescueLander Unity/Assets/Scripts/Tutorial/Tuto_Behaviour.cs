using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tuto_Behaviour : MonoBehaviour {
	
	private GameObject tuto;
	public int step = 1;
	private List<GameObject> texts = new List<GameObject>();
	private bool first = true;
	private GameObject currentText;
	private GameObject ship;
	private Rigidbody2D rigid;
	private Zoom zoom;

	void Awake () {
		ship = GameObject.Find("101(Clone)");
		rigid = ship.GetComponent<Rigidbody2D>();
		zoom =  ship.transform.FindChild("Zoomer").GetComponent<Zoom>();
		tuto = transform.FindChild("Tutorial").gameObject;
		foreach(Transform child in tuto.transform){
			if(child.transform.parent == tuto.transform){
				texts.Add(child.gameObject);
				if(child.name == "ChatText_1"){
					currentText = child.gameObject;
				}
			}
		}
	}
	

	void Update () {
	
		if(Input.touchCount > 0 & step !=7 || Input.GetMouseButtonUp(0)& step !=7){
			step++;
			first = true;
		}

		if(first){
			foreach(GameObject text in texts){
				if(text.name == "ChatText_"+step){
					currentText.SetActive(false);
					text.SetActive(true);
					currentText = text;
				}
			}

				first = false;
		}
		if(step == 7){
			zoom.enabled = false;
			zoom.zoom = "out";
			zoom.CheckInvoke();
		}
	}
}
