using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Touch_Manger : MonoBehaviour {
	RuntimePlatform platform = Application.platform;
	public Camera uicamera;
	//[HideInInspector]
	public bool paused;
	private float originalY;
	public GameObject ship;
	private Rigidbody2D rigid;
	public GameObject uiColumnExtended;
	public GameObject positioner;


	void Awake(){
		originalY = uiColumnExtended.transform.position.y;
		rigid = ship.GetComponent<Rigidbody2D>();
	}



	void Update () {
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					
					Ray ray;
					RaycastHit hit;
					
					ray = uicamera.ScreenPointToRay(Input.GetTouch(0).position);
					
					if (Physics.Raycast(ray.origin,ray.direction * 100, out hit)){
						Debug.Log("Hit");
						switch(hit.collider.name){
							
						case "Pause_Button" :
							Pause(hit.transform.gameObject);
							break;
						
							
						default :

							break;
							
						}
						
					}
					
				}
			}
		}else if(platform == RuntimePlatform.WindowsEditor){
			
			if(Input.GetMouseButtonUp(0)){
				
				Ray ray;
				RaycastHit hit;

				ray = uicamera.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray.origin,ray.direction * 100, out hit)) {

					Debug.Log("Hit");
					switch(hit.collider.name){
						
					case "Pause_Button" :
						Pause(hit.transform.gameObject);
						break;
						
						
					default :
						
						break;
						
					}
					
				}
				
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)){

			if(Application.loadedLevelName == "Menu"){
				Application.Quit();
			}else{
				if(paused){
					Pause(null);
				}else{
					Application.LoadLevel("Menu");
				}
			}

		}

	}


	void Pause(GameObject temp){

		if(!paused){
			paused = true;
			rigid.isKinematic = true;
 			Debug.Log("Pause");
		}else{

			paused = false;
			rigid.isKinematic = false;
			Debug.Log("UnPause");
		}
	}


}
