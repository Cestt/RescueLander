using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {

	private string Items;
	private Touch_Manager touch;
	public GameObject Test;
	void Awake() {
		touch = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();
		if(Application.loadedLevelName == "Menu")
		if (Advertisement.isSupported) {
			Debug.Log("Ads supported");
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("37545",false);
		} else {
			//Test.GetComponent<tk2dTextMesh>().text = "Ads not supported";
		}
	}
	void Update(){
		if(Advertisement.isInitialized){
			Debug.Log("initialized");
		}else{
			Debug.Log("Not initialized");
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
	public void Launch(string Item,string Type){
		if(Advertisement.isInitialized){
			Items = Item;
			ShowOptions options = new ShowOptions();
			options.resultCallback = HandleResult;
			options.pause = touch.Pause(null,true);
			if(Type == "Rewarded"){
				if(Advertisement.isReady("rewardedVideoZone")) {
					Advertisement.Show("rewardedVideoZone",options);
				}
			}else{

					Advertisement.Show(null,options);

			}



		}else{
			Debug.Log("Ad not ready");
		}

	}

}