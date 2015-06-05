///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorDissolve editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorDissolve))]
  public sealed class SpriteColorDissolveEditor : Editor
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

    private SpriteColorDissolve baseTarget;

    /// <summary>
    /// OnInspectorGUI.
    /// </summary>
    public override void OnInspectorGUI()
    {
      if (baseTarget == null)
        baseTarget = this.target as SpriteColorDissolve;

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

        baseTarget.dissolveAmount = SpriteColorFXEditorHelper.SliderWithReset(@"Amount", SpriteColorFXEditorHelper.TooltipNoiseAmount, baseTarget.dissolveAmount * 100.0f, 0.0f, 100.0f, 0.0f) * 0.01f;

        DissolveShaderType newShaderType = (DissolveShaderType)EditorGUILayout.EnumPopup(new GUIContent(@"Shader", @"Texture type"), baseTarget.shaderType);
        if (newShaderType != baseTarget.shaderType)
          baseTarget.SetShaderType(newShaderType);

        if (baseTarget.shaderType != DissolveShaderType.Normal)
        {
          SpriteColorHelper.PixelOp newPixelOp = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Blend mode", @"Blend modes"), baseTarget.pixelOp);
          if (newPixelOp != baseTarget.pixelOp)
            baseTarget.SetPixelOp(newPixelOp);
        }

        DisolveTextureType newTextureType = (DisolveTextureType)EditorGUILayout.EnumPopup(@"Dissolve type", baseTarget.disolveTextureType);
        if (newTextureType != baseTarget.disolveTextureType)
          baseTarget.SetTextureType(newTextureType);

        if (baseTarget.disolveTextureType == DisolveTextureType.Custom)
          baseTarget.disolveTexture = EditorGUILayout.ObjectField(@"Dissolve texture", baseTarget.disolveTexture, typeof(Texture), false) as Texture;

        baseTarget.dissolveUVScale = SpriteColorFXEditorHelper.SliderWithReset(@"Dissolve UV scale", SpriteColorFXEditorHelper.TooltipNoiseAmount, baseTarget.dissolveUVScale, 0.1f, 5.0f, 1.0f);

        baseTarget.dissolveInverse = EditorGUILayout.Toggle(new GUIContent(@"Invert", SpriteColorFXEditorHelper.TooltipNoiseAmount), baseTarget.dissolveInverse);

        if (baseTarget.shaderType != DissolveShaderType.Normal)
          baseTarget.dissolveBorderWitdh = SpriteColorFXEditorHelper.SliderWithReset(@"Border witdh", SpriteColorFXEditorHelper.TooltipNoiseAmount, baseTarget.dissolveBorderWitdh * 500.0f, 0.0f, 100.0f, 50.0f) * 0.002f;

        if (baseTarget.shaderType == DissolveShaderType.BorderColor)
          baseTarget.dissolveBorderColor = SpriteColorFXEditorHelper.ColorWithReset(@"Border color", SpriteColorFXEditorHelper.TooltipNoiseAmount, baseTarget.dissolveBorderColor, Color.grey);
        else if (baseTarget.shaderType == DissolveShaderType.BorderTexture)
        {
          baseTarget.borderTexture = EditorGUILayout.ObjectField(@"Border texture", baseTarget.borderTexture, typeof(Texture), false) as Texture;
          baseTarget.borderUVScale = SpriteColorFXEditorHelper.SliderWithReset(@"Border UV scale", SpriteColorFXEditorHelper.TooltipNoiseAmount, baseTarget.borderUVScale, 0.1f, 5.0f, 1.0f);
        }

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
            baseTarget.dissolveAmount = 0.0f;
            baseTarget.dissolveBorderWitdh = 0.1f;
            baseTarget.dissolveBorderColor = Color.grey;
            baseTarget.dissolveUVScale = 1.0f;
            baseTarget.borderUVScale = 1.0f;
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