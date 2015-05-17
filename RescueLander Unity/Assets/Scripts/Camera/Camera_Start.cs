using UnityEngine;
using System.Collections;

public class Camera_Start : MonoBehaviour {

	private tk2dCamera camtk2d;
	private GameObject ship;

	void Start () {
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		camtk2d = GetComponent<tk2dCamera>();
		if(Application.loadedLevelName != "Menu"){
			camtk2d.ZoomFactor = 2;
			Camera.main.transform.position = new Vector3 (ship.transform.position.x,ship.transform.position.y,
			                                              Camera.main.transform.position.z);;
		}else{
			if(dataManger.manager.Camposition == "Forward"){
				Camera.main.transform.position = new Vector3 (3840 - camtk2d.nativeResolutionWidth/2,Camera.main.transform.position.y,
				                                              Camera.main.transform.position.z);
			}
		}

	}
	

}
