using UnityEngine;
using System.Collections;

public class Cameraposition : MonoBehaviour {

	public GameObject ship;
	public int maxScrollX;
	private Transform shipPos;
	private Vector3 tempPos;
	public bool BoundedScroll = true;
	private Camera cam;
	private	float height;
	private	float width;
	// Use this for initialization
	void Awake () {
		tempPos = new Vector3 (0, 0, 0);
		cam = Camera.main;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Follow();

	
	}

	void Follow(){


		if(ship != null){

			if(BoundedScroll){

				shipPos = ship.transform;
				height = 2f * cam.orthographicSize;
				width = height * cam.aspect;

				
				if(ship.transform.position.x > width / 2 & ship.transform.position.x < maxScrollX - width/2){
					tempPos.x = shipPos.position.x - width/2;
				} 
				if(ship.transform.position.y > height / 2 & ship.transform.position.y < 1080 - height/2){
					tempPos.y = shipPos.position.y - height/2;
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
