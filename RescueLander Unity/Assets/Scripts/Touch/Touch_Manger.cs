using UnityEngine;
using System.Collections;

public class Touch_Manger : MonoBehaviour {
	RuntimePlatform platform = Application.platform;
	public Camera uicamera;



	void Awake(){

	}



	void Update () {
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					
					RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
					
					if(_hit.collider!= null){
						Debug.Log("Hit");
						switch(_hit.collider.name){
							
						case "Pause_Button" :
							Pause();
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
						Pause();
						break;
						
						
					default :
						
						break;
						
					}
					
				}
				
			}
		}

	}
	void checkTouch(Vector3 pos){
		 Ray ray = uicamera.ScreenPointToRay(pos);
		 RaycastHit hit;
		if (Physics.Raycast(ray, out hit,Mathf.Infinity)){
			Debug.Log("Hit");

			switch(hit.transform.name){

			case "Play":
				Application.LoadLevel("test_demo");
				Debug.Log("Loading");
				break;
			

			case "Pause_Button":
				Pause();
				break;
			}
		}
	}

	void Pause(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
			Debug.Log("Pause");
		}else{
			Time.timeScale = 1;
			Debug.Log("UnPause");
		}
	}
}
