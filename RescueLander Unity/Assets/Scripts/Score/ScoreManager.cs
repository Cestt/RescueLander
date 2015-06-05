﻿using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {


	public int timeScore;
	public float scoreReductionTime;
	private GameObject ship;
	private Damage damage;
	private Movement movement;


	void Awake(){
		if (Application.loadedLevelName != "Menu") {
			if(Application.loadedLevelName.Contains("Tuto")){
				ship = GameObject.Find("101(Clone)");
			}else{
				ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
			}
			damage = ship.GetComponent<Damage>();
			movement = ship.GetComponent<Movement>();
		}

	}

	void Start () {
	
		InvokeRepeating("timeScoreCalc",0,scoreReductionTime);

	}
	
	// Update is called once per frame
	void Update () {



	}

	void timeScoreCalc(){

		if(timeScore > 0){

			timeScore -= 1;

		}else{
			CancelInvoke("timeScoreCalc");
		}


	}
	public float scoreCalc(){

		CancelInvoke ("timeScoreCalc");
		float totalScore = timeScore + damage.life + movement.fuel;

		return totalScore;
	}
}
