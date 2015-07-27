using UnityEngine;
using System.Collections;

public class IceCube : MonoBehaviour {

	tk2dSpriteAnimator animatorAstronaut;
	Movement move;
	public float timeCube = 3; 
	GameObject ship;
	GameObject[] ice = new GameObject[4];
	float timeChange;
	float timeTotal;
	int iceAct;
	AstronautPickUp pickUp;
	float distIni;
	void Awake () {
		animatorAstronaut = transform.parent.GetComponent<tk2dSpriteAnimator> ();
		ship = GameObject.Find (dataManger.manager.actualShip + "(Clone)");
		move = ship.GetComponent<Movement> ();
		ice [0] = transform.FindChild ("IceBlock_00").gameObject;
		ice [1] = transform.FindChild ("IceBlock_01").gameObject;
		ice [2] = transform.FindChild ("IceBlock_02").gameObject;
		ice [3] = transform.FindChild ("IceBlock_03").gameObject;
		timeChange = timeCube / 4f;
		timeTotal = 0;
		iceAct = 0;
		pickUp = transform.parent.GetComponent<AstronautPickUp> ();
		distIni = pickUp.pickUpDistance;
		pickUp.pickUpDistance = -1;
	//	Debug.Log ("TIEMPO CAMBIO: "+ timeChange);
	}
	
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.name == "Feet" & (ship.gameObject.transform.eulerAngles.magnitude < 50 || ship.gameObject.transform.eulerAngles.magnitude > 310)){
			if (move.motor){
				timeTotal += Time.deltaTime;
				
				//Debug.Log ("TIEMPO RESTANTE: "+ timeTotal);
				if (timeTotal >= timeChange){
					//Debug.Log ("CAMBIO "+ iceAct);
					timeTotal -= timeChange;
					ice[iceAct].SetActive(false);
					iceAct++;
					if (iceAct < 4){
						ice[iceAct].SetActive(true);
					}else{
						pickUp.pickUpDistance = distIni;
						animatorAstronaut.Play ();
						gameObject.SetActive (false);
					}
				}
				/*timeCube -= Time.deltaTime;
				Debug.Log ("TIEMPO RESTANTE: "+ timeCube);
				if (timeCube <= 0){
					animatorAstronaut.Play ();
					gameObject.SetActive (false);
				}*/
			}
		}
	}
	/*
	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.name == "Feet" & animator.Playing) {
			animator.Pause ();
		}
	}*/
}
