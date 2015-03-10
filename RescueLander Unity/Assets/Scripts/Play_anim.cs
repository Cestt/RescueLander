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

		if(x > 0)
		{
			x = amplitude * Mathf.Cos (omega * index);
		}
		transform.localScale = new Vector3(Mathf.Clamp(x, 0.5f, 1.5f),Mathf.Clamp(x, 0.5f, 1.5f),0);
	}
}
