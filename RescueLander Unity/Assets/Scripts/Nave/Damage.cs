using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public int life = 0;
	public int damageThreshold = 0;
	private ShipAstronautPickUp shipastronautpickup;

	// Use this for initialization
	void Start () {
	
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();

	}
	
	// Update is called once per frame
	void Update () {

		if(life < 0){

			if(shipastronautpickup.Astronaut != null){

				shipastronautpickup.Astronaut = null;

			}

			dataManger.manager.Save();
			Destroy(this.gameObject);

		}

	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(coll.relativeVelocity.magnitude > damageThreshold){

				life -= (int)coll.relativeVelocity.magnitude - damageThreshold;
				Debug.Log("Hull damage: " + ((int)coll.relativeVelocity.magnitude - damageThreshold));

			}else{

				Debug.Log("No hull damage");

			}
		}
	}
	
}
