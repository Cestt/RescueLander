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
	[HideInInspector]
	public bool tutoComplete;
	[HideInInspector]
	public List<string>worldUnlocks = new List<string>();
	[HideInInspector]
	public int timePrompFuel;
	
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
			Data2 data2 = (Data2)bf.Deserialize(file);
			file.Close();
			
			data2.unlocksMars = unlocksMars;
			data2.unlocksIce = unlocksIce;
			data2.Sounds = Sounds;
			data2.Music = Music;
			data2.actualShip = actualShip;
			data2.color1r = color1r;
			data2.color1g = color1g;
			data2.color1b = color1b;
			data2.color2r = color2r;
			data2.color2g = color2g;
			data2.color2b = color2b;
			data2.shipUnlocks = shipUnlocks;
			data2.coins = coins;
			data2.totalStars = totalStars;
			data2.fuelPowerUps = fuelPowerUps;
			data2.magnetPowerUps = magnetPowerUps;
			data2.shieldPowerUps = shieldPowerUps;
			data2.inverted = inverted;
			data2.tutorial = tutorial;
			data2.nextPowerUp = nextPowerUp;
			data2.nextPowerUpName = nextPowerUpName;
			data2.actualWorld = actualWorld;
			data2.tutoComplete = tutoComplete;
			data2.worldUnlocks = worldUnlocks;
			data2.timePrompFuel = timePrompFuel;
			if(complete){
				for(int i = 1; i <= levelsMars; i++){
					if(starsMars["Level_"+i] > data2.starsMars["Level_"+i]){
						//totalStars -= data2.stars["Level_"+i];
						data2.starsMars["Level_"+i] = starsMars["Level_"+i];
						//totalStars += stars["Level_"+i];
						data2.totalStars = totalStars;
						Debug.Log("Save Stars");
					}
					if(scoresMars["Level_"+i] > data2.scoresMars["Level_"+i]){
						data2.scoresMars["Level_"+i] = scoresMars["Level_"+i];
						Debug.Log("Save Scores");
					}else{
						scoresMars["Level_"+i] = data2.scoresMars["Level_"+i];
					}
					
					
				}
				for(int i = 1; i <= levelsIce; i++){
					if(starsIce["Level_"+i] > data2.starsIce["Level_"+i]){
						//totalStars -= data2.stars["Level_"+i];
						data2.starsIce["Level_"+i] = starsIce["Level_"+i];
						//totalStars += stars["Level_"+i];
						data2.totalStars = totalStars;
						Debug.Log("Save Stars");
					}
					if(scoresIce["Level_"+i] > data2.scoresIce["Level_"+i]){
						data2.scoresIce["Level_"+i] = scoresIce["Level_"+i];
						Debug.Log("Save Scores");
					}else{
						scoresIce["Level_"+i] = data2.scoresIce["Level_"+i];
					}
					
					
				}
			}
			
			
			
			file = File.Create(Application.persistentDataPath + "/data.jmma");
			
			
			bf.Serialize(file,data2);
			file.Close();
		}
		
		
	}
	
	public void Load(){
		
		if(File.Exists(Application.persistentDataPath + "/data.jmma")){
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmma",FileMode.Open);
			Data2 data2 = (Data2)bf.Deserialize(file);
			
			unlocksMars = data2.unlocksMars;
			unlocksIce = data2.unlocksIce;
			Sounds = data2.Sounds;
			Music = data2.Music;
			actualShip = data2.actualShip;
			color1r = data2.color1r;
			color1g = data2.color1g;
			color1b = data2.color1b;
			color2r = data2.color2r;
			color2g = data2.color2g;
			color2b = data2.color2b;
			shipUnlocks = data2.shipUnlocks;
			coins = data2.coins;
			totalStars = data2.totalStars;
			fuelPowerUps = data2.fuelPowerUps;
			magnetPowerUps = data2.magnetPowerUps;
			shieldPowerUps = data2.shieldPowerUps;
			inverted = data2.inverted;
			tutorial = data2.tutorial;
			nextPowerUp = data2.nextPowerUp;
			actualWorld = data2.actualWorld;
			nextPowerUpName = data2.nextPowerUpName;
			tutoComplete = data2.tutoComplete;
			worldUnlocks = data2.worldUnlocks;
			timePrompFuel = data2.timePrompFuel;
			if (worldUnlocks.Contains("Ice")){
				GameObject.Find ("World_Buttons").transform.FindChild("Button_World_Ice/World2_Text").gameObject.SetActive(true);
				Transform tempWorld = GameObject.Find ("World_Buttons").transform.FindChild("Button_World_Ice/Button_Buy");
				if (tempWorld != null)
					tempWorld.gameObject.SetActive(false);
				
			}
			for(int i = 1; i<= levelsMars ; i++){
				starsMars["Level_"+i] = data2.starsMars["Level_"+i];
				scoresMars["Level_"+i] = data2.scoresMars["Level_"+i];
				
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
							}else{
								tempChild =  temp.transform.FindChild("LevelStar_"+j);
								tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Lose");
							}
						}
						tempChild =  temp.transform.FindChild("Level_Score");
						tempChild.GetComponent<tk2dTextMesh>().text = "Score :"
							+ scoresMars["Level_"+i].ToString();		
					}else{
						tempChild =  temp.transform.FindChild("Level_Number");
						tempChild.GetComponent<tk2dTextMesh>().color = new Color32(164,182,182,255);
					}
				}
				
			}
			for(int i = 1; i <= levelsIce; i++){
				starsIce["Level_"+i] = data2.starsIce["Level_"+i];
				scoresIce["Level_"+i] = data2.scoresIce["Level_"+i];
				
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
								tempChild =  temp.transform.FindChild("LevelStar_"+j);
								tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Win");
							}else{
								tempChild =  temp.transform.FindChild("LevelStar_"+j);
								tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Lose");
							}	
						}
						tempChild =  temp.transform.FindChild("Level_Score");
						tempChild.GetComponent<tk2dTextMesh>().text ="Score: "+ scoresIce["Level_"+i].ToString();
					}else if (i<= unlocksMars){
						for(int j = 1; j<=3; j++){
							tempChild =  temp.transform.FindChild("LevelStar_"+j);
							tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Lose");
						}
						tempChild =  temp.transform.FindChild("Level_Number");
						tempChild.GetComponent<tk2dTextMesh>().color = new Color32(164,182,182,255);
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
				Parsedata();
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
				Data data = new Data();
				data.tutorial = 1;
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
				Parsedata();
			}
			
		}
		
	}
	private void Parsedata(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/data.jmma");
		Data2 data2 = new Data2();
		FileStream file2 = File.Open(Application.persistentDataPath + "/data.jmm",FileMode.Open);
		Data data = (Data)bf.Deserialize(file2);
		data2.tutorial= data.tutorial;
		data2.unlocksMars = data.unlocks;
		data2.Sounds = data.Sounds;
		data2.Music = data.Music;
		data2.actualShip = data.actualShip;
		data2.coins = data.coins;
		data2.coinsAcumulated = data.coinsAcumulated;
		data2.coinsSpend = data.coinsSpend;
		data2.color1b = data.color1b;
		data2.color1g = data.color1g;
		data2.color1r = data.color1r;
		data2.color2b = data.color2b;
		data2.color2g = data.color2g;
		data2.color2r = data.color2r;
		data2.fuelPowerUps = data.fuelPowerUps;
		data2.shieldPowerUps = data.shieldPowerUps;
		data2.magnetPowerUps = data.magnetPowerUps;
		data2.inverted = data.inverted;
		data2.scoresMars = data.scores;
		data2.shipUnlocks = data.shipUnlocks;
		data2.socialPending = data.socialPending;
		data2.starsMars = data.stars;
		data2.totalStars = data.totalStars;
		data2.unlocksIce = 1;
		actualWorld = "Mars";
		data2.actualWorld = actualWorld;
		unlocksIce = data2.unlocksIce;
		tutoComplete = false;
		data2.tutoComplete = tutoComplete;
		data2.worldUnlocks.Add("Mars");
		worldUnlocks = data2.worldUnlocks;
		timePrompFuel = 0;
		data2.timePrompFuel = timePrompFuel;
		Debug.Log("Unlocks Ice: "+ unlocksIce);
		for(int i = 1; i <= levelsIce; i++){
			data2.starsIce.Add("Level_"+i,0);
			data2.scoresIce.Add("Level_"+i,0);
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
		bf.Serialize(file,data2);
		file.Close();
	}
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
class Data2 {
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
	[HideInInspector]
	public string actualWorld;
	[HideInInspector]
	public bool tutoComplete;
	[HideInInspector]
	public List<string>worldUnlocks = new List<string>();
	[HideInInspector]
	public int timePrompFuel;
}

