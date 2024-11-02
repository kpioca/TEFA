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
#ifndef HOLOGRAM_INC
#define HOLOGRAM_INC

half _Distortion;
half _BlinkStrength;
half _BlinkSpeed;
half4 _Tint;
half _ScanlineStrength;
half _ScanlineCount;
half _ScanlineSpeed;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  half2 uv = 0.5 + (i.uv - 0.5);

  pixel.r = SampleSprite(half2(uv.x + _MainTex_TexelSize.x * _Distortion, uv.y)).r;
  pixel.b = SampleSprite(half2(uv.x - _MainTex_TexelSize.y * _Distortion, uv.y)).z;

  pixel.rgb *= 0.5 + 0.5 * 16.0 * uv.x * uv.y * (1.0 - uv.x) * (1.0 - uv.y);

  pixel.rgb *= _Tint.rgb;

  float scanline = (1.0 - _ScanlineStrength) + _ScanlineStrength * sin(_ScanlineSpeed * _Time.y + uv.y * _ScanlineCount * 100.0);
  scanline *= (1.0 - _BlinkStrength) + _BlinkStrength * sin(_BlinkSpeed * _Time.y);

  pixel.rgb *= scanline;

  return half4(pixel.rgb, lerp(pixel.a * _Tint.a, pixel.a, scanline));
}

#endif
