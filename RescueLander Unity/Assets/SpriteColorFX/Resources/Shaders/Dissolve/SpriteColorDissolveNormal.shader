///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Dissolve Normal"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

    _DissolveTex("Dissolve (RGB)", 2D) = "white" {}

	_DissolveAmount("Dissolve amount", Range(0.0, 1.0)) = 0.0

	_DissolveUVScale("Dissolve UV scale", Range(0.1, 5.0)) = 1.0

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

	Name "Sprite Color Dissolve"
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

      uniform sampler2D _MainTex;
      uniform sampler2D _DissolveTex;

      uniform float _DissolveAmount;
	  uniform float _DissolveUVScale;
      uniform float _DissolveInverseOne;
      uniform float _DissolveInverseTwo;

      float4 frag(v2f i) : COLOR
      {
		float4 pixel = tex2D(_MainTex, i.texcoord) * i.color;
        
		float4 dissolve = _DissolveInverseOne - tex2D(_DissolveTex, i.texcoord * _DissolveUVScale) * _DissolveInverseTwo;

		float4 final = lerp(0.0, pixel, int(dissolve.r + _DissolveAmount));

		return float4(final.rgb * pixel.a, final.a * pixel.a);
	  }
	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}