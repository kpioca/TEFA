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
#ifndef BLACKANDWHITE_INC
#define BLACKANDWHITE_INC

half _Threshold;
half _Softness;
half _Exposure;
half _Red;
half _Green;
half _Blue;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  half3 tc = pixel.rgb * (exp2(pixel.rgb) * (half3)_Exposure);

  half lum = Luminance(tc);

  half3 color = half3(lerp(pixel.r, lum, _Red),
                      lerp(pixel.g, lum, _Green),
                      lerp(pixel.b, lum, _Blue));

  color = smoothstep(_Threshold - _Softness, _Threshold + _Softness, color.r + color.b + color.b);

  return half4(color, pixel.a);
}

#endif
