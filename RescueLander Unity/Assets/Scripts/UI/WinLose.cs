﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinLose : MonoBehaviour {

	public GameObject WinSprite;
	public GameObject LoseSprite;
	public GameObject winText;
	public GameObject UI1;
	public GameObject UI2;
	WinHalo_Anim haloanim;
	tk2dTextMesh text;
	ScoreManager scoreManager;
	// Use this for initialization
	void Awake () {
	
		haloanim = WinSprite.GetComponentInChildren<WinHalo_Anim>();
		text = winText.GetComponent<tk2dTextMesh> ();
		scoreManager = this.GetComponent<ScoreManager> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void End(string result){

		if(result == "Win"){
			StartCoroutine("Win");
		}

		if(result == "Lose"){
			StartCoroutine("Lose");
		}

		//TakeScreenShot(1);

	}
	void TakeScreenShot(int superSize){


		Application.CaptureScreenshot(Application.persistentDataPath + "Screenshots/screenShot",superSize);
	
	}

	IEnumerator Win(){
		
		//haloanim.Win =true;
		WinSprite.SetActive (true);
		int totalScore = (int)scoreManager.scoreCalc ();
		text.text ="Score: "+ totalScore.ToString();
		UI1.SetActive (false);
		UI2.SetActive (false);
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("Menu");
	}
	IEnumerator Lose(){

		LoseSprite.SetActive (true);
		UI1.SetActive (false);
		UI2.SetActive (false);
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("Menu");
	}
}
