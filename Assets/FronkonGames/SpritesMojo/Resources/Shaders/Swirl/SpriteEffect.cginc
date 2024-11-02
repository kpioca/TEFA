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
#ifndef SWIRL_INC
#define SWIRL_INC

half _Strength;
half _Torsion;
half2 _Center;

inline fixed4 CustomPixel(VertexOutput i)
{
  half2 uv = i.uv;
  uv = uv - _Center;
  half percent = (_Strength - length(uv)) / _Strength;

  if (percent < 1.0 && percent >= 0.0)
  {
    half theta = percent * percent * _Torsion * 8.0;
    
    half s = sin(theta), c = cos(theta);
    uv = half2(uv.x * c - uv.y * s, uv.x * s + uv.y * c);
  }

  uv += _Center;

  return SampleSprite(uv);
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  return pixel;
}

#endif
