///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Dissolve Border Texture Subtract"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

    _DissolveTex("Dissolve (RGB)", 2D) = "white" {}

    _BorderTex("Border (RGB)", 2D) = "white" {}

	_DissolveAmount("Dissolve amount", Range(0.0, 1.0)) = 0.0

    _DissolveLineWitdh("Dissolve line size", Range(0.0, 0.2)) = 0.1
    
	_DissolveUVScale("Dissolve UV scale", Range(0.1, 5.0)) = 1.0

	_BorderUVScale("Border UV scale", Range(0.1, 5.0)) = 1.0

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
      #pragma target 3.0

	  #include "UnityCG.cginc"
      #include "../SpriteColorFXCG.cginc"

	  DISSOLVE_TEXTURE_FRAG(Subtract)

	  ENDCG
	}
  }

  Fallback "Sprites/Default"
}