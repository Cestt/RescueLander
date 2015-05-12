using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {
	void Awake() {
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("37545",true);
		} else {
			Debug.Log("Platform not supported");
		}
	}
	
	void OnGUI() {
		if(GUI.Button(new Rect(10, 10, 150, 50), Advertisement.isReady() ? "Show Ad" : "Waiting...")) {
			// Show with default zone, pause engine and print result to debug log

			ShowOptions options = new ShowOptions();
			options.resultCallback = HandleResult;
			options.pause = true;
			Advertisement.Show("pictureZone", options);
		}
	}
	void HandleResult (ShowResult result){

		switch (result) {
		
		case ShowResult.Failed :
			Debug.Log("Ad failed");
			break;
		case ShowResult.Finished:
			Debug.Log("Ad finished");
			break;
		case ShowResult.Skipped :
			Debug.Log("Ad Skipped");
			break;
		}
	}
}