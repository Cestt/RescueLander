using UnityEngine;
using System.Collections;

public class Menu_ship : MonoBehaviour {


	void Awake () {
		GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/"+dataManger.manager.actualShip, typeof(Sprite)) as Sprite;
		SpriteColorFX.SpriteColorTintMask3 tint = GetComponent<SpriteColorFX.SpriteColorTintMask3>();
		tint.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
		tint.colorMaskRed.r = dataManger.manager.color1r;
		tint.colorMaskRed.g = dataManger.manager.color1g;
		tint.colorMaskRed.b = dataManger.manager.color1b;
		tint.colorMaskGreen.r = dataManger.manager.color2r;
		tint.colorMaskGreen.g = dataManger.manager.color2g;
		tint.colorMaskGreen.b = dataManger.manager.color2b;
	}
	

}
