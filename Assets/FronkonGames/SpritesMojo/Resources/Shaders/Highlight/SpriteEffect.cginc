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
#ifndef HIGHLIGHT_INC
#define HIGHLIGHT_INC

half _Progress;
half _Angle;
half _Size;
half _Smooth;
half _Intensity;
half4 _Tint;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  _Progress = 0.5;
  _Angle = 3.1415927 * 0.45;
  _Size = 0.1;
  _Smooth = 0.2;
  _Intensity = 1.0;
  _Tint = half4(0.8, 0.8, 0.8, 1.0);

  i.uv = i.uv - float2(_Progress, 0.5);
  float a = atan2(i.uv.x, i.uv.y) + _Angle;
  float d = cos(floor(0.5 + a / PI) * PI - a) * length(i.uv);
  float dist = 1.0 - smoothstep(_Size, _Size + _Smooth, d);

  pixel.rgb = lerp(pixel.rgb, pixel.rgb + (_Tint.rgb * dist * _Intensity), _Tint.a);
  
  return half4(pixel.rgb, pixel.a);
}

#endif
