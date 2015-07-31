using UnityEngine;
using System.Collections;

public class Instantiate_Ship : MonoBehaviour {


	void Awake () {

		if(Application.loadedLevelName.Contains("Tuto")){
			GameObject instance = Instantiate(Resources.Load("Prefabs/101", typeof(GameObject))) as GameObject;
			instance.transform.position = new Vector3(transform.position.x - 7,transform.position.y + 52, transform.position.z - 50);
			switch(Application.loadedLevelName){
			case "Tuto_1":
				instance.GetComponent<ShipAstronautDrop>().totalAstronauts = 0;
				break;
			case "Tuto_2":
				instance.GetComponent<ShipAstronautDrop>().totalAstronauts = 1;
				break;
			case "Tuto_3":
				instance.GetComponent<ShipAstronautDrop>().totalAstronauts = 1;
				break;
			}
		}else{
			GameObject instance = Instantiate(Resources.Load("Prefabs/"+dataManger.manager.actualShip, typeof(GameObject))) as GameObject;
			SpriteColorFX.SpriteColorMasks3 tintMask = instance.GetComponent<SpriteColorFX.SpriteColorMasks3>();
			tintMask.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip+"_Shader", typeof(Texture2D)) as Texture2D;
			if(dataManger.manager.color1r == 0 & dataManger.manager.color1g == 0 & dataManger.manager.color1b == 0){
				
				switch(dataManger.manager.actualShip){
				case "Ship01" :
					tintMask.colorMaskRed = new Color32(249,176,0,255);
					tintMask.colorMaskGreen = new Color32(197,0,0,255);
					break;
				case "369" :
					
					tintMask.colorMaskRed = new Color32(207,207,207,255);
					tintMask.colorMaskGreen = new Color32(106,161,185,255);
					break;
				case "Taboo" :
					tintMask.colorMaskRed = new Color32(247,233,32,255);
					tintMask.colorMaskGreen = new Color32(255,127,0,255);
					break;
				case "UFLO" :
					tintMask.colorMaskRed = new Color32(147,104,181,255);
					tintMask.colorMaskGreen = new Color32(255,127,0,255);
					break;
				case "Box" :
					tintMask.colorMaskRed = new Color32(198,156,109,255);
					tintMask.colorMaskGreen = new Color32(247,49,56,255);
					
					break;
				case "Mush" :
					tintMask.colorMaskRed = new Color32(184,154,121,255);
					tintMask.colorMaskGreen = new Color32(255,0,0,255);
					
					break;
				case "Bow" :
					tintMask.colorMaskRed = new Color32(147,104,181,255);
					tintMask.colorMaskGreen = new Color32(255,127,0,255);
					
					break;
				}
			}else{
				tintMask.colorMaskRed.r = dataManger.manager.color1r;
				tintMask.colorMaskRed.g = dataManger.manager.color1g;
				tintMask.colorMaskRed.b = dataManger.manager.color1b;
				tintMask.colorMaskGreen.r = dataManger.manager.color2r;
				tintMask.colorMaskGreen.g = dataManger.manager.color2g;
				tintMask.colorMaskGreen.b = dataManger.manager.color2b;
			}
			instance.transform.position = new Vector3(transform.position.x - 7,transform.position.y + 52, transform.position.z - 50);
		}




		Debug.Log("Ship instantiated");
	}
	

}
