using UnityEngine;
using System.Collections;

public class WinShoot : MonoBehaviour {

	public GameObject ship;
	private bool first = true;
	public ScreenShoter screenshooter;


	void Update () {
	
		if(first & Vector2.Distance(transform.position,ship.transform.position) < 10000 & Time.time > 2f){
			Debug.Log("Shoot");
			screenshooter.LaunchScreenshot(125,125,true);
			first = false;
		}

	}
}
