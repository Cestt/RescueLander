using UnityEngine;
using System.Collections;

public class ShipSmoke : MonoBehaviour {

	public GameObject ship;
	private Movement movement;
	private ParticleSystem particlesystem;
	// Use this for initialization
	void Awake () {
	
		movement = ship.GetComponent<Movement>();
		particlesystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(movement.motor){
			particlesystem.enableEmission = true;
		}
		if(!movement.motor){
			particlesystem.enableEmission = false;
		}

	}

}
