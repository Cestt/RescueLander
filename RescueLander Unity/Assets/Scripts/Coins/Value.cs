using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Value : MonoBehaviour {

	public int Cost;
	[HideInInspector]
	public string _Type;
	[HideInInspector]
	public bool bought = false;

	public enum Type{
		PowerUp,Ship,World
	}

	public Type Tipo;

	void Awake(){
		if(Tipo == Type.PowerUp){
			_Type = "PowerUp";
		}
		if(Tipo == Type.Ship){
			_Type = "Ship";
		}
		if(Tipo == Type.World){
			_Type = "World";
		}
		if (_Type != "World") {
			tk2dTextMesh text = gameObject.transform.FindChild ("Button_Buy_Up/ButtonShip_Price").GetComponent<tk2dTextMesh> ();
			text.text = Cost.ToString ();
		}
	}

}
