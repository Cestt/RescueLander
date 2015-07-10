using UnityEngine;
using System.Collections;
/// <summary>
/// Color_ unlock : Unlock garaje colors at load time.
/// </summary>
public class Color_Unlock : MonoBehaviour {

	/// <summary>
	/// Manuel Alirangues.
	/// </summary>
	void Awake () {
		Color_Enabled[] allChildren = GetComponentsInChildren<Color_Enabled>();


		foreach (Color_Enabled child in allChildren) {


			if(child.StarsRequired <= dataManger.manager.totalStars & child.transform.parent == gameObject.transform){
				child.enabled = true;
				Transform tempGrandchild = child.transform.FindChild("Color01_Graphic/Requisite");
				if(tempGrandchild != null){
					tempGrandchild.gameObject.SetActive(false);
				}

			}
		}
	}
	

}
