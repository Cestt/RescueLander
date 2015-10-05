using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {

	private string Items;
	private Touch_Manager touch;
	public GameObject Test;
	InterstitialAd interstitial;
	private GameObject fuelBar;
	private float fuelBarOriginalSize;
	public int Fuel_Recover = 20;
	int timesRepeat = 0;
	AsyncOperation async;

	void Awake() {
		interstitial = new InterstitialAd("ca-app-pub-6225305526112070/2285766744");
		touch = GameObject.Find("Game Manager").GetComponent<Touch_Manager>();

			
				Advertisement.allowPrecache = true;
				Advertisement.Initialize ("37545",false);
			


		//ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
	}
	void OnLevelWasLoaded(int level) {

		if (Application.loadedLevelName != "Menu") {

			AdRequest request = new AdRequest.Builder ().Build ();
			interstitial = new InterstitialAd("ca-app-pub-6225305526112070/2285766744");
			interstitial.LoadAd (request);

		} else {
			if (PlayerPrefs.GetInt ("Ads") == 1) {
				GameObject.Find ("MoreCoins_Button").SetActive (false);
			}
		}
		if (PlayerPrefs.GetInt ("Ads") == 1) {
			GameObject.Find ("UI_Camera").transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Coins_Menu/NoAds_Panel").gameObject.SetActive(false);
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
				GameObject temp = GameObject.Find("UI_Camera");
				fuelBar = temp.transform.FindChild("Anchor (UpperLeft)/UIBase_Left/BarraFondo_Fuel/BarraFuel").gameObject;
				fuelBarOriginalSize = fuelBar.GetComponent<tk2dSlicedSprite>().dimensions.x;
				Debug.Log ("Ship : " + GameObject.Find(dataManger.manager.actualShip + "(Clone)").name);
				Movement movement = GameObject.Find(dataManger.manager.actualShip + "(Clone)").GetComponent<Movement>();
				movement.fuel += (movement.originalFuel  * Fuel_Recover)/100;
				tk2dSlicedSprite sliced = fuelBar.GetComponent<tk2dSlicedSprite>();
				sliced.dimensions = new Vector2( 
				                                sliced.dimensions.x  + ((fuelBarOriginalSize * Fuel_Recover)/100),sliced.dimensions.y);
				break;
			case "Shield":
				dataManger.manager.nextPowerUp = true;
				//PlayerPrefs.SetInt("TimeShield", int.Parse(System.DateTime.UtcNow.ToString("MMddHHmm")));
				dataManger.manager.Save(true);
				Application.LoadLevel(Application.loadedLevel);
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
	public void loadlevel(int Level, string World){
		StartCoroutine(LoadLevelAsync(Level,World));
	}
	
	IEnumerator LoadLevelAsync(int Level,string World){
		
		async = Application.LoadLevelAsync("Level_" + Level + "_" + World);
		async.allowSceneActivation = false;
		yield return null;
		StartCoroutine("WaitToAdvert");
		Debug.Log("Loading complete");
	}
	
	IEnumerator WaitToAdvert(){
		if(Advertisement.isInitialized){
			async.allowSceneActivation = true;
			Debug.Log("Advertiment ready");
		}else{
			yield return new WaitForSeconds(0.2f);
			Debug.Log("Waiting to Advertiment");
			StartCoroutine("WaitToAdvert");
			timesRepeat++;
			if(timesRepeat == 15){
				Advertisement.allowPrecache = true;
				Advertisement.Initialize ("37545",false);

			}
			if(timesRepeat == 60){
				async.allowSceneActivation = true;
			}
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