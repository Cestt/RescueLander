using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tuto_Behaviour : MonoBehaviour {
	
	public GameObject tuto;
	public int step = 1;
	private List<GameObject> texts = new List<GameObject>();
	public bool first = true;
	private bool once = true;
	private GameObject currentText;
	private GameObject ship;
	private Rigidbody2D rigid;
	private Zoom zoom;
	private int prevStep;
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
		}
	
		if(Input.touchCount > 0  & step !=7 & step !=9 & step != 10 & step != 11
		   || Input.GetMouseButtonUp(0)& step !=7 & step !=9 & step != 10 & step != 11){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
				step++;
				first = true;
				nextStep();
			}

		}


		if(step == 7){
			if(once){
				zoom.enabled = false;
				ship.GetComponent<Rigidbody2D>().isKinematic = false;
				zoom.zoom = "out";
				zoom.CheckInvoke();
				once = false;
			}


		}

		if(step == 9){
			if(once){
			ship.GetComponent<Rigidbody2D>().isKinematic = false;
			ship.GetComponent<Rigidbody2D>().fixedAngle = false;
				once = false;
			}
		}
		if(step == 11 & Input.touchCount > 0 || step == 11 & Input.GetMouseButtonUp(0)){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
			Application.LoadLevel("Tuto_"+dataManger.manager.tutorial);
			}
			
		}
		if(step == 10 & Input.touchCount > 0  || step == 10 & Input.GetMouseButtonUp(0)){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
			transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
			GameObject.Find("Game Manager").GetComponent<WinLose>().End("Win");
			step++;
			first = true;
			nextStep();
			}
			
		}
	}
	public void nextStep(){
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
