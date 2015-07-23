using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using GooglePlayGames;

public class dataManger : MonoBehaviour {

	public static dataManger manager;


	[HideInInspector]
	public int fuelPowerUps;
	[HideInInspector]
	public int shieldPowerUps;
	[HideInInspector]
	public int magnetPowerUps;
	[HideInInspector]
	public string Camposition = "";
	[HideInInspector]
	public int unlocksMars = 1;
	//[HideInInspector]
	public int unlocksIce = 1;
	[HideInInspector]
	public int actualLevel;
	[HideInInspector]
	public string actualWorld;
	[HideInInspector]
	public bool Sounds = true;
	[HideInInspector]
	public bool Music = true;
	[HideInInspector]
	public Dictionary<string,int> starsMars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scoresMars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> starsIce = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scoresIce = new Dictionary<string, int>();
	[HideInInspector]
	public string actualShip = "Ship01";
	[HideInInspector]
	public int totalStars;
	public int levelsMars;
	public int levelsIce;
	[HideInInspector]
	public int coins;
	[HideInInspector]
	public float color1r;
	[HideInInspector]
	public float color1g;
	[HideInInspector]
	public float color1b;
	[HideInInspector]
	public float color2r;
	[HideInInspector]
	public float color2g;
	[HideInInspector]
	public float color2b;
	private List<GameObject> Ships = new List<GameObject>();
	[HideInInspector]
	public List<string> shipUnlocks = new List<string>();
	[HideInInspector]
	public bool inverted;
	[HideInInspector]
	public Dictionary<string,string> socialPending = new Dictionary<string, string>();
	[HideInInspector]
	public int coinsSpend;
	[HideInInspector]
	public int coinsAcumulated;
	//[HideInInspector]
	public int tutorial = 1;
	[HideInInspector]
	public int partidas;
	public bool nextPowerUp;
	[HideInInspector]
	public string nextPowerUpName;

	private GameObject temp;



	void Awake () {

		Screen.sleepTimeout = SleepTimeout.SystemSetting;
		if(manager == null){

			manager = this;
			DontDestroyOnLoad(gameObject);

		}else if(manager != this){

			Destroy(gameObject);

		}

		if(Application.loadedLevelName == "Menu"){
			Initialize();
		}
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		StartCoroutine("tryLogin");
	}
	private IEnumerator tryLogin(){
		yield return null;
		Social.localUser.Authenticate((bool success) => {

		});
	}

	


	public void Save(bool complete){

		if(File.Exists(Application.persistentDataPath + "/data.jmma")){
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmma",FileMode.Open);
			Data data = (Data)bf.Deserialize(file);
			file.Close();

			data.unlocksMars = unlocksMars;
			data.Sounds = Sounds;
			data.Music = Music;
			data.actualShip = actualShip;
			data.color1r = color1r;
			data.color1g = color1g;
			data.color1b = color1b;
			data.color2r = color2r;
			data.color2g = color2g;
			data.color2b = color2b;
			data.shipUnlocks = shipUnlocks;
			data.coins = coins;
			data.totalStars = totalStars;
			data.fuelPowerUps = fuelPowerUps;
			data.magnetPowerUps = magnetPowerUps;
			data.shieldPowerUps = shieldPowerUps;
			data.inverted = inverted;
			data.tutorial = tutorial;
			data.nextPowerUp = nextPowerUp;
			data.nextPowerUpName = nextPowerUpName;
			if(complete){
				for(int i = 1; i <= levelsMars; i++){
					if(starsMars["Level_"+i] > data.starsMars["Level_"+i]){
						//totalStars -= data.stars["Level_"+i];
						data.starsMars["Level_"+i] = starsMars["Level_"+i];
						//totalStars += stars["Level_"+i];
						data.totalStars = totalStars;
						Debug.Log("Save Stars");
					}
					if(scoresMars["Level_"+i] > data.scoresMars["Level_"+i]){
						data.scoresMars["Level_"+i] = scoresMars["Level_"+i];
						Debug.Log("Save Scores");
					}else{
						scoresMars["Level_"+i] = data.scoresMars["Level_"+i];
					}
					
					
				}
			}

			

			 file = File.Create(Application.persistentDataPath + "/data.jmm");

			
			bf.Serialize(file,data);
			file.Close();
		}

		
	}
	
