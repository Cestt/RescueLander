﻿using UnityEngine;
using System.Collections;

public class WinHalo_Anim : MonoBehaviour
{
	void Update ()
	{
		transform.rotation = Quaternion.AngleAxis(gameObject.transform.eulerAngles.magnitude+0.5f,Vector3.forward);
	}
}