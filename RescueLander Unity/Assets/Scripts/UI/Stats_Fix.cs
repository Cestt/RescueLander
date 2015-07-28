using UnityEngine;
using System.Collections;

public class Stats_Fix : MonoBehaviour {

	private Vector3[] originalPosition;

	void Start(){
		originalPosition = new Vector3[10];
		originalPosition [0] = transform.FindChild("Button_Ship01/Stats").position;
		originalPosition [1] = transform.FindChild ("Button_Box/Stats").position;
		originalPosition [2] = transform.FindChild ("Button_369/Stats").position;
		originalPosition [3] = transform.FindChild ("Button_Big/Stats").position;
		originalPosition [4] = transform.FindChild ("Button_Taboo/Stats").position;
		originalPosition [5] = transform.FindChild ("Button_Jupitar/Stats").position;
		originalPosition [6] = transform.FindChild ("Button_Mush/Stats").position;
		originalPosition [7] = transform.FindChild ("Button_UFLO/Stats").position;
		originalPosition [8] = transform.FindChild ("Button_Evolve/Stats").position;
		originalPosition [9] = transform.FindChild ("Button_Bow/Stats").position;
	}
	// Update is called once per frame
	void Update () {
		transform.FindChild("Button_Ship01/Stats").position = originalPosition [0];  
		transform.FindChild ("Button_Box/Stats").position = originalPosition [1];  
		transform.FindChild ("Button_369/Stats").position = originalPosition [2];  
		transform.FindChild ("Button_Big/Stats").position = originalPosition [3];  
		transform.FindChild ("Button_Taboo/Stats").position = originalPosition [4];  
		transform.FindChild ("Button_Jupitar/Stats").position = originalPosition [5];  
		transform.FindChild ("Button_Mush/Stats").position = originalPosition [6];  
		transform.FindChild ("Button_UFLO/Stats").position = originalPosition [7];  
		transform.FindChild ("Button_Evolve/Stats").position = originalPosition [8];  
		transform.FindChild ("Button_Bow/Stats").position = originalPosition [9];  
	}
}
