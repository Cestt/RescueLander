using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zoom : MonoBehaviour {

	public tk2dCamera cam;
	public float zoomProgression;
	public float scale;
	public List<GameObject> UI = new List<GameObject>();
	private List<Vector3> originalScales = new List<Vector3>();

	void Awake(){

		foreach (GameObject temp in UI) {
			originalScales.Add(temp.transform.localScale); 
		}
	
	}

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

		if (zoom == "in") {
						
						cam.ZoomFactor += zoomProgression;
						Debug.Log ("Zooming in");

						foreach (GameObject temp in UI) {

							temp.transform.localScale = new Vector3(scale,scale,temp.transform.localScale.z);

						}
				}


		if(zoom == "out"){


			int index = 0;
			foreach(GameObject temp in UI){
				
				temp.transform.localScale = originalScales[index];
				index++;
				
			}

			while(cam.ZoomFactor > 1){

				cam.ZoomFactor -= zoomProgression;
				yield return new WaitForSeconds(Time.fixedDeltaTime);

			}
			
		}

	}
}
