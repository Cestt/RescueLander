using UnityEngine;
using System.Collections;

public class Coin_Global : MonoBehaviour {


	tk2dTextMesh text;

	void Awake(){
		text = GetComponent<tk2dTextMesh>();

	}
	void LateUpdate(){
		text.text = dataManger.manager.coins.ToString();
	}

}
