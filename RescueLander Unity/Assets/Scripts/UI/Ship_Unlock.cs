using UnityEngine;
using System.Collections;

public class Ship_Unlock : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		foreach(Transform child in GetComponentsInChildren<Transform>()){
			if(child.parent == gameObject.transform){
				for(int i = 0; i < dataManger.manager.shipUnlocks.Count; i++){
					if(dataManger.manager.shipUnlocks[i] == child.name.Substring(11)){
						Destroy(child.FindChild("Button_Buy").gameObject);
					}
					Debug.Log("Ships count " + dataManger.manager.shipUnlocks[0]);
				}
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
