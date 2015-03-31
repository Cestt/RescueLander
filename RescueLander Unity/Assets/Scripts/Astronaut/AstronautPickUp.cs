using UnityEngine;
using System.Collections;

public class AstronautPickUp : MonoBehaviour {

	public GameObject ship;
	public float pickUpDistance = 200f;
	private ShipAstronautPickUp shipastronautpickup;

	//Javi
	[HideInInspector]public bool picked = false;


	void Awake () {
	
		shipastronautpickup = ship.GetComponent<ShipAstronautPickUp>();


	}
	
	// Update is called once per frame
	void Update () {

		if(ship !=null){

			if(Vector2.Distance(this.transform.position,ship.transform.position) < pickUpDistance * 100f ){
				
				shipastronautpickup.Pickable = true;
				shipastronautpickup.Astronaut = this.gameObject;
				Debug.Log ("Astronaut distance reached");
				picked = true;
				
			}else if(shipastronautpickup.Astronaut == this.gameObject){
				
				shipastronautpickup.Pickable = false;
				shipastronautpickup.Astronaut = null;
				
			}

		}

				
	
	}
}
