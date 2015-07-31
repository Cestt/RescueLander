using UnityEngine;
using System.Collections;

public class Key_Areas : MonoBehaviour {

	private float actualTime;
	public float TimerTime = 3;
	private bool running;
	private GameObject ship;
	private Tuto_Behaviour0 tuto;
	private GameObject tutoGameobject;


	void Awake(){
		ship = GameObject.Find("101(Clone)");
		tuto = GameObject.Find("UI_Camera").GetComponent<Tuto_Behaviour0>();
		tutoGameobject = GameObject.Find("UI_Camera").transform.FindChild("Tutorial").gameObject;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(tuto.step == 3){
			running = true;
			StartTimer();
		}
		if(tuto.step == 5){
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
		if(actualTime + TimerTime < Time.time & running & tuto.step == 3){
			//ship.GetComponent<Rigidbody2D>().isKinematic = true;
			running = false;
			tuto.step++;
			tuto.first = true;
			//tuto.nextStep();
			tutoGameobject.SetActive(true);
			transform.localPosition = new Vector3(0,270,0);
		}
		if(actualTime + TimerTime < Time.time & running & tuto.step == 5){
			ship.GetComponent<Rigidbody2D>().isKinematic = true;
			running = false;
			GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu").gameObject.SetActive(true);
			GameObject.Find("UI_Camera").transform.FindChild("Prompt_Menu/Shop_Bg_01/Prompt_TutoReward_1").gameObject.SetActive(true);

				tuto.tuto.SetActive(false);
			tuto.step++;
			tuto.first = true;
			//tuto.nextStep();
			
		}
	}
}
