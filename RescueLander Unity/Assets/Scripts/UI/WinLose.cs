using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinLose : MonoBehaviour {

	private GameObject WinSprite;
	private GameObject LoseSprite;
	private GameObject winText;
	private GameObject UI1;
	private GameObject UI2;
	private GameObject UI3;

	private ScreenShoter screenshoter;
	private bool first;
	private int totalScore;
	WinHalo_Anim haloanim;
	tk2dTextMesh text;
	ScoreManager scoreManager;
	Coin_Manager coin_manager;
	public GameObject[] stars;
	private float actualTime;
	public float WinTimer;
	private GameObject MisionAcomplished;

	void Awake () {
	
//		haloanim = WinSprite.GetComponentInChildren<WinHalo_Anim>();
		if (Application.loadedLevelName != "Menu") {
			GameObject uicamera = GameObject.Find("UI_Camera");
			WinSprite = uicamera.transform.FindChild("WinLayout").gameObject;
			winText = WinSprite.transform.FindChild("Resume/Pic_Frame/WinScore_Txt").gameObject;
			MisionAcomplished = WinSprite.transform.FindChild("Win_Text").gameObject;
			for(int j = 1; j <= 3; j++){
				stars[j-1] = WinSprite.transform.FindChild("Win_Text/Win_Star"+j+"_On").gameObject;
			}
			LoseSprite = uicamera.transform.FindChild("LoseLayout").gameObject;
			UI1 =  uicamera.transform.FindChild("Anchor (UpperLeft)").gameObject;
			UI2 =  uicamera.transform.FindChild("Anchor (UpperRight)").gameObject;
			UI3 =  uicamera.transform.FindChild("Anchor (LowerCenter)").gameObject;
			coin_manager = GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();
			text = winText.GetComponent<tk2dTextMesh> ();
			scoreManager =  GameObject.Find("ScoreCoin_Manager").GetComponent<ScoreManager> ();
			first = true;	
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != "Menu") {
			if(Time.time > actualTime + WinTimer ){
				WinSprite.transform.FindChild("Resume").gameObject.SetActive (true);
				MisionAcomplished.SetActive (false);
			}
			if(MisionAcomplished.activeInHierarchy & Input.touchCount > 0){
				WinSprite.transform.FindChild("Resume").gameObject.SetActive (true);
				MisionAcomplished.SetActive (false);
			}	
		}

	}

	public void End(string result){

		if(result == "Win"){
			if(first){
				if(dataManger.manager.actualLevel == dataManger.manager.unlocks){
					dataManger.manager.unlocks++;
				}

				totalScore = (int)scoreManager.scoreCalc ();
				if(totalScore > 0 & totalScore <= 500){
					dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 1;
					dataManger.manager.coins += coin_manager.OneStarCoin;
					WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text 
						= coin_manager.OneStarCoin.ToString();
					WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins").GetComponent<tk2dTextMesh>().text 
						= "Finished with 1 Star:";
				}
				if(totalScore > 500 & totalScore <= 1500){
					dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 2;
					dataManger.manager.coins += coin_manager.TwoStarCoin;
					stars[1].SetActive(true);
					WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text 
						= coin_manager.TwoStarCoin.ToString();
					WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins").GetComponent<tk2dTextMesh>().text 
						= "Finished with 2 Stars:";
				}
				if(totalScore > 1500){
					dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 3;
					dataManger.manager.coins += coin_manager.ThreeStarCoin;
					stars[1].SetActive(true);
					stars[2].SetActive(true);
					WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text 
						= coin_manager.ThreeStarCoin.ToString();
					WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins").GetComponent<tk2dTextMesh>().text 
						= "Finished with 3 Stars:";
				}
				dataManger.manager.scores["Level_"+dataManger.manager.actualLevel] = totalScore;
				WinSprite.transform.FindChild("Resume/CoinCount/Level Title").GetComponent<tk2dTextMesh>().text = "Level "+dataManger.manager.actualLevel.ToString();
				WinSprite.transform.FindChild("Resume/CoinCount/Collected Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = coin_manager.levelCoins.ToString();
				WinSprite.transform.FindChild("Resume/CoinCount/Total Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = dataManger.manager.coins.ToString();
				dataManger.manager.Save(true);
				first = false;
				Win();
			}

		}

		if(result == "Lose"){
			LoseSprite.transform.FindChild("CoinCount/Collected Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = coin_manager.levelCoins.ToString();
			LoseSprite.transform.FindChild("CoinCount/Total Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = dataManger.manager.coins.ToString();
			dataManger.manager.Save(true);
			Lose();
		}



	}


	void Win(){

		//haloanim.Win =true;
		WinSprite.SetActive(true);
		WinSprite.transform.FindChild("Resume").gameObject.SetActive (false);
		MisionAcomplished.SetActive (true);
		actualTime = Time.time;
//		text.text = Localization_Bridge.loc.Score +": "+ totalScore.ToString();
		UI1.SetActive (false);
		UI2.SetActive (false);
		UI3.SetActive (false);



	}
	void Lose(){

		LoseSprite.SetActive (true);
		UI1.SetActive (false);
		UI2.SetActive (false);
		UI3.SetActive (false);

	}
}
