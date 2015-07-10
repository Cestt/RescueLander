using UnityEngine;
using System.Collections;

public class Patas : MonoBehaviour {

	Damage dmg;
	public int DamageReduction;
	void Awake(){
		dmg = GetComponentInParent<Damage>();
	}

	void OnTriggerEnter2D(Collider2D other) {

		dmg.dmgReduction = DamageReduction;

	}
	void OnTriggerExit2D(Collider2D other) {
		
		dmg.dmgReduction = 0;
		
	}
}
