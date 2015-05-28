using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Garaje_Manager : MonoBehaviour {

	private GameObject Layouts;
	public List<Transform> LayoutChild = new List<Transform>();

	void Awake () {
		GameObject uicamera = GameObject.Find("UI_Camera");
		Transform Garage = uicamera.transform.FindChild("Garage_Menu");
		Layouts = Garage.FindChild("Shop_Bg_01").gameObject;
		foreach(Transform child in Layouts.transform){
			if(child.parent == Layouts.transform){
				Debug.Log ("Child Found");
				LayoutChild.Add(child);
			}
		}
	}
	
	public void LayoutChanger(string Layout){
		foreach(Transform currentLayout in LayoutChild){
			if(Layout != "Paint" & currentLayout.name == "Header"){
				Debug.Log("Change Name");
				currentLayout.GetComponentInChildren<tk2dTextMesh>().text = "Paint";
			}
			if(Layout == "Paint" & currentLayout.name == "Header" ){
				currentLayout.GetComponentInChildren<tk2dTextMesh>().text = "Garage";
			}
			if(currentLayout.name == Layout+"_Menu"){
				currentLayout.gameObject.SetActive(true);

			}else if(currentLayout.name != "Header" & currentLayout.name != "Exit_Button" & currentLayout.name != "CoinCount_Ico"
			         & currentLayout.name != "StarCount_Ico"){
				currentLayout.gameObject.SetActive(false);
			}
		}
	}
}
