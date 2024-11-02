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
#ifndef SHIFT_INC
#define SHIFT_INC

half _NoiseStrength;
half _NoiseSpeed;

half _RadialShift;

half2 _RedShift;
half2 _GreenShift;
half2 _BlueShift;

inline half4 CustomPixel(VertexOutput input)
{
  half2 uvR, uvG, uvB;
  half2 red, green, blue;
  
  half4 pixel = (half4)0;

#ifdef LINEAR
  uvR = half2(-_RedShift.x, _RedShift.y);
  uvG = half2(-_GreenShift.x, _GreenShift.y);
  uvB = half2(-_BlueShift.x, _BlueShift.y);

#ifdef NOISE_ON
  half randSin = 1.0 - (Rand1(_SinTime.x * _NoiseSpeed * 0.001) * 2.0);
  half randCos = 1.0 - (Rand1(_CosTime.x * _NoiseSpeed * 0.001) * 2.0);

  uvR += half2(randSin, randCos) * _NoiseStrength * 5.0;
  uvB += half2(randCos, randSin) * _NoiseStrength * 5.0;
#endif // NOISE_ON
  uvR *= _MainTex_TexelSize;
  uvG *= _MainTex_TexelSize;
  uvB *= _MainTex_TexelSize;

  red = SampleSprite(input.uv + uvR).ra * _Color.r;
  green = SampleSprite(input.uv + uvG).ga * _Color.g;
  blue = SampleSprite(input.uv + uvB).ba * _Color.b;

  pixel = half4(red.x * red.y, green.x * green.y, blue.x * blue.y, (red.y + green.y + blue.y) / 3.0);
#endif // LINEAR

#ifdef RADIAL
  _RadialShift *= 0.2;
#ifdef NOISE_ON
  _RadialShift += _NoiseStrength * Rand1(_SinTime.w * _NoiseSpeed * 0.0005) * 0.2;
#endif // NOISE_ON
  _RadialShift *= distance(input.uv, 0.5);

  half2 colorVec = normalize(input.uv - 0.5);

  pixel = SampleSprite(input.uv);

  uvR = half2(input.uv.x - (colorVec.x * _RadialShift), input.uv.y - (colorVec.y * _RadialShift));
  uvB = half2(input.uv.x + (colorVec.x * _RadialShift), input.uv.y + (colorVec.y * _RadialShift));
  
  red = SampleSprite(uvR).ra * _Color.r;
  green = pixel.ga;
  blue = SampleSprite(uvB).ba * _Color.b;

  pixel = half4(red.x * red.y, green.x * green.y, blue.x * blue.y, (red.y + green.y + blue.y) / 3.0);
#endif // RADIAL

  return pixel;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  return pixel;
}

#endif
