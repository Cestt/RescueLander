using UnityEngine;
using System.Collections;

public class Finish_Platform : MonoBehaviour {
	private Tuto_Behaviour1 tuto;
	private bool first = true;
	// Use this for initialization
	void Awake () {
		tuto = GameObject.Find("UI_Camera").GetComponent<Tuto_Behaviour1>();
	}
	
	void OnCollisionStay2D(Collision2D coll) {

		if(coll.gameObject.name == "101(Clone)" & coll.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f & first){
			first = false;
			GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
			GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_2").gameObject.SetActive(true);
			tuto.step++;
			tuto.first = false;
		}
		
	}
	void Update () {
	
	}
}
