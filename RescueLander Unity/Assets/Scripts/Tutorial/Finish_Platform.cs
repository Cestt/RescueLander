using UnityEngine;
using System.Collections;

public class Finish_Platform : MonoBehaviour {
	private Tuto_Behaviour0 tuto;
	private bool first = true;
	// Use this for initialization
	void Awake () {
		tuto = GameObject.Find("UI_Camera").GetComponent<Tuto_Behaviour0>();
	}
	
	void OnCollisionStay2D(Collision2D coll) {

		if(coll.gameObject.name == "101(Clone)" & coll.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f & first & tuto.step == 4){
			first = false;
			GameObject.Find("Game Manager").GetComponent<WinLose>().End("Win");
			tuto.step++;
			tuto.first = false;
		}
		
	}
	void Update () {
	
	}
}
