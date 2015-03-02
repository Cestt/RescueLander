using UnityEngine;
using System.Collections;

public class ShipAstronautPickUp : MonoBehaviour {

	public tk2dTextMesh text;
	private TextAstronaut textastronaut;
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
		textastronaut = text.GetComponent<TextAstronaut>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Pickable = true & rigid.velocity.magnitude == 0 & Astronaut != null){


			Pickable = false;
			Destroy(Astronaut);
			Astronaut = null;
			astronautPicked++;
			dataManger.manager.pickedAstronauts += astronautPicked;
			dataManger.manager.Save();
			textastronaut.UpdateText();
			Debug.Log("Astronaut Picked");

		}


	
	}
}
