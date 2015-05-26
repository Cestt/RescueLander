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

// Burn.
inline float3 Burn(float3 s, float3 d)
{
  return 1.0 - (1.0 - d) / s;
}

// Color.
inline float3 Color(float3 s, float3 d)
{
  d = RGB2HSV(d);
  d.z = RGB2HSV(s).z;

  return HSV2RGB(d);
}

// Darken.
inline float3 Darken(float3 s, float3 d)
{
  return min(s, d);
}

// Darker.
inline float3 Darker(float3 s, float3 d)
{
  return (s.r + s.g + s.b < d.r + d.g + d.b) ? s : d;
}

// Difference.
inline float3 Difference(float3 s, float3 d)
{
  return abs(d - s);
}

// Divide.
inline float3 Divide(float3 s, float3 d)
{
  return d / s;
}

// Dodge.
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
inline float3 Hue(float3 s, float3 d)
{
  d = RGB2HSV(d);
  d.x = RGB2HSV(s).x;

  return HSV2RGB(d);
}

// HardLight.
inline float3 HardLight(float3 s, float3 d)
{
  float3 color;
  color.r = (s.r < 0.5) ? 2.0 * s.r * d.r : 1.0 - 2.0 * (1.0 - s.r) * (1.0 - d.r);
  color.g = (s.g < 0.5) ? 2.0 * s.g * d.g : 1.0 - 2.0 * (1.0 - s.g) * (1.0 - d.g);
  color.b = (s.b < 0.5) ? 2.0 * s.b * d.b : 1.0 - 2.0 * (1.0 - s.b) * (1.0 - d.b);

  return color;
}

// Lighten.
inline float3 Lighten(float3 s, float3 d)
{
  return max(s, d);
}

// Lighter.
inline float3 Lighter(float3 s, float3 d)
{
  return (s.x + s.y + s.z > d.x + d.y + d.z) ? s : d;
}

// Multiply.
inline float3 Multiply(float3 s, float3 d)
{
  return s * d;
}

// Overlay.
inline float3 Overlay(float3 s, float3 d)
{
  float3 color;
  color.r = (d.r < 0.5) ? 2.0 * s.r * d.r : 1.0 - 2.0 * (1.0 - s.r) * (1.0 - d.r);
  color.g = (d.g < 0.5) ? 2.0 * s.g * d.g : 1.0 - 2.0 * (1.0 - s.g) * (1.0 - d.g);
  color.b = (d.b < 0.5) ? 2.0 * s.b * d.b : 1.0 - 2.0 * (1.0 - s.b) * (1.0 - d.b);

  return color;
}

// Screen.
inline float3 Screen(float3 s, float3 d)
{
  return s + d - s * d;
}

// SoftLight.
inline float3 SoftLight(float3 s, float3 d)
{
  float3 color;
  color.r = (s.r < 0.5) ? d.r - (1.0 - 2.0 * s.r) * d.r * (1.0 - d.r) : (d.r < 0.25) ? d.r + (2.0 * s.r - 1.0) * d.r * ((16.0 * d.r - 12.0) * d.r + 3.0) : d.r + (2.0 * s.r - 1.0) * (sqrt(d.r) - d.r);
  color.g = (s.g < 0.5) ? d.g - (1.0 - 2.0 * s.g) * d.g * (1.0 - d.g) : (d.g < 0.25) ? d.g + (2.0 * s.g - 1.0) * d.g * ((16.0 * d.g - 12.0) * d.g + 3.0) : d.r + (2.0 * s.g - 1.0) * (sqrt(d.g) - d.g);
  color.b = (s.b < 0.5) ? d.b - (1.0 - 2.0 * s.b) * d.b * (1.0 - d.b) : (d.b < 0.25) ? d.b + (2.0 * s.b - 1.0) * d.b * ((16.0 * d.b - 12.0) * d.b + 3.0) : d.b + (2.0 * s.b - 1.0) * (sqrt(d.b) - d.b);

  return color;
}

// PinLight.
inline float3 PinLight(float3 s, float3 d)
{
  float3 color;
  color.r = (2.0 * s.r - 1.0 > d.r) ? 2.0 * s.r - 1.0 : (s.r < 0.5 * d.r) ? 2.0 * s.r : d.r;
  color.g = (2.0 * s.g - 1.0 > d.g) ? 2.0 * s.g - 1.0 : (s.g < 0.5 * d.g) ? 2.0 * s.g : d.g;
  color.b = (2.0 * s.b - 1.0 > d.b) ? 2.0 * s.b - 1.0 : (s.b < 0.5 * d.b) ? 2.0 * s.b : d.b;

  return color;
}

// Saturation.
inline float3 Saturation(float3 s, float3 d)
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
inline float3 VividLight(float3 s, float3 d)
{
  float3 color;
  color.r = (s.r < 0.5) ? 1.0 - (1.0 - d.r) / (2.0 * s.r) : d.r / (2.0 * (1.0 - s.r));
  color.g = (s.g < 0.5) ? 1.0 - (1.0 - d.g) / (2.0 * s.g) : d.g / (2.0 * (1.0 - s.g));
  color.b = (s.b < 0.5) ? 1.0 - (1.0 - d.b) / (2.0 * s.b) : d.r / (2.0 * (1.0 - s.b));

  return color;
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

// Select pixel operation.
float3 PixelOp(int pixelOp, float3 s, float3 d)
{
  if (pixelOp == 0)
	return Additive(s, d);
  else if (pixelOp == 1)
	return Burn(s, d);
  else if (pixelOp == 2)
	return Color(s, d);
  else if (pixelOp == 3)
	return Darken(s, d);
  else if (pixelOp == 4)
	return Darker(s, d);
  else if (pixelOp == 5)
	return Difference(s, d);
  else if (pixelOp == 6)
	return Divide(s, d);
  else if (pixelOp == 7)
	return Dodge(s, d);
  else if (pixelOp == 8)
	return HardMix(s, d);
  else if (pixelOp == 9)
	return Hue(s, d);
  else if (pixelOp == 10)
	return HardLight(s, d);
  else if (pixelOp == 11)
	return Lighten(s, d);
  else if (pixelOp == 12)
	return Lighter(s, d);
  else if (pixelOp == 13)
	return Luminosity(s, d);
  else if (pixelOp == 14)
	return Multiply(s, d);
  else if (pixelOp == 15)
	return Overlay(s, d);
  else if (pixelOp == 16)
	return PinLight(s, d);
  else if (pixelOp == 17)
	return Saturation(s, d);
  else if (pixelOp == 18)
	return Screen(s, d);
  else if (pixelOp == 19)
	return d;
  else if (pixelOp == 20)
	return SoftLight(s, d);
  else if (pixelOp == 21)
	return Subtract(s, d);
  else if (pixelOp == 22)
	return VividLight(s, d);

  return s;
}

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
