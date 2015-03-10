using UnityEngine;
using System.Collections;

public class Start_Anim : MonoBehaviour
{

	public float amplitudeX = 10.0f;
	public float amplitudeY = 5.0f;
	public float omegaX = 1.0f;
	public float omegaY = 5.0f;
	float index;
	
	public void Update()
	{
		index += Time.deltaTime;
		float x = amplitudeX*Mathf.Cos (omegaX*index);
		float y = amplitudeY*Mathf.Sin (omegaY*index);
		transform.localPosition = new Vector3(x,y,0);

		float angle = Mathf.Atan2(x,y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
