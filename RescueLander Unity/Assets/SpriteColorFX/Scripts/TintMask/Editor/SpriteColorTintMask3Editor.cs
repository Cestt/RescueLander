///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorMask editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorTintMask3))]
  public sealed class SpriteColorTintMask3Editor : Editor
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

    private bool foldoutRed = false;
    private bool foldoutGreen = false;
    private bool foldoutBlue = false;

    private SpriteColorTintMask3 baseTarget;

    /// <summary>
    /// OnInspectorGUI.
    /// </summary>
    public override void OnInspectorGUI()
    {
      if (baseTarget == null)
        baseTarget = this.target as SpriteColorTintMask3;

      EditorGUIUtility.LookLikeControls();

      EditorGUI.indentLevel = 0;

      EditorGUIUtility.labelWidth = 125.0f;

      EditorGUILayout.BeginVertical();
      {
        EditorGUILayout.Separator();

        /////////////////////////////////////////////////
        // Common.
        /////////////////////////////////////////////////
        EditorGUIUtility.fieldWidth = 40.0f;

        baseTarget.strength = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strength * 100.0f), 0, 100, 100) * 0.01f;

        if ((foldoutRed = EditorGUILayout.Foldout(foldoutRed, @"Mask red")) == true)
        {
          EditorGUI.indentLevel++;

          baseTarget.strengthMaskRed = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strengthMaskRed * 100.0f), 0, 100, 100) * 0.01f;

          //baseTarget.pixelOpMaskRed = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Pixel op", @"Color pixel operation"), baseTarget.pixelOpMaskRed);

          baseTarget.colorMaskRed = EditorGUILayout.ColorField(@"Color", baseTarget.colorMaskRed);

          EditorGUI.indentLevel--;
        }

        if ((foldoutGreen = EditorGUILayout.Foldout(foldoutGreen, @"Mask green")) == true)
        {
          EditorGUI.indentLevel++;

          baseTarget.strengthMaskGreen = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strengthMaskGreen * 100.0f), 0, 100, 100) * 0.01f;

          //baseTarget.pixelOpMaskGreen = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Pixel op", @"Color pixel operation"), baseTarget.pixelOpMaskGreen);

          baseTarget.colorMaskGreen = EditorGUILayout.ColorField(@"Color", baseTarget.colorMaskGreen);

          EditorGUI.indentLevel--;
        }

        if ((foldoutBlue = EditorGUILayout.Foldout(foldoutBlue, @"Mask blue")) == true)
        {
          EditorGUI.indentLevel++;

          baseTarget.strengthMaskBlue = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strengthMaskBlue * 100.0f), 0, 100, 100) * 0.01f;

          //baseTarget.pixelOpMaskBlue = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Pixel op", @"Color pixel operation"), baseTarget.pixelOpMaskBlue);

          baseTarget.colorMaskBlue = EditorGUILayout.ColorField(@"Color", baseTarget.colorMaskBlue);

          EditorGUI.indentLevel--;
        }

        baseTarget.textureMask = EditorGUILayout.ObjectField(new GUIContent(@"Mask (RGBA)", SpriteColorFXEditorHelper.TooltipTextureMask), baseTarget.textureMask, typeof(Texture2D), false) as Texture2D;

        EditorGUILayout.Separator();

        /////////////////////////////////////////////////
        // Misc.
        /////////////////////////////////////////////////

        EditorGUILayout.BeginHorizontal();
        {
          GUILayout.FlexibleSpace();

          if (SpriteColorFXEditorHelper.ButtonImage(string.Empty, SpriteColorFXEditorHelper.TooltipInfo, @"Icons/ic_info") == true)
            Application.OpenURL(SpriteColorFXEditorHelper.DocumentationURL);

          GUILayout.Space(10.0f);

          if (GUILayout.Button(new GUIContent(@"Reset all", SpriteColorFXEditorHelper.TooltipResetAll), GUILayout.Width(82.0f)) == true)
          {
            baseTarget.strength = 1.0f;
          }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        if (string.IsNullOrEmpty(Warnings) == false)
        {
          EditorGUILayout.HelpBox(Warnings, MessageType.Warning);

          EditorGUILayout.Separator();
        }

        if (string.IsNullOrEmpty(Errors) == false)
        {
          EditorGUILayout.HelpBox(Errors, MessageType.Error);

          EditorGUILayout.Separator();
        }

        if (string.IsNullOrEmpty(Help) == false)
          EditorGUILayout.HelpBox(Help, MessageType.Info);
      }
      EditorGUILayout.EndVertical();

      Warnings = Errors = string.Empty;

      if (GUI.changed == true)
        EditorUtility.SetDirty(target);

      EditorGUIUtility.LookLikeControls();

      EditorGUI.indentLevel = 0;

      EditorGUIUtility.labelWidth = 125.0f;
    }
  }
}