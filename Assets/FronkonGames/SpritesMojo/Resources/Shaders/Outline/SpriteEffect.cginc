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
#ifndef OUTLINE_INC
#define OUTLINE_INC

sampler2D _OutlineTex;

half _OutlineMode;
int _OutlineSize;
half _AlphaThreshold;
half4 _OutlineColor0;
half4 _OutlineColor1;
half _GradientScale;
half _GradientOffset;
half2 _OutlineTexVel;
half _OutlineTexScale;
half _OutlineTexAngle;

inline fixed4 CustomPixel(VertexOutput input)
{
  half4 pixel = SampleSprite(input.uv);

  for (int i = 1; i < _OutlineSize + 1; ++i)
  {
    half4 pixelUp = SampleSprite(input.uv + half2(0.0, i * _MainTex_TexelSize.y));
    half4 pixelDown = SampleSprite(input.uv - half2(0.0, i *  _MainTex_TexelSize.y));
    half4 pixelRight = SampleSprite(input.uv + half2(i * _MainTex_TexelSize.x, 0.0));
    half4 pixelLeft = SampleSprite(input.uv - half2(i * _MainTex_TexelSize.x, 0.0));
    
    if (pixel.a <= _AlphaThreshold)
    {
      if (pixelUp.a > 0.0 || pixelDown.a > 0.0 || pixelRight.a > 0.0 || pixelLeft.a > 0.0)
      {
#ifdef OUTLINE_SOLID
        pixel = (half4)1.0 * _OutlineColor0;
#elif OUTLINE_GRADIENT
    
#ifdef GRADIENT_VERTICAL
        //final = lerp(_OutlineColor1, _OutlineColor0, IN.localPos.y * _GradientScale + _GradientOffset);
        pixel = lerp(_OutlineColor1, _OutlineColor0, input.uv.y * _GradientScale + _GradientOffset);
#else
        //final = lerp(_OutlineColor0, _OutlineColor1, IN.localPos.x * _GradientScale + _GradientOffset);
        pixel = lerp(_OutlineColor0, _OutlineColor1, input.uv.x * _GradientScale + _GradientOffset);
#endif // GRADIENT_VERTICAL
    
#elif OUTLINE_TEXTURE
        pixel = SampleTexture(_OutlineTex, UVTransform(input.uv, _OutlineTexScale, _OutlineTexVel, _OutlineTexAngle)) * _OutlineColor0;
#endif // OUTLINE_SOLID
      }
    }
  }

  return pixel;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  return pixel;
}

#endif
