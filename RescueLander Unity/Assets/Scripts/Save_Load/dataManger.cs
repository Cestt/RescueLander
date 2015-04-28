using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class dataManger : MonoBehaviour {

	public static dataManger manager;

	//[HideInInspector]
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
	public int levels;

	private GameObject temp;



	void Awake () {


		if(manager == null){

			manager = this;
			DontDestroyOnLoad(gameObject);

		}else if(manager != this){

			Destroy(gameObject);

		}

		if(Application.loadedLevelName == "Menu"){
			Initialize();
		}


	}

	


	public void Save(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/data.jmm");
		Data data = new Data();

		data.unlocks = unlocks;
		data.Sounds = Sounds;
		data.Music = Music;

		
		for(int i = 1; i <= levels; i++){
			Debug.Log("Save: "+i);
			data.stars["Level_"+i] = stars["Level_"+i];
			data.scores["Level_"+i] = scores["Level_"+i];

		}
		


		bf.Serialize(file,data);
		file.Close();
		
	}
	
	public void Load(){

		if(File.Exists(Application.persistentDataPath + "/data.jmm")){

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmm",FileMode.Open);
			Data data = (Data)bf.Deserialize(file);

			unlocks = data.unlocks;

			for(int i = 1; i<= levels ; i++){
				stars["Level_"+i] = data.stars["Level_"+i];
				scores["Level_"+i] = data.scores["Level_"+i];

				temp = GameObject.Find("Level_"+i);
				
				if(i<=unlocks){
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

			file.Close();

		}
		
	}

	public void Initialize(){
		if(File.Exists(Application.persistentDataPath + "/data.jmm")){
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmm",FileMode.Open);
			Data data = (Data)bf.Deserialize(file);
			
			unlocks = data.unlocks;

			for(int i = 1; i<= levels ; i++){
				stars["Level_"+i] = data.stars["Level_"+i];
				scores["Level_"+i] = data.scores["Level_"+i];

				temp = GameObject.Find("Level_"+i);

				if(i<=unlocks){
					Transform tempChild =  temp.transform.FindChild("Level_Number");
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
			
		}else{

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/data.jmm");
			Data data = new Data();
			
			data.unlocks = 1;
			unlocks = data.unlocks;

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
	public int unlocks;
	[HideInInspector]
	public bool Sounds;
	[HideInInspector]
	public bool Music;
	[HideInInspector]
	public Dictionary<string,int> stars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scores = new Dictionary<string, int>();
}
