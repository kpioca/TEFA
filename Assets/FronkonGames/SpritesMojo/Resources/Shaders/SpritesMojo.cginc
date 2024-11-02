///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Fronkon Games @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#ifndef SPRITESMOJO_INC
#define SPRITESMOJO_INC

static const half PI = 3.1415926535;
static const half Epsilon = 1.0e-10;
static const half3 Half3_Zero = (half3)0.0;
static const half3 Half3_Half = (half3)0.5;
static const half3 Half3_One = (half3)1.0;
static const half3 Half3_Two = (half3)2.0;
static const half3 LuminanceConstants = half3(0.2125, 0.7154, 0.0721);

#include "UnitySprites.cginc"

#if defined(_ALPHA_CLIP)
half _Cutoff;

#define ALPHA_CLIP(pixel, color) clip((pixel.a * color.a) - _Cutoff);
#else
#define ALPHA_CLIP(pixel, color)
#endif

struct appdata
{
  half4 vertex    : POSITION;
  half2 uv        : TEXCOORD0;
  half4 color     : COLOR;

  UNITY_VERTEX_OUTPUT_STEREO
};

struct VertexOutput
{
  half4 vertex    : SV_POSITION;
  half4 color     : COLOR;
  float2 uv       : TEXCOORD0;
#if defined(_ENABLE_GRABPASS)
  half4 grabPos   : TEXCOORD2;
#endif
#if FOG_ON
  UNITY_FOG_COORDS(6)
#endif
};

half4 _MainTex_ST, _MainTex_TexelSize;

#if defined(_ENABLE_GRABPASS)
  sampler2D _GrabTexture;
  float4 _GrabTexture_TexelSize;
#endif

half _Amount;

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

// 2D rand range.
inline float RandRange(float2 value, float min, float max)
{
  return min + Rand2(value) * (max - min);
}

// Saturation.
inline half3 Saturation(half3 pixel, half saturation)
{
  half luminance = Luminance(pixel);
  
  return (pixel - luminance) * saturation + luminance;
}

// Pure Hue to linear RGB.
inline half3 FG_HUEtoRGB(float hue)
{
  float R = abs(hue * 6.0 - 3.0) - 1.0;
  float G = 2.0 - abs(hue * 6.0 - 2.0);
  float B = 2.0 - abs(hue * 6.0 - 4.0);

  return saturate(half3(R, G, B));
}

// RGB to HCV (hue / chroma / value).
inline half3 FG_RGBtoHCV(half3 rgb)
{
  half4 bg = half4(rgb.bg, -1.0, 2.0 / 3.0);
  half4 gb = half4(rgb.gb, 0.0, -1.0 / 3.0);

  half4 p = (rgb.g < rgb.b) ? bg : gb;
  half4 xy = half4(p.xyw, rgb.r);
  half4 yz = half4(rgb.r, p.yzx);

  half4 q = (rgb.r < p.x) ? xy : yz;
  half c = q.x - min(q.w, q.y);
  half h = abs((q.w - q.y) / (6.0 * c + Epsilon) + q.z);

  return half3(h, c, q.x);
}

// RGB to HSV.
inline half3 FG_RGBtoHSV(half3 rgb)
{
  half3 hcv = FG_RGBtoHCV(rgb);
  half s = hcv.y / (hcv.z + Epsilon);

  return half3(hcv.x, s, hcv.z);
}

// HUE to RGB.
inline half3 FG_HUEtoRGB(half h)
{
  half r = abs(h * 6.0 - 3.0) - 1.0;
  half g = 2.0 - abs(h * 6.0 - 2.0);
  half b = 2.0 - abs(h * 6.0 - 4.0);

  return saturate(half3(r, g, b));
}

// HSV to RGB.
inline half3 FG_HSVtoRGB(half3 hsv)
{
  half3 rgb = FG_HUEtoRGB(hsv.x);

  return ((rgb - 1.0) * hsv.y + 1.0) * hsv.z;
}

// HSL to linear RGB.
inline half3 FG_HSLtoRGB(half3 hsl)
{
  half3 rgb = FG_HUEtoRGB(hsl.x);
  float C = (1.0 - abs(2.0 * hsl.z - 1.0)) * hsl.y;

  return (rgb - 0.5) * C + hsl.z;
}

// Linear rgb to HSL.
inline half3 FG_RGBtoHSL(half3 rgb)
{
  half3 HCV = FG_RGBtoHCV(rgb);
  float L = HCV.z - HCV.y * 0.5;
  float S = HCV.y / (1.0 - abs(L * 2.0 - 1.0) + 1e-10);
  
  return half3(HCV.x, S, L);
}

// RGB -> HSV http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
inline half3 FG_RGB2HSV(half3 c)
{
	const half4 K = half4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);

  half4 p = lerp(half4(c.bg, K.wz), half4(c.gb, K.xy), step(c.b, c.g));
  half4 q = lerp(half4(p.xyw, c.r), half4(c.r, p.yzx), step(p.x, c.r));

  half d = q.x - min(q.w, q.y);

	return half3(abs(q.z + (q.w - q.y) / (6.0 * d + Epsilon)), d / (q.x + Epsilon), q.x);
}

