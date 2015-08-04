using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Tuto_Behaviour0 : MonoBehaviour {

	public GameObject tuto;
	public int step = 1;
	private List<GameObject> texts = new List<GameObject>();
	public bool first = true;
	public bool once = true;
	private GameObject currentText;
	private GameObject ship;
	private Rigidbody2D rigid;
	private Zoom zoom;
	private int prevStep;
	private Camera uicamera;
	private GameObject heigthbar;
	
	
	void Awake () {

	prevStep = step;
	ship = GameObject.Find("101(Clone)");
	ship.GetComponent<Rigidbody2D>().fixedAngle = true;
	ship.GetComponent<Rigidbody2D>().isKinematic = true;
	uicamera = GameObject.Find ("UI_Camera").GetComponent<Camera> ();
	rigid = ship.GetComponent<Rigidbody2D>();
	zoom =  ship.transform.FindChild("Zoomer").GetComponent<Zoom>();
	tuto = transform.FindChild("Tutorial").gameObject;
	foreach(Transform child in tuto.transform){
		if(child.transform.parent == tuto.transform){
			texts.Add(child.gameObject);
			if(child.name == "Step1"){
				currentText = child.gameObject;
			}
		}
	}
	heigthbar = transform.FindChild("Tutorial/Step2/ObjectiveHeight").gameObject;
	zoom.enabled = false;
	zoom.cam.ZoomFactor = 1f;
}


void Update () {
	if (prevStep != step) {
		once = true;
		prevStep = step;
	}


	if (Input.touchCount > 0) {
		if (Input.GetTouch (0).phase == TouchPhase.Began) {
			
			Ray ray;
			Ray ray2;
			RaycastHit hit;
			
			ray = uicamera.ScreenPointToRay (Input.GetTouch(0).position);
			ray2 = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			
			if (Physics.Raycast (ray.origin, ray.direction * 100, out hit) || Physics.Raycast (ray2.origin, ray.direction * 100, out hit)) {
				
				if (hit.collider.name == "Exit_Button") {
						dataManger.manager.fuelPowerUps++;
						dataManger.manager.shieldPowerUps++;
						dataManger.manager.magnetPowerUps++;
					dataManger.manager.tutorial = 2;
					dataManger.manager.Save(true);
					Application.LoadLevel("Tuto_2");
				}
			}
		}
	}
	if (step == 1) {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			currentText.SetActive(false);
			currentText = texts.Where(x => x.name == "Step2").SingleOrDefault();
			currentText.SetActive(true);
			nextStep();
		}
	}
	if (step == 2) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			step++;
			transform.FindChild("Tutorial/Step2/Tap_Left").gameObject.SetActive(false);
			transform.FindChild("Tutorial/Step2/Tap_Right").gameObject.SetActive(false);
			ship.GetComponent<Rigidbody2D>().isKinematic = false;
			transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().Play("GhostShip_01");
			transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().wrapMode = WrapMode.Loop;
			
		}
	}
	
	if (step == 3) {
			if(ship.transform.position.y > 860){
				step++;
				transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().Play("GhostShip_02");
				transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().wrapMode = WrapMode.Loop;

		}
	}
	
	if (step == 5) {
			if (Input.touchCount > 0  && Input.GetTouch (0).phase == TouchPhase.Began) {
			
			currentText.SetActive(false);
			transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
			transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_1").gameObject.SetActive(true);
			transform.FindChild("WinLayout").gameObject.SetActive(false);
			step++;
		}


	if (step == 6 & !transform.FindChild ("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_1").gameObject.activeInHierarchy) {
			dataManger.manager.fuelPowerUps++;
			dataManger.manager.shieldPowerUps++;
			dataManger.manager.magnetPowerUps++;
			dataManger.manager.tutorial = 2;
			dataManger.manager.Save(true);
			Application.LoadLevel("Tuto_2");
	}
		
	}

	
}

public void nextStep(){
	StartCoroutine("Onestep");
}

	IEnumerator Onestep(){
		yield return null;
		
		first = false;
		step++;
		
	}
}
