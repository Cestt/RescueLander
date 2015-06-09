﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tuto_Behaviour1 : MonoBehaviour {
	
	public GameObject tuto;
	public int step = 1;
	private List<GameObject> texts = new List<GameObject>();
	public bool first = true;
	private GameObject currentText;
	private GameObject ship;
	private Rigidbody2D rigid;
	private Zoom zoom;

	void Awake () {
		ship = GameObject.Find("101(Clone)");
		ship.GetComponent<Rigidbody2D>().fixedAngle = true;
		ship.GetComponent<Rigidbody2D>().isKinematic = true;
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
	
		if(Input.touchCount > 0 & step != 6  & step != 7 & step != 8
		   || Input.GetMouseButtonUp(0) & step != 6 & step != 7  & step != 8){
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
		if(step == 2){
			zoom.enabled = false;
			zoom.zoom = "out";
			zoom.CheckInvoke();
			
		}
		if(step == 5){
			tuto.SetActive(false);
			ship.GetComponent<Rigidbody2D>().isKinematic = false;
			ship.GetComponent<Rigidbody2D>().fixedAngle = false;
			
		}
		if(step == 7 & Input.touchCount > 0 || step == 7 & Input.GetMouseButtonUp(0)){
			tuto.SetActive(false);
			ship.GetComponent<Rigidbody2D>().isKinematic = false;
			ship.GetComponent<Rigidbody2D>().fixedAngle = false;
			transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
			dataManger.manager.shieldPowerUps++;
			dataManger.manager.magnetPowerUps++;
			dataManger.manager.fuelPowerUps++;
			GameObject.Find("Game Manager").GetComponent<WinLose>().End("Win");

			
		}
		if(step == 8 & Input.touchCount > 0 || step == 8 & Input.GetMouseButtonUp(0)){
			Application.LoadLevel("Tuto_"+dataManger.manager.tutorial);
			
		}
	}
}
