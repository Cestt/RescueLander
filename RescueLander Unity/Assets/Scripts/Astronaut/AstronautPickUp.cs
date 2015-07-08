using UnityEngine;
using System.Collections;

public class AstronautPickUp : MonoBehaviour {

	private GameObject ship;
	public float pickUpDistance = 200f;
	private ShipAstronautPickUp shipastronautpickup;
	private GameObject iceCube;

	void Awake () {
		if (Application.loadedLevelName.Contains ("Tuto")){
			ship = GameObject.Find ("101(Clone)");
		}else{
			ship = GameObject.Find (dataManger.manager.actualShip+"(Clone)");
		}
		shipastronautpickup = ship.GetComponent<ShipAstronautPickUp>();
		//iceCube = transform.FindChild ("IceCube").gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if(ship !=null){

			if(Vector2.Distance(this.transform.position,ship.transform.position) < pickUpDistance * 100f){
				
				shipastronautpickup.Pickable = true;
				shipastronautpickup.Astronaut = this.gameObject;

				
			}else if(shipastronautpickup.Astronaut == this.gameObject){
				
				shipastronautpickup.Pickable = false;
				shipastronautpickup.Astronaut = null;
				
			}

		}

				
	
	}
}
