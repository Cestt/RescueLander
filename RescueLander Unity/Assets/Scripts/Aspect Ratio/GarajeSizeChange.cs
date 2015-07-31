using UnityEngine;
using System.Collections;

public class GarajeSizeChange : MonoBehaviour {

	tk2dCamera cam;
	Vector2 Aspect;
	float AspectCalc;
	void Awake () {
		cam = Camera.main.GetComponent<tk2dCamera>();
		Aspect =  AspectRatio.GetAspectRatio(Screen.width,Screen.height);
		AspectCalc = Aspect.x/Aspect.y;
		Resize();
	}
	
	// Update is called once per frame
	void Resize () {
		if(AspectCalc < 1.6 ){
			if(!Application.loadedLevelName.Contains("Tuto"))
			GameObject.Find("UI_Camera").transform.FindChild("Garage_Menu").localScale = new Vector3(0.83f,0.83f,1f);
			if(Application.loadedLevelName.Contains("Tuto")){
				GameObject.Find("UI_Camera").transform.FindChild("Tutorial/Step1/Chat_Astronaut").localScale = new Vector3(0.65f,0.65f,1f);
				GameObject.Find("UI_Camera").transform.FindChild("Tutorial/Step1/Chat_Astronaut").localPosition = new Vector3(157f,-16f,0f);
			}

		}
	}
}
