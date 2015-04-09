using UnityEngine;
using System.Collections;

public class ShipAstronautDrop : MonoBehaviour {


	private Rigidbody2D rigid;
	private ShipAstronautPickUp shipastronautpickup;
	private int astronautsDroped;
	public int totalAstronauts;
	public GameObject gameManager;
	private ScoreManager scoreManager;
	private WinLose winLose;

	void Awake () {

		rigid = this.rigidbody2D;
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		scoreManager = gameManager.GetComponent<ScoreManager> ();
		winLose = gameManager.GetComponent<WinLose> ();
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D coll) {

		if (coll.gameObject.name == "Landing Platform"){
			
			if(rigid.velocity.magnitude == 0 & shipastronautpickup.astronautPicked != 0){


				astronautsDroped += shipastronautpickup.astronautPicked;
				shipastronautpickup.astronautPicked = 0;
				//dataManger.manager.Save();
				Debug.Log("Astronaut dropped "+ astronautsDroped);
				
			}

			if(astronautsDroped >= totalAstronauts){

				winLose.End("Win");
			}
			
		}
		
	}
}
