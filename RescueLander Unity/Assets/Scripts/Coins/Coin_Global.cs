using UnityEngine;
using System.Collections;

public class Coin_Global : MonoBehaviour {


	tk2dTextMesh text;

	void Awake(){
		text = GetComponent<tk2dTextMesh>();

	}
	IEnumerator Start () {
		while (true) {
			text.text = dataManger.manager.coins.ToString();
			yield return new WaitForSeconds(1f);
		}
	}


}
