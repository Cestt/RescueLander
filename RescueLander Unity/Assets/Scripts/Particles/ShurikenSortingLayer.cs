using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShurikenSortingLayer : MonoBehaviour
{
	public string SortingLayer;
	public int OrderInLayer = 0;
	
	public void Sort()
	{
		particleSystem.renderer.sortingLayerName = SortingLayer;
		particleSystem.renderer.sortingOrder = OrderInLayer;
	}
}