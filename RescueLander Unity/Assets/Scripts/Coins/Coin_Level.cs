using UnityEngine;
using System.Collections;

public class Coin_Level : MonoBehaviour {

	
	tk2dTextMesh text;
	Coin_Manager coin_manager;
	void Awake(){
		text = GetComponent<tk2dTextMesh>();
		coin_manager = GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();
		
	}
	void Update() {
			text.text = coin_manager.levelCoins.ToString();
	}


}
