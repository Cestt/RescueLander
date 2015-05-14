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

		Debug.Log ("Found " + allChildren.Length + " Children");
		foreach (Color_Enabled child in allChildren) {


			if(child.StarsRequired <= dataManger.manager.totalStars){
				child.enabled = true;
				//Transform tempGrandchild = child.FindChild("Requisite");
				//tempGrandchild.gameObject.SetActive(false);
			}
		}
	}
	

}
