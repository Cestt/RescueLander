///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Masks 3"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

	_MaskTex("Mask (RGB)", 2D) = "black" {}

	_MaskRedTex("Mask (RGB)", 2D) = "white" {}
	_MaskGreenTex("Mask (RGB)", 2D) = "white" {}
	_MaskBlueTex("Mask (RGB)", 2D) = "white" {}

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

      uniform float _StrengthRed = 1.0f;
	  uniform int _PixelOpRed = 0;
      uniform fixed4 _ColorRed;
	  uniform fixed4 _UVRedTexParams;
	  uniform float _UVRedTexAngle;

      uniform float _StrengthGreen = 1.0f;
	  uniform int _PixelOpGreen = 0;
      uniform fixed4 _ColorGreen;
	  uniform fixed4 _UVGreenTexParams;
	  uniform float _UVGreenTexAngle;

      uniform float _StrengthBlue = 1.0f;
	  uniform int _PixelOpBlue = 0;
      uniform fixed4 _ColorBlue;
	  uniform fixed4 _UVBlueTexParams;
	  uniform float _UVBlueTexAngle;

      uniform sampler2D _MainTex;
      uniform sampler2D _MaskTex;
	  uniform sampler2D _MaskRedTex;
	  uniform sampler2D _MaskGreenTex;
	  uniform sampler2D _MaskBlueTex;

	  inline fixed2 UVCoordOp(fixed2 uv, float2 scale, float2 velocity, float angle)
	  {
	    float cosAngle = cos(angle);
		float sinAngle = sin(angle);

	    uv = mul(uv, float2x2(cosAngle, -sinAngle, sinAngle, cosAngle));
        uv *= scale;
		uv += velocity * _Time.y;

		return uv;
	  }

      float4 frag(v2f i) : COLOR
      {
        // Diffuse and renderer color.
        float4 pixel = tex2D(_MainTex, i.texcoord) * i.color;

        // Mask (R = Mask red, G = Mask green, B = Mask blue).
        float3 mask = tex2D(_MaskTex, i.texcoord).rgb;

        float3 colorMaskR = float3(0.0, 0.0, 0.0);
        float3 colorMaskG = float3(0.0, 0.0, 0.0);
        float3 colorMaskB = float3(0.0, 0.0, 0.0);

		float3 colorMask = float3(0.0, 0.0, 0.0);

		// Red mask.
		if (mask.r > 0.0)
		{
		  colorMaskR = mask.r * _StrengthRed;
		  colorMask += PixelOp(_PixelOpRed, pixel.rgb, _ColorRed.rgb * colorMaskR * tex2D(_MaskRedTex, UVCoordOp(i.texcoord, _UVRedTexParams.xy, _UVRedTexParams.zw, _UVRedTexAngle)).rgb);
		}

		// Green mask.
		if (mask.g > 0.0)
		{
		  colorMaskG = mask.g * _StrengthGreen;
		  colorMask += PixelOp(_PixelOpGreen, pixel.rgb, _ColorGreen.rgb * colorMaskG * tex2D(_MaskGreenTex, UVCoordOp(i.texcoord, _UVGreenTexParams.xy, _UVGreenTexParams.zw, _UVGreenTexAngle)).rgb);
		}

		// Blue mask.
		if (mask.b > 0.0)
		{
		  colorMaskB = mask.b * _StrengthBlue;
		  colorMask += PixelOp(_PixelOpBlue, pixel.rgb, _ColorBlue.rgb * colorMaskB * tex2D(_MaskBlueTex, UVCoordOp(i.texcoord, _UVBlueTexParams.xy, _UVBlueTexParams.zw, _UVBlueTexAngle)).rgb);
		}

        float3 final = lerp(pixel.rgb, colorMask, colorMaskR + colorMaskG + colorMaskB);

        return float4(lerp(pixel.rgb, final, _Strength) * pixel.a, pixel.a);
      }
	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}