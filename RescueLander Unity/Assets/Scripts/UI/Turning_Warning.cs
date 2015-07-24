using UnityEngine;
using System.Collections;

public class Turning_Warning : MonoBehaviour {

	GameObject lean;
	void Awake () {
		lean = GameObject.Find("LeanDanger_Ico");
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.eulerAngles.magnitude < 325 || transform.eulerAngles.magnitude > 35){
			lean.SetActive(true);
		}else{
			lean.SetActive(false);
		}
	}
}
