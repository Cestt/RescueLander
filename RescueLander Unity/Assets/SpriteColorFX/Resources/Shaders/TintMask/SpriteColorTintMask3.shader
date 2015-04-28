///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Tint Mask 3"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

	_MaskTex("Mask (RGB)", 2D) = "black" {}

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

      struct appdata_t
      {
        float4 vertex   : POSITION;
        float4 color    : COLOR;
        float2 texcoord : TEXCOORD0;
      };

      struct v2f
      {
        float4 vertex   : SV_POSITION;
        fixed4 color    : COLOR;
        fixed2 texcoord : TEXCOORD0;
      };

      uniform fixed4 _Color;

      uniform float _Strength = 1.0f;

      uniform float _StrengthRed = 1.0f;
	  uniform fixed4 _ColorRed;

      uniform float _StrengthGreen = 1.0f;
	  uniform fixed4 _ColorGreen;

      uniform float _StrengthBlue = 1.0f;
	  uniform fixed4 _ColorBlue;

      v2f vert(appdata_t i)
      {
        v2f o;
        o.vertex = mul(UNITY_MATRIX_MVP, i.vertex);
        o.texcoord = i.texcoord;
        o.color = i.color * _Color;
#ifdef PIXELSNAP_ON
        o.vertex = UnityPixelSnap(o.vertex);
#endif

        return o;
      }

      uniform sampler2D _MainTex;
      uniform sampler2D _MaskTex;

      float4 frag(v2f i) : COLOR
      {
	    // Diffuse and renderer color.
	    float4 pixel = tex2D(_MainTex, i.texcoord) * i.color;

	    // Mask (R = Mask red, G = Mask green, B = Mask blue).
	    float3 mask = tex2D(_MaskTex, i.texcoord).rgb;

		float3 colorMaskR = mask.r * _StrengthRed;
		float3 colorMaskG = mask.g * _StrengthGreen;
		float3 colorMaskB = mask.b * _StrengthBlue;

		float3 colorMask = (_ColorRed.rgb * colorMaskR) + (_ColorGreen.rgb * colorMaskG) + (_ColorBlue.rgb * colorMaskB);

		float3 final = lerp(pixel.rgb, pixel.rgb * colorMask, colorMaskR + colorMaskG + colorMaskB);

		return float4(lerp(pixel.rgb, final, _Strength) * pixel.a, pixel.a);
	  }
	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}