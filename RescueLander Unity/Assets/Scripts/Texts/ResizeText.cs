using UnityEngine;
using System.Collections;

public class ResizeText : MonoBehaviour {

	float widthIni;
	float heightIni;
	tk2dTextMesh textMesh;
	public bool button;
	
	void Start (){
		textMesh = GetComponent<tk2dTextMesh>();
		widthIni = textMesh.GetEstimatedMeshBoundsForString(textMesh.text).size.x;
		heightIni = textMesh.GetEstimatedMeshBoundsForString(textMesh.text).size.y;
	}
	
	
	public void ChangeText(string newText){
		float newWidth = textMesh.GetEstimatedMeshBoundsForString(newText).size.x;
		float percentX = widthIni/newWidth;
		if (button){
			if (percentX < 1){
				textMesh.scale = new Vector3(textMesh.scale.x*percentX,textMesh.scale.y*percentX,1);
			}
		}else{
			float newHeight = textMesh.GetEstimatedMeshBoundsForString(newText).size.y;
			float percentY = heightIni/newHeight;
			if (percentX > 1)
				percentX = 1;
			if (percentY > 1)
				percentY = 1;
			textMesh.scale = new Vector3(textMesh.scale.x*percentX,textMesh.scale.y*percentY,1);
		}
		textMesh.text = newText;
	}
}
