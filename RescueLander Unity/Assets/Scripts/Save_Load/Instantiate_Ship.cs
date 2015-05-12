using UnityEngine;
using System.Collections;

public class Instantiate_Ship : MonoBehaviour {


	void Awake () {
		GameObject instance = Instantiate(Resources.Load("Prefabs/"+dataManger.manager.actualShip, typeof(GameObject))) as GameObject;
		SpriteColorFX.SpriteColorTintMask3 tint = instance.GetComponent<SpriteColorFX.SpriteColorTintMask3>();
		tint.textureMask = Resources.Load("Sprites/"+dataManger.manager.actualShip, typeof(Texture2D)) as Texture2D;
		instance.transform.position = new Vector3(transform.position.x,transform.position.y + 52, transform.position.z - 50);
		Debug.Log("Ship instantiated");
	}
	

}
