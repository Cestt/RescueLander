using UnityEngine;
using System.Collections;


public class Scroll : MonoBehaviour {

	public float min;
	public float max;
	public float speed = 0.1F;
	public Color LineColor;
	void Update() {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(0, touchDeltaPosition.y * speed, 0);

			transform.position = new Vector2 (transform.position.x,Mathf.Clamp(transform.position.y,max,min));

		}
	}
	void OnDrawGizmos() {
		Gizmos.color = LineColor;
		Gizmos.DrawLine(new Vector2(transform.position.x - 200, transform.position.y + min),
		                new Vector2(transform.position.x + 200, transform.position.y + min));
		Gizmos.DrawLine(new Vector2(transform.position.x - 200, transform.position.y - max),
		                new Vector2(transform.position.x + 200, transform.position.y - max));
	}
}