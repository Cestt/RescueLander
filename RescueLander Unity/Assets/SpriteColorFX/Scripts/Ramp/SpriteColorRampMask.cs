///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Sprite Color Ramp with maks.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("Sprite Color FX/Color Ramp Mask")]
  public sealed class SpriteColorRampMask : MonoBehaviour
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
    /// Max luminance range (0.0f).
    /// </summary>
    public float luminanceRangeMax = 1.0f;

    /// <summary>
    /// Palettes.
    /// </summary>
    [SerializeField]
    public SpriteColorRampPalettes[] palettes;

    /// <summary>
    /// Texture mask (RGBA).
    /// </summary>
    public Texture2D textureMask;

    private SpriteRenderer spriteRenderer = null;

    private float textureHeightInv = 1.0f;

    private void OnEnable()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
      {
        palettes = new SpriteColorRampPalettes[3];

        Texture2D rampsTex = Resources.Load<Texture2D>(@"Textures/SpriteColorRamps");
        rampsTex.wrapMode = TextureWrapMode.Repeat;
        rampsTex.filterMode = FilterMode.Point;

        Shader spriteColorFXShader = Resources.Load<Shader>(@"Shaders/Ramp/SpriteColorRampMask");

        if (rampsTex != null && spriteColorFXShader != null)
        {
          spriteRenderer.sharedMaterial = new Material(spriteColorFXShader);
          spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorRampMask";
          spriteRenderer.sharedMaterial.SetTexture(@"_RampsTex", rampsTex);

          textureHeightInv = 1.0f / rampsTex.height;
        }
        else
        {
          Debug.LogWarning(@"Failed to load necessary files, SpriteColorRampMask disabled.");

          this.enabled = false;
        }
      }
      else
      {
        Debug.LogWarning(string.Format("'{0}' without SpriteRenderer, SpriteColorRampMask disabled.", gameObject.name));

        this.enabled = false;
      }
    }

    private void OnDisable()
    {
      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null && string.CompareOrdinal(spriteRenderer.sharedMaterial.name, @"Sprites/Default") == 0)
        spriteRenderer.sharedMaterial = new Material(Shader.Find(@"Sprites/Default"));
    }

    private void Update()
    {
      if (spriteRenderer == null)
        spriteRenderer = GetComponent<SpriteRenderer>();

      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null)
      {
        spriteRenderer.sharedMaterial.SetTexture(SpriteColorHelper.ShaderRampMaskTex, textureMask);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderStrengthParam, strength);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderGammaCorrectParam, gammaCorrect);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderUVScrollParam, Mathf.Clamp(uvScroll, 0.0f, 1.0f));

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderInvertLumParam, invertLum ? -1.0f : 1.0f);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderLumRangeMinParam, luminanceRangeMin);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderLumRangeMaxParam, luminanceRangeMax);

        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderRampRedIdx, ((float)palettes[0] + 0.5f) * textureHeightInv);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderRampGreenIdx, ((float)palettes[1] + 0.5f) * textureHeightInv);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderRampBlueIdx, ((float)palettes[2] + 0.5f) * textureHeightInv);
      }
    }
  }
}
