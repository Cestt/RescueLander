///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEditor;
using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorMasks3 editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorMasks3))]
	public sealed class SpriteColorMasks3Editor : Editor
	{
    /// <summary>
    /// Help text.
    /// </summary>
    public string Help { get; set; }

    /// <summary>
    /// Warnings.
    /// </summary>
    public string Warnings { get; set; }

    /// <summary>
    /// Errors.
    /// </summary>
    public string Errors { get; set; }

		private SpriteColorMasks3 baseTarget;

		private bool[] foldoutsMasks = new bool[3];
		
    /// <summary>
    /// OnInspectorGUI.
    /// </summary>
    public override void OnInspectorGUI()
		{
			if (baseTarget == null)
				baseTarget = base.target as SpriteColorMasks3;

			EditorGUIUtility.LookLikeControls();
			
      EditorGUI.indentLevel = 0;

			EditorGUIUtility.labelWidth = 125.0f;

			EditorGUILayout.BeginVertical();
      {
			  EditorGUILayout.Separator();

			  EditorGUIUtility.fieldWidth = 40.0f;
			
        baseTarget.strength = (float)SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strength * 100.0f), 0, 100, 100) * 0.01f;

			  if (foldoutsMasks[0] = EditorGUILayout.Foldout(foldoutsMasks[0], @"#1 mask (red)"))
			  {
				  EditorGUI.indentLevel++;

				  baseTarget.strengthMaskRed = (float)SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strengthMaskRed * 100f), 0, 100, 100) * 0.01f;
				  
          baseTarget.pixelOpMaskRed = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Pixel op", @"Color pixel operation"), baseTarget.pixelOpMaskRed);
				  
          baseTarget.colorMaskRed = EditorGUILayout.ColorField(@"Color", baseTarget.colorMaskRed);
				  
          baseTarget.textureMaskRed = (EditorGUILayout.ObjectField(new GUIContent(@"Texture (RGB)", SpriteColorFXEditorHelper.TooltipTextureMask), baseTarget.textureMaskRed, typeof(Texture2D), false) as Texture2D);
				  if (baseTarget.textureMaskRed != null)
					  UVParamsInspectorGUI(ref baseTarget.textureMaskRedUVParams, ref baseTarget.textureMaskRedUVAngle);

				  EditorGUI.indentLevel--;
			  }

			  if (foldoutsMasks[1] = EditorGUILayout.Foldout(foldoutsMasks[1], @"#2 mask (green)"))
			  {
				  EditorGUI.indentLevel++;
				  
          baseTarget.strengthMaskGreen = (float)SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strengthMaskGreen * 100f), 0, 100, 100) * 0.01f;
				  
          baseTarget.pixelOpMaskGreen = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Pixel op", @"Color pixel operation"), baseTarget.pixelOpMaskGreen);
				  
          baseTarget.colorMaskGreen = EditorGUILayout.ColorField(@"Color", baseTarget.colorMaskGreen);
				  
          baseTarget.textureMaskGreen = (EditorGUILayout.ObjectField(new GUIContent(@"Texture (RGB)", SpriteColorFXEditorHelper.TooltipTextureMask), baseTarget.textureMaskGreen, typeof(Texture2D), false) as Texture2D);
				  if (baseTarget.textureMaskGreen != null)
					  UVParamsInspectorGUI(ref baseTarget.textureMaskGreenUVParams, ref baseTarget.textureMaskGreenUVAngle);

				  EditorGUI.indentLevel--;
			  }
			
        if (foldoutsMasks[2] = EditorGUILayout.Foldout(foldoutsMasks[2], @"#3 mask (blue)"))
			  {
				  EditorGUI.indentLevel++;

				  baseTarget.strengthMaskBlue = (float)SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strengthMaskBlue * 100f), 0, 100, 100) * 0.01f;
          
          baseTarget.pixelOpMaskBlue = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Pixel op", @"Color pixel operation"), baseTarget.pixelOpMaskBlue);
          
          baseTarget.colorMaskBlue = EditorGUILayout.ColorField(@"Color", baseTarget.colorMaskBlue);
          
          baseTarget.textureMaskBlue = (EditorGUILayout.ObjectField(new GUIContent(@"Texture (RGB)", SpriteColorFXEditorHelper.TooltipTextureMask), baseTarget.textureMaskBlue, typeof(Texture2D), false) as Texture2D);
				  if (baseTarget.textureMaskBlue != null)
					  this.UVParamsInspectorGUI(ref baseTarget.textureMaskBlueUVParams, ref baseTarget.textureMaskBlueUVAngle);

				  EditorGUI.indentLevel--;
			  }

			  EditorGUILayout.Separator();

			  baseTarget.textureMask = (EditorGUILayout.ObjectField(new GUIContent(@"Mask #1 (RGB)", SpriteColorFXEditorHelper.TooltipTextureMask), this.baseTarget.textureMask, typeof(Texture2D), false) as Texture2D);

			  EditorGUILayout.Separator();

			  EditorGUILayout.BeginHorizontal();
        {
			    GUILayout.FlexibleSpace();

			    if (SpriteColorFXEditorHelper.ButtonImage(string.Empty, SpriteColorFXEditorHelper.TooltipInfo, @"Icons/ic_info"))
				    Application.OpenURL(SpriteColorFXEditorHelper.DocumentationURL);

			    GUILayout.Space(10f);

          if (GUILayout.Button(new GUIContent(@"Reset all", SpriteColorFXEditorHelper.TooltipResetAll), GUILayout.Width(82.0f)) == true)
          {
            baseTarget.strength = 1.0f;

            baseTarget.strengthMaskRed = 1.0f;
            baseTarget.pixelOpMaskRed = SpriteColorHelper.PixelOp.Multiply;
            baseTarget.colorMaskRed = Color.white;
            baseTarget.textureMaskRedUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            baseTarget.textureMaskRedUVAngle = 0.0f;

            baseTarget.strengthMaskGreen = 1.0f;
            baseTarget.pixelOpMaskGreen = SpriteColorHelper.PixelOp.Multiply;
            baseTarget.colorMaskGreen = Color.white;
            baseTarget.textureMaskGreenUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            baseTarget.textureMaskGreenUVAngle = 0.0f;

            baseTarget.strengthMaskBlue = 1.0f;
            baseTarget.pixelOpMaskBlue = SpriteColorHelper.PixelOp.Multiply;
            baseTarget.colorMaskBlue = Color.white;
            baseTarget.textureMaskBlueUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            baseTarget.textureMaskBlueUVAngle = 0.0f;
          }
        }
			  EditorGUILayout.EndHorizontal();

			  EditorGUILayout.Separator();
			  
        if (string.IsNullOrEmpty(Warnings) == false)
			  {
				  EditorGUILayout.HelpBox(Warnings, MessageType.Warning);
				  EditorGUILayout.Separator();
			  }
			  
        if (string.IsNullOrEmpty(this.Errors) == false)
			  {
				  EditorGUILayout.HelpBox(this.Errors, MessageType.Error);
				  EditorGUILayout.Separator();
			  }

			  if (string.IsNullOrEmpty(this.Help) == false)
				  EditorGUILayout.HelpBox(this.Help, MessageType.Info);
      }
			EditorGUILayout.EndVertical();
			
      Errors = Warnings = string.Empty;
			
      if (GUI.changed == true)
				EditorUtility.SetDirty(base.target);

			EditorGUIUtility.LookLikeControls();

			EditorGUI.indentLevel = 0;

			EditorGUIUtility.labelWidth = 125.0f;
		}

		private void UVParamsInspectorGUI(ref Vector4 uvParams, ref float angle)
		{
      EditorGUI.indentLevel++;

      uvParams.x = SpriteColorFXEditorHelper.SliderWithReset(@"U coord scale", @"U texture coordinate scale", uvParams.x, -5f, 5f, 1f);
      uvParams.y = SpriteColorFXEditorHelper.SliderWithReset(@"V coord scale", @"V texture coordinate scale", uvParams.y, -5f, 5f, 1f);
      uvParams.z = SpriteColorFXEditorHelper.SliderWithReset(@"U coord vel", @"U texture coordinate velocity", uvParams.z, -2f, 2f, 0f);
      uvParams.w = SpriteColorFXEditorHelper.SliderWithReset(@"V coord vel", @"V texture coordinate velocity", uvParams.w, -2f, 2f, 0f);
      angle = SpriteColorFXEditorHelper.SliderWithReset(@"UV angle", @"UV rotation angle", angle, 0f, 360f, 0f);
			
      EditorGUI.indentLevel--;
		}
	}
}