	public void Load(){

		if(File.Exists(Application.persistentDataPath + "/data.jmma")){

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmma",FileMode.Open);
			Data data = (Data)bf.Deserialize(file);

			unlocksMars = data.unlocksMars;
			unlocksIce = data.unlocksIce;
			Sounds = data.Sounds;
			Music = data.Music;
			actualShip = data.actualShip;
			color1r = data.color1r;
			color1g = data.color1g;
			color1b = data.color1b;
			color2r = data.color2r;
			color2g = data.color2g;
			color2b = data.color2b;
			shipUnlocks = data.shipUnlocks;
			coins = data.coins;
			totalStars = data.totalStars;
			fuelPowerUps = data.fuelPowerUps;
			magnetPowerUps = data.magnetPowerUps;
			shieldPowerUps = data.shieldPowerUps;
			inverted = data.inverted;
			tutorial = data.tutorial;
			nextPowerUp = data.nextPowerUp;
			nextPowerUpName = data.nextPowerUpName;

			for(int i = 1; i<= levelsMars ; i++){
				starsMars["Level_"+i] = data.starsMars["Level_"+i];
				scoresMars["Level_"+i] = data.scoresMars["Level_"+i];

				temp = GameObject.Find("Level_"+i+"_Mars");
				Transform tempChild;
				if(temp != null){
					tempChild =  temp.transform.FindChild("Level_Score");
					tempChild.GetComponent<tk2dTextMesh>().text = "Score :"
						+ scoresMars["Level_"+i].ToString();
					if(i<=unlocksMars){
						
						tempChild =  temp.transform.FindChild("Level_Number");
						tempChild.GetComponent<tk2dTextMesh>().color = new Color(255,195,0,255);
						for(int j = 1; j<=3; j++){
							if(j<=starsMars["Level_"+i]){
								tempChild =  temp.transform.FindChild("LevelStar_"+j);
								tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Win");
							}
							
						}
						tempChild =  temp.transform.FindChild("Level_Score");
						tempChild.GetComponent<tk2dTextMesh>().text = "Score :"
							+ scoresMars["Level_"+i].ToString();		
					}
				}
				
			}
			for(int i = 1; i <= levelsIce; i++){
				starsIce["Level_"+i] = data.starsIce["Level_"+i];
				scoresIce["Level_"+i] = data.scoresIce["Level_"+i];
				
				temp = GameObject.Find("Level_"+i+"_Ice");
				Transform tempChild;
				if(temp!= null){
					tempChild =  temp.transform.FindChild("Level_Score");
					tempChild.GetComponent<tk2dTextMesh>().text = "Score :"
						+ scoresIce["Level_"+i].ToString();
					if(i<=unlocksIce ){
						tempChild =  temp.transform.FindChild("Level_Number");
						tempChild.GetComponent<tk2dTextMesh>().color = new Color(255,195,0,255);
						for(int j = 1; j<=3; j++){
							if(j<=starsIce["Level_"+i]){
								tempChild =  temp.transform.FindChild("LevelStar"+j);
								tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Win");
							}
							
						}
						tempChild =  temp.transform.FindChild("Level_Score");
						tempChild.GetComponent<tk2dTextMesh>().text ="Score: "+ scoresIce["Level_"+i].ToString();

				}
					
				}
			}
			file.Close();

		}
		
	}

	public void Initialize(){
		if(File.Exists(Application.persistentDataPath + "/data.jmm") & File.Exists(Application.persistentDataPath + "/data.jmma")){
			Debug.Log("File found");
			Load();
			
		}else{
			if(File.Exists(Application.persistentDataPath + "/data.jmm")){
				ParseData();
				Load();

			}else{
				if(PlayerPrefs.GetInt("Ads") == null){
					PlayerPrefs.SetInt("Ads",0);
				}
				if(PlayerPrefs.GetInt("Garaje") == null){
					PlayerPrefs.SetInt("Garaje",0);
				}
				
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Create(Application.persistentDataPath + "/data.jmm");
				Data2 data = new Data2();
				data.tutorial = 4;
				data.unlocks = 1;
				unlocksMars = data.unlocks;
				tutorial = data.tutorial;
				data.actualShip = actualShip;
				shipUnlocks.Add("Ship01");
				data.shipUnlocks = shipUnlocks;
				for(int i = 1; i <= levelsMars; i++){
					data.stars.Add("Level_"+i,0);
					data.scores.Add("Level_"+i,0);
					starsMars.Add("Level_"+i,0);
					scoresMars.Add("Level_"+i,0);
					
					
					temp = GameObject.Find("Level_"+i+"_Mars");
					if(i<=unlocksMars & temp!=null){
						Transform tempChild =  temp.transform.FindChild("Level_Number");
						tempChild.GetComponent<tk2dTextMesh>().color = new Color(255,195,0,255);
						for(int j = 1; j<=3; j++){
							if(j<=starsMars["Level_"+i]){
								tempChild =  temp.transform.FindChild("LevelStar"+j);
								tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Win");
							}
							
						}
						tempChild =  temp.transform.FindChild("Level_Score");
						tempChild.GetComponent<tk2dTextMesh>().text ="Score: "+ scoresMars["Level_"+i].ToString();
						
					}
					
				}
				
				bf.Serialize(file,data);
				file.Close();
				ParseData();
			}

		}

	}
	private void ParseData(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/data.jmma");
		Data data = new Data();
		FileStream file2 = File.Open(Application.persistentDataPath + "/data.jmm",FileMode.Open);
		Data2 data2 = (Data2)bf.Deserialize(file2);
		data.tutorial= data2.tutorial;
		data.unlocksMars = data2.unlocks;
		data.Sounds = data2.Sounds;
		data.Music = data2.Music;
		data.actualShip = data2.actualShip;
		data.coins = data2.coins;
		data.coinsAcumulated = data2.coinsAcumulated;
		data.coinsSpend = data2.coinsSpend;
		data.color1b = data2.color1b;
		data.color1g = data2.color1g;
		data.color1r = data2.color1r;
		data.color2b = data2.color2b;
		data.color2g = data2.color2g;
		data.color2r = data2.color2r;
		data.fuelPowerUps = data2.fuelPowerUps;
		data.shieldPowerUps = data2.shieldPowerUps;
		data.magnetPowerUps = data2.magnetPowerUps;
		data.inverted = data2.inverted;
		data.scoresMars = data2.scores;
		data.shipUnlocks = data2.shipUnlocks;
		data.socialPending = data2.socialPending;
		data.starsMars = data2.stars;
		data.totalStars = data2.totalStars;
		data.unlocksIce = 21;
		unlocksIce = data.unlocksIce;
		Debug.Log("Unlocks Ice: "+ unlocksIce);
		for(int i = 1; i <= levelsIce; i++){
			data.starsIce.Add("Level_"+i,0);
			data.scoresIce.Add("Level_"+i,0);
			starsIce.Add("Level_"+i,0);
			scoresIce.Add("Level_"+i,0);

			temp = GameObject.Find("Level_"+i+"_Ice");
			if(i<=unlocksIce & temp!=null){
				Transform tempChild =  temp.transform.FindChild("Level_Number");
				tempChild.GetComponent<tk2dTextMesh>().color = new Color(255,195,0,255);
				for(int j = 1; j<=3; j++){
					if(j<=starsIce["Level_"+i]){
						tempChild =  temp.transform.FindChild("LevelStar"+j);
						tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Win");
					}
					
				}
				tempChild =  temp.transform.FindChild("Level_Score");
				tempChild.GetComponent<tk2dTextMesh>().text ="Score: "+ scoresIce["Level_"+i].ToString();
				
			}
		}
		bf.Serialize(file,data);
		file.Close();
	}
}

