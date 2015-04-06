using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zoom : MonoBehaviour {

	public tk2dCamera cam;
	public float zoomProgression;
	public bool Finish;

	void Awake(){


	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){

				StopCoroutine("Zooming");
				StartCoroutine(Zooming("in"));

			

		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){
			
			if(cam.ZoomFactor > 1){
				
				StopCoroutine("Zooming");
				StartCoroutine(Zooming("out"));

			}
			
			
		}
	}



	IEnumerator Zooming(string zoom){

		if (zoom == "in") {
				
				while(cam.ZoomFactor < 2){
					
					cam.ZoomFactor += zoomProgression;
					yield return new WaitForSeconds(Time.fixedDeltaTime);
					
				}
				cam.ZoomFactor = 2;
						
				}


		if(zoom == "out"){


			while(cam.ZoomFactor > 1){

				cam.ZoomFactor -= zoomProgression;
				yield return new WaitForSeconds(Time.fixedDeltaTime);

			}
			cam.ZoomFactor = 1;
			
		}

	}
}
