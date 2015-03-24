using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ShurikenSortingLayer))]
public class shurikenSortingEditor : Editor
{
	public ShurikenSortingLayer shurikenSortingLayer;
	
	public override void OnInspectorGUI()
	{
		
		
		DrawDefaultInspector();
		
		
		if(GUILayout.Button("Commit"))
		{
			shurikenSortingLayer.Sort();
			EditorUtility.SetDirty(shurikenSortingLayer);
			
		}
	}
	public void Awake(){
		shurikenSortingLayer = (ShurikenSortingLayer) target;
	}
}