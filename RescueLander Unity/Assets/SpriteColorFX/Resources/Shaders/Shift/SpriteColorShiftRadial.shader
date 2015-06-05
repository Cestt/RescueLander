///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Shift Radial"
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

      float _Strength = 0.0;
      float _NoiseAmount = 0.0;
      float _NoiseSpeed = 0.0;

      sampler2D _MainTex;

      float4 frag(v2f i) : COLOR
      {
		float shift = _Strength;
#ifdef USE_NOISE
		shift += _NoiseAmount * Rand1(_SinTime.w * _NoiseSpeed);
#endif
        shift *= distance(i.texcoord, 0.5);

        float2 colorVec = normalize(i.texcoord - 0.5);

        float2 uvR = float2(i.texcoord.x - (colorVec.x * shift), i.texcoord.y - (colorVec.y * shift));
        float2 uvG = float2(i.texcoord.x, i.texcoord.y);
        float2 uvB = float2(i.texcoord.x + (colorVec.x * shift), i.texcoord.y + (colorVec.y * shift));

		float2 red = tex2D(_MainTex, uvR).ra * i.color.r;
		float2 green = tex2D(_MainTex, uvG).ga * i.color.g;
		float2 blue = tex2D(_MainTex, uvB).ba * i.color.b;
 
        float4 final = float4(red.x * red.y, green.x * green.y, blue.x * blue.y, (red.y + green.y + blue.y) * 0.333);

        return final;
	  }
	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}