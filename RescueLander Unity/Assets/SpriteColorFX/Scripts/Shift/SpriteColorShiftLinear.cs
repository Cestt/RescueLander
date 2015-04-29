///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Color shift linear.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("Sprite Color FX/Color Shift Linear")]
  public sealed class SpriteColorShiftLinear : MonoBehaviour
  {
    /// <summary>
    /// Red shift channel.
    /// </summary>
    public Vector2 redShift = Vector2.zero;

    /// <summary>
    /// Green shift channel.
    /// </summary>
    public Vector2 greenShift = Vector2.zero;

    /// <summary>
    /// Blue shift channel.
    /// </summary>
    public Vector2 blueShift = Vector2.zero;

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
        Shader shader = Resources.Load<Shader>(@"Shaders/Shift/SpriteColorShiftLinear");

        if (shader != null)
        {
          spriteRenderer.sharedMaterial = new Material(shader);
          spriteRenderer.sharedMaterial.name = @"Sprite/SpriteColorShiftLinear";
        }
        else
        {
          Debug.LogWarning(@"Failed to load necessary files, SpriteColorShift disabled.");

          this.enabled = false;
        }
      }
      else
      {
        Debug.LogWarning(string.Format("'{0}' without SpriteRenderer, SpriteColorShift disabled.", gameObject.name));

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
        spriteRenderer.sharedMaterial.SetFloat("_RedShiftX", redShift.x);
        spriteRenderer.sharedMaterial.SetFloat("_RedShiftY", redShift.y);

        spriteRenderer.sharedMaterial.SetFloat("_GreenShiftX", greenShift.x);
        spriteRenderer.sharedMaterial.SetFloat("_GreenShiftY", greenShift.y);

        spriteRenderer.sharedMaterial.SetFloat("_BlueShiftX", blueShift.x);
        spriteRenderer.sharedMaterial.SetFloat("_BlueShiftY", blueShift.y);

        spriteRenderer.sharedMaterial.SetFloat("_NoiseAmount", noiseAmount * 0.1f);
        spriteRenderer.sharedMaterial.SetFloat("_NoiseSpeed", noiseSpeed * 0.005f);
      }
    }
  }
}
