using UnityEngine;
using System.Collections;

public class ScoreTextTime : MonoBehaviour {

	tk2dTextMesh textMesh;
	Touch_Manager touchmanager;
	float time = 0;
	int sec = 0;
	int min = 0;
	ResizeText resize;
	void Awake(){
		textMesh = GetComponent<tk2dTextMesh>();
		touchmanager = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();
		textMesh.text = "Time  00:00";
		resize = GetComponent<ResizeText>();
	}

	void Update(){
		if (!touchmanager.paused)
			time += Time.deltaTime;
		//Si pasa un segundo se cambia el texto
		if (time >= 1) {
			time -= 1;
			sec++;
			if (sec == 60){
				sec = 0;
				min++;
			}
			string secText;
			string minText;
			if (sec < 10)
				secText = "0" + sec;
			else
				secText = "" + sec;
			if (min < 10)
				minText = "0" + min;
			else
				minText = "" + min;

			resize.ChangeText  
				(Localization_Bridge.manager.GetTextValue("RescueLander.IngameTime")+ minText + ":" + secText); 
		}
	}
}
