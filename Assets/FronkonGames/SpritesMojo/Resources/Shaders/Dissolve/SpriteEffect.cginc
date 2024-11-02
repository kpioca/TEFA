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
#ifndef DISOLVE_INC
#define DISOLVE_INC

sampler2D _DissolveTex;
sampler2D _DissolveBorderTex;

half _DissolveSlide;
half _DissolveShape;
int _DissolveInverse;
half _DissolveBorderSize;
half _DissolveUVScale;
half4 _DissolveBorderColorInside;
half4 _DissolveBorderColorOutside;
half _DissolveBorderTexUVScale;

inline half4 CustomPixel(VertexOutput i)
{
  half2 uv = i.uv;
  half4 dissolveTex = SampleTexture(_DissolveTex, uv * _DissolveUVScale);
  half dissolveChannel = (dissolveTex.r + dissolveTex.g + dissolveTex.b) / 3.0;
  dissolveChannel = _DissolveInverse == 1 ? 1.0 - dissolveChannel : dissolveChannel;

  half clipScope = dissolveChannel - _DissolveSlide;
  clip(clipScope);

  half4 pixel = SampleSprite(uv);
  if (pixel.a == 0.0)
    discard;

#if defined (DISSOLVE_COLOR) || (DISSOLVE_TEXTURE)
  half v = smoothstep(_DissolveSlide, _DissolveSlide + _DissolveBorderSize, dissolveChannel);
  half v2 = step(clipScope, _DissolveBorderSize * (-exp(-100.0 * _DissolveSlide) + 1.0));
#endif

#ifdef DISSOLVE_COLOR
  pixel *= v2 * lerp(_DissolveBorderColorOutside, _DissolveBorderColorInside, v) + (1.0 - v2);
#elif DISSOLVE_TEXTURE
  half4 borderTex = SampleTexture(_DissolveBorderTex, uv.xy * _DissolveBorderTexUVScale) * pixel.a;
  pixel *= v2 * lerp(_DissolveBorderColorOutside * borderTex, _DissolveBorderColorInside, v) + (1.0 - v2);
#endif

  return pixel;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  return pixel;
}

#endif
