using UnityEngine;
using System.Collections;

public class ShipAstronautDrop : MonoBehaviour {


	private Rigidbody2D rigid;
	private ShipAstronautPickUp shipastronautpickup;
	private int astronautsDroped;
	public int totalAstronauts;
	private GameObject gameManager;
	private WinLose winLose;


	void Awake () {
		gameManager = GameObject.Find("Game Manager");
		rigid = this.GetComponent<Rigidbody2D>();
		shipastronautpickup = this.GetComponent<ShipAstronautPickUp>();
		winLose = gameManager.GetComponent<WinLose> ();
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D coll) {

		if (coll.gameObject.name == "Landing Platform"){
			
			if(rigid.velocity.magnitude == 0 & shipastronautpickup.astronautPicked != 0 & astronautsDroped < totalAstronauts){



				//shipastronautpickup.astronautPicked = 0;
				//dataManger.manager.Save();
				Debug.Log("Astronaut dropped "+ astronautsDroped);
				if(Application.loadedLevelName.Contains("Tuto")){
					GameObject.Find("UI_Camera").GetComponent<Tuto_Behaviour>().step++;

				}
				
			}

			if(rigid.velocity.magnitude == 0 & shipastronautpickup.astronautPicked >= totalAstronauts & (gameObject.transform.eulerAngles.magnitude < 50 || gameObject.transform.eulerAngles.magnitude > 320) & Application.loadedLevelName != "Tuto_1" & Application.loadedLevelName != "Tuto_2"){

				winLose.End("Win",false);
			}
			
		}
		
	}
}
