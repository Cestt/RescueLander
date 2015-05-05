using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zoom : MonoBehaviour {
	//Camera component that has the Zoom
	public tk2dCamera cam;
	//Zoom speed
	public float zoomProgression;
	//Zooming in or out?
	private string zoom;


	/// <summary>
	/// Check collisions for zoom
	/// </summary>
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){

			CancelInvoke("Zooming");
			zoom = "in";
			InvokeRepeating("Zooming",0,Time.fixedDeltaTime);

			

		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){
			
			if(cam.ZoomFactor > 1){
				
				CancelInvoke("Zooming");
				zoom = "out";
				InvokeRepeating("Zooming",0,Time.fixedDeltaTime);

			}
			
			
		}
	}


	/// <summary>
	/// Zoom in/out depending on zoom variable.
	/// </summary>
	void Zooming(){

		if (zoom == "in") {
			///cam.Zoomfactor is actually the amount of zoom.
			if(cam.ZoomFactor < 2){
					
				cam.ZoomFactor += zoomProgression;
									
			}
			if(cam.ZoomFactor >= 2){
				cam.ZoomFactor = 2;
				CancelInvoke("Zooming");
			}
				
						
		}


		if(zoom == "out"){


			if(cam.ZoomFactor > 1){

				cam.ZoomFactor -= zoomProgression;

			}
			if(cam.ZoomFactor <= 1){

				cam.ZoomFactor = 1;
				CancelInvoke("Zooming");
			
			}

		}

	}
}
