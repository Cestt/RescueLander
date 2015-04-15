using UnityEngine;
using System.Collections;



public class Positionarrow : MonoBehaviour {

	[HideInInspector]
	public bool visible = true;
	public GameObject ship;
	public int distance;
	private bool first = true;
	private tk2dTextMesh text;
	private Transform arrowText;
	private Quaternion rotation;
	private Vector3 position;

	void Awake () {

		arrowText = transform.FindChild("Arrow_Text");
		text = arrowText.gameObject.GetComponent<tk2dTextMesh>();
		rotation = arrowText.gameObject.transform.rotation;
	
	}
	

	void Update () {

		if(ship != null){
			if(ship.GetComponent<Renderer>().isVisible){
				visible = true;
			}
			
			if(!ship.GetComponent<Renderer>().isVisible){
				visible = false;
			}
		}


		if(!visible & ship != null){

			position = arrowText.transform.localPosition;
			Vector3 shipPosition = ship.transform.position;
			Vector3 camPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);
			Vector3 temp = camPosition;
			temp.z = 0;
			this.transform.position = temp;

			shipPosition.x = shipPosition.x - camPosition.x;
			shipPosition.y = shipPosition.y - camPosition.y;
			
			float angle = Mathf.Atan2 (shipPosition.y, shipPosition.x) * Mathf.Rad2Deg;
			this.transform.rotation = Quaternion.Euler (new Vector3(0, 0, angle + 180));

			distance = (int)Vector2.Distance(shipPosition,transform.position);
			text.text = ((int)(distance/100)).ToString();

			arrowText.gameObject.transform.localPosition = position;
			arrowText.gameObject.transform.rotation = rotation;
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
