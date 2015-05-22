using UnityEngine;
using System.Collections;

public class Ship_Unlock : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		foreach(Transform child in GetComponentsInChildren<Transform>()){
			if(child.parent == gameObject.transform){
				if(dataManger.manager.shipUnlocks.Contains(child.name.Substring(11))){
					Destroy(child.FindChild("Button_Buy").gameObject);
				}
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
