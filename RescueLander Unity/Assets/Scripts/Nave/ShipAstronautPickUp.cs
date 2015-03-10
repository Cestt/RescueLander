using UnityEngine;
using System.Collections;

public class ShipAstronautPickUp : MonoBehaviour {


	[HideInInspector]
	public bool Pickable = false;
	[HideInInspector]
	public GameObject Astronaut;
	[HideInInspector]
	public int astronautPicked = 0;
	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody2D>();

	
	}
	
	// Update is called once per frame
	void Update () {

		if(Pickable = true & rigid.velocity.magnitude == 0 & Astronaut != null){


			Pickable = false;
			Destroy(Astronaut);
			Astronaut = null;
			astronautPicked++;
			Debug.Log("Astronaut Picked");

		}


	
	}
}
