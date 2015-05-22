///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Sprite Color Mask 3.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
	[AddComponentMenu("Sprite Color FX/Color Masks 3")]
	public sealed class SpriteColorMasks3 : MonoBehaviour
	{
    /// <summary>
    /// Strength [0..1].
    /// </summary>
    public float strength = 1.0f;

    /// <summary>
    /// Texture mask (RGB).
    /// </summary>
    public Texture2D textureMask;

#region #1 Mask (red channel).
    /// <summary>
    /// Mask strength [0..1].
    /// </summary>
    public float strengthMaskRed = 1.0f;

    /// <summary>
    /// Pixel operation.
    /// </summary>
    public SpriteColorHelper.PixelOp pixelOpMaskRed = SpriteColorHelper.PixelOp.Multiply;

    /// <summary>
    /// Mask color.
    /// </summary>
    public Color colorMaskRed = Color.white;

    /// <summary>
    /// Mask texture.
    /// </summary>
    public Texture2D textureMaskRed;

    /// <summary>
    /// Mask texture UV coordinate params (U scale, V scale, U velocity, V velocity).
    /// </summary>
    public Vector4 textureMaskRedUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Mask texture angle [0..360].
    /// </summary>
    public float textureMaskRedUVAngle;
#endregion

#region #2 Mask (green channel).
    /// <summary>
    /// Mask strength [0..1].
    /// </summary>
    public float strengthMaskGreen = 1.0f;

    /// <summary>
    /// Pixel operation.
    /// </summary>
    public SpriteColorHelper.PixelOp pixelOpMaskGreen = SpriteColorHelper.PixelOp.Multiply;

    /// <summary>
    /// Mask color.
    /// </summary>
    public Color colorMaskGreen = Color.white;

    /// <summary>
    /// Mask texture.
    /// </summary>
    public Texture2D textureMaskGreen;

    /// <summary>
    /// Mask texture UV coordinate params (U scale, V scale, U velocity, V velocity).
    /// </summary>
    public Vector4 textureMaskGreenUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Mask texture angle [0..360].
    /// </summary>
    public float textureMaskGreenUVAngle;
#endregion

#region #3 Mask (blue channel).
    /// <summary>
    /// Mask strength [0..1].
    /// </summary>
    public float strengthMaskBlue = 1.0f;

    /// <summary>
    /// Pixel operation.
    /// </summary>
    public SpriteColorHelper.PixelOp pixelOpMaskBlue = SpriteColorHelper.PixelOp.Multiply;

    /// <summary>
    /// Mask color.
    /// </summary>
    public Color colorMaskBlue = Color.white;

    /// <summary>
    /// Mask texture.
    /// </summary>
    public Texture2D textureMaskBlue;

    /// <summary>
    /// Mask texture UV coordinate params (U scale, V scale, U velocity, V velocity).
    /// </summary>
    public Vector4 textureMaskBlueUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Mask texture angle [0..360].
    /// </summary>
    public float textureMaskBlueUVAngle;
#endregion

		private SpriteRenderer spriteRenderer;

		private void OnEnable()
		{
			spriteRenderer = base.GetComponent<SpriteRenderer>();
			if (spriteRenderer != null)
			{
				Shader shader = Resources.Load<Shader>(@"Shaders/Masks/SpriteColorMasks3");
				if (shader != null)
				{
					spriteRenderer.sharedMaterial = new Material(shader);
					spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorMasks3";
				}
				else
				{
					Debug.LogWarning(@"Failed to load necessary files, SpriteColorMasks3 disabled.");
					base.enabled = false;
				}
			}
			else
			{
				Debug.LogWarning(string.Format("'{0}' without SpriteRenderer, SpriteColorMasks3 disabled.", base.gameObject.name));
				base.enabled = false;
			}
		}

		private void OnDisable()
		{
			if (spriteRenderer != null && spriteRenderer.sharedMaterial != null && string.CompareOrdinal(spriteRenderer.sharedMaterial.name, @"Sprites/Default") != 0)
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

        // #1
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderStrengthRedParam, strengthMaskRed);
        spriteRenderer.sharedMaterial.SetInt(SpriteColorHelper.ShaderPixelOpRedParam, (int)pixelOpMaskRed);
        spriteRenderer.sharedMaterial.SetColor(SpriteColorHelper.ShaderColorRedParam, colorMaskRed);
        spriteRenderer.sharedMaterial.SetTexture(SpriteColorHelper.ShaderMaskRedParam, textureMaskRed);
        spriteRenderer.sharedMaterial.SetVector(SpriteColorHelper.ShaderUVRedParam, textureMaskRedUVParams);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderUVAngleRedParam, textureMaskRedUVAngle * Mathf.Deg2Rad);

        // #2
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderStrengthGreenParam, strengthMaskGreen);
        spriteRenderer.sharedMaterial.SetInt(SpriteColorHelper.ShaderPixelOpGreenParam, (int)pixelOpMaskGreen);
        spriteRenderer.sharedMaterial.SetColor(SpriteColorHelper.ShaderColorGreenParam, colorMaskGreen);
        spriteRenderer.sharedMaterial.SetTexture(SpriteColorHelper.ShaderMaskGreenParam, textureMaskGreen);
        spriteRenderer.sharedMaterial.SetVector(SpriteColorHelper.ShaderUVGreenParam, textureMaskGreenUVParams);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderUVAngleGreenParam, textureMaskGreenUVAngle * Mathf.Deg2Rad);

        // #3
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderStrengthBlueParam, strengthMaskBlue);
        spriteRenderer.sharedMaterial.SetInt(SpriteColorHelper.ShaderPixelOpBlueParam, (int)pixelOpMaskBlue);
        spriteRenderer.sharedMaterial.SetColor(SpriteColorHelper.ShaderColorBlueParam, colorMaskBlue);
        spriteRenderer.sharedMaterial.SetTexture(SpriteColorHelper.ShaderMaskBlueParam, textureMaskBlue);
        spriteRenderer.sharedMaterial.SetVector(SpriteColorHelper.ShaderUVBlueParam, textureMaskBlueUVParams);
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderUVAngleBlueParam, textureMaskBlueUVAngle * Mathf.Deg2Rad);
      }
		}
	}
}