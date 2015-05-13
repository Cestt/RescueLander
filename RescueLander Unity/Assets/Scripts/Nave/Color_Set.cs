using UnityEngine;
using System.Collections;

public class Color_Set : MonoBehaviour {

	public GameObject ShipGaraje;
	private SpriteColorFX.SpriteColorTintMask3 tint;
	private SpriteColorFX.SpriteColorTintMask3 tintGaraje;

	void Awake () {
		tintGaraje = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorTintMask3> ();
		if (Application.loadedLevelName != "Menu") {
			tint = GameObject.Find(dataManger.manager.actualShip + "(Clone)").GetComponent<SpriteColorFX.SpriteColorTintMask3> ();
			if (dataManger.manager.color1b < 1000) {
				tint.colorMaskRed = new Color(dataManger.manager.color1r,dataManger.manager.color1g,dataManger.manager.color1b);
				tint.colorMaskGreen = new Color(dataManger.manager.color2r,dataManger.manager.color2g,dataManger.manager.color2b);
			}
		}
		if (dataManger.manager.color1b < 1000) {
			tintGaraje.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
			tintGaraje.colorMaskRed = new Color(dataManger.manager.color1r,dataManger.manager.color1g,dataManger.manager.color1b);
			tintGaraje.colorMaskGreen = new Color(dataManger.manager.color2r,dataManger.manager.color2g,dataManger.manager.color2b);
		}
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
}
