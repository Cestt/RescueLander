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
		zoom.cam.ZoomFactor = 1;
	}


	void Update () {
		if (prevStep != step) {
			once = true;
		}

		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				
				Ray ray;
				Ray ray2;
				RaycastHit hit;
				
				ray = uicamera.ScreenPointToRay (Input.mousePosition);
				ray2 = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				if (Physics.Raycast (ray.origin, ray.direction * 100, out hit) || Physics.Raycast (ray2.origin, ray.direction * 100, out hit)) {
					if (hit.collider.name == "Ok_Button") {
						nextStep();
						currentText.transform.FindChild("Prompt_Menu_Turn").gameObject.SetActive(false);
					}
				}
			}
		}
		if (step == 2) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				//currentText
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
			step++;

		}
	}
}
