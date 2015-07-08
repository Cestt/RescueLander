using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	private float timeSeconds = 0;
	private float timeMinutes = 0;

	void Start () {
		InvokeRepeating("Show",0,1);
	}
	
	// Update is called once per frame
	void Show () {
		timeSeconds++;
		if(timeSeconds == 60){
			timeMinutes++;
			timeSeconds = 0;
		}
		if(timeMinutes < 10){
			if(timeSeconds < 10){
				//text = "0"+timeMinutes+":"+"0"+timeSeconds;
			}else;
				//text = "0"+timeMinutes+":"+timeSeconds;
		}else{
			if(timeSeconds < 10){
				//text = timeMinutes+":"+"0"+timeSeconds;
			}else;
			//text = timeMinutes+":"+timeSeconds;
		}
	}
}
