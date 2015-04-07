using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Movement))]
public class balanceeditor : Editor
{
	public Movement BalanceScript;
	
	public override void OnInspectorGUI()
	{


		DrawDefaultInspector();
		

		if(GUILayout.Button("Javi pulsa aqui"))
		{

			BalanceScript.BalanceCalc();
			EditorUtility.SetDirty(BalanceScript);

		}
	}
	public void Awake(){
		BalanceScript = (Movement) target;
	}
}
