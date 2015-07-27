using UnityEngine;
using System.Collections;

public class IceGhost : MonoBehaviour {

	GameObject ship;
	GameObject ghost;
	float time;

	void Awake(){
		ghost = GameObject.Find ("UI_Camera").transform.FindChild ("Tuto_IceBlock").gameObject;
		ship = GameObject.Find (dataManger.manager.actualShip + "(Clone)");
		time = 0;
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.name == "Feet" & (ship.gameObject.transform.eulerAngles.magnitude < 50 || ship.gameObject.transform.eulerAngles.magnitude > 310)){
			time+= Time.deltaTime;
			if (time > 1){
				ghost.SetActive(false);
				Destroy(this);
			}
		}
	}
}
