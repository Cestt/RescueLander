using UnityEngine;
using System.Collections;

public class Instantiate_Ship : MonoBehaviour {


	void Awake () {
		GameObject instance = Instantiate(Resources.Load("Prefabs/"+dataManger.manager.actualShip, typeof(GameObject))) as GameObject;
		SpriteColorFX.SpriteColorTintMask3 tint = instance.GetComponent<SpriteColorFX.SpriteColorTintMask3>();
		tint.colorMaskRed.r = dataManger.manager.color1r;
		tint.colorMaskRed.g = dataManger.manager.color1g;
		tint.colorMaskRed.b = dataManger.manager.color1b;
		tint.colorMaskGreen.r = dataManger.manager.color2r;
		tint.colorMaskGreen.g = dataManger.manager.color2g;
		tint.colorMaskGreen.b = dataManger.manager.color2b;
		tint.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
		instance.transform.position = new Vector3(transform.position.x,transform.position.y + 52, transform.position.z - 50);
	}
	

}
