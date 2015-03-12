using UnityEngine;
using System.Collections;

public class Cameraposition : MonoBehaviour {

	public GameObject ship;
	public int maxScrollX;
	private Camera cam;
	private Transform shipPos;
	private Vector3 tempPos;
	public bool BoundedScroll = true;
	// Use this for initialization
	void Awake () {
	
		cam = Camera.main;
		shipPos = ship.transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Follow();

	
	}

	void Follow(){


		if(ship != null){

			if(BoundedScroll){
				tempPos = cam.transform.position;
				
				if(ship.transform.position.x > cam.pixelWidth / 2 & ship.transform.position.x < maxScrollX - cam.pixelWidth/2){
					tempPos.x = shipPos.position.x - cam.pixelWidth/2;
				} 
				if(ship.transform.position.y > cam.pixelHeight / 2 & ship.transform.position.y < 1080 - cam.pixelHeight/2){
					tempPos.y = shipPos.position.y - cam.pixelHeight/2;
				}
				cam.transform.position = tempPos;
			}else{
				
				tempPos = cam.transform.position;
				tempPos.x = shipPos.position.x - cam.pixelWidth;
				tempPos.y = shipPos.position.y - cam.pixelHeight;
				
				cam.transform.position = tempPos;
			}

		}


	}


}
