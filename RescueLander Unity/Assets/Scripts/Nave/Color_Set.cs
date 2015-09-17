using UnityEngine;
using System.Collections;


public class Color_Set : MonoBehaviour {
	private SpriteColorFX.SpriteColorMasks3 tint;
	private GameObject ShipGaraje;
	private GameObject ShipGaraje2;
	private GameObject ShipMenu;
	private SpriteColorFX.SpriteColorMasks3 tintGaraje;

	void OnLevelWasLoaded(int level){
		if (Application.loadedLevelName == "Menu") {
			if (dataManger.manager.color1b < 1000) {
				SpriteColorFX.SpriteColorMasks3 tintMask;
				ShipMenu = GameObject.Find("Ship_Anim").transform.FindChild("Ship01").gameObject;
				tintMask = 
					ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
				Color32[] temp = dataManger.manager.colorDictionary[dataManger.manager.actualShip];
				tintMask.colorMaskRed = temp[0];
				tintMask.colorMaskGreen = temp[1];
			}
		}
	}
	void Awake () {

		GameObject uicamera = GameObject.Find("UI_Camera");
		ShipGaraje = uicamera.transform.FindChild ("Garage_Menu/Canvas/Shop_Bg_01/Paint_Menu/TV/Ship01_Garage").gameObject;
		ShipGaraje2 = uicamera.transform.FindChild ("Garage_Menu/Canvas/Shop_Bg_01/Ships_Menu/TV/Ship01_Garage").gameObject;
		tintGaraje = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3> ();

		if (dataManger.manager.color1b < 1000) {
			tintGaraje.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Sprites/" + dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
			tintGaraje.textureMask = Resources.Load ("Sprites/" + dataManger.manager.actualShip + "_Shader", typeof(Texture2D)) as Texture2D;
			tintGaraje.colorMaskRed = new Color (dataManger.manager.color1r, dataManger.manager.color1g, dataManger.manager.color1b);
			tintGaraje.colorMaskGreen = new Color (dataManger.manager.color2r, dataManger.manager.color2g, dataManger.manager.color2b);
		}
		if (Application.loadedLevelName == "Menu") {
			if (dataManger.manager.color1b < 1000) {
			SpriteColorFX.SpriteColorMasks3 tintMask;
			ShipMenu = GameObject.Find("Ship_Anim").transform.FindChild("Ship01").gameObject;

			tintMask = 
				ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
			tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
			Color32[] temp = dataManger.manager.colorDictionary[dataManger.manager.actualShip];
			tintMask.colorMaskRed = temp[0];
			tintMask.colorMaskGreen = temp[1];
			}
		}
		SpriteSet (true,"");
	}
	
	public void ColorSet(Color color,string zone){

		if (zone == "A") {
			
			dataManger.manager.color1r = color.r;
			dataManger.manager.color1g = color.g;
			dataManger.manager.color1b = color.b;	
			if (Application.loadedLevelName != "Menu") {
				tint.colorMaskRed = color;
			}


		}
		if (zone == "B") {
			
			dataManger.manager.color2r = color.r;
			dataManger.manager.color2g = color.g;
			dataManger.manager.color2b = color.b;
			if (Application.loadedLevelName != "Menu") {
				tint.colorMaskGreen = color;
			}


		}
		SpriteColorFX.SpriteColorMasks3 tintMask;
		ShipMenu = GameObject.Find("Ship_Anim").transform.FindChild("Ship01").gameObject;
		tintMask = 
			ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
		tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
		tintMask.colorMaskRed = new Color (dataManger.manager.color1r, dataManger.manager.color1g, dataManger.manager.color1b);
		tintMask.colorMaskGreen = new Color (dataManger.manager.color2r, dataManger.manager.color2g, dataManger.manager.color2b);
		dataManger.manager.colorDictionary[dataManger.manager.actualShip] = new Color32[]{(Color32) tintMask.colorMaskRed,
			(Color32) tintMask.colorMaskGreen};
		Color32[] temp = dataManger.manager.colorDictionary[dataManger.manager.actualShip];
		dataManger.manager.Save(true);
	}

