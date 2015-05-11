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
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		Color_Enabled tempColorEnable;

		foreach (Transform child in allChildren) {
			tempColorEnable =  child.gameObject.GetComponent<Color_Enabled>();

			if(tempColorEnable.StarsRequired <= dataManger.manager.totalStars){
				tempColorEnable.enabled = true;
				//Transform tempGrandchild = child.FindChild("Requisite");
				//tempGrandchild.gameObject.SetActive(false);
			}
		}
	}
	

}
