using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class World_Change : MonoBehaviour {

	private List<GameObject> Levels = new List<GameObject>();

	void Awake () {
		GameObject temp = GameObject.Find("Levels");
		foreach(Transform child in temp.transform){
			if(child.parent == temp.transform){
				Levels.Add(child.gameObject);
			}
		}
		if(dataManger.manager.actualWorld == "Mars"){
			ChangeLevelName("Mars");
		}else{
			ChangeLevelName("Ice");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ChangeLevelName(string World){
		dataManger.manager.actualWorld = World;
		dataManger.manager.Save(false);
		for(int i = 0; i < Levels.Count; i++){
			if(Levels[i].name.Substring(7,1) == "_")
				Levels[i].name = Levels[i].name.Substring(0,8)+World;
			else
				Levels[i].name = Levels[i].name.Substring(0,9)+World;
		}
		UpdateLevels(World);
	}
	void UpdateLevels(string World){
		dataManger.manager.Load();
		GameObject temp = GameObject.Find("Background");
		if(World == "Mars"){
			temp.transform.FindChild("MidParalax_A").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_A");
			temp.transform.FindChild("MidParalax_A 1").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_A");
			temp.transform.FindChild("MidParalax_B").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_B");
			temp.transform.FindChild("MidParalax_B 1").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_B");
			temp.transform.FindChild("RatioGround_Extra/RatioGround_Tile_B").GetComponent<tk2dTiledSprite>().SetSprite("GroundBg_Tile");
			temp.transform.FindChild("RatioGround_Extra/RatioGround_Tile").GetComponent<tk2dTiledSprite>().SetSprite("GroundBg_TileTop");
			temp.transform.FindChild("BG_Sprite01").GetComponent<tk2dSprite>().SetSprite("Background_01");
			temp.transform.FindChild("Rocks_Mars").gameObject.SetActive(true);
			temp.transform.FindChild("Rocks_Ice").gameObject.SetActive(false);
		}else{
			temp.transform.FindChild("MidParalax_A").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_Ice_A");
			temp.transform.FindChild("MidParalax_A 1").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_Ice_A");
			temp.transform.FindChild("MidParalax_B").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_Ice_B");
			temp.transform.FindChild("MidParalax_B 1").GetComponent<tk2dSprite>().SetSprite("Mid_Paralax_Ice_B");
			temp.transform.FindChild("RatioGround_Extra/RatioGround_Tile_B").GetComponent<tk2dTiledSprite>().SetSprite("GroundBg_Tile_Ice");
			temp.transform.FindChild("RatioGround_Extra/RatioGround_Tile").GetComponent<tk2dTiledSprite>().SetSprite("GroundBg_TileTop_Ice");
			temp.transform.FindChild("BG_Sprite01").GetComponent<tk2dSprite>().SetSprite("Background_02");
			temp.transform.FindChild("Rocks_Mars").gameObject.SetActive(false);
			temp.transform.FindChild("Rocks_Ice").gameObject.SetActive(true);
		}

	}
}
