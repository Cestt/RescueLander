using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {

	GameObject camera2D;
	GameObject ship;
	GameObject[] backA = new GameObject[3];
	GameObject[] backB = new GameObject[3];
	float posX;
	float shipPosX;
	public float velA = 0.5f;
	public float velB = 0.25f;
	bool first;

	void Awake () {
		camera2D = GameObject.Find ("Camera 2DTK");
		backA [0] = transform.FindChild ("MidParalax_A").gameObject;
		backA [1] = transform.FindChild ("MidParalax_A 1").gameObject;
		backA [2] = transform.FindChild ("MidParalax_A 2").gameObject;
		backB [0] = transform.FindChild ("MidParalax_B").gameObject;
		backB [1] = transform.FindChild ("MidParalax_B 1").gameObject;
		backB [2] = transform.FindChild ("MidParalax_B 2").gameObject;
		posX = camera2D.transform.position.x;
		first = true;
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (first) {
			//posX = camera2D.transform.position.x;
			//shipPosX = ship.transform.position.x;
			posX = ship.transform.position.x;
			first = false;
			return;
		}
		/*float distX = camera2D.transform.position.x - posX;
		float distShip = Mathf.Abs(ship.transform.position.x - camera2D.transform.position.x);
		float distShipX = ship.transform.position.x - shipPosX;
		if (distX != 0 & distShip < 200 & Mathf.Abs (distShipX) > 0.5f) {
			for (int i=0; i<3; i++) {
				backA [i].transform.localPosition = new Vector3 (backA [i].transform.localPosition.x - (distX * velA),
			                                               backA [i].transform.localPosition.y);
			}
		
			for (int i=0; i<3; i++) {
				backB [i].transform.localPosition = new Vector3 (backB [i].transform.localPosition.x - (distX * velB),
			                                               backB [i].transform.localPosition.y);
			}
		}
		posX = camera2D.transform.position.x;
		shipPosX = ship.transform.position.x;*/
		if (ship != null) {
			float distX = ship.transform.position.x - posX;
			if (distX != 0) {
				for (int i=0; i<3; i++) {
					backA [i].transform.localPosition = new Vector3 (backA [i].transform.localPosition.x - (distX * velA),
				                                                 backA [i].transform.localPosition.y, 800);
				}
				for (int i=0; i<3; i++) {
					backB [i].transform.localPosition = new Vector3 (backB [i].transform.localPosition.x - (distX * velB),
				                                                 backB [i].transform.localPosition.y, 850);
				}
			}
			posX = ship.transform.position.x;
		}
	}
}
