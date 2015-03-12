using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class dataManger : MonoBehaviour {

	public static dataManger manager;

	[HideInInspector]
	public int unlocks;
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

		unlocks = data.unlocks;
		
		for(int i = 0; i <= levels; i++){
			data.stars["level"+i] = stars["level"+i];
			data.scores["scores"+i] = stars["level"+i];

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

			for(int i = 0; i<= levels ; i++){
				stars["level"+i] = data.stars["level"+i];
				scores["level"+i] = data.scores["scores"+i];
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

			for(int i = 0; i<= levels ; i++){
				stars["level"+i] = data.stars["level"+i];
				scores["level"+i] = data.scores["scores"+i];

				temp = GameObject.Find("level"+i);
			}
			
			file.Close();
			
		}else{

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/data.jmm");
			Data data = new Data();
			
			data.unlocks = 0;
			unlocks = data.unlocks;

			for(int i = 0; i <= levels; i++){
				data.stars.Add("level"+i,0);
				data.scores.Add("level"+0,0);
				stars.Add("level"+i,0);
				scores.Add("level"+i,0);


				temp = GameObject.Find("level"+i);

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
	public Dictionary<string,int> stars = new Dictionary<string, int>();
	[HideInInspector]
	public Dictionary<string,int> scores = new Dictionary<string, int>();
}
