using UnityEngine;
using System.Collections;

public class AstronautPickUp : MonoBehaviour {

	public GameObject ship;
	public int pickUpDistance = 200;
	private ShipAstronautPickUp shipastronautpickup;


	void Start () {
	
		shipastronautpickup = ship.GetComponent<ShipAstronautPickUp>();


	}
	
	// Update is called once per frame
	void Update () {

		if(ship !=null){

			if(Vector2.Distance(this.transform.position,ship.transform.position) < pickUpDistance * 100 ){
				
				shipastronautpickup.Pickable = true;
				shipastronautpickup.Astronaut = this.gameObject;
				Debug.Log ("Astronaut distance reached" + gameObject.name);
				
			}else if(shipastronautpickup.Astronaut == this.gameObject){
				
				shipastronautpickup.Pickable = false;
				shipastronautpickup.Astronaut = null;
				
			}

		}

				
	
	}
}
