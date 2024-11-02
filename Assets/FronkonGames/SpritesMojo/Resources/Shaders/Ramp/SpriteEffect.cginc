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
#ifndef RAMP_INC
#define RAMP_INC

half4 _RampColor0;
half4 _RampColor1;
half4 _RampColor2;
half4 _RampColor3;
half4 _RampColor4;
half _LumRangeMin;
half _LumRangeMax;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  half lum = Luminance(pixel.rgb);
  lum = (1.0 / (_LumRangeMax - _LumRangeMin)) * (lum - _LumRangeMin);

#ifdef INVERT_RAMP
  if (lum < 0.25)
    pixel = lerp(_RampColor3, _RampColor4, lum * 4.0);
  else if (lum < 0.5)
    pixel = lerp(_RampColor2, _RampColor3, (lum - 0.25) * 4.0);
  else if (lum < 0.75)
    pixel = lerp(_RampColor1, _RampColor2, (lum - 0.5) * 4.0);
  else
    pixel = lerp(_RampColor0, _RampColor1, (lum - 0.75) * 4.0);
#else
  if (lum < 0.25)
    pixel = lerp(_RampColor0, _RampColor1, lum * 4.0);
  else if (lum < 0.5)
    pixel = lerp(_RampColor1, _RampColor2, (lum - 0.25) * 4.0);
  else if (lum < 0.75)
    pixel = lerp(_RampColor2, _RampColor3, (lum - 0.5) * 4.0);
  else
    pixel = lerp(_RampColor4, _RampColor4, (lum - 0.75) * 4.0);
#endif

  return pixel;
}

#endif
