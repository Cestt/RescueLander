using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Tuto_Behaviour : MonoBehaviour {
	
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
		zoom.enabled = false;
		zoom.cam.ZoomFactor = 1.25f;
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
					if (hit.collider.name == "Ok_Button") {
						nextStep();
						currentText.transform.FindChild("Prompt_Menu_Turn").gameObject.SetActive(false);
					}
					if (hit.collider.name == "Exit_Button") {
						dataManger.manager.tutorial = 4;
						dataManger.manager.Save(true);
						Application.LoadLevel("Menu");
					}
				}
			}
		}
		if (step == 2) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				currentText.SetActive(false);
				currentText = texts.Where(x => x.name == "Step2").SingleOrDefault();
				currentText.SetActive(true);
				transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().Play("GhostShip_03");
				transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().wrapMode = WrapMode.Loop;
				nextStep();
			}
		}
		if (step == 3) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				transform.FindChild("Tutorial/Step2/Chat_Astronaut").gameObject.SetActive(false);
				transform.FindChild("Tutorial/Step2/Chat_Box").gameObject.SetActive(false);
				transform.FindChild("Tutorial/Step2/ChatText_1").gameObject.SetActive(false);
				transform.FindChild("Tutorial/Step2/TapToContinue").gameObject.SetActive(false);
				ship.GetComponent<Rigidbody2D>().fixedAngle = false;
				ship.GetComponent<Rigidbody2D>().isKinematic = false;
				nextStep();
			}
		}
		if (step == 5) {
			if(once){
				transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().Stop();
				transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().Play("GhostShip_04");
				transform.FindChild("Tutorial/Step2/GhostShip").GetComponent<Animation>().wrapMode = WrapMode.Loop;
				once = false;
			}
		}
		if (step == 6) {
			if(once){
				GameObject.Find("Game Manager").GetComponent<WinLose>().End("Win");
				step++;
			}
		}
		if (step == 7) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {

				currentText.SetActive(false);
				transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
				transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_2").gameObject.SetActive(true);
				transform.FindChild("WinLayout").gameObject.SetActive(false);
				step++;
			}


			
		}

		if (step == 8 & !transform.FindChild ("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_2").gameObject.activeInHierarchy) {
			dataManger.manager.tutorial = 4;
			dataManger.manager.Save(true);
			Application.LoadLevel("Menu");
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
