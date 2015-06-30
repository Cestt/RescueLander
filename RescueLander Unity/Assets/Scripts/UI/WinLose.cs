using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class WinLose : MonoBehaviour {

	private GameObject WinSprite;
	private GameObject LoseSprite;
	private GameObject winText;
	private GameObject UI1;
	private GameObject UI2;
	private GameObject UI3;
	private GameObject UI4;
	private GameObject UI5;

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
	private Damage damage;
	private Social_Manager socialManager;
	private tk2dTextMesh scoreText;
	private tk2dTextMesh scoreTotalText;
	private int scoreNumber;
	private int scoreAct;
	private GameObject scoreObject;
	private int scoreChangeText;
	private string scoreTextRemaining;
	private int scoreTotal;
	private GameObject ship;
	public int lifeThrdStarPercent = 90;
	Ads ads;
	private Sound_Manager soundManager;
	Touch_Manager touch;
	private bool adsInters = false;
	private GameObject new_record;
	private GameObject new_color;
	private GameObject promptWorldComplete;
	private bool worldComplete = false;
	private int starsNewColor;

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
			if (Application.loadedLevelName.Contains ("Tuto")){
				ship = GameObject.Find ("101(Clone)");
			}else{
				new_record = MisionAcomplished.transform.FindChild("Score_Resumen/New Record").gameObject;
				new_color = WinSprite.transform.FindChild("Resume/NewColor_Unlocked").gameObject;
				ship = GameObject.Find (dataManger.manager.actualShip+"(Clone)");
				UI3 =  uicamera.transform.FindChild("Anchor (LowerCenter)").gameObject;
				UI4 =  uicamera.transform.FindChild("Anchor (LowerLeft)").gameObject;
				UI5 =  uicamera.transform.FindChild("Anchor (LowerRight)").gameObject;
				coin_manager = GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();
				scoreManager =  GameObject.Find("ScoreCoin_Manager").GetComponent<ScoreManager> ();
				scoreTotalText = MisionAcomplished.transform.FindChild("Score_Resumen/Score_Total").gameObject.GetComponent<tk2dTextMesh> ();
				if (dataManger.manager.actualLevel <= 21)
					promptWorldComplete = GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_MarsFinished").gameObject;
				//Se obtiene el numero de estrellas necesario para obtener el siguiente color

				GameObject colorbuttons = uicamera.transform.FindChild("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/Color_Buttons").gameObject;
				starsNewColor = 10000;
				foreach (Transform child in colorbuttons.transform) {
					int starAct = child.gameObject.GetComponent<Color_Enabled>().StarsRequired;
					if (dataManger.manager.totalStars < starAct && starAct < starsNewColor)
						starsNewColor = starAct;
				}
				//starsNewColor += dataManger.manager.stars["Level_"+dataManger.manager.actualLevel];
			}
			touch = GetComponent<Touch_Manager>();
			damage = ship.GetComponent<Damage>();
			LoseSprite = uicamera.transform.FindChild("LoseLayout").gameObject;
			UI1 =  uicamera.transform.FindChild("Anchor (UpperLeft)").gameObject;
			UI2 =  uicamera.transform.FindChild("Anchor (UpperRight)").gameObject;
			text = winText.GetComponent<tk2dTextMesh> ();
			ads = GameObject.Find("Data Manager"). GetComponent<Ads>();

			socialManager = GameObject.Find ("Game Manager").GetComponent<Social_Manager>();
			soundManager = GameObject.Find ("Game Manager").GetComponent<Sound_Manager>();
			first = true;	
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != "Menu" & !Application.loadedLevelName.Contains("Tuto") & !adsInters) {
			if(Time.time > actualTime + WinTimer & WinSprite.activeInHierarchy & dataManger.manager.actualLevel < 21
			   ){
				adsInters = true;
				WinSprite.transform.FindChild("Resume").gameObject.SetActive (true);
				MisionAcomplished.SetActive (false);
				dataManger.manager.partidas++;
				if(dataManger.manager.partidas >= 3){
					dataManger.manager.partidas = 0;
					ads.LaunchInterstital();
				}
			}
			if((MisionAcomplished.activeInHierarchy & Input.touchCount > 0 
			    && dataManger.manager.actualLevel < 21) || (dataManger.manager.actualLevel == 21 && worldComplete && !promptWorldComplete.activeInHierarchy
			    )){
				adsInters = true;
				dataManger.manager.partidas++;
				if(dataManger.manager.partidas >= 3){
					dataManger.manager.partidas = 0;
					ads.LaunchInterstital();
				}
				WinSprite.transform.FindChild("Resume").gameObject.SetActive (true);
				MisionAcomplished.SetActive (false);

			}
			/*Debug.Log(dataManger.manager.actualLevel );
			if (dataManger.manager.actualLevel == 21)
			Debug.Log ("WIN: "+ WinSprite.activeInHierarchy +", MISION: "+ MisionAcomplished.activeInHierarchy + 
			           "TIME: " + Time.time + "ACTUAL: " + actualTime + "Timer: "+WinTimer);*/
			if (MisionAcomplished.activeInHierarchy & (Input.touchCount > 0 || Time.time > actualTime + WinTimer) && 
			    dataManger.manager.actualLevel == 21 && !worldComplete){
				GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
				touch.actualPrompt = promptWorldComplete;
				touch.actualPrompt.SetActive(true);
				MisionAcomplished.SetActive (false);
				worldComplete = true;
			}
		}

	}

	public void End(string result){
		touch.adLimit = 0;
		if(first){

			if(result == "Win"){
				if(!LoseSprite.activeInHierarchy){
					Movement mov = ship.GetComponent<Movement>();
					float fuel = mov.GetComponent<Movement>().fuel;
					float maxFuel = mov.GetComponent<Movement>().originalFuel;

					
					if(dataManger.manager.actualLevel == dataManger.manager.unlocks & !Application.loadedLevelName.Contains("Tuto")){
						dataManger.manager.unlocks++;
					}
					if(!Application.loadedLevelName.Contains("Tuto")){
						coin_manager.LevelCoin(coin_manager.levelCoins);
						scoreManager.stopScore();
						totalScore = (int)scoreManager.timeScore;

						dataManger.manager.totalStars -= dataManger.manager.stars["Level_"+dataManger.manager.actualLevel];
						if(damage.life >= (((float)damage.maxLife*lifeThrdStarPercent)/100f)){
							dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 3;
							dataManger.manager.coins += coin_manager.ThreeStarCoin;
							stars[1].SetActive(true);
							stars[2].SetActive(true);
							WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text 
								= coin_manager.ThreeStarCoin.ToString();
							WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins").GetComponent<tk2dTextMesh>().text 
								= "3 Stars:";
							//ACHIEVEMENT
							Social.ReportProgress("CgkIuv-YgIkeEAIQCQ", 100.0f, (bool success) => {
								socialManager.Check("Achievement","CgkIuv-YgIkeEAIQCQ",success);
							});
						}
						else if(damage.life >= (((float)damage.maxLife*50)/100f)){
							dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 2;
							dataManger.manager.coins += coin_manager.TwoStarCoin;
							stars[1].SetActive(true);
							WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text 
								= coin_manager.TwoStarCoin.ToString();
							WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins").GetComponent<tk2dTextMesh>().text 
								= "2 Stars:";
						}
						else {
							dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 1;
							dataManger.manager.coins += coin_manager.OneStarCoin;
							WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text 
								= coin_manager.OneStarCoin.ToString();
							WinSprite.transform.FindChild("Resume/CoinCount/LevelFinished Coins").GetComponent<tk2dTextMesh>().text 
								= "1 Star:";
						}
						dataManger.manager.totalStars += dataManger.manager.stars["Level_"+dataManger.manager.actualLevel];
						if ((int)scoreManager.scoreCalc() > dataManger.manager.scores["Level_"+dataManger.manager.actualLevel])
							new_record.SetActive(true);

						if (starsNewColor <= dataManger.manager.totalStars ){
							new_color.SetActive(true);
							Debug.Log ("NUEVO COLOR!!!");
						}
						dataManger.manager.scores["Level_"+dataManger.manager.actualLevel] = (int)scoreManager.scoreCalc();
						WinSprite.transform.FindChild("Resume/CoinCount/Level Title").GetComponent<tk2dTextMesh>().text = "Level "+dataManger.manager.actualLevel.ToString();
						WinSprite.transform.FindChild("Resume/CoinCount/Collected Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = coin_manager.levelCoins.ToString();
						WinSprite.transform.FindChild("Resume/CoinCount/Total Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = dataManger.manager.coins.ToString();
						
						first = false;
						Win();
					}else{
						first = false;
						dataManger.manager.tutorial++;
						stars[1].SetActive(true);
						stars[2].SetActive(true);
						WinSprite.SetActive(true);
						LoseSprite.SetActive(false);
						WinSprite.transform.FindChild("Resume").gameObject.SetActive (false);
						MisionAcomplished.SetActive (true);
					}
					
					dataManger.manager.Save(true);
				}
			

			}

			if(result == "Lose"){


				dataManger.manager.Save(true);
				Lose();
			}


		}
	}


	void Win(){

		//haloanim.Win =true;
		soundManager.PlaySound("Win");
		WinSprite.SetActive(true);
		LoseSprite.SetActive(false);
		WinSprite.transform.FindChild("Resume").gameObject.SetActive (false);
		MisionAcomplished.SetActive (true);
		actualTime = Time.time;
//		text.text = Localization_Bridge.loc.Score +": "+ totalScore.ToString();
		UI1.SetActive (false);
		UI2.SetActive (false);
		UI3.SetActive (false);
		UI4.SetActive(true);
		UI5.SetActive(true);
		//Muestra los score
		scoreAct = 1;
		InvokeRepeating("ShowScore",0,2f);
		//ACHIEVEMENT: Can't touch this
		if (damage.life == damage.maxLife){
			Social.ReportProgress("CgkIuv-YgIkeEAIQAQ", 100.0f, (bool success) => {
				socialManager.Check("Achievement","CgkIuv-YgIkeEAIQAQ",success);
			});
		}
		//ACHIEVEMENT:
		if (dataManger.manager.actualLevel == dataManger.manager.levels){
			Social.ReportProgress("CgkIuv-YgIkeEAIQCw", 100.0f, (bool success) => {
				socialManager.Check("Achievement","CgkIuv-YgIkeEAIQCw",success);
			});
		}
		//ACHIEVEMENT
		if (dataManger.manager.totalStars == dataManger.manager.levels*3){
			Social.ReportProgress("CgkIuv-YgIkeEAIQDA", 100.0f, (bool success) => {
				socialManager.Check("Achievement","CgkIuv-YgIkeEAIQDA",success);
			});
		}
		//Sumamos las puntuaciones totales y se publica en el leaderboard
		int new_score = 0;
		for (int i=1; i<dataManger.manager.unlocks; i++){
			new_score += dataManger.manager.scores["Level_"+i];
		}
		Debug.Log ("Leaderboard nueva puntuacion: "+ new_score);
		Social.ReportScore(new_score, "CgkIuv-YgIkeEAIQDQ", (bool success) => {
			socialManager.Check("Leaderboard",new_score.ToString(),success);
		});
	}
	void Lose(){

		soundManager.PlaySound("Lose");
		dataManger.manager.partidas++;
		if(dataManger.manager.partidas >= 3){
			dataManger.manager.partidas = 0;
			ads.LaunchInterstital();
		}
		LoseSprite.SetActive (true);
		UI1.SetActive (false);
		UI2.SetActive (false);
		if(!Application.loadedLevelName.Contains("Tuto")){
		UI3.SetActive (false);
		UI4.SetActive(true);
		UI5.SetActive(true);
		}else{
			GameObject.Find("UI_Camera").transform.FindChild("Tutorial").gameObject.SetActive(false);
		}

	}

	private void ShowScore(){
		if (scoreAct == 1){
			scoreTotal = 0;
			scoreObject = MisionAcomplished.transform.FindChild("Score_Resumen/Score_Time").gameObject;
			scoreObject.SetActive(true);
			scoreText = scoreObject.GetComponent<tk2dTextMesh>();
			scoreNumber = scoreManager.timeScore;
			scoreAct++;
			scoreChangeText = 0;
			scoreTextRemaining = "Time Score";
			InvokeRepeating("ChangeScore",0,0.05f);
		}else if (scoreAct == 2){
			scoreObject.SetActive(false);
			scoreObject = MisionAcomplished.transform.FindChild("Score_Resumen/Score_Life").gameObject;
			scoreObject.SetActive(true);
			scoreText = scoreObject.GetComponent<tk2dTextMesh>();
			scoreNumber = damage.life;
			scoreChangeText = 0;
			scoreTextRemaining = "Life Score";
			InvokeRepeating("ChangeScore",0,0.05f);
			scoreAct++;
		}else if (scoreAct == 3){
			scoreObject.SetActive(false);
			scoreObject = MisionAcomplished.transform.FindChild("Score_Resumen/Score_Fuel").gameObject;
			scoreObject.SetActive(true);
			scoreText = scoreObject.GetComponent<tk2dTextMesh>();
			scoreNumber = (int)GameObject.Find(dataManger.manager.actualShip + "(Clone)").GetComponent<Movement>().fuel;
			scoreChangeText = 0;
			scoreTextRemaining = "Fuel Score";
			InvokeRepeating("ChangeScore",0,0.05f);
			scoreAct++;
		}else if (scoreAct == 4){
			CancelInvoke("ShowScore");
		}
	}

	private void ChangeScore(){
		if (scoreChangeText < 10){
			scoreText.text = scoreTextRemaining +" "+ Random.Range(0,scoreNumber).ToString();
			scoreChangeText++;
		}else{
			scoreText.text = scoreTextRemaining +" "+ scoreNumber.ToString();
			scoreTotal += scoreNumber;
			scoreTotalText.text = "Score "+ scoreTotal.ToString();
			CancelInvoke("ChangeScore");
		}
	}
}
