///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Ramp"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

    // Color ramps 'Resources/Textures/SpriteColorRamps.png'.
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

      uniform float _Strength = 1.0f;

      uniform float _RampIdx = 0.0f;

      uniform float _GammaCorrect = 1.2f;

      uniform float _UVScroll = 0.0f;

      uniform float _InvertLum = 1.0f;

      uniform float _LumRangeMin = 0.0f;
      uniform float _LumRangeMax = 1.0f;

      uniform sampler2D _MainTex;
      uniform sampler2D _RampsTex;

      float4 frag(v2f i) : COLOR
      {
	    // Diffuse and Renderer color.
	    float4 pixel = tex2D(_MainTex, i.texcoord) * i.color;

	    // Gamma correct.
	    pixel = pow(pixel, _GammaCorrect);

	    // Luminance.
	    float lum = Luminance601(pixel.rgb) * _InvertLum;

	    pixel = pow(pixel, 1.0f / _GammaCorrect);

	    // Luminance range.
	    lum = (1.0f / (_LumRangeMax - _LumRangeMin)) * (lum - _LumRangeMin);

	    // Ramp.
	    float3 final = tex2D(_RampsTex, float2(lum + _UVScroll, _RampIdx)).rgb;

	    return float4(lerp(pixel.rgb, final.rgb, _Strength) * pixel.a, pixel.a);
	  }
	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}