///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorShiftSphere editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorShiftRadial))]
  public sealed class SpriteColorShiftRadialEditor : Editor
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

    private SpriteColorShiftRadial baseTarget;

    /// <summary>
    /// OnInspectorGUI.
    /// </summary>
    public override void OnInspectorGUI()
    {
      if (baseTarget == null)
        baseTarget = this.target as SpriteColorShiftRadial;

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

        baseTarget.strength = SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(baseTarget.strength * 100.0f), 0, 100, 0) * 0.01f;

        baseTarget.noiseAmount = SpriteColorFXEditorHelper.SliderWithReset(@"Noise amount", SpriteColorFXEditorHelper.TooltipNoiseAmount, baseTarget.noiseAmount * 100.0f, 0.0f, 100.0f, 0.0f) * 0.01f;

        baseTarget.noiseSpeed = SpriteColorFXEditorHelper.SliderWithReset(@"Noise speed", SpriteColorFXEditorHelper.TooltipNoiseSpeed, baseTarget.noiseSpeed * 100.0f, 0.0f, 100.0f, 0.0f) * 0.01f;

        EditorGUIUtility.fieldWidth = 60.0f;

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
            baseTarget.strength = 0.0f;
            baseTarget.noiseAmount = 0.0f;
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