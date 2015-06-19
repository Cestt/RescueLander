using System;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {

	private string Items;
	private Touch_Manager touch;
	public GameObject Test;
	InterstitialAd interstitial;
	void Awake() {
		interstitial = new InterstitialAd("ca-app-pub-6225305526112070/2285766744");
		touch = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();
		if(Application.loadedLevelName == "Menu")
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("37545",false);
		} else {
			//Test.GetComponent<tk2dTextMesh>().text = "Ads not supported";
		}

	}
	void OnLevelWasLoaded(int level) {
		if(Application.loadedLevelName != "Menu"){

			AdRequest request = new AdRequest.Builder().Build();
			interstitial.LoadAd(request);
		}

		
	}
	void Update(){
	
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
	public void LaunchInterstital(){

	if(PlayerPrefs.GetInt("Ads") != null)
		if(PlayerPrefs.GetInt("Ads") == 0){
			if (interstitial.IsLoaded()) {
				interstitial.Show();
			}
		}

	}

}