using UnityEngine;
using System.Collections;


public class WinHalo_Anim : MonoBehaviour
{
	[HideInInspector]
	public bool Win;
	void Update ()
	{
		if(Win){
			transform.rotation = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude+0.5f,Vector3.forward);
		}

	}
}
