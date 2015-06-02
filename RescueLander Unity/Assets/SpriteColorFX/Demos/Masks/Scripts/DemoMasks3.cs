///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorMasks3 Demo.
  /// </summary>
  [RequireComponent(typeof(SpriteColorMasks3))]
  public sealed class DemoMasks3 : MonoBehaviour
  {
    public bool showGUI = true;

    public bool changeColors = true;

    public bool changeBlendModes = true;

    private readonly float changeTime = 1.0f;

    private SpriteColorMasks3 spriteColorMask;

    private float timeToChange = 0.0f;

    private Texture[] textureColors = new Texture[3];

    private void OnEnable()
    {
      spriteColorMask = gameObject.GetComponent<SpriteColorMasks3>();
      if (spriteColorMask == null)
        this.enabled = false;
      else
      {
        if (changeBlendModes == true)
          ChangeBlendMode();

        if (changeColors == true)
          ChangeColors();

        timeToChange = Random.Range(0.0f, changeTime * 0.5f);

        UpdateTextureColors();
      }
    }

    private void Update()
    {
      timeToChange += Time.deltaTime;

      if (spriteColorMask.enabled == true && timeToChange >= changeTime)
      {
        if (changeBlendModes == true)
          ChangeBlendMode();

        if (changeColors == true)
          ChangeColors();

        timeToChange = 0.0f;
      }
    }

    private void OnGUI()
    {
      if (showGUI == false)
        return;

      Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

      float width = 150.0f;
      const float height = 150.0f;

      GUILayout.BeginArea(new Rect(screenPosition.x - (width * 0.5f), screenPosition.y - (height * 0.5f), width, height), GUI.skin.box);
      {
        spriteColorMask.enabled = GUILayout.Toggle(spriteColorMask.enabled, @" Enable masks");

        GUI.enabled = spriteColorMask.enabled;

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label(@"Strength", GUILayout.Width(50.0f));

          spriteColorMask.strength = GUILayout.HorizontalSlider(spriteColorMask.strength, 0.0f, 1.0f);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
          if (GUILayout.Button("<<") == true)
          {
            changeBlendModes = false;

            if (spriteColorMask.pixelOp > 0)
              spriteColorMask.SetPixelOp(spriteColorMask.pixelOp - 1);
            else
              spriteColorMask.SetPixelOp(SpriteColorHelper.PixelOp.VividLight);
          }

          GUILayout.FlexibleSpace();

          GUILayout.Label(spriteColorMask.pixelOp.ToString(), GUILayout.ExpandWidth(true));

          GUILayout.FlexibleSpace();

          if (GUILayout.Button(">>") == true)
          {
            changeBlendModes = false;

            if (spriteColorMask.pixelOp < SpriteColorHelper.PixelOp.VividLight)
              spriteColorMask.SetPixelOp(spriteColorMask.pixelOp + 1);
            else
              spriteColorMask.SetPixelOp(SpriteColorHelper.PixelOp.Additive);
          }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label(@"Mask #1", GUILayout.Width(50.0f));

          spriteColorMask.strengthMaskRed = GUILayout.HorizontalSlider(spriteColorMask.strengthMaskRed, 0.0f, 1.0f);

          GUILayout.Box(textureColors[0], GUILayout.Width(24.0f));
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label(@"Mask #2", GUILayout.Width(50.0f));

          spriteColorMask.strengthMaskGreen = GUILayout.HorizontalSlider(spriteColorMask.strengthMaskGreen, 0.0f, 1.0f);

          GUILayout.Box(textureColors[1], GUILayout.Width(24.0f));
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label(@"Mask #3", GUILayout.Width(50.0f));

          spriteColorMask.strengthMaskBlue = GUILayout.HorizontalSlider(spriteColorMask.strengthMaskBlue, 0.0f, 1.0f);

          GUILayout.Box(textureColors[2], GUILayout.Width(24.0f));
        }
        GUILayout.EndHorizontal();
      }
      GUILayout.EndArea();

      width = 200.0f;

      GUI.enabled = false;
      GUI.Label(new Rect(Screen.width - 275.0f, Screen.height - 22.0f, 200.0f, 42.0f), @"Art by www.charlieart.pl");
    }

    private void ChangeBlendMode()
    {
      System.Array values = System.Enum.GetValues(typeof(SpriteColorHelper.PixelOp));
      
      spriteColorMask.SetPixelOp((SpriteColorHelper.PixelOp)values.GetValue(Random.Range(0, values.Length)));
    }

    private void ChangeColors()
    {
      spriteColorMask.colorMaskRed = PrettyColor();
      spriteColorMask.colorMaskGreen = PrettyColor();
      spriteColorMask.colorMaskBlue = PrettyColor();

      UpdateTextureColors();
    }

    private Color PrettyColor()
    {
      Color color = new Color();

      color.r = Random.Range(-0.3f, 0.3f) + 0.5f;
      color.g = Random.Range(-0.3f, 0.3f) + 0.5f;
      color.b = Random.Range(-0.3f, 0.3f) + 0.5f;
      color.a = 1.0f;

      return color;
    }

    private void UpdateTextureColors()
    {
      textureColors[0] = MakeTexture(12, 12, spriteColorMask.colorMaskRed);
      textureColors[1] = MakeTexture(12, 12, spriteColorMask.colorMaskGreen);
      textureColors[2] = MakeTexture(12, 12, spriteColorMask.colorMaskBlue);
    }

    private Vector3 RGB2HSV(Color color)
    {
      Vector3 hsv = Vector3.zero;

      float r = color.r;
      float g = color.g;
      float b = color.b;

      float max = Mathf.Max(r, Mathf.Max(g, b));
      if (max <= 0.0f)
        return hsv;

      float min = Mathf.Min(r, Mathf.Min(g, b));
      float dif = max - min;

      if (max > min)
      {
        if (g == max)
          hsv.x = (b - r) / dif * 60.0f + 120.0f;
        else if (b == max)
          hsv.x = (r - g) / dif * 60.0f + 240.0f;
        else if (b > g)
          hsv.x = (g - b) / dif * 60.0f + 360.0f;
        else
          hsv.x = (g - b) / dif * 60.0f;

        if (hsv.x < 0.0f)
          hsv.x = hsv.x + 360.0f;
      }
      else
        hsv.x = 0.0f;

      hsv.x *= 1.0f / 360.0f;
      hsv.y = (dif / max) * 1.0f;
      hsv.z = max;

      return hsv;
    }

    public static Color HSV2RGB(Vector3 hsv)
    {
      float r = hsv.z;
      float g = hsv.z;
      float b = hsv.z;

      if (hsv.y != 0.0f)
      {
        float max = hsv.z;
        float dif = hsv.z * hsv.y;
        float min = hsv.z - dif;

        float h = hsv.x * 360.0f;

        if (h < 60.0f)
        {
          r = max;
          g = h * dif / 60.0f + min;
          b = min;
        }
        else if (h < 120.0f)
        {
          r = -(h - 120.0f) * dif / 60.0f + min;
          g = max;
          b = min;
        }
        else if (h < 180.0f)
        {
          r = min;
          g = max;
          b = (h - 120.0f) * dif / 60.0f + min;
        }
        else if (h < 240.0f)
        {
          r = min;
          g = -(h - 240.0f) * dif / 60.0f + min;
          b = max;
        }
        else if (h < 300.0f)
        {
          r = (h - 240.0f) * dif / 60.0f + min;
          g = min;
          b = max;
        }
        else if (h <= 360.0f)
        {
          r = max;
          g = min;
          b = -(h - 360.0f) * dif / 60.0f + min;
        }
        else
        {
          r = 0.0f;
          g = 0.0f;
          b = 0.0f;
        }
      }

      return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), 1.0f);
    }

    private Texture2D MakeTexture(int width, int height, Color col)
    {
      Color[] pix = new Color[width * height];
      for (int i = 0; i < pix.Length; ++i)
        pix[i] = col;

      Texture2D result = new Texture2D(width, height);
      result.SetPixels(pix);
      result.Apply();

      return result;
    }
  }
}
