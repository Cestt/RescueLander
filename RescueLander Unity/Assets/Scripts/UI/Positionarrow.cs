using UnityEngine;
using System.Collections;



public class Positionarrow : MonoBehaviour {

	//[HideInInspector]
	public bool visible = true;
	public GameObject ship;
	public int distance;
	private bool first = true;
	private tk2dTextMesh text;
	private Transform arrowText;
	private Quaternion rotation;
	private Vector3 position;
	private Renderer render;
	private GameObject shipIco;
	private Vector2 tempVector;
	private tk2dCamera tk2dcamera;

	void Awake () {
		ship = GameObject.Find(dataManger.manager.actualShip+"(Clone)");
		tk2dcamera = Camera.main.GetComponent<tk2dCamera>();
		render = ship.GetComponent<Renderer> ();
		arrowText = transform.FindChild("Distance");
		text = arrowText.gameObject.GetComponent<tk2dTextMesh>();
		rotation = arrowText.gameObject.transform.rotation;
		shipIco = gameObject.transform.FindChild ("ShipIco").gameObject;
	
	}
	

	void Update () {
		if(distance/10 > 100 & distance/10 < 150 & ship != null){

		}
		if(distance/10 > 200  & ship !=null){
			ship.GetComponent<Damage>().life = -1;
		}

		if(ship != null){
			if(render.isVisible){
				visible = true;
			}
			
			if(!render.isVisible){
				visible = false;
			}
		}


		if(!visible & ship != null){
			if(Camera.main.transform.position.x >ship.transform.position.x){
				if(Camera.main.transform.position.x - tk2dcamera.nativeResolutionWidth/2/tk2dcamera.ZoomFactor > ship.transform.position.x){
					tempVector.x = Camera.main.transform.position.x - tk2dcamera.nativeResolutionWidth/2/tk2dcamera.ZoomFactor;
				}else{ 
					tempVector.x = ship.transform.position.x;
				}
			}else{
				if(Camera.main.transform.position.x + tk2dcamera.nativeResolutionWidth/2/tk2dcamera.ZoomFactor < ship.transform.position.x){
					tempVector.x = Camera.main.transform.position.x + tk2dcamera.nativeResolutionWidth/2/tk2dcamera.ZoomFactor;
				}else{ 
					tempVector.x = ship.transform.position.x;
				}
			}

			if(Camera.main.transform.position.y >ship.transform.position.y){
				if(Camera.main.transform.position.y - tk2dcamera.nativeResolutionHeight/2/tk2dcamera.ZoomFactor > ship.transform.position.y){
					tempVector.y = Camera.main.transform.position.y - tk2dcamera.nativeResolutionHeight/2/tk2dcamera.ZoomFactor;
				}else{ 
					tempVector.y = ship.transform.position.y;
				}
			}else{
				if(Camera.main.transform.position.y + tk2dcamera.nativeResolutionHeight/2/tk2dcamera.ZoomFactor < ship.transform.position.y){
					tempVector.y = Camera.main.transform.position.y + tk2dcamera.nativeResolutionHeight/2/tk2dcamera.ZoomFactor;
				}else{ 
					tempVector.y = ship.transform.position.y;
				}
			}
			position = arrowText.transform.localPosition;
			Vector3 shipPosition = ship.transform.position;
			Vector3 camPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);
			Vector3 temp = camPosition;
			temp.z = 0;
			this.transform.position = temp;
			distance = (int)Vector2.Distance(shipPosition,tempVector);
			shipPosition.x = shipPosition.x - camPosition.x;
			shipPosition.y = shipPosition.y - camPosition.y;
			
			float angle = Mathf.Atan2 (shipPosition.y, shipPosition.x) * Mathf.Rad2Deg;
			this.transform.rotation = Quaternion.Euler (new Vector3(0, 0, angle-90));


			text.text = ((int)(distance/10)).ToString();
			shipIco.transform.eulerAngles = ship.transform.eulerAngles;
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