[Serializable]
class Data2 {
	[HideInInspector]
	public int fuelPowerUps;
	[HideInInspector]
	public int shieldPowerUps;
	[HideInInspector]
	public int magnetPowerUps;
	[HideInInspector]
	public int unlocks;
	[HideInInspector]
	public string actualShip;
	[HideInInspector]
	public bool Sounds = true;
	[HideInInspector]
	public bool Music = true;
	[HideInInspector]
	public Dictionary<string,int> stars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scores = new Dictionary<string, int>();
	[HideInInspector]
	public int totalStars;
	[HideInInspector]
	public int coins;
	[HideInInspector]
	public float color1r;
	[HideInInspector]
	public float color1g;
	[HideInInspector]
	public float color1b;
	[HideInInspector]
	public float color2r;
	[HideInInspector]
	public float color2g;
	[HideInInspector]
	public float color2b;
	[HideInInspector]
	public List<string> shipUnlocks = new List<string>();
	[HideInInspector]
	public bool inverted;
	[HideInInspector]
	public Dictionary<string,string> socialPending = new Dictionary<string, string>();
	[HideInInspector]
	public int coinsSpend;
	[HideInInspector]
	public int coinsAcumulated;
	[HideInInspector]
	public int tutorial;
}

[Serializable]
class Data {
	[HideInInspector]
	public int fuelPowerUps;
	[HideInInspector]
	public int shieldPowerUps;
	[HideInInspector]
	public int magnetPowerUps;
	[HideInInspector]
	public int unlocksMars;
	[HideInInspector]
	public int unlocksIce;
	[HideInInspector]
	public string actualShip;
	[HideInInspector]
	public bool Sounds = true;
	[HideInInspector]
	public bool Music = true;
	[HideInInspector]
	public Dictionary<string,int> starsMars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scoresMars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> starsIce = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scoresIce = new Dictionary<string, int>();
	[HideInInspector]
	public int totalStars;
	[HideInInspector]
	public int coins;
	[HideInInspector]
	public float color1r;
	[HideInInspector]
	public float color1g;
	[HideInInspector]
	public float color1b;
	[HideInInspector]
	public float color2r;
	[HideInInspector]
	public float color2g;
	[HideInInspector]
	public float color2b;
	[HideInInspector]
	public List<string> shipUnlocks = new List<string>();
	[HideInInspector]
	public bool inverted;
	[HideInInspector]
	public Dictionary<string,string> socialPending = new Dictionary<string, string>();
	[HideInInspector]
	public int coinsSpend;
	[HideInInspector]
	public int coinsAcumulated;
	[HideInInspector]
	public int tutorial;
	[HideInInspector]
	public bool nextPowerUp;
	[HideInInspector]
	public string nextPowerUpName;
}

