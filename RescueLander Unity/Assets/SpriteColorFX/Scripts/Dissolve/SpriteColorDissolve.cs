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
  /// Dissolve texture type. If you want to use your own, set 'Custom'.
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
    Diamonds,
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
    /// Pixel operation.
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
    /// Border texture.
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
      Shader shader = null;

      if (this.shaderType == DissolveShaderType.Normal)
        shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveNormal");
      else if (this.shaderType == DissolveShaderType.BorderColor)
        shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveColor");
      else if (this.shaderType == DissolveShaderType.BorderTexture)
        shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveTexture");

      if (shader != null)
      {
        spriteRenderer.sharedMaterial = new Material(shader);
        spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorDissolve";
      }
      else
      {
        Debug.LogWarning(@"Failed to load necessary files, SpriteColorDissolve disabled.");

        this.enabled = false;
      }
    }

    /// <summary>
    /// Set the pixel color operation.
    /// </summary>
    [System.Obsolete(@"No longer needed. You can uso this.pixelOp", false)]
    public void SetPixelOp(SpriteColorHelper.PixelOp pixelOp)
    {
      this.pixelOp = pixelOp;
    }

    /// <summary>
    /// Set the dissolve texture type.
    /// </summary>
    public void SetTextureType(DisolveTextureType textureType)
    {
      if (textureType != disolveTextureType)
      {
        disolveTextureType = textureType;
        switch (disolveTextureType)
        {
          case DisolveTextureType.Burn: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Burn"); break;
          case DisolveTextureType.Explosion: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Explosion"); break;
          case DisolveTextureType.Grow: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Grow"); break;
          case DisolveTextureType.Horizontal: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Horizontal"); break;
          case DisolveTextureType.Organic: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Organic"); break;
          case DisolveTextureType.Pixel: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Pixel"); break;
          case DisolveTextureType.Plasma: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Plasma"); break;
          case DisolveTextureType.Sphere: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Sphere"); break;
          case DisolveTextureType.Vertical: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Vertical"); break;
          case DisolveTextureType.Diamonds: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Diamonds"); break;
          case DisolveTextureType.Radial: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Radial"); break;
          case DisolveTextureType.Radial5: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Radial5"); break;
          case DisolveTextureType.RaysCenter: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/RaysCenter"); break;
          case DisolveTextureType.RaysCorner: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/RaysCorner"); break;
          case DisolveTextureType.Spiral: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Spiral"); break;
          case DisolveTextureType.SpiralFast1: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/SpiralFast1"); break;
          case DisolveTextureType.SpiralFast2: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/SpiralFast2"); break;
          case DisolveTextureType.SpiralFract: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/SpiralFract"); break;
          case DisolveTextureType.Squares: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Squares"); break;
          case DisolveTextureType.Waves: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/Waves"); break;
          case DisolveTextureType.WavesVertical: disolveTexture = Resources.Load<Texture>(@"Textures/Dissolve/WavesVertical"); break;
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
        spriteRenderer.sharedMaterial.SetTexture("_DissolveTex", disolveTexture);

        spriteRenderer.sharedMaterial.SetInt("_PixelOp", (int)pixelOp);

        if (shaderType == DissolveShaderType.BorderTexture)
        {
          spriteRenderer.sharedMaterial.SetTexture("_BorderTex", borderTexture);
          spriteRenderer.sharedMaterial.SetFloat("_BorderUVScale", borderUVScale);
        }

        spriteRenderer.sharedMaterial.SetFloat("_DissolveAmount", 1.0f - dissolveAmount);

        if (shaderType != DissolveShaderType.Normal)
          spriteRenderer.sharedMaterial.SetFloat("_DissolveLineWitdh", dissolveBorderWitdh);

        if (shaderType == DissolveShaderType.BorderColor)
          spriteRenderer.sharedMaterial.SetColor("_DissolveLineColor", dissolveBorderColor);

        spriteRenderer.sharedMaterial.SetFloat("_DissolveUVScale", dissolveUVScale);
        spriteRenderer.sharedMaterial.SetFloat("_DissolveInverseOne", dissolveInverse == false ? 0.0f : 1.0f);
        spriteRenderer.sharedMaterial.SetFloat("_DissolveInverseTwo", dissolveInverse == false ? -1.0f : 1.0f);
      }
    }
  }
}
