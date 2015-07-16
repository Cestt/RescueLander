using UnityEngine;
using System.Collections;

public class LifeBarStars : MonoBehaviour {

	Damage damage;
	// Use this for initialization
	tk2dSprite thirdStar;
	tk2dSprite secondStar;
	//public Sprite loseStar;

	void Awake () {
		damage = GameObject.Find (dataManger.manager.actualShip+"(Clone)").GetComponent<Damage>();
		thirdStar = transform.parent.FindChild ("StarLife_3").gameObject.GetComponent<tk2dSprite> ();
		secondStar = transform.parent.FindChild ("StarLife_2").gameObject.GetComponent<tk2dSprite> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (damage.life < (((float)damage.maxLife * 90) / 100f) && thirdStar.CurrentSprite.name != "Estrella_Lose") {
			thirdStar.SetSprite("Estrella_Lose");
			Debug.Log("Apaga tercerca ESTRELLA");
		}
		if (damage.life < (((float)damage.maxLife * 50) / 100f)) {
			secondStar.SetSprite("Estrella_Lose");
			Debug.Log ("Apaga segunda estrella");
			Destroy (this);
		}
	}
}
