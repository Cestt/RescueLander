using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zoom : MonoBehaviour {
	//Camera component that has the Zoom
	public tk2dCamera cam;
	//Zoom speed
	public float zoomProgression;
	//Zooming in or out?
	public string zoom;

	public bool enabled = true;

	void Awake(){
		cam = Camera.main.GetComponent<tk2dCamera>();
	}

	/// <summary>
	/// Check collisions for zoom
	/// </summary>
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(enabled){
				zoom = "in";
				CheckInvoke();
			}


			

		}
	}
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(enabled){
				if (cam.ZoomFactor < 2){
					zoom = "in";
					CheckInvoke();
				}
			}


			
			
			
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){
			if(enabled){
				if(cam.ZoomFactor > 1){
					
					
					zoom = "out";
					CheckInvoke();
					
				}
			}
			
		}
	}

	public void CheckInvoke(){
		CancelInvoke("Zooming");
		if(IsInvoking("Zooming")){
			CheckInvoke();
		}else{
			InvokeRepeating("Zooming",0,Time.fixedDeltaTime);
		}
	}


	/// <summary>
	/// Zoom in/out depending on zoom variable.
	/// </summary>
	 bool Zooming(){

		if (zoom == "in") {
			///cam.Zoomfactor is actually the amount of zoom.
			if(cam.ZoomFactor < 2){
					
				cam.ZoomFactor += zoomProgression;
				return false;
									
			}
			if(cam.ZoomFactor >= 2){
				cam.ZoomFactor = 2;
				CancelInvoke("Zooming");
				return true;
			}
				
			return false;			
		}


		if(zoom == "out"){


			if(cam.ZoomFactor > 1){

				cam.ZoomFactor -= zoomProgression;
				return false;

			}
			if(cam.ZoomFactor <= 1){

				cam.ZoomFactor = 1;
				CancelInvoke("Zooming");
				return true;

			}
			return false;
		}
		return false;
	}
	 
}
