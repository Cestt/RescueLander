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
	
		if(Input.touchCount > 0  & step !=7 & step !=10 & step != 11 & step != 12
		   || Input.GetMouseButtonUp(0)& step !=7 & step !=10 & step != 11 & step != 12){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
				step++;
				first = true;
				nextStep();
				transform.FindChild("ObjectiveArea").gameObject.SetActive(false);
				transform.FindChild("ObjectiveArea1").gameObject.SetActive(false);
				tuto.gameObject.SetActive(true);
			}

		}
		if(Input.touchCount > 0 &step == 7
		   || Input.GetMouseButtonUp(0)&step == 7){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
				if(ship.GetComponent<Rigidbody2D>().isKinematic)
					ship.GetComponent<Rigidbody2D>().isKinematic = false;
				tuto.SetActive(false);
			}
			
		}

		if(step == 7){
			if(once & ship != null){
				zoom.enabled = false;
				ship.GetComponent<Rigidbody2D>().isKinematic = false;
				transform.FindChild("ObjectiveArea").gameObject.SetActive(true);
				zoom.zoom = "out";
				zoom.CheckInvoke();
				once = false;
			}


		}

		if(step == 10){
			if(once & ship != null){
			ship.GetComponent<Rigidbody2D>().isKinematic = false;
			ship.GetComponent<Rigidbody2D>().fixedAngle = false;
				transform.FindChild("ObjectiveArea1").gameObject.SetActive(true);
				tuto.gameObject.SetActive(false);
				once = false;
			}
		}
		if(step == 12 & Input.touchCount > 0 || step == 12 & Input.GetMouseButtonUp(0)){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
			Application.LoadLevel("Tuto_"+dataManger.manager.tutorial);
			}
			
		}
		if(step == 11 & Input.touchCount > 0  || step == 11 & Input.GetMouseButtonUp(0)){
			if(Input.GetTouch(0).phase == TouchPhase.Began){
			transform.FindChild("Prompt_Menu").gameObject.SetActive(false);
			transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_1").gameObject.SetActive(false);
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
		yield return null;
		if(first){
			first = false;
			foreach(GameObject text in texts){
				if(text.name == "ChatText_"+step){
					currentText.SetActive(false);
					text.SetActive(true);
					currentText = text;
				}
			}

		}
	}
}
