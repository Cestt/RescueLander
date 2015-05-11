using UnityEngine;
using System.Collections;

public class Color_Set : MonoBehaviour {

	public GameObject ShipGaraje;
	private SpriteColorFX.SpriteColorTintMask3 tint;
	private SpriteColorFX.SpriteColorTintMask3 tintGaraje;

	void Awake () {
		tintGaraje = ShipGaraje.GetComponent<SpriteColorFX.SpriteColorTintMask3> ();
		tint = GetComponent<SpriteColorFX.SpriteColorTintMask3> ();
		if (dataManger.manager.color1b != null) {
			tint.colorMaskRed = new Color(dataManger.manager.color1r,dataManger.manager.color1g,dataManger.manager.color1b);
			tint.colorMaskGreen = new Color(dataManger.manager.color2r,dataManger.manager.color2g,dataManger.manager.color2b);
			tintGaraje.colorMaskRed = new Color(dataManger.manager.color1r,dataManger.manager.color1g,dataManger.manager.color1b);
			tintGaraje.colorMaskGreen = new Color(dataManger.manager.color2r,dataManger.manager.color2g,dataManger.manager.color2b);
		}
	}
	
	public void ColorSet(Color color,string zone){
		if (zone == "A") {
			
			dataManger.manager.color1r = color.r;
			dataManger.manager.color1g = color.g;
			dataManger.manager.color1b = color.b;	
			tint.colorMaskRed = color;
			dataManger.manager.Save(true);
		}
		if (zone == "B") {
			
			dataManger.manager.color2r = color.r;
			dataManger.manager.color2g = color.g;
			dataManger.manager.color2b = color.b;
			tint.colorMaskGreen = color;
			dataManger.manager.Save(true);
		}

	}
}
