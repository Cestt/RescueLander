using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShurikenSortingLayer : MonoBehaviour
{
	public string SortingLayer;
	public int OrderInLayer = 0;
	
	public void Sort()
	{
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = SortingLayer;
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = OrderInLayer;
	}
}