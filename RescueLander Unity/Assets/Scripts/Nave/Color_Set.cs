﻿using UnityEngine;
using System.Collections;

public class Color_Set : MonoBehaviour {
	private SpriteColorFX.SpriteColorMasks3 tint;
	private GameObject ShipGaraje;
	private GameObject ShipGaraje2;
	private SpriteColorFX.SpriteColorMasks3 tintGaraje;

	void Awake () {
		GameObject uicamera = GameObject.Find("UI_Camera");
		ShipGaraje = uicamera.transform.FindChild ("Garage_Menu/Shop_Bg_01/Paint_Menu/TV/Ship01_Garage").gameObject;
		ShipGaraje2 = uicamera.transform.FindChild ("Garage_Menu/Shop_Bg_01/Ships_Menu/TV/Ship01_Garage").gameObject;
		tintGaraje = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3> ();
		if (Application.loadedLevelName != "Menu") {
			tint = GameObject.Find(dataManger.manager.actualShip + "(Clone)").GetComponent<SpriteColorFX.SpriteColorMasks3> ();
			if (dataManger.manager.color1b < 1000) {
				tint.colorMaskRed = new Color(dataManger.manager.color1r,dataManger.manager.color1g,dataManger.manager.color1b);
				tint.colorMaskGreen = new Color(dataManger.manager.color2r,dataManger.manager.color2g,dataManger.manager.color2b);
			}
		}
		if (dataManger.manager.color1b < 1000) {
			tintGaraje.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Sprites/" + dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
			tintGaraje.textureMask = Resources.Load ("Sprites/" + dataManger.manager.actualShip + "_Shader", typeof(Texture2D)) as Texture2D;
			tintGaraje.colorMaskRed = new Color (dataManger.manager.color1r, dataManger.manager.color1g, dataManger.manager.color1b);
			tintGaraje.colorMaskGreen = new Color (dataManger.manager.color2r, dataManger.manager.color2g, dataManger.manager.color2b);
		}	
		SpriteSet ();
	}
	
	public void ColorSet(Color color,string zone){

		if (zone == "A") {
			
			dataManger.manager.color1r = color.r;
			dataManger.manager.color1g = color.g;
			dataManger.manager.color1b = color.b;	
			if (Application.loadedLevelName != "Menu") {
				tint.colorMaskRed = color;
			}
			Debug.Log("Color Set");
			dataManger.manager.Save(true);
		}
		if (zone == "B") {
			
			dataManger.manager.color2r = color.r;
			dataManger.manager.color2g = color.g;
			dataManger.manager.color2b = color.b;
			if (Application.loadedLevelName != "Menu") {
				tint.colorMaskGreen = color;
			}
			Debug.Log("Color Set");
			dataManger.manager.Save(true);
		}

	}

	public void SpriteSet(){
		SpriteColorFX.SpriteColorMasks3 tintMask;

			ShipGaraje2.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
			ShipGaraje.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
			ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>().textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;

		switch(dataManger.manager.actualShip){
			case "Ship01" :
			tintMask = 
				ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
			tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
			tintMask.colorMaskRed = new Color32(249,176,0,255);
			tintMask.colorMaskGreen = new Color32(197,0,0,255);
				break;
			case "369" :
			tintMask = 
				ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
			tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
			tintMask.colorMaskRed = new Color32(207,207,207,255);
			tintMask.colorMaskGreen = new Color32(106,161,185,255);
				break;
			case "Taboo" :
			tintMask = 
				ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
			tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
			tintMask.colorMaskRed = new Color32(247,233,32,255);
			tintMask.colorMaskGreen = new Color32(255,127,0,255);
				break;
			case "UFLO" :
			tintMask = 
				ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
			tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
			tintMask.colorMaskRed = new Color32(147,104,181,255);
			tintMask.colorMaskGreen = new Color32(255,127,0,255);
				break;
			case "Box" :
				
				break;
			case "Mush" :
				
				break;
			case "Bow" :
				
				break;
		}

	}
}
