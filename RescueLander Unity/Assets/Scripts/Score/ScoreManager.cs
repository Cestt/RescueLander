using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {


	public int timeScore;
	public float scoreReductionTime;


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
	public float scoreCalc(float life, float fuel){

		float totalScore = 0;

		return totalScore;
	}
}
