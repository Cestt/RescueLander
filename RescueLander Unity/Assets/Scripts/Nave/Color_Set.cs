using UnityEngine;
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
		SpriteSet ("Paint");
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

	public void SpriteSet(string Part){
		if (Part == "Paint") {

		} else {
			ShipGaraje2.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
			ShipGaraje2.GetComponent<SpriteColorFX.SpriteColorMasks3>().textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
			ShipGaraje.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip + "_High", typeof(Sprite)) as Sprite;
			ShipGaraje.GetComponent<SpriteColorFX.SpriteColorMasks3>().textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
		}

	}
}
