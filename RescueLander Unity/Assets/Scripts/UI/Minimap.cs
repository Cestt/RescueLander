using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minimap : MonoBehaviour {

	private const int MapHeight = 1080;
	public int MapWidth;
	private BoxCollider2D coll;
	public List<GameObject> Astronauts = new List<GameObject>();
	public List<GameObject> AstronautsIco = new List<GameObject>();
	private GameObject Ship;
	public GameObject Ship_Ico;

	// Use this for initialization
	void Awake () {
		Ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");
		coll = GetComponent<BoxCollider2D>();
		for (int i = 1; i<= Ship.GetComponent<ShipAstronautDrop>().totalAstronauts; i++) {
			Astronauts[i-1]= GameObject.Find("Astronaut_0"+i);
		}

	}
	void Start () {
		float mapx;
		float mapy;
		Vector2 tempVector;

		for(int i = 0; i < Astronauts.Count;i++) {

			mapx = (100 * Astronauts[i].transform.position.x)/MapWidth;
			mapy = (100 * Astronauts[i].transform.position.y)/MapHeight;
			tempVector = gameObject.transform.position;
			tempVector.x = transform.position.x + ((coll.size.x * mapx)/100);
			tempVector.y = transform.position.y + ((coll.size.y * mapy)/100);
			AstronautsIco[i].transform.position = tempVector; 

		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float mapx;
		float mapy;
		Vector2 tempVector;

		if(Ship.GetComponent<Renderer>().isVisible){
			Ship_Ico.SetActive(true);
		}else{
			Ship_Ico.SetActive(false);
		}

		mapx = (100 * Ship.transform.position.x)/MapWidth;
		mapy = (100 * Ship.transform.position.y)/MapHeight;
		tempVector = gameObject.transform.position;
		tempVector.x = transform.position.x + ((coll.size.x * mapx)/100);
		tempVector.y = transform.position.y + ((coll.size.y * mapy)/100);
		Ship_Ico.transform.position = tempVector; 
	}
}
