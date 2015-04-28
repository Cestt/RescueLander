using UnityEngine;
using System.Collections;

public class Halo_Anim : MonoBehaviour
{
	public bool win = false;
	void Update ()
	{
		if(win){
			transform.rotation = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude+0.5f,Vector3.forward);
		}
	}

}
