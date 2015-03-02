using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class dataManger : MonoBehaviour {

	public static dataManger manager;

	public int pickedAstronauts;
	public int dropedAstronauts;
	public tk2dTextMesh text;
	private TextAstronaut textastronaut;



	void Awake () {

		textastronaut = text.GetComponent<TextAstronaut>();

		if(manager == null){

			manager = this;
			DontDestroyOnLoad(gameObject);

		}else if(manager != this){

			Destroy(gameObject);

		}


	}

	void Start () {

		Load();
		textastronaut.UpdateText();

	}
	


	public void Save(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/data.jmm");
		Data data = new Data();

		data.pickedAstronauts = pickedAstronauts;
		data.dropedAstronauts = dropedAstronauts;

		bf.Serialize(file,data);
		file.Close();
		
	}
	
	public void Load(){

		if(File.Exists(Application.persistentDataPath + "/data.jmm")){

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/data.jmm",FileMode.Open);
			Data data = (Data)bf.Deserialize(file);

			pickedAstronauts = data.pickedAstronauts;
			dropedAstronauts = data.dropedAstronauts;

			file.Close();

		}
		
	}
}

[Serializable]
class Data {
	[HideInInspector]
	public int pickedAstronauts;
	[HideInInspector]
	public int dropedAstronauts;

}
