using UnityEngine;
using System.Collections;

public class TextAstronaut : MonoBehaviour {

	[HideInInspector]
	public int pickedAstronauts = 0;
	[HideInInspector]
	public int dropedAstronauts = 0;
	private int percentaje;
	private tk2dTextMesh text;

	void Awake () {

		text = GetComponent<tk2dTextMesh>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateText(){

		percentaje = (int)(dropedAstronauts * 100) / pickedAstronauts;
		text.text = "Picked astronauts: " + pickedAstronauts + "  Dropped astronuts: " + dropedAstronauts +" "+ percentaje + " precent saved";
		text.Commit();

	}
}
