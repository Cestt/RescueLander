using UnityEngine;
using System.Collections;


public class Scroll : MonoBehaviour {

	public float max;
	public float min;
	public float speed = 0.1F;
	public Color LineColor;
	void Update() {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(0, -touchDeltaPosition.y * speed, 0);

			transform.localPosition = new Vector2 (transform.localPosition.x,Mathf.Clamp(transform.localPosition.y,min,max));
		}
	}
	void OnDrawGizmos() {
		Gizmos.color = LineColor;
		Gizmos.DrawLine(new Vector2(transform.localPosition.x - 200, transform.localPosition.y + max),
		                new Vector2(transform.localPosition.x + 200, transform.localPosition.y + max));
		Gizmos.DrawLine(new Vector2(transform.localPosition.x - 200, transform.localPosition.y - min),
		                new Vector2(transform.localPosition.x + 200, transform.localPosition.y - min));
	}
}