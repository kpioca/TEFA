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
#ifndef DUOTONE_INC
#define DUOTONE_INC

half _Threshold;
half _Softness;
half3 _BrightColor;
half3 _DarkColor;
half _LumRangeMin;
half _LumRangeMax;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  half lum = Luminance(pixel.rgb);
  lum = (1.0 / (_LumRangeMax - _LumRangeMin)) * (lum - _LumRangeMin);

  if (_Softness <= 0.0)
    pixel.rgb = (lum >= _Threshold) ? _BrightColor : _DarkColor;
  else
  {
    half3 midColor = (_BrightColor + _DarkColor) * 0.5;
    half3 dstPixel;
    
    if (lum >= _Threshold)
      pixel.rgb = lerp(midColor, _BrightColor, smoothstep(_Threshold, _Threshold + ((1.0 - _Threshold) * _Softness), lum));
    else
      pixel.rgb = lerp(_DarkColor, midColor, smoothstep(_Threshold - ((1.0 - _Threshold) * _Softness), _Threshold, lum));
  }

  return pixel;
}

#endif
