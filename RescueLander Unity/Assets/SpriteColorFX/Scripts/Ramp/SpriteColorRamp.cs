///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Sprite Color Ramp.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("Sprite Color FX/Color Ramp")]
  public sealed class SpriteColorRamp : MonoBehaviour
  {
    /// <summary>
    /// Effect strength.
    /// </summary>
    public float strength = 1.0f;

    /// <summary>
    /// Gamma.
    /// </summary>
    public float gammaCorrect = 1.2f;

    /// <summary>
    /// UV scroll.
    /// </summary>
    public float uvScroll = 0.0f;

    /// <summary>
    /// Invert luminance.
    /// </summary>
    public bool invertLum = false;

    /// <summary>
    /// Min luminance range (0.0f).
    /// </summary>
    public float luminanceRangeMin = 0.0f;

    /// <summary>
    /// Max luminance range (1.0f).
    /// </summary>
    public float luminanceRangeMax = 1.0f;

    /// <summary>
    /// Palette.
    /// </summary>
    public SpriteColorRampPalettes palette;

    private SpriteRenderer spriteRenderer;

    private float textureHeightInv = 1.0f;

    private void OnEnable()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
      {
        Texture2D rampsTex = Resources.Load<Texture2D>(@"Textures/SpriteColorRamps");
        rampsTex.wrapMode = TextureWrapMode.Repeat;
        rampsTex.filterMode = FilterMode.Point;

        Shader shader = Resources.Load<Shader>(@"Shaders/Ramp/SpriteColorRamp");

        if (rampsTex != null && shader != null)
        {
          spriteRenderer.sharedMaterial = new Material(shader);
          spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorRamp";
          spriteRenderer.sharedMaterial.SetTexture(@"_RampsTex", rampsTex);

          textureHeightInv = 1.0f / rampsTex.height;
        }
        else
        {
          Debug.LogWarning(@"Failed to load necessary files, SpriteColorRamp disabled.");

          this.enabled = false;
        }
      }
      else
      {
        Debug.LogWarning(string.Format("'{0}' without SpriteRenderer, SpriteColorRamp disabled.", gameObject.name));

        this.enabled = false;
      }
    }

    private void OnDisable()
    {
      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null && string.CompareOrdinal(spriteRenderer.sharedMaterial.name, @"Sprites/Default") != 0)
        spriteRenderer.sharedMaterial = new Material(Shader.Find(@"Sprites/Default"));
    }

    private void Update()
    {
      if (spriteRenderer == null)
        spriteRenderer = GetComponent<SpriteRenderer>();

      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null)
      {
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderStrengthParam, strength);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderGammaCorrectParam, gammaCorrect);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderUVScrollParam, Mathf.Clamp(uvScroll, 0.0f, 1.0f));

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderInvertLumParam, invertLum ? -1.0f : 1.0f);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderLumRangeMinParam, luminanceRangeMin);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderLumRangeMaxParam, luminanceRangeMax);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderRampIdxParam, ((float)palette + 0.5f) * textureHeightInv);
      }
    }
  }
}
