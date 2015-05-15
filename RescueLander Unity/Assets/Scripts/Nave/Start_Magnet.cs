using UnityEngine;
using System.Collections;

public class Start_Magnet : MonoBehaviour {
	public bool On;
	void OnTriggerEnter2D(Collider2D other) {
		if (On) {
			if(other.name == "Coin"){
				other.GetComponent<Coin_Move>().Chase = true;	
			}
		}
	}
}