	public void SpriteSet(bool complete,string shipChange){
		SpriteColorFX.SpriteColorMasks3 tintMask;

		if(complete){
				ShipGaraje2.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
				ShipGaraje.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
				ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>().textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;

			if(dataManger.manager.color1r == 0 & dataManger.manager.color1g == 0 & dataManger.manager.color1b == 0){
				switch(dataManger.manager.actualShip){
				case "Ship01" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(1,1,1,255);
					tintMask.colorMaskGreen = new Color32(197,0,0,255);
					break;
				case "369" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(207,207,207,255);
					tintMask.colorMaskGreen = new Color32(106,161,185,255);
					break;
				case "Taboo" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(247,233,32,255);
					tintMask.colorMaskGreen = new Color32(255,127,0,255);
					break;
				case "UFLO" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(147,104,181,255);
					tintMask.colorMaskGreen = new Color32(255,127,0,255);
					break;
				case "Box" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(198,156,109,255);
					tintMask.colorMaskGreen = new Color32(247,49,56,255);
					
					break;
				case "Mush" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(184,154,121,255);
					tintMask.colorMaskGreen = new Color32(255,0,0,255);
					
					break;
				case "Bow" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(147,104,181,255);
					tintMask.colorMaskGreen = new Color32(255,127,0,255);
					
					break;
				case "Big" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(124,124,124,124);
					tintMask.colorMaskGreen = new Color32(124,124,124,124);
					break;
				case "Jupitar" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(0,0,255,255);
					tintMask.colorMaskGreen = new Color32(255,255,255,255);
					break;
				case "Evolve" :
					tintMask = 
						ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(204,204,204,255);
					tintMask.colorMaskGreen = new Color32(0,0,0,255);
					break;
				}
				
			}else{
				Color32[] temp = dataManger.manager.colorDictionary[dataManger.manager.actualShip];
				tintMask = 
					ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.colorMaskRed = temp[0];
				tintMask.colorMaskGreen = temp[1];
				dataManger.manager.color1r = temp[0].r;
				dataManger.manager.color1g = temp[0].g;
				dataManger.manager.color1b = temp[0].b;
				dataManger.manager.color2r = temp[1].r;
				dataManger.manager.color2g = temp[1].g;
				dataManger.manager.color2b = temp[1].b;
			
				switch(dataManger.manager.actualShip){
				case "Ship01" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;

					break;
				case "369" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;

					break;
				case "Taboo" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;

					break;
				case "UFLO" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
			
					break;
				case "Box" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					
					break;
				case "Mush" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;

					break;
				case "Bow" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					
					break;
				case "Big" :
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;

					break;
				case "Jupitar" :
					tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;

					break;
				case "Evolve" :
					tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;

					break;
				}
			}
			if(Application.loadedLevelName == "Menu"){
				
				ShipMenu.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip, typeof(Sprite)) as Sprite;
				ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>().textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
				if(dataManger.manager.color1r == 0 & dataManger.manager.color1g == 0 & dataManger.manager.color1b == 0){
					switch(dataManger.manager.actualShip){
					case "Ship01" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(249,176,1,255);
						tintMask.colorMaskGreen = new Color32(197,0,0,255);
						break;
					case "369" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(207,207,207,255);
						tintMask.colorMaskGreen = new Color32(106,161,185,255);
						break;
					case "Taboo" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(247,233,32,255);
						tintMask.colorMaskGreen = new Color32(255,127,0,255);
						break;
					case "UFLO" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(147,104,181,255);
						tintMask.colorMaskGreen = new Color32(255,127,0,255);
						break;
					case "Box" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(198,156,109,255);
						tintMask.colorMaskGreen = new Color32(247,49,56,255);
						
						break;
					case "Mush" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(184,154,121,255);
						tintMask.colorMaskGreen = new Color32(255,0,0,255);
						
						break;
					case "Bow" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(147,104,181,255);
						tintMask.colorMaskGreen = new Color32(255,127,0,255);
						
						break;
					case "Big" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(124,124,124,124);
						tintMask.colorMaskGreen = new Color32(124,124,124,124);
						break;
					case "Jupitar" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(0,0,255,255);
						tintMask.colorMaskGreen = new Color32(255,255,255,255);
						break;
					case "Evolve" :
						tintMask = 
							ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
						tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
						tintMask.colorMaskRed = new Color32(204,204,204,255);
						tintMask.colorMaskGreen = new Color32(0,0,0,255);
						break;
					}

				}else{
					tintMask = 
						ShipMenu.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;

					Color32[] temp = dataManger.manager.colorDictionary[dataManger.manager.actualShip];
					tintMask.colorMaskRed = temp[0];
					tintMask.colorMaskGreen = temp[1];

				}
			}
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
					tintMask = 
						ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(198,156,109,255);
					tintMask.colorMaskGreen = new Color32(247,49,56,255);
					
					break;
				case "Mush" :
					tintMask = 
						ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(184,154,121,255);
					tintMask.colorMaskGreen = new Color32(255,0,0,255);

					break;
				case "Bow" :
					tintMask = 
						ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
					tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
					tintMask.colorMaskRed = new Color32(147,104,181,255);
					tintMask.colorMaskGreen = new Color32(255,127,0,255);
					
					break;
			case "Big" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(124,124,124,124);
				tintMask.colorMaskGreen = new Color32(124,124,124,124);
				break;
			case "Jupitar" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(0,0,255,255);
				tintMask.colorMaskGreen = new Color32(255,255,255,255);
				break;
			case "Evolve" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(204,204,204,255);
				tintMask.colorMaskGreen = new Color32(0,0,0,255);
				break;
			}

		}else{
			ShipGaraje2.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+shipChange + "_High", typeof(Sprite)) as Sprite;
			
			switch(shipChange){
			case "Ship01" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(249,176,0,255);
				tintMask.colorMaskGreen = new Color32(197,0,0,255);
				break;
			case "369" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(207,207,207,255);
				tintMask.colorMaskGreen = new Color32(106,161,185,255);
				break;
			case "Taboo" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(247,233,32,255);
				tintMask.colorMaskGreen = new Color32(255,127,0,255);
				break;
			case "UFLO" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(147,104,181,255);
				tintMask.colorMaskGreen = new Color32(255,127,0,255);
				break;
			case "Box" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(198,156,109,255);
				tintMask.colorMaskGreen = new Color32(247,49,56,255);
				break;
			case "Mush" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(184,154,121,255);
				tintMask.colorMaskGreen = new Color32(255,0,0,255);
				break;
			case "Bow" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(147,104,181,255);
				tintMask.colorMaskGreen = new Color32(255,127,0,255);
				break;
			case "Big" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(124,124,124,255);
				tintMask.colorMaskGreen = new Color32(124,124,124,255);
				break;
			case "Jupitar" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(0,0,255,255);
				tintMask.colorMaskGreen = new Color32(255,255,255,255);
				break;
			case "Evolve" :
				tintMask = 
					ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>();
				tintMask.textureMask = Resources.Load("Sprites/"+shipChange+"_Shader_High", typeof(Texture2D)) as Texture2D;
				tintMask.colorMaskRed = new Color32(204,204,204,255);
				tintMask.colorMaskGreen = new Color32(0,0,0,255);
				break;
			}
		}

	}
}
