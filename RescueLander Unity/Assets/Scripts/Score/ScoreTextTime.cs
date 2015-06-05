using UnityEngine;
using System.Collections;

public class ScoreTextTime : MonoBehaviour {

	tk2dTextMesh textMesh;
	ScoreManager scoreManager;

	void Awake(){
		textMesh = GetComponent<tk2dTextMesh>();
		scoreManager = GameObject.Find("ScoreCoin_Manager").GetComponent<ScoreManager> ();
	}

	void Update(){
		textMesh.text = "Score: "+scoreManager.timeScore.ToString();
	}
}
