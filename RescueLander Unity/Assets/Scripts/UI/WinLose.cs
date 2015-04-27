using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinLose : MonoBehaviour {

	public GameObject WinSprite;
	public GameObject LoseSprite;
	public GameObject winText;
	public GameObject UI1;
	public GameObject UI2;

	private ScreenShoter screenshoter;
	private bool first;
	private int totalScore;
	WinHalo_Anim haloanim;
	tk2dTextMesh text;
	ScoreManager scoreManager;
	// Use this for initialization
	void Awake () {
	
		haloanim = WinSprite.GetComponentInChildren<WinHalo_Anim>();
		text = winText.GetComponent<tk2dTextMesh> ();
		scoreManager = this.GetComponent<ScoreManager> ();
		first = true;

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
				dataManger.manager.scores["Level_"+dataManger.manager.actualLevel] = totalScore;
				dataManger.manager.Save();
				first = false;
			}
			Win();
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
		text.text ="Score: "+ totalScore.ToString();
		UI1.SetActive (false);
		UI2.SetActive (false);

	}
	void Lose(){

		LoseSprite.SetActive (true);
		UI1.SetActive (false);
		UI2.SetActive (false);

	}
}
