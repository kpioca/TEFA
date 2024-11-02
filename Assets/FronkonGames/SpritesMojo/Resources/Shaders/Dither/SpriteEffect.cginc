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
#ifndef BLEND_INC
#define BLEND_INC

sampler2D _DitheringTex;
float _Dithering;
float _PaletteSpace;
float _Pixelation;

inline fixed4 CustomPixel(VertexOutput i)
{
  half2 fragCoord = i.uv / _MainTex_TexelSize.xy;
  half2 uvPixellated = floor(fragCoord / _Pixelation) * _Pixelation;

  return SampleSprite(uvPixellated * _MainTex_TexelSize.xy);
}

inline half Quantize(half inp, half period)
{
  return floor((inp + period / 2.0) / period) * period;
}

inline half Bayer4x4(half2 uvScreenSpace)
{
  half2 bayerCoord = floor(uvScreenSpace / _Pixelation);
  bayerCoord = fmod(bayerCoord, 4.0);
  
  const half4x4 bayerMat = half4x4(1.0, 9.0, 3.0, 11.0,
                                   13.0, 5.0, 15.0, 7.0,
                                   4.0, 12.0, 2.0, 10.0,
                                   16.0, 8.0, 14.0, 6.0) / 16.0;

  int bayerIndex = int(bayerCoord.x + bayerCoord.y * 4.0);
  if (bayerIndex == 0) return bayerMat[0][0];
  if (bayerIndex == 1) return bayerMat[0][1];
  if (bayerIndex == 2) return bayerMat[0][2];
  if (bayerIndex == 3) return bayerMat[0][3];
  if (bayerIndex == 4) return bayerMat[1][0];
  if (bayerIndex == 5) return bayerMat[1][1];
  if (bayerIndex == 6) return bayerMat[1][2];
  if (bayerIndex == 7) return bayerMat[1][3];
  if (bayerIndex == 8) return bayerMat[2][0];
  if (bayerIndex == 9) return bayerMat[2][1];
  if (bayerIndex == 10) return bayerMat[2][2];
  if (bayerIndex == 11) return bayerMat[2][3];
  if (bayerIndex == 12) return bayerMat[3][0];
  if (bayerIndex == 13) return bayerMat[3][1];
  if (bayerIndex == 14) return bayerMat[3][2];
  if (bayerIndex == 15) return bayerMat[3][3];

  return 10.0;
}

inline half Bayer8x8(half2 uvScreenSpace)
{
  return SampleTexture(_DitheringTex, uvScreenSpace / (_Pixelation * 8.0)).r;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  half2 fragCoord = i.uv / _MainTex_TexelSize.xy;
  half2 uvPixellated = floor(fragCoord / _Pixelation) * _Pixelation;

#ifdef DITHERING_BAYER4x4
  pixel += (Bayer4x4(fragCoord) - 0.5) * _PaletteSpace;
#elif DITHERING_BAYER8x8
  pixel += (Bayer8x8(fragCoord) - 0.5) * _PaletteSpace;
#elif DITHERING_NOISE
  pixel += (Rand2(uvPixellated) - 0.5) * _PaletteSpace;
#else
  pixel += (Rand2(half2(Rand2(uvPixellated), _Time.y)) - 0.5) * _PaletteSpace;
#endif

  pixel.rgb = half3(Quantize(pixel.r, _PaletteSpace), Quantize(pixel.g, _PaletteSpace), Quantize(pixel.b, _PaletteSpace));

  return pixel;
}

#endif
