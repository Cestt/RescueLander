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
	public int unlocks = 1;
	[HideInInspector]
	public int actualLevel;
	[HideInInspector]
	public bool Sounds = true;
	[HideInInspector]
	public bool Music = true;
	[HideInInspector]
	public Dictionary<string,int> stars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scores = new Dictionary<string, int>();
	[HideInInspector]
	public string actualShip = "Ship01";
	[HideInInspector]
	public int totalStars;
	public int levels;
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

		if(File.Exists(Application.persistentDataPath + "/data.jmm")){
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmm",FileMode.Open);
			Data data = (Data)bf.Deserialize(file);
			file.Close();

			data.unlocks = unlocks;
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
			if(complete){
				for(int i = 1; i <= levels; i++){
					if(stars["Level_"+i] > data.stars["Level_"+i]){
						//totalStars -= data.stars["Level_"+i];
						data.stars["Level_"+i] = stars["Level_"+i];
						//totalStars += stars["Level_"+i];
						data.totalStars = totalStars;
						Debug.Log("Save Stars");
					}
					if(scores["Level_"+i] > data.scores["Level_"+i]){
						data.scores["Level_"+i] = scores["Level_"+i];
						Debug.Log("Save Scores");
					}else{
						scores["Level_"+i] = data.scores["Level_"+i];
					}
					
					
				}
			}

			

			 file = File.Create(Application.persistentDataPath + "/data.jmm");

			
			bf.Serialize(file,data);
			file.Close();
		}

		
	}
	
	public void Load(){

		if(File.Exists(Application.persistentDataPath + "/data.jmm")){

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmm",FileMode.Open);
			Data data = (Data)bf.Deserialize(file);

			unlocks = data.unlocks;
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


			for(int i = 1; i<= levels ; i++){
				stars["Level_"+i] = data.stars["Level_"+i];
				scores["Level_"+i] = data.scores["Level_"+i];

				temp = GameObject.Find("Level_"+i);
				Transform tempChild;
				tempChild =  temp.transform.FindChild("Level_Score");
				tempChild.GetComponent<tk2dTextMesh>().text ="Score: "+ scores["Level_"+i].ToString();
				if(i<=unlocks){

					tempChild =  temp.transform.FindChild("Level_Number");
					tempChild.GetComponent<tk2dTextMesh>().color = new Color(255,195,0,255);
					for(int j = 1; j<=3; j++){
						if(j<=stars["Level_"+i]){
							tempChild =  temp.transform.FindChild("LevelStar_"+j);
							tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Win");
						}
						
					}
					tempChild =  temp.transform.FindChild("Level_Score");
					tempChild.GetComponent<tk2dTextMesh>().text ="Score: "+ scores["Level_"+i].ToString();
				}
			}
			file.Close();

		}
		
	}

	public void Initialize(){
		if(File.Exists(Application.persistentDataPath + "/data.jmm")){
			Debug.Log("File found");
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
			unlocks = data.unlocks;
			tutorial = data.tutorial;
			data.actualShip = actualShip;
			shipUnlocks.Add("Ship01");
			data.shipUnlocks = shipUnlocks;
			for(int i = 1; i <= levels; i++){
				data.stars.Add("Level_"+i,0);
				data.scores.Add("Level_"+i,0);
				stars.Add("Level_"+i,0);
				scores.Add("Level_"+i,0);


				temp = GameObject.Find("Level_"+i);
				if(i<=unlocks & temp!=null){
					Transform tempChild =  temp.transform.FindChild("Level_Number");
					tempChild.GetComponent<tk2dTextMesh>().color = new Color(255,195,0,255);
					for(int j = 1; j<=3; j++){
						if(j<=stars["Level_"+i]){
							tempChild =  temp.transform.FindChild("LevelStar"+j);
							tempChild.GetComponent<tk2dSprite>().SetSprite("Estrella_Win");
						}
						
					}
					tempChild =  temp.transform.FindChild("Level_Score");
					tempChild.GetComponent<tk2dTextMesh>().text ="Score: "+ scores["Level_"+i].ToString();

				}

			}
			
			bf.Serialize(file,data);
			file.Close();
		}

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
