﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Shift Linear"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

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

      // Define this add noise effect.
      #define USE_NOISE

	  uniform float _NoiseAmount = 0.0;
	  uniform float _NoiseSpeed = 1.0;

      uniform float _RedShiftX = 0.0;
      uniform float _RedShiftY = 0.0;

      uniform float _GreenShiftX = 0.0;
      uniform float _GreenShiftY = 0.0;

      uniform float _BlueShiftX = 0.0;
      uniform float _BlueShiftY = 0.0;

      uniform sampler2D _MainTex;

      float4 frag(v2f i) : COLOR
      {
        float2 uvR = float2(-_RedShiftX, _RedShiftY);
        float2 uvG = float2(-_GreenShiftX, _GreenShiftY);
        float2 uvB = float2(-_BlueShiftX, _BlueShiftY);

#ifdef USE_NOISE
		float randSin = 1.0 - (Rand1(_SinTime.x * _NoiseSpeed) * 2.0);
        float randCos = 1.0 - (Rand1(_CosTime.x * _NoiseSpeed) * 2.0);

		uvR += float2(randSin, randCos) * _NoiseAmount;
		uvB += float2(randCos, randSin) * _NoiseAmount;
#endif
        float2 red = tex2D(_MainTex, i.texcoord + uvR).ra;
        float2 green = tex2D(_MainTex, i.texcoord + uvG).ga;
        float2 blue = tex2D(_MainTex, i.texcoord + uvB).ba;

        float4 final = float4(red.x * red.y, green.x * green.y, blue.x * blue.y, (red.y + green.y + blue.y) * 0.333);

		return final;
	  }
	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}