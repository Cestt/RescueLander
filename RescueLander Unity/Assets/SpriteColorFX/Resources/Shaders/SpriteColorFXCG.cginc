///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Luminance.
inline float Luminance601(float3 pixel)
{
  return dot(float3(0.299f, 0.587f, 0.114f), pixel);
}

// RGB <-> HSV http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
inline float3 RGB2HSV(float3 c)
{
  const float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
  const float Epsilon = 1.0e-10;

  float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
  float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

  float d = q.x - min(q.w, q.y);

  return float3(abs(q.z + (q.w - q.y) / (6.0 * d + Epsilon)), d / (q.x + Epsilon), q.x);
}

inline float3 HSV2RGB(float3 c)
{
  const float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
  float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);

  return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

// 1D rand.
inline float Rand1(float value)
{
  return frac(sin(value) * 43758.5453123);
}

// 2D rand.
inline float Rand2(float2 value)
{
  return frac(sin(dot(value * 0.123, float2(12.9898, 78.233))) * 43758.5453);
}

// 3D rand.
inline float Rand3(float3 value)
{
  return frac(sin(dot(value, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
}

// Additive.
inline float3 Additive(float3 s, float3 d)
{
  return s + d;
}

// Color burn.
inline float3 Burn(float3 s, float3 d)
{
  return 1.0 - (1.0 - d) / s;
}

// Color.
float3 Color(float3 s, float3 d)
{
  s = RGB2HSV(s);
  s.z = RGB2HSV(d).z;

  return HSV2RGB(s);
}

// Darken.
inline float3 Darken(float3 s, float3 d)
{
  return min(s, d);
}

// Darker color.
inline float3 Darker(float3 s, float3 d)
{
  return (Luminance601(s) < Luminance601(d)) ? s : d;
}

// Difference.
inline float3 Difference(float3 s, float3 d)
{
  return abs(d - s);
}

// Divide.
inline float3 Divide(float3 s, float3 d)
{
  return (d > 0.0) ? s / d : s;
}

// Color dodge.
inline float3 Dodge(float3 s, float3 d)
{
  return d / (1.0 - s);
}

// HardMix.
inline float3 HardMix(float3 s, float3 d)
{
  return floor(s + d);
}

// Hue.
float3 Hue(float3 s, float3 d)
{
  d = RGB2HSV(d);
  d.x = RGB2HSV(s).x;

  return HSV2RGB(d);
}

// HardLight.
float3 HardLight(float3 s, float3 d)
{
  return (s < 0.5) ? 2.0 * s * d : 1.0 - 2.0 * (1.0 - s) * (1.0 - d);
}

// Lighten.
inline float3 Lighten(float3 s, float3 d)
{
  return max(s, d);
}

// Lighter color.
inline float3 Lighter(float3 s, float3 d)
{
  return (Luminance601(s) > Luminance601(d)) ? s : d;
}

// Multiply.
inline float3 Multiply(float3 s, float3 d)
{
  return s * d;
}

// Overlay.
float3 Overlay(float3 s, float3 d)
{
  return (s > 0.5) ? 1.0 - 2.0 * (1.0 - s) * (1.0 - d) : 2.0 * s * d;
}

// Screen.
inline float3 Screen(float3 s, float3 d)
{
  return s + d - s * d;
}

// Solid.
inline float3 Solid(float3 s, float3 d)
{
  return d;
}

// Soft light.
float3 SoftLight(float3 s, float3 d)
{
  return (1.0 - s) * s * d + s * (1.0 - (1.0 - s) * (1.0 - d));
}

// Pin light.
float3 PinLight(float3 s, float3 d)
{
  return (2.0 * s - 1.0 > d) ? 2.0 * s - 1.0 : (s < 0.5 * d) ? 2.0 * s : d;
}

// Saturation.
float3 Saturation(float3 s, float3 d)
{
  d = RGB2HSV(d);
  d.y = RGB2HSV(s).y;

  return HSV2RGB(d);
}

// Subtract.
inline float3 Subtract(float3 s, float3 d)
{
  return s - d;
}

// VividLight.
float3 VividLight(float3 s, float3 d)
{
  return (s < 0.5) ? (s > 0.0 ? 1.0 - (1.0 - d) / (2.0 * s) : s) : (s < 1.0 ? d / (2.0 * (1.0 - s)) : s);
}

// Luminosity.
float3 Luminosity(float3 s, float3 d)
{
  float dLum = Luminance601(d);
  float sLum = Luminance601(s);

  float lum = sLum - dLum;

  float3 c = d + lum;
  float minC = min(min(c.r, c.g), c.b);
  float maxC = max(max(c.r, c.b), c.b);

  if (minC < 0.0)
    return sLum + ((c - sLum) * sLum) / (sLum - minC);
  else if (maxC > 1.0)
    return sLum + ((c - sLum) * (1.0 - sLum)) / (maxC - sLum);

  return c;
}

// Dissolve color pixel shader.
#define DISSOLVE_COLOR_FRAG(PixelOp) \
  sampler2D _MainTex; \
  sampler2D _DissolveTex; \
  \
  float _DissolveAmount; \
  float _DissolveLineWitdh; \
  fixed4 _DissolveLineColor; \
  float _DissolveUVScale; \
  float _DissolveInverseOne; \
  float _DissolveInverseTwo; \
  \
  float4 frag(v2f i) : COLOR \
  { \
    float4 pixel = tex2D(_MainTex, i.texcoord) * i.color; \
  \
    float4 dissolve = _DissolveInverseOne - tex2D(_DissolveTex, i.texcoord * _DissolveUVScale) * _DissolveInverseTwo; \
  \
    int isClear = int(dissolve.r + _DissolveAmount); \
  \
    float3 border = lerp(0.0, _DissolveLineColor.rgb > 0.0 ? PixelOp(pixel.rgb, _DissolveLineColor.rgb) : pixel.rgb, isClear); \
  \
    return float4(lerp(border, pixel.rgb, int(dissolve.r + _DissolveAmount - _DissolveLineWitdh)) * pixel.a, lerp(0.0, 1.0, isClear) * pixel.a); \
  }

// Dissolve texture pixel shader.
#define DISSOLVE_TEXTURE_FRAG(PixelOp) \
  sampler2D _MainTex; \
  sampler2D _DissolveTex; \
  sampler2D _BorderTex; \
  \
  float _DissolveAmount; \
  float _DissolveLineWitdh; \
  float _DissolveUVScale; \
  float _DissolveInverseOne; \
  float _DissolveInverseTwo; \
  float _BorderUVScale; \
  \
  float4 frag(v2f i) : COLOR \
  { \
    float4 pixel = tex2D(_MainTex, i.texcoord) * i.color; \
  \
    fixed4 pixelBorder = tex2D(_BorderTex, i.texcoord * _BorderUVScale) * i.color; \
  \
    float4 dissolve = _DissolveInverseOne - tex2D(_DissolveTex, i.texcoord * _DissolveUVScale) * _DissolveInverseTwo; \
  \
    int isClear = int(dissolve.r + _DissolveAmount); \
  \
    float3 border = lerp(0.0, pixelBorder.rgb > 0.0 ? PixelOp(pixel.rgb, pixelBorder.rgb) : pixel.rgb, isClear); \
  \
    return float4(lerp(border, pixel.rgb, int(dissolve.r + _DissolveAmount - _DissolveLineWitdh)) * pixel.a, lerp(0.0, 1.0, isClear) * pixel.a); \
  }

// Masks3 pixel shader.
#define MASKS3_FRAG(PixelOp) \
  float _Strength = 1.0f; \
  \
  float _StrengthRed = 1.0f; \
  fixed4 _ColorRed; \
  fixed4 _UVRedTexParams; \
  float _UVRedTexAngle; \
  \
  float _StrengthGreen = 1.0f; \
  fixed4 _ColorGreen; \
  fixed4 _UVGreenTexParams; \
  float _UVGreenTexAngle; \
  \
  float _StrengthBlue = 1.0f; \
  fixed4 _ColorBlue; \
  fixed4 _UVBlueTexParams; \
  float _UVBlueTexAngle; \
  \
  sampler2D _MainTex; \
  sampler2D _MaskTex; \
  sampler2D _MaskRedTex; \
  sampler2D _MaskGreenTex; \
  sampler2D _MaskBlueTex; \
  \
  inline fixed2 UVCoordOp(fixed2 uv, float2 scale, float2 velocity, float angle) \
  { \
    float cosAngle = cos(angle); \
    float sinAngle = sin(angle); \
  \
    uv = mul(uv, float2x2(cosAngle, -sinAngle, sinAngle, cosAngle)); \
    uv *= scale; \
    uv += velocity * _Time.y; \
  \
    return uv; \
  } \
  \
  float4 frag(v2f i) : COLOR \
  { \
    float4 pixel = tex2D(_MainTex, i.texcoord) * i.color; \
    float3 mask = tex2D(_MaskTex, i.texcoord).rgb; \
  \
    float3 colorMaskR = float3(0.0, 0.0, 0.0); \
    float3 colorMaskG = float3(0.0, 0.0, 0.0); \
    float3 colorMaskB = float3(0.0, 0.0, 0.0); \
  \
    float3 colorMask = float3(0.0, 0.0, 0.0); \
  \
    if (mask.r > 0.0) \
    { \
	  colorMaskR = mask.r * _StrengthRed; \
      colorMask += _ColorRed.rgb * colorMaskR * tex2D(_MaskRedTex, UVCoordOp(i.texcoord, _UVRedTexParams.xy, _UVRedTexParams.zw, _UVRedTexAngle)).rgb; \
    } \
  \
    if (mask.g > 0.0) \
    { \
      colorMaskG = mask.g * _StrengthGreen; \
      colorMask += _ColorGreen.rgb * colorMaskG * tex2D(_MaskGreenTex, UVCoordOp(i.texcoord, _UVGreenTexParams.xy, _UVGreenTexParams.zw, _UVGreenTexAngle)).rgb; \
    } \
  \
    if (mask.b > 0.0) \
    { \
      colorMaskB = mask.b * _StrengthBlue; \
      colorMask += _ColorBlue.rgb * colorMaskB * tex2D(_MaskBlueTex, UVCoordOp(i.texcoord, _UVBlueTexParams.xy, _UVBlueTexParams.zw, _UVBlueTexAngle)).rgb; \
    } \
  \
    float3 final = lerp(pixel.rgb, PixelOp(pixel.rgb, colorMask), colorMaskR + colorMaskG + colorMaskB); \
  \
    return float4(lerp(pixel.rgb, final, _Strength) * pixel.a, pixel.a); \
 }

// Common vertex shader.

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

fixed4 _Color;

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
