using UnityEngine;
using System.Collections;

public class Coin_Move : MonoBehaviour {
	private GameObject ship;
	public int speed;
	public bool Chase = false;
	private PowerUp_Manager powerManager;
	private int coinDistance = 65;
	public int CoinValue;
	bool first;
	void Awake(){
		ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		powerManager = GameObject.Find("Game Manager").GetComponent<PowerUp_Manager>();
	}

	void Update () {
		if (Chase) {
			transform.position = Vector3.MoveTowards(transform.position,ship.transform.position,speed*Time.deltaTime);
		}
		if (ship != null) {
			if(powerManager.On & Vector2.Distance (transform.position, ship.transform.position) < 200){
				Chase = true;
				first = true;

			}
			if (Vector2.Distance (transform.position, ship.transform.position) < coinDistance ) {
				dataManger.manager.coins += CoinValue;
				Destroy(gameObject);		
			}
			if(!powerManager.On & first){
				Chase = false;
				first = false;
			}
		}


	}

}
