using UnityEngine;
using System.Collections;

public class Coin_Move : MonoBehaviour {
	private GameObject ship;
	public int speed;
	[HideInInspector]
	public bool Chase = false;
	void Awake(){
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
	}

	void Update () {
		if (Chase) {
			transform.position = Vector3.MoveTowards(transform.position,ship.transform.position,speed);
		}
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.name == ship.name) {
			dataManger.manager.coins += 1;
			Destroy(gameObject);
		}
	}
}
