using UnityEngine;
using System.Collections;

public class distanceScript : MonoBehaviour {

	private Positionarrow positionarrow;
	private tk2dTextMesh text;

	void Awake () {
	
		positionarrow = this.GetComponentInParent<Positionarrow>();
		text = this.GetComponent<tk2dTextMesh>();

	}
	
	// Update is called once per frame
	void Update () {

		if(!positionarrow.visible){

			float dist = (int) Vector2.Distance(positionarrow.ship.transform.position, this.transform.parent.transform.position)/10;
			
			text.text = dist.ToString();

		} 

	
	}
}
