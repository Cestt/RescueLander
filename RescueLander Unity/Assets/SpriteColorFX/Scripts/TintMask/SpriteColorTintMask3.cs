///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Sprite Color Mask.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("Sprite Color FX/Mask")]
  public sealed class SpriteColorTintMask3 : MonoBehaviour
  {
    /// <summary>
    /// Effect strength.
    /// </summary>
    public float strength = 1.0f;

    public float strengthMaskRed = 1.0f;
    public SpriteColorHelper.PixelOp pixelOpMaskRed = SpriteColorHelper.PixelOp.Solid;
    public Color colorMaskRed = Color.white;

    public float strengthMaskGreen = 1.0f;
    public SpriteColorHelper.PixelOp pixelOpMaskGreen = SpriteColorHelper.PixelOp.Solid;
    public Color colorMaskGreen = Color.white;

    public float strengthMaskBlue = 1.0f;
    public SpriteColorHelper.PixelOp pixelOpMaskBlue = SpriteColorHelper.PixelOp.Solid;
    public Color colorMaskBlue = Color.white;

    /// <summary>
    /// Texture mask (RGBA).
    /// </summary>
    public Texture2D textureMask;

    private SpriteRenderer spriteRenderer = null;

    private void OnEnable()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
      {
        Shader spriteColorFXShader = Resources.Load<Shader>(@"Shaders/TintMask/SpriteColorTintMask3");

        if (spriteColorFXShader != null)
        {
          spriteRenderer.sharedMaterial = new Material(spriteColorFXShader);
          spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorTintMask3";
        }
        else
        {
          Debug.LogWarning(@"Failed to load necessary files, SpriteColorMask disabled.");

          this.enabled = false;
        }
      }
      else
      {
        Debug.LogWarning(string.Format("'{0}' without SpriteRenderer, SpriteColorMask disabled.", gameObject.name));

        this.enabled = false;
      }
    }

    private void OnDisable()
    {
      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null && string.CompareOrdinal(spriteRenderer.sharedMaterial.name, @"Sprites/Default") == 0)
      {
        spriteRenderer.sharedMaterial = new Material(Shader.Find(@"Sprites/Default"));
        spriteRenderer.sharedMaterial.name = @"Sprites/Default";
      }
    }

    private void Update()
    {
      if (spriteRenderer == null)
        spriteRenderer = GetComponent<SpriteRenderer>();

      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null)
      {
        spriteRenderer.sharedMaterial.SetTexture(SpriteColorHelper.ShaderMaskTex, textureMask);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderStrengthParam, strength);

        spriteRenderer.sharedMaterial.SetFloat("_StrengthRed", strengthMaskRed);
        spriteRenderer.sharedMaterial.SetColor("_ColorRed", colorMaskRed);

        spriteRenderer.sharedMaterial.SetFloat("_StrengthGreen", strengthMaskGreen);
        spriteRenderer.sharedMaterial.SetColor("_ColorGreen", colorMaskGreen);

        spriteRenderer.sharedMaterial.SetFloat("_StrengthBlue", strengthMaskBlue);
        spriteRenderer.sharedMaterial.SetColor("_ColorBlue", colorMaskBlue);
      }
    }
  }
}
