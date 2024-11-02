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
#ifndef RGBGLITCH_INC
#define RGBGLITCH_INC

half _Speed;
half _Amplitude;

// Return 1 if value inside 1D range.
inline float InsideRange(float value, float bottom, float top)
{
  return step(bottom, value) - step(top, value);
}

inline half4 CustomPixel(VertexOutput i)
{
  half2 uv = i.uv;
  half4 pixel = SampleSprite(uv);

  half time = floor(_Time.y * _Speed * 20.0);

  float maxOffset = _Amplitude / 2.0;

  // Horizontal slices.
  for (float s = 0.0; s < 10.0 * _Amplitude; s += 1.0)
  {
    float sliceY = Rand2(float2(time, 2345.0 + s));
    float sliceH = Rand2(float2(time, 9035.0 + s)) * 0.25;
    float hOffset = RandRange(float2(time, 9625.0 + s), -maxOffset, maxOffset);
    
    float2 uvOff = uv;
    uvOff.x += hOffset;

    if (InsideRange(uv.y, sliceY, frac(sliceY + sliceH)) == 1.0)
      pixel = SampleSprite(uvOff);
  }

  // RGB offset.
  float maxColOffset = _Amplitude / 6.0;
  float rnd = Rand2(float2(time, 9545.0));
  float2 colOffset = float2(RandRange(float2(time, 9545.0), -maxColOffset, maxColOffset), RandRange(float2(time, 7205.0), -maxColOffset, maxColOffset));

  if (rnd < 0.33)
    pixel.r = SampleSprite(uv + colOffset).r;
  else if (rnd < 0.66)
    pixel.g = SampleSprite(uv + colOffset).g;
  else
    pixel.b = SampleSprite(uv + colOffset).b;

  return pixel;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  return pixel;
}

#endif
