using UnityEngine;
using System.Collections;

public class Play_anim : MonoBehaviour
{
	public float amplitude = 1.0f;
	public float omega = 1.0f;
	float index;
	
	public void Update()
	{
		index += Time.deltaTime;
		float x = 0;

		x = amplitude * Mathf.Cos (omega * index);
		if (x < 0)
			x *= -1;
		if (x < 1)
			x += 0.5f;

		transform.localScale = new Vector3(x,x,0);
	}
}
