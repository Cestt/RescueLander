using UnityEngine;
using System.Collections;

public class ShipAstronautDrop : MonoBehaviour {

	public tk2dTextMesh text;
	private TextAstronaut textastronaut;
	private Rigidbody2D rigid;
	private ShipAstronautPickUp shipastronautpickup;

	void Start () {

		rigid = this.rigidbody2D;
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		textastronaut = text.GetComponent<TextAstronaut>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D coll) {

		if (coll.gameObject.name == "Platform"){
			
			if(rigid.velocity.magnitude == 0 & shipastronautpickup.astronautPicked != 0){


				dataManger.manager.dropedAstronauts += shipastronautpickup.astronautPicked;
				textastronaut.UpdateText();
				shipastronautpickup.astronautPicked = 0;
				dataManger.manager.Save();
				Debug.Log("Astronaut dropped");
				
			}
			
		}
		
	}
}
