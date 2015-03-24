using UnityEngine;
using System.Collections;

public class Cameraposition : MonoBehaviour {

	public GameObject ship;
	public int maxScrollX;
	private Camera cam;
	private Transform shipPos;
	private Vector3 tempPos;
	private tk2dCamera camera;
	 
	public bool BoundedScroll = true;
	// Use this for initialization
	void Awake () {

		camera = this.GetComponent<tk2dCamera>();
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
				

					tempPos.x = shipPos.position.x;
				 

					tempPos.y = shipPos.position.y;

				cam.transform.position = tempPos;
				cam.transform.position = 
					new Vector3(Mathf.Clamp(cam.transform.position.x,cam.pixelWidth/2+100,
					                        maxScrollX - cam.pixelWidth/2-100),Mathf.Clamp(cam.transform.position.y,0 + cam.pixelHeight/2,1080 - cam.pixelHeight/2),
					            cam.transform.position.z);
			}else{
				
				tempPos = cam.transform.position;
				tempPos.x = shipPos.position.x - cam.pixelWidth;
				tempPos.y = shipPos.position.y - cam.pixelHeight;
				
				cam.transform.position = tempPos;
			}

		}


	}


}
