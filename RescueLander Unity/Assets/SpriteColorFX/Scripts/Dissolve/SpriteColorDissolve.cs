///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Shader usef for the effect.
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
    Custom,
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
    /// Dissolve type.
    /// </summary>
    public DissolveShaderType shaderType = DissolveShaderType.Normal;

    /// <summary>
    /// 
    /// </summary>
    public SpriteColorHelper.PixelOp pixelOp = SpriteColorHelper.PixelOp.Solid;

    /// <summary>
    /// Dissolve texture type.
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
      {
        switch (pixelOp)
        {
          case SpriteColorHelper.PixelOp.Solid: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorSolid"); break;
          case SpriteColorHelper.PixelOp.Additive: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorAdditive"); break;
          case SpriteColorHelper.PixelOp.Subtract: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorSubtract"); break;
          case SpriteColorHelper.PixelOp.Multiply: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorMultiply"); break;
          case SpriteColorHelper.PixelOp.Divide: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorDivide"); break;
          case SpriteColorHelper.PixelOp.Darken: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorDarken"); break;
          case SpriteColorHelper.PixelOp.Lighten: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorLighten"); break;
          case SpriteColorHelper.PixelOp.Screen: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorScreen"); break;
          case SpriteColorHelper.PixelOp.Dodge: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorDodge"); break;
          case SpriteColorHelper.PixelOp.HardMix: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorHardMix"); break;
          case SpriteColorHelper.PixelOp.Difference: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderColorDifference"); break;
        }
      }
      else if (this.shaderType == DissolveShaderType.BorderTexture)
      {
        switch (pixelOp)
        {
          case SpriteColorHelper.PixelOp.Solid: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureSolid"); break;
          case SpriteColorHelper.PixelOp.Additive: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureAdditive"); break;
          case SpriteColorHelper.PixelOp.Subtract: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureSubtract"); break;
          case SpriteColorHelper.PixelOp.Multiply: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureMultiply"); break;
          case SpriteColorHelper.PixelOp.Divide: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureDivide"); break;
          case SpriteColorHelper.PixelOp.Darken: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureDarken"); break;
          case SpriteColorHelper.PixelOp.Lighten: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureLighten"); break;
          case SpriteColorHelper.PixelOp.Screen: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureScreen"); break;
          case SpriteColorHelper.PixelOp.Dodge: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureDodge"); break;
          case SpriteColorHelper.PixelOp.HardMix: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureHardMix"); break;
          case SpriteColorHelper.PixelOp.Difference: shader = Resources.Load<Shader>(@"Shaders/Dissolve/SpriteColorDissolveBorderTextureDifference"); break;
        }
      }

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
    public void SetPixelOp(SpriteColorHelper.PixelOp pixelOp)
    {
      if (this.pixelOp != pixelOp)
      {
        this.pixelOp = pixelOp;

        SetShaderType(shaderType);
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
      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null && string.CompareOrdinal(spriteRenderer.sharedMaterial.name, @"Sprites/Default") == 0)
        spriteRenderer.sharedMaterial = new Material(Shader.Find(@"Sprites/Default"));
    }

    private void Update()
    {
      if (spriteRenderer == null)
        spriteRenderer = GetComponent<SpriteRenderer>();

      if (spriteRenderer != null && spriteRenderer.sharedMaterial != null)
      {
        spriteRenderer.sharedMaterial.SetTexture("_DissolveTex", disolveTexture);

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
