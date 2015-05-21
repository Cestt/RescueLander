using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinLose : MonoBehaviour {

	public GameObject WinSprite;
	public GameObject LoseSprite;
	public GameObject winText;
	private GameObject UI1;
	private GameObject UI2;
	private GameObject UI3;

	private ScreenShoter screenshoter;
	private bool first;
	private int totalScore;
	WinHalo_Anim haloanim;
	tk2dTextMesh text;
	ScoreManager scoreManager;
	// Use this for initialization
	void Awake () {
	
//		haloanim = WinSprite.GetComponentInChildren<WinHalo_Anim>();
		if (Application.loadedLevelName != "Menu") {
			GameObject uicamera = GameObject.Find("UI_Camera");
			WinSprite = uicamera.transform.FindChild("WinLayout").gameObject;
			winText = WinSprite.transform.FindChild("Pic_Frame/WinScore_Txt").gameObject;
			LoseSprite = uicamera.transform.FindChild("LoseLayout").gameObject;
			UI1 =  uicamera.transform.FindChild("Anchor (UpperLeft)").gameObject;
			UI2 =  uicamera.transform.FindChild("Anchor (UpperRight)").gameObject;
			UI3 =  uicamera.transform.FindChild("Anchor (LowerCenter)").gameObject;

			text = winText.GetComponent<tk2dTextMesh> ();
			scoreManager = this.GetComponent<ScoreManager> ();
			first = true;	
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void End(string result){

		if(result == "Win"){
			if(first){
				if(dataManger.manager.actualLevel == dataManger.manager.unlocks){
					dataManger.manager.unlocks++;
				}

				totalScore = (int)scoreManager.scoreCalc ();
				if(totalScore > 0 & totalScore <= 500){
					dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 1;
				}
				if(totalScore > 500 & totalScore <= 1500){
					dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 2;
				}
				if(totalScore > 1500){
					dataManger.manager.stars["Level_"+dataManger.manager.actualLevel] = 3;
				}
				dataManger.manager.scores["Level_"+dataManger.manager.actualLevel] = totalScore;
				dataManger.manager.Save(true);
				first = false;
				Win();
			}

		}

		if(result == "Lose"){
			Lose();
		}

		//TakeScreenShot(1);

	}
	void TakeScreenShot(int superSize){


		Application.CaptureScreenshot(Application.persistentDataPath + "Screenshots/screenShot",superSize);
	
	}

	void Win(){

		//haloanim.Win =true;
		WinSprite.SetActive (true);
//		text.text = Localization_Bridge.loc.Score +": "+ totalScore.ToString();
		UI1.SetActive (false);
		UI2.SetActive (false);
		UI3.SetActive (false);



	}
	void Lose(){

		LoseSprite.SetActive (true);
		UI1.SetActive (false);
		UI2.SetActive (false);
		UI3.SetActive (false);

	}
}
