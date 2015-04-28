﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Utilities for Sprite Color FX.
  /// </summary>
  public static class SpriteColorHelper
  {
    /// <summary>
    /// Pixel color operations.
    /// </summary>
    public enum PixelOp
    {
      Solid,
      Additive,
      Subtract,
      Multiply,
      Divide,
      Darken,
      Lighten,
      Screen,
      Dodge,
      HardMix,
      Difference,
    }

    // Shaders params.
    public static readonly string ShaderStrengthParam = @"_Strength";
    public static readonly string ShaderGammaCorrectParam = @"_GammaCorrect";
    public static readonly string ShaderUVScrollParam = @"_UVScroll";
    public static readonly string ShaderInvertLumParam = @"_InvertLum";
    public static readonly string ShaderLumRangeMinParam = @"_LumRangeMin";
    public static readonly string ShaderLumRangeMaxParam = @"_LumRangeMax";
    public static readonly string ShaderRampIdxParam = @"_RampIdx";
    public static readonly string ShaderRampRedIdx = @"_RampRedIdx";
    public static readonly string ShaderRampGreenIdx = @"_RampGreenIdx";
    public static readonly string ShaderRampBlueIdx = @"_RampBlueIdx";
    public static readonly string ShaderMaskTex = @"_MaskTex";
  }
}