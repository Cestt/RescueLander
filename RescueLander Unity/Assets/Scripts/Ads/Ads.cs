using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {

	private string Items;
	private Touch_Manager touch;
	void Awake() {
		touch = GetComponent<Touch_Manager>();
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("37545",true);
		} else {
			Debug.Log("Platform not supported");
		}
	}

	void HandleResult (ShowResult result){

		switch (result) {
		
		case ShowResult.Failed :
			Debug.Log("Ad failed");

			break;
		case ShowResult.Finished:
			Debug.Log("Ad finished");
			switch(Items){
			case "Fuel":
				dataManger.manager.fuelPowerUps++;
				break;
			case "Shield":
				dataManger.manager.shieldPowerUps++;
				break;
			case "Magnet":
				dataManger.manager.magnetPowerUps++;
				break;
			}

			break;
		case ShowResult.Skipped :
			Debug.Log("Ad Skipped");

			break;
		}
	}
	public void Launch(string Item){
		Items = Item;
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleResult;
		options.pause = touch.Pause(null);
		Advertisement.Show("pictureZone", options);
	}
}