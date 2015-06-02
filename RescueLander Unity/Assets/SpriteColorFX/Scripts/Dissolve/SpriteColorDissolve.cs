///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Shader used for the effect.
  /// </summary>
  public enum DissolveShaderType
  {
    /// <summary>
    /// Normal dissolve effect.
    /// </summary>
    Normal,

    /// <summary>
    /// Dissolve effect with border color.
    /// </summary>
    BorderColor,

    /// <summary>
    /// Dissolve effect with border texture.
    /// </summary>
    BorderTexture,
  }

  /// <summary>
  /// Dissolve texture type. If you want to use your own, set 'Custom' and change 'borderTexture'.
  /// </summary>
  public enum DisolveTextureType
  {
    Burn,
    Explosion,
    Grow,
    Horizontal,
    Organic,
    Pixel,
    Plasma,
    Sphere,
    Vertical,
    Radial,
    Radial5,
    RaysCenter,
    RaysCorner,
    Spiral,
    SpiralFast1,
    SpiralFast2,
    SpiralFract,
    Squares,
    Waves,
    WavesVertical,

    Custom = 99,
  }

  /// <summary>
  /// Color dissolve.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("Sprite Color FX/Color Dissolve")]
  public sealed class SpriteColorDissolve : MonoBehaviour
  {
    /// <summary>
    /// Use SetShaderType().
    /// </summary>
    public DissolveShaderType shaderType = DissolveShaderType.Normal;

    /// <summary>
    /// Use SetPixelOp().
    /// </summary>
    public SpriteColorHelper.PixelOp pixelOp = SpriteColorHelper.PixelOp.Solid;

    /// <summary>
    /// Use SetTextureType().
    /// </summary>
    public DisolveTextureType disolveTextureType = DisolveTextureType.Burn;

    /// <summary>
    /// Dissolve texture.
    /// </summary>
    public Texture disolveTexture;

    /// <summary>
    /// Border texture. Change this if you want to use a custom texture.
    /// </summary>
    public Texture borderTexture;

    /// <summary>
    /// Dissolve amount [0..1].
    /// </summary>
    public float dissolveAmount = 0.0f;

    /// <summary>
    /// Invert the effect.
    /// </summary>
    public bool dissolveInverse = false;

    /// <summary>
    /// Dissolve line witdh [0..0.2].
    /// </summary>
    public float dissolveBorderWitdh = 0.1f;

    /// <summary>
    /// Dissolve line color.
    /// </summary>
    public Color dissolveBorderColor = Color.grey;

    /// <summary>
    /// Dissolve noise amount [0..1].
    /// </summary>
    public float dissolveNoiseAmount = 0.25f;

    /// <summary>
    /// Dissolve UV scale [0.1..5].
    /// </summary>
    public float dissolveUVScale = 1.0f;

    /// <summary>
    /// Border UV scale [0.1..5].
    /// </summary>
    public float borderUVScale = 1.0f;

    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Set the shader type.
    /// </summary>
    public void SetShaderType(DissolveShaderType shaderType)
    {
      this.shaderType = shaderType;

      SetPixelOp(pixelOp);
    }

    /// <summary>
    /// Set the pixel color operation.
    /// </summary>
    public void SetPixelOp(SpriteColorHelper.PixelOp pixelOp)
    {
      this.pixelOp = pixelOp;

      string shaderPath = string.Empty;

      if (this.shaderType == DissolveShaderType.Normal)
        shaderPath = @"Shaders/Dissolve/SpriteColorDissolveNormal";
      else
        shaderPath = string.Format("Shaders/Dissolve/SpriteColorDissolve{0}{1}", shaderType.ToString(), this.pixelOp.ToString());

      Shader shader = Resources.Load<Shader>(shaderPath);
      if (shader != null)
      {
        spriteRenderer.sharedMaterial = new Material(shader);
        spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorDissolve";
      }
      else
      {
        Debug.LogWarning(string.Format("Failed to load '{0}', SpriteColorDissolve disabled.", shaderPath));

        this.enabled = false;
      }
    }

    /// <summary>
    /// Set the dissolve texture type.
    /// </summary>
    public void SetTextureType(DisolveTextureType textureType)
    {
      if (textureType != disolveTextureType)
      {
        disolveTextureType = textureType;

        if (disolveTextureType != DisolveTextureType.Custom)
        {
          string texturePath = string.Format("Textures/Dissolve/{0}", disolveTextureType.ToString());

          Texture texture = Resources.Load<Texture>(texturePath);
          if (texture != null)
            disolveTexture = texture;
          else
          {
            Debug.LogWarning(string.Format("Failed to load '{0}', SpriteColorDissolve disabled.", texturePath));

            this.enabled = false;
          }
        }
      }
    }

    private void OnEnable()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
        SetShaderType(shaderType);
      else
      {
        Debug.LogWarning(string.Format("'{0}' without SpriteRenderer, SpriteColorDissolve disabled.", gameObject.name));

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
        spriteRenderer.sharedMaterial.SetTexture(@"_DissolveTex", disolveTexture);

        if (shaderType == DissolveShaderType.BorderTexture)
        {
          spriteRenderer.sharedMaterial.SetTexture(@"_BorderTex", borderTexture);
          spriteRenderer.sharedMaterial.SetFloat(@"_BorderUVScale", borderUVScale);
        }

        spriteRenderer.sharedMaterial.SetFloat(@"_DissolveAmount", 1.0f - dissolveAmount);

        if (shaderType != DissolveShaderType.Normal)
          spriteRenderer.sharedMaterial.SetFloat(@"_DissolveLineWitdh", dissolveBorderWitdh);

        if (shaderType == DissolveShaderType.BorderColor)
          spriteRenderer.sharedMaterial.SetColor(@"_DissolveLineColor", dissolveBorderColor);

        spriteRenderer.sharedMaterial.SetFloat(@"_DissolveUVScale", dissolveUVScale);
        spriteRenderer.sharedMaterial.SetFloat(@"_DissolveInverseOne", dissolveInverse == false ? 0.0f : 1.0f);
        spriteRenderer.sharedMaterial.SetFloat(@"_DissolveInverseTwo", dissolveInverse == false ? -1.0f : 1.0f);
      }
    }
  }
}
