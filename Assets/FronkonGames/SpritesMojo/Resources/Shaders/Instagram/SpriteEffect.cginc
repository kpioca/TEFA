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
#ifndef INSTAGRAM_INC
#define INSTAGRAM_INC

half _Filter;
half _BW;
half4 _Slope;
half4 _Offset;
half4 _Power;
half _FilmContrast;

inline half3 Contrast(half3 pixel, half4 con)
{
  half3 c = con.rgb * con.a;
  half3 t = (1.0 - c) / 2.0;
  
  t = 0.5;
  pixel = (1.0 - c.rgb) * t + c.rgb * pixel;

  return pixel;
}

inline half3 Sig(half3 s)
{
  return 1.0 / (1.0 + (exp(-(s - 0.5) * 7.0))); 
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  pixel.rgb = pow(clamp(((pixel.rgb * _Slope) + _Offset), 0.0, 1.0), _Power);
  pixel.rgb = Saturation(pixel.rgb, _BW).rgb;

  if (_FilmContrast == 1.0)
    pixel.rgb = Sig(pixel.rgb);

  pixel = clamp(pixel, 0.0, 1.0);

  return pixel;
}

#endif
