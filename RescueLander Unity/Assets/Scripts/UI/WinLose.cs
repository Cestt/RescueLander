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
				ship = GameObject.Find (dataManger.manager.actualShip+"(Clone)");
				UI3 =  uicamera.transform.FindChild("Anchor (LowerCenter)").gameObject;
				UI4 =  uicamera.transform.FindChild("Anchor (LowerLeft)").gameObject;
				UI5 =  uicamera.transform.FindChild("Anchor (LowerRight)").gameObject;
				coin_manager = GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();
				scoreManager =  GameObject.Find("ScoreCoin_Manager").GetComponent<ScoreManager> ();
				scoreTotalText = MisionAcomplished.transform.FindChild("Score_Resumen/Score_Total").gameObject.GetComponent<tk2dTextMesh> ();
			}
			damage = ship.GetComponent<Damage>();
			LoseSprite = uicamera.transform.FindChild("LoseLayout").gameObject;
			UI1 =  uicamera.transform.FindChild("Anchor (UpperLeft)").gameObject;
			UI2 =  uicamera.transform.FindChild("Anchor (UpperRight)").gameObject;
			text = winText.GetComponent<tk2dTextMesh> ();


			socialManager = GameObject.Find ("Game Manager").GetComponent<Social_Manager>();
			first = true;	
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != "Menu" & !Application.loadedLevelName.Contains("Tuto")) {
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
				Movement mov = ship.GetComponent<Movement>();
				float fuel = mov.GetComponent<Movement>().fuel;
				float maxFuel = mov.GetComponent<Movement>().originalFuel;

				if(dataManger.manager.actualLevel == dataManger.manager.unlocks & !Application.loadedLevelName.Contains("Tuto")){
					dataManger.manager.unlocks++;
				}
				if(!Application.loadedLevelName.Contains("Tuto")){
					scoreManager.stopScore();
					totalScore = (int)scoreManager.timeScore;
					
					if(totalScore > 1500 & damage.life >= (((float)damage.maxLife*lifeThrdStarPercent)/100f)){
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
					else if(totalScore > 500 & damage.life >= (((float)damage.maxLife*50)/100f)){
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
			LoseSprite.transform.FindChild("CoinCount/Collected Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = coin_manager.levelCoins.ToString();
			LoseSprite.transform.FindChild("CoinCount/Total Coins/CoinCount_Number").GetComponent<tk2dTextMesh>().text = dataManger.manager.coins.ToString();
			dataManger.manager.Save(true);
			Lose();
		}



	}


	void Win(){

		//haloanim.Win =true;
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
		for (int i=1; i<=dataManger.manager.actualLevel; i++){
			new_score += dataManger.manager.scores["Level_"+i];
		}
		Debug.Log ("Leaderboard nueva puntuacion: "+ new_score);
		Social.ReportScore(new_score, "CgkIuv-YgIkeEAIQDQ", (bool success) => {
			socialManager.Check("Leaderboard",new_score.ToString(),success);
		});
	}
	void Lose(){

		LoseSprite.SetActive (true);
		UI1.SetActive (false);
		UI2.SetActive (false);
		UI3.SetActive (false);
		UI4.SetActive(true);
		UI5.SetActive(true);

	}

	private void ShowScore(){
		if (scoreAct == 1){
			scoreTotal = 0;
			scoreObject = MisionAcomplished.transform.FindChild("Score_Resumen/Score_Time").gameObject;
			scoreObject.SetActive(true);
			scoreText = scoreObject.GetComponent<tk2dTextMesh>();
			scoreNumber = scoreManager.timeScore;
			Debug.Log ("Time:"+scoreNumber);
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
			Debug.Log ("Life:"+scoreNumber);
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
			Debug.Log ("Fuel:"+scoreNumber);
			scoreChangeText = 0;
			scoreTextRemaining = "Fuel Score";
			InvokeRepeating("ChangeScore",0,0.05f);
			scoreAct++;
		}else if (scoreAct == 4){
			CancelInvoke("ShowScore");
			Debug.Log ("Cancelando ShowScore");
		}
	}

	private void ChangeScore(){
		if (scoreChangeText < 10){
			scoreText.text = scoreTextRemaining +" "+ Random.Range(0,scoreNumber).ToString();
			scoreChangeText++;
		}else{
			scoreText.text = scoreTextRemaining +" "+ scoreNumber.ToString();
			scoreTotal += scoreNumber;
			Debug.Log ("ScoreTotal:"+scoreTotal);
			scoreTotalText.text = "Score "+ scoreTotal.ToString();
			CancelInvoke("ChangeScore");
			Debug.Log ("Cancelando ChangeScore");
		}
	}
}