// HSV -> RGB http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
inline half3 FG_HSV2RGB(half3 c)
{
	const half4 K = half4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
  half3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);

	return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

inline half3 FG_Additive(half3 base, half3 blend)    { return base + blend; }
inline half3 FG_Average(half3 base, half3 blend)     { return (base + blend) * 0.5; }
inline half3 FG_ColorBurn(half3 base, half3 blend)   { return (blend == (half3)0.0) ? blend : max(((half3)1.0 - (((half3)1.0 - base) / blend)), (half3)0.0); }
inline half3 FG_ColorDodge(half3 base, half3 blend)  { return (blend == (half3)1.0) ? blend : min(base / ((half3)1.0 - blend), (half3)1.0); }
inline half3 FG_Darken(half3 base, half3 blend)      { return min(blend, base); }
inline half3 FG_Difference(half3 base, half3 blend)  { return abs(base - blend); }
inline half3 FG_Divide(half3 base, half3 blend)      { return min((half3)1.0, base / blend); }
inline half3 FG_Exclusion(half3 base, half3 blend)   { return base + blend - (half3)2.0 * base * blend; }
inline half3 FG_Reflect(half3 base, half3 blend)     { return (blend == (half3)1.0) ? blend : min(base * base / ((half3)1.0 - blend), (half3)1.0); }
inline half3 FG_Glow(half3 base, half3 blend)        { return FG_Reflect(blend, base); }
inline half3 FG_Overlay(half3 base, half3 blend)     { return base < (half3)0.5 ? ((half3)2.0 * base * blend) : ((half3)1.0 - (half3)2.0 * ((half3)1.0 - base) * ((half3)1.0 - blend)); }
inline half3 FG_HardLight(half3 base, half3 blend)   { return FG_Overlay(blend, base); }
inline half3 FG_VividLight(half3 base, half3 blend)  { return (blend < (half3)0.5) ? FG_ColorBurn(base, ((half3)2.0 * blend)) : FG_ColorDodge(base, ((half3)2.0 * (blend - (half3)0.5))); }
inline half3 FG_HardMix(half3 base, half3 blend)     { return (FG_VividLight(base, blend) < (half3)0.5) ? (half3)0.0 : (half3)1.0; }
inline half3 FG_Lighten(half3 base, half3 blend)     { return max(blend, base); }
inline half3 FG_LinearBurn(half3 base, half3 blend)  { return max(base + blend - (half3)1.0, (half3)0.0); }
inline half3 FG_LinearDodge(half3 base, half3 blend) { return min(base + blend, (half3)1.0); }
inline half3 FG_LinearLight(half3 base, half3 blend) { return blend < (half3)0.5 ? FG_LinearBurn(base, ((half3)2.0 * blend)) : FG_LinearDodge(base, ((half3)2.0 * (blend - (half3)0.5))); }
inline half3 FG_Multiply(half3 base, half3 blend)    { return base * blend; }
inline half3 FG_Negation(half3 base, half3 blend)    { return (half3)1.0 - abs((half3)1.0 - base - blend); }
inline half3 FG_Normal(half3 base, half3 blend)      { return blend; }
inline half3 FG_Phoenix(half3 base, half3 blend)     { return min(base, blend) - max(base, blend) + (half3)1.0; }
inline half3 FG_PinLight(half3 base, half3 blend)    { return (blend < (half3)0.5) ? FG_Darken(base, ((half3)2.0 * blend)) : FG_Lighten(base, ((half3)2.0 * (blend - (half3)0.5))); }
inline half3 FG_Screen(half3 base, half3 blend)      { return (half3)1.0 - (((half3)1.0 - base) * ((half3)1.0 - blend)); }
inline half3 FG_SoftLight(half3 base, half3 blend)   { return (blend < (half3)0.5) ? ((half3)2.0 * base * blend + base * base * ((half3)1.0 - (half3)2.0 * blend)) : (sqrt(base) * ((half3)2.0 * blend - (half3)1.0) + (half3)2.0 * base * ((half3)1.0 - blend)); }
inline half3 FG_Substract(half3 base, half3 blend)   { return max(base + blend - (half3)1.0, (half3)0.0); }
inline half3 FG_Subtract(half3 base, half3 blend)    { return max(base - blend - (half3)1.0, (half3)0.0); }

