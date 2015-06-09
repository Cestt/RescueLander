using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minimap : MonoBehaviour {

	private const int MapHeight = 1080;
	public int MapWidth;
	private BoxCollider2D coll;
	public List<GameObject> Astronauts = new List<GameObject>();
	public List<GameObject> AstronautsIco = new List<GameObject>();
	private GameObject ship;
	public GameObject Ship_Ico;
	private GameObject Platform;
	private GameObject Platform_Ico;

	// Use this for initialization
	void Awake () {
		Platform = GameObject.Find("Landing Platform");
		Platform_Ico = transform.FindChild("Minimap_PlatformIco").gameObject;
		if(Application.loadedLevelName.Contains("Tuto")){
			ship = GameObject.Find("101(Clone)");
		}else{
			ship = GameObject.Find(dataManger.manager.actualShip + "(Clone)");

		}
		coll = GetComponent<BoxCollider2D>();
		for (int i = 1; i<= ship.GetComponent<ShipAstronautDrop>().totalAstronauts; i++) {
			Astronauts[i-1]= GameObject.Find("Astronaut_0"+i);
		}

	}
	void Start () {
		float mapx;
		float mapy;
		Vector2 tempVector;
		mapx = (100 * Platform.transform.position.x)/MapWidth;
		mapy = (100 * Platform.transform.position.y)/MapHeight;
		tempVector.x = transform.position.x + ((coll.size.x * mapx)/100);
		tempVector.y = transform.position.y + ((coll.size.y * mapy)/100);
		Platform_Ico.transform.position = tempVector; 
		if(Astronauts[0] != null){
			for(int i = 0; i < Astronauts.Count;i++) {
				
				mapx = (100 * Astronauts[i].transform.position.x)/MapWidth;
				mapy = (100 * Astronauts[i].transform.position.y)/MapHeight;
				tempVector = gameObject.transform.position;
				tempVector.x = transform.position.x + ((coll.size.x * mapx)/100);
				tempVector.y = transform.position.y + ((coll.size.y * mapy)/100);
				AstronautsIco[i].transform.position = tempVector; 
				
			}
		}


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Astronauts[0] == null){
			Destroy(AstronautsIco[0]);
		}
		if(Astronauts[1] == null){
			Destroy(AstronautsIco[1]);
		}
		if(Astronauts[2] == null){
			Destroy(AstronautsIco[2]);
		}
		float mapx;
		float mapy;
		Vector2 tempVector;

		if(ship.GetComponent<Renderer>().isVisible){
			Ship_Ico.SetActive(true);
		}else{
			Ship_Ico.SetActive(false);
		}

		mapx = (100 * ship.transform.position.x)/MapWidth;
		mapy = (100 * ship.transform.position.y)/MapHeight;
		tempVector = gameObject.transform.position;
		tempVector.x = transform.position.x + ((coll.size.x * mapx)/100);
		tempVector.y = transform.position.y + ((coll.size.y * mapy)/100);
		Ship_Ico.transform.position = tempVector; 
	}
}
