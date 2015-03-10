using UnityEngine;
using System.Collections;

public class ShipAstronautDrop : MonoBehaviour {


	private Rigidbody2D rigid;
	private ShipAstronautPickUp shipastronautpickup;
	private int astronautsDroped;
	public int totalAstronauts;

	void Awake () {

		rigid = this.rigidbody2D;
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D coll) {

		if (coll.gameObject.name == "Platform"){
			
			if(rigid.velocity.magnitude == 0 & shipastronautpickup.astronautPicked != 0){

				shipastronautpickup.astronautPicked = 0;
				astronautsDroped += shipastronautpickup.astronautPicked;
				dataManger.manager.Save();
				Debug.Log("Astronaut dropped");
				
			}

			if(astronautsDroped == totalAstronauts){


			}
			
		}
		
	}
}
