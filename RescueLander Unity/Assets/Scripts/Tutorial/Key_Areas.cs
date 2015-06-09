using UnityEngine;
using System.Collections;

public class Key_Areas : MonoBehaviour {

	private float actualTime;
	public float TimerTime;
	private bool running;
	private GameObject ship;
	private Tuto_Behaviour tuto;


	void Awake(){
		ship = GameObject.Find("101(Clone)");
		tuto = GameObject.Find("UI_Camera").GetComponent<Tuto_Behaviour>();
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(tuto.step == 7){
			running = true;
			StartTimer();
		}
		if(tuto.step == 9){
			running = true;
			StartTimer();
		}

	}
	void OnTriggerExit2D(Collider2D coll) {

			running = false;
		
	}

	void StartTimer(){
		actualTime = Time.time;
	}
	void Update(){
		if(actualTime + TimerTime < Time.time & running & tuto.step == 7){
			ship.GetComponent<Rigidbody2D>().isKinematic = true;
			running = false;
			tuto.step++;
			tuto.first = true;

		}
		if(actualTime + TimerTime < Time.time & running & tuto.step == 9){
			ship.GetComponent<Rigidbody2D>().isKinematic = true;
			running = false;
			GameObject.Find("Game Manager").GetComponent<WinLose>().End("Win");
				tuto.tuto.SetActive(false);
			tuto.step++;
			tuto.first = true;
			
		}
	}
}
