///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Ramp Mask"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

	_MaskTex("Mask (RGBA)", 2D) = "black" {}

    // Color ramps 'Resources/Textures/SpriteColorRamp.png'.
    _RampsTex("Ramps (RGB)", 2D) = "" {}

    _Color("Tint", Color) = (1, 1, 1, 1)

	[MaterialToggle]
	PixelSnap("Pixel snap", Float) = 0
  }

  // Techniques (http://unity3d.com/support/documentation/Components/SL-SubShader.html).
  SubShader
  {
    // Tags (http://docs.unity3d.com/Manual/SL-CullAndDepth.html).
    Tags
	{
      "Queue" = "Transparent" 
      "IgnoreProjector" = "True" 
      "RenderType" = "Transparent" 
      "PreviewType" = "Plane"
      "CanUseSpriteAtlas" = "True"
	}

    Cull Off
    Lighting Off
    ZWrite Off
    Fog { Mode Off }
    Blend One OneMinusSrcAlpha

    // Pass 0 (http://docs.unity3d.com/Manual/SL-Pass.html).
	Pass
	{
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma fragmentoption ARB_precision_hint_fastest
	  #pragma multi_compile DUMMY PIXELSNAP_ON
      #pragma target 2.0

	  #include "UnityCG.cginc"
      #include "../SpriteColorFXCG.cginc"

      float _Strength = 1.0f;

      float _RampRedIdx = 0.0f;
      float _RampGreenIdx = 0.0f;
      float _RampBlueIdx = 0.0f;

      float _GammaCorrect = 1.2f;

      float _UVScroll = 0.0f;

      float _InvertLum = 0.0f;

      float _LumRangeMin = 0.0f;
      float _LumRangeMax = 1.0f;

      sampler2D _MainTex;
      sampler2D _MaskTex;
      sampler2D _RampsTex;

      float4 frag(v2f i) : COLOR
      {
	    // Diffuse and renderer color.
	    float4 pixel = tex2D(_MainTex, i.texcoord) * i.color;

	    // Masks (R = Ramp Red, G = Ramp Green, B = Ramp Blue, A = Intensity).
	    float4 mask = tex2D(_MaskTex, i.texcoord);

	    // Gamma correct.
	    pixel = pow(pixel, _GammaCorrect);

	    // Luminance.
	    float lum = Luminance601(pixel.rgb) * _InvertLum;

	    pixel = pow(pixel, 1.0f / _GammaCorrect);

	    // Luminance range.
	    lum = (1.0f / (_LumRangeMax - _LumRangeMin)) * (lum - _LumRangeMin);

	    // Ramps.
	    float3 rampRed = tex2D(_RampsTex, float2(lum + _UVScroll, _RampRedIdx)).rgb * mask.r;
	    float3 rampGreen = tex2D(_RampsTex, float2(lum + _UVScroll, _RampGreenIdx)).rgb * mask.g;
	    float3 rampBlue = tex2D(_RampsTex, float2(lum + _UVScroll, _RampBlueIdx)).rgb * mask.b;

		float3 final = lerp(pixel.rgb, rampRed + rampGreen + rampBlue, mask.a);

	    return float4(lerp(pixel.rgb, final.rgb, _Strength) * pixel.a, pixel.a);
	  }
	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}