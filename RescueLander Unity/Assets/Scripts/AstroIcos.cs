using UnityEngine;
using System.Collections;

public class AstroIcos : MonoBehaviour
{
	public GameObject astroIco1;
	public GameObject astroIco2;
	public GameObject astroIco3;

	ShipAstronautPickUp shipAstronautPickUp;
	// Use this for initialization
	void Awake ()
	{
		shipAstronautPickUp = GameObject.Find ("Ship").GetComponent<ShipAstronautPickUp> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (shipAstronautPickUp.astronautPicked)
		{
			case 1:
				astroIco1.GetComponent<tk2dSprite>().SetSprite("Astronaut_Ico");
				break;
			case 2:
				astroIco2.GetComponent<tk2dSprite>().SetSprite("Astronaut_Ico");
				break;
			case 3:
				astroIco3.GetComponent<tk2dSprite>().SetSprite("Astronaut_Ico");
				break;
		}
	}
}
