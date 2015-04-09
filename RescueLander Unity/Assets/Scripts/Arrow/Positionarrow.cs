using UnityEngine;
using System.Collections;



public class Positionarrow : MonoBehaviour {

	[HideInInspector]
	public bool visible = true;
	public GameObject ship;
	public int distance;
	private bool first = true;

	void Awake () {


	
	}
	

	void Update () {

		if(ship.renderer.isVisible){
			visible = true;
		}

		if(!ship.renderer.isVisible){
			visible = false;
		}

		if(!visible){


			Vector3 shipPosition = ship.transform.position;
			Vector3 camPosition = new Vector3(Camera.main.transform.position.x + Camera.main.pixelHeight/2, Camera.main.transform.position.y + Camera.main.pixelWidth/2);
			Vector3 temp = camPosition;
			temp.z = 0;
			this.transform.position = temp;

			shipPosition.x = shipPosition.x - camPosition.x;
			shipPosition.y = shipPosition.y - camPosition.y;
			
			float angle = Mathf.Atan2 (shipPosition.y, shipPosition.x) * Mathf.Rad2Deg;
			this.transform.rotation = Quaternion.Euler (new Vector3(0, 0, angle - 90));

			first = true;
			
		}
		
		if(visible){


			if(first){
				Vector2 temp = this.transform.position;
				temp.x = -1000;
				this.transform.position = temp;
				first = false;
			}

		}
	
	}


}
