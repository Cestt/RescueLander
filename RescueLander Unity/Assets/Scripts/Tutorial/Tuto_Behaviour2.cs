using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tuto_Behaviour2 : MonoBehaviour {
	
	public GameObject tuto;
	public int step = 1;
	private List<GameObject> texts = new List<GameObject>();
	public bool first = true;
	private GameObject currentText;
	private GameObject ship;
	private Rigidbody2D rigid;
	private Zoom zoom;
	private int prevStep;
	private bool once = true;

	void Awake () {
		prevStep = step;
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
		if(prevStep != step){
			once = true;
			prevStep = step;
		}
	
		if(Input.touchCount > 0 & step != 3 
		   || Input.GetMouseButtonUp(0) & step != 3 ){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
			step++;
			first = true;
			nextStep();
			}
		}


		if(step == 2){
			if(once){
				zoom.enabled = false;
				zoom.zoom = "out";
				zoom.CheckInvoke();
				once = false;
			}

			
		}
		if(step == 3){
			if(once){
				zoom.enabled = true;
				tuto.SetActive(false);
				ship.GetComponent<Rigidbody2D>().isKinematic = false;
				ship.GetComponent<Rigidbody2D>().fixedAngle = false;
				once = false;
			}

			
		}

		if(step == 5){
			if(once){
				tuto.SetActive(false);
				transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
				dataManger.manager.coins += 500;
				GameObject.Find("Game Manager").GetComponent<WinLose>().End("Win");
				once = false;
				step++;
			}
			
		}
		if(step == 6){
			if(once){
				Application.LoadLevel("Menu");
				once = false;
			}
			
		}

	}
	void nextStep(){
		StartCoroutine("Onestep");
	}
	IEnumerator Onestep(){
		if(first){
			foreach(GameObject text in texts){
				if(text.name == "ChatText_"+step){
					currentText.SetActive(false);
					text.SetActive(true);
					currentText = text;
				}
			}
			
			first = false;
			yield return null;
		}
	}
}
