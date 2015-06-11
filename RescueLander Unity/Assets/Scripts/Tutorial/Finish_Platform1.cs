using UnityEngine;
using System.Collections;

public class Finish_Platform1 : MonoBehaviour {
	private Tuto_Behaviour2 tuto;
	// Use this for initialization
	void Awake () {
		tuto = GameObject.Find("UI_Camera").GetComponent<Tuto_Behaviour2>();
	}
	
	void OnCollisionStay2D(Collision2D coll) {
		Debug.Log("Enter");
		if(coll.gameObject.name == "101(Clone)" & coll.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f 
		   & coll.gameObject.GetComponent<ShipAstronautPickUp>().astronautPicked == 1){
			coll.gameObject.GetComponent<ShipAstronautPickUp>().astronautPicked = 0;
			GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
			GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_3").gameObject.SetActive(true);
			tuto.step++;
			tuto.first = false;
		}
		
	}
	void Update () {
	
	}
}
