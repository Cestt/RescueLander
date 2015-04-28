///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Color shift radial.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("Sprite Color FX/Color Shift Radial")]
  public sealed class SpriteColorShiftRadial : MonoBehaviour
  {
    /// <summary>
    /// Effect strength [0..1].
    /// </summary>
    public float strength = 0.0f;

    /// <summary>
    /// Noise amount [0..1].
    /// </summary>
    public float noiseAmount = 0.0f;

    /// <summary>
    /// Noise speed [0..1].
    /// </summary>
    public float noiseSpeed = 0.0f;

    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
      {
        Shader shader = Resources.Load<Shader>(@"Shaders/Shift/SpriteColorShiftRadial");

        if (shader != null)
        {
          spriteRenderer.sharedMaterial = new Material(shader);
          spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorShiftRadial";
        }
        else
        {
          Debug.LogWarning(@"Failed to load necessary files, SpriteColorShiftRadial disabled.");

          this.enabled = false;
        }
      }
      else
      {
        Debug.LogWarning(string.Format("'{0}' without SpriteRenderer, SpriteColorShiftRadial disabled.", gameObject.name));

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
        spriteRenderer.sharedMaterial.SetFloat(SpriteColorHelper.ShaderStrengthParam, strength * 0.1f);

        spriteRenderer.sharedMaterial.SetFloat("_NoiseAmount", noiseAmount * 0.1f);
        spriteRenderer.sharedMaterial.SetFloat("_NoiseSpeed", noiseSpeed * 0.005f);
      }
    }
  }
}
