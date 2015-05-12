using UnityEngine;
using System.Collections;

public class Cameraposition : MonoBehaviour {

	private GameObject ship;
	public int maxScrollX;
	private Camera cam;
	private Transform shipPos;
	private Vector3 tempPos;
	private tk2dCamera camera;
	 
	public bool BoundedScroll = true;
	// Use this for initialization
	void Awake () {
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
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
					cam.transform.position = Vector3.MoveTowards(transform.position,tempPos,500*Time.deltaTime);


				cam.transform.position = 
					new Vector3(Mathf.Clamp(cam.transform.position.x,camera.nativeResolutionWidth/2/camera.ZoomFactor,
					                        maxScrollX - camera.nativeResolutionWidth/2/camera.ZoomFactor),
					            Mathf.Clamp(cam.transform.position.y,0 + camera.nativeResolutionHeight/2/camera.ZoomFactor,1080 - camera.nativeResolutionHeight/2/camera.ZoomFactor),
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
