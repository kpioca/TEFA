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
#ifndef BLACKHOLE_INC
#define BLACKHOLE_INC

half _Radius;
half _Distortion;
half2 _Center;
half4 _BlackHoleColor;

inline fixed4 CustomPixel(VertexOutput i)
{
  half4 pixel = _BlackHoleColor;

  half2 uv = i.uv;
  if (length(uv - _Center) > _Radius)
  {
    half2 centerDelta = _Center - uv;
    half2 mappedCoord = uv + centerDelta * (1.0 / (1.0 + (_Radius - length(centerDelta)) / (_Distortion * 0.1)));
    pixel = SampleSprite(mappedCoord);
  }

  return pixel;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  return pixel;
}

#endif
