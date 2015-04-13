///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorRamp editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorRamp))]
  public sealed class SpriteColorRampEditor : Editor
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
    
    private SpriteColorRamp baseTarget;

    /// <summary>
    /// OnInspectorGUI.
    /// </summary>
    public override void OnInspectorGUI()
    {
      if (baseTarget == null)
      {
        baseTarget = this.target as SpriteColorRamp;
      }

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

        baseTarget.palette = (SpriteColorRampPalettes)EditorGUILayout.EnumPopup(@"Palette", baseTarget.palette);

        baseTarget.strength = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strength * 100.0f), 0, 100, 100) * 0.01f;

        baseTarget.gammaCorrect = SpriteColorFXEditorHelper.SliderWithReset(@"Gamma", SpriteColorFXEditorHelper.TooltipGamma, baseTarget.gammaCorrect, 0.5f, 3.0f, 1.2f);

        baseTarget.uvScroll = SpriteColorFXEditorHelper.SliderWithReset(@"UV Scroll", SpriteColorFXEditorHelper.TooltipUVScroll, baseTarget.uvScroll, 0.0f, 1.0f, 0.0f);

        EditorGUIUtility.fieldWidth = 60.0f;

        SpriteColorFXEditorHelper.MinMaxSliderWithReset(@"Luminance range", SpriteColorFXEditorHelper.TooltipLuminanceRange, ref baseTarget.luminanceRangeMin, ref baseTarget.luminanceRangeMax, 0.0f, 1.0f, 0.0f, 1.0f);

        baseTarget.invertLum = SpriteColorFXEditorHelper.ToogleWithReset(@"Invert luminance", SpriteColorFXEditorHelper.TooltipInvertLuminance, baseTarget.invertLum, false);

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

            baseTarget.gammaCorrect = 1.2f;

            baseTarget.uvScroll = 0.0f;

            baseTarget.invertLum = false;

            baseTarget.luminanceRangeMin = 0.0f;

            baseTarget.luminanceRangeMax = 1.0f;
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