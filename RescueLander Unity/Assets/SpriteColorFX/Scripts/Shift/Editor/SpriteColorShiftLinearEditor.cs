///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorShiftLinear editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorShiftLinear))]
  public sealed class SpriteColorShiftLinearEditor : Editor
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

    private SpriteColorShiftLinear baseTarget;

    /// <summary>
    /// OnInspectorGUI.
    /// </summary>
    public override void OnInspectorGUI()
    {
      if (baseTarget == null)
        baseTarget = this.target as SpriteColorShiftLinear;

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

        baseTarget.redShift = SpriteColorFXEditorHelper.Vector2WithReset(@"Red offset", SpriteColorFXEditorHelper.TooltipRedOffset, baseTarget.redShift, Vector2.zero);

        baseTarget.greenShift = SpriteColorFXEditorHelper.Vector2WithReset(@"Green offset", SpriteColorFXEditorHelper.TooltipGreenOffset, baseTarget.greenShift, Vector2.zero);

        baseTarget.blueShift = SpriteColorFXEditorHelper.Vector2WithReset(@"Blue offset", SpriteColorFXEditorHelper.TooltipBlueOffset, baseTarget.blueShift, Vector2.zero);

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
            baseTarget.redShift = Vector2.zero;
            baseTarget.greenShift = Vector2.zero;
            baseTarget.blueShift = Vector2.zero;
            
            baseTarget.noiseAmount = 0.0f;
            baseTarget.noiseSpeed = 1.0f;
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