using UnityEngine;
using System.Collections;

public class AstroIcos : MonoBehaviour
{
	public GameObject astroIco1;
	public GameObject astroIco2;
	public GameObject astroIco3;

	public GameObject astro1;
	public GameObject astro2;
	public GameObject astro3;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(astro1 != null && astro1.GetComponent<AstronautPickUp>().picked == true)
			astroIco1.GetComponent<tk2dSprite>().SetSprite("Astronaut_Ico");

		if(astro2 != null && astro2.GetComponent<AstronautPickUp>().picked == true)
			astroIco2.GetComponent<tk2dSprite>().SetSprite("Astronaut_Ico");

		if(astro3 != null && astro3.GetComponent<AstronautPickUp>().picked == true)
			astroIco3.GetComponent<tk2dSprite>().SetSprite("Astronaut_Ico");
	}
}