// Color blend ops.
inline half3 BlendColor(int op, half3 base, half3 blend)
{
  switch (op)
  {
    case 0:  return FG_Additive(base, blend);
    case 1:  return FG_Average(base, blend);
    case 2:  return FG_ColorBurn(base, blend);
    case 3:  return FG_ColorDodge(base, blend);
    case 4:  return FG_Darken(base, blend);
    case 5:  return FG_Difference(base, blend);
    case 6:  return FG_Exclusion(base, blend);
    case 7:  return FG_Reflect(base, blend);
    case 8:  return FG_Glow(base, blend);
    case 9:  return FG_Overlay(base, blend);
    case 10: return FG_HardLight(base, blend);
    case 11: return FG_VividLight(base, blend);
    case 12: return FG_HardMix(base, blend);
    case 13: return FG_Lighten(base, blend);
    case 14: return FG_LinearBurn(base, blend);
    case 15: return FG_LinearDodge(base, blend);
    case 16: return FG_LinearLight(base, blend);
    case 17: return FG_Multiply(base, blend);
    case 18: return FG_Negation(base, blend);
    case 19: return FG_Normal(base, blend);
    case 20: return FG_Phoenix(base, blend);
    case 21: return FG_PinLight(base, blend);
    case 22: return FG_Screen(base, blend);
    case 23: return FG_SoftLight(base, blend);
    case 24: return FG_Substract(base, blend);
    case 25: return FG_Subtract(base, blend);
    default: return blend;
  }
}

// UV ops.
inline half2 UVTransform(half2 uv, half scale, half2 velocity, half angle)
{
  half cosAngle = cos(angle);
  half sinAngle = sin(angle);

  uv = mul(uv, half2x2(cosAngle, -sinAngle, sinAngle, cosAngle));
  uv *= scale;
  uv += velocity * _Time.y;

  return uv;
}

inline half4 SampleSprite(float2 uv)
{
  half4 pixel = SampleSpriteTexture(uv);

#if ETC1_EXTERNAL_ALPHA
  pixel.a = tex2D(_AlphaTex, uv).r;
#endif
#if UNITY_COLORSPACE_GAMMA
  pixel.rgb = GammaToLinearSpace(pixel);
#endif  
  return pixel;
}

inline half4 SampleTexture(sampler2D tex, float2 uv)
{
  half4 pixel = tex2D(tex, uv);
#if UNITY_COLORSPACE_GAMMA
  pixel.rgb = GammaToLinearSpace(pixel);
#endif
  return pixel;
}

#include "SpriteEffect.cginc"

#if defined(_COLOR_ADJUST)
  half _Vibrance;
  half _Brightness;
  half _Contrast;
  half _Hue;
  half _Saturation;
  half _Gamma;

  // Color adjust.
  inline half4 ColorAdjust(half4 pixel)
  {
    // Vibrance.
    half sat = max(pixel.r, max(pixel.g, pixel.b)) - min(pixel.r, min(pixel.g, pixel.b));
    pixel.rgb = lerp(Luminance(pixel).xxx, pixel.rgb, (1.0 + (_Vibrance * (1.0 - (sign(_Vibrance) * sat)))));

    // Brightness.
    pixel.rgb += _Brightness;

    // Contrast.
    pixel.rgb = (pixel.rgb - 0.5) * ((1.015 * (_Contrast + 1.0)) / (1.015 - _Contrast)) + 0.5;

    // Hue & saturation.
    half3 hsv = FG_RGB2HSV(pixel.rgb);

    hsv.x += _Hue;
    hsv.y *= _Saturation;

    pixel.rgb = saturate(FG_HSV2RGB(hsv));

    // Gamma.
    pixel.rgb = pow(pixel.rgb, _Gamma);

    return pixel;
  }

  #define COLOR_ADJUST(pixel) pixel = ColorAdjust(pixel);
#else
  #define COLOR_ADJUST(pixel)
#endif

VertexOutput vert(appdata v)
{
  VertexOutput o;

  UNITY_SETUP_INSTANCE_ID(v);
  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(v);

  o.vertex = UnityFlipSprite(v.vertex, _Flip);
#if defined(_CUSTOM_VERT)
  v.vertex = CustomVert(v.vertex);
#endif
  o.vertex = UnityObjectToClipPos(v.vertex);

  o.uv = TRANSFORM_TEX(v.uv, _MainTex);
  o.color = v.color * _Color * _RendererColor;

#if FOG_ON
  UNITY_TRANSFER_FOG(o, o.vertex);
#endif

#if defined(_ENABLE_GRABPASS)
  o.grabPos = ComputeGrabScreenPos(o.vertex);
#endif

#ifdef PIXELSNAP_ON
  o.vertex = UnityPixelSnap(o.vertex);
#endif

  return o;
}

half4 frag(VertexOutput i) : SV_Target
{
#if defined(_CUSTOM_PIXEL)
  half4 pixel = CustomPixel(i) * _Color;
#else
  half4 pixel = SampleSprite(i.uv) * _Color;
#endif

#if PREMULTIPLYALPHA_ON
  pixel.rgb *= pixel.a;
#endif

  half4 final = SpriteEffect(pixel, i);

  COLOR_ADJUST(final)

  final = lerp(pixel, final, _Amount) * pixel.a;

#if UNITY_COLORSPACE_GAMMA
  final.rgb = LinearToGammaSpace(final.rgb);
#endif

  return final;
}

#endif  // SPRITESMOJO_INC
