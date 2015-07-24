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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ChangeLevelName(string World){
		for(int i = 0; i < Levels.Count; i++){
			if(Levels[i].name.Substring(7,1) == "_")
				Levels[i].name = Levels[i].name.Substring(0,8)+World;
			else
				Levels[i].name = Levels[i].name.Substring(0,9)+World;
		}
		UpdateLevels(World);
	}
	void UpdateLevels(string World){
		/*for(int i = 1; i<= levelsMars ; i++){
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
		}*/
	}
}
