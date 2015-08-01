using UnityEngine;
using System.Collections;

public class Coin_Move : MonoBehaviour {
	private GameObject ship;
	public int speed;
	public bool Chase = false;
	private PowerUp_Manager powerManager;
	private Coin_Manager coin_manager;
	private int coinDistance = 65;
	public float CoinValue;
	private Sound_Manager soundManager;

	bool first;
	void Awake(){
		if (Application.loadedLevelName.Contains("Tuto"))
			ship = GameObject.Find("101(Clone)");
		else
			ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		powerManager = GameObject.Find("Game Manager").GetComponent<PowerUp_Manager>();
		coin_manager = GameObject.Find("ScoreCoin_Manager").GetComponent<Coin_Manager>();
		soundManager = GameObject.Find("Game Manager").GetComponent<Sound_Manager>();
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
				coin_manager.LevelCoin(CoinValue);
				soundManager.PlaySound("Coin");
				Destroy(gameObject);		
			}
			if(!powerManager.On & first){
				Chase = false;
				first = false;
			}
		}


	}

}
