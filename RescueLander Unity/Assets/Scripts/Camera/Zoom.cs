using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	public tk2dCamera cam;
	public float zoomProgression;


	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){

			if(cam.ZoomFactor < 2){

				StartCoroutine(Zooming("in"));


			}
			

		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Floor"){
			
			if(cam.ZoomFactor > 1){
				
				StartCoroutine(Zooming("out"));
				
				
			}
			
			
		}
	}



	IEnumerator Zooming(string zoom){

		if(zoom == "in"){
			cam.ZoomFactor += zoomProgression;
			Debug.Log("Zooming in");

		}
		if(zoom == "out"){

			while(cam.ZoomFactor > 1){

				cam.ZoomFactor -= zoomProgression;
				yield return new WaitForSeconds(Time.fixedDeltaTime);

			}
			
		}

	}
}
