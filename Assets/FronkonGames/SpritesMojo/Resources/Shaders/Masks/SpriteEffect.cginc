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
#ifndef MASKS_INC
#define MASKS_INC

sampler2D _MaskTex;

sampler2D _MaskRedTex;
half _MaskRedStrength;
int _RedBlendOp;
half4 _MaskRedTint;
half2 _MaskRedVel;
half _MaskRedScale;
half _MaskRedAngle;

sampler2D _MaskGreenTex;
half _MaskGreenStrength;
int _GreenBlendOp;
half4 _MaskGreenTint;
half2 _MaskGreenVel;
half _MaskGreenScale;
half _MaskGreenAngle;

sampler2D _MaskBlueTex;
half _MaskBlueStrength;
int _BlueBlendOp;
half4 _MaskBlueTint;
half2 _MaskBlueVel;
half _MaskBlueScale;
half _MaskBlueAngle;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  half4 mask = SampleTexture(_MaskTex, i.uv);
  mask.r *= _MaskRedStrength;
  mask.g *= _MaskGreenStrength;
  mask.b *= _MaskBlueStrength;
  mask.rgb *= mask.a;

  half3 colorMaskR = mask.r * BlendColor(_RedBlendOp,   pixel.rgb, _MaskRedTint.rgb   * SampleTexture(_MaskRedTex,   UVTransform(i.uv, _MaskRedScale,   _MaskRedVel,   _MaskRedAngle)).rgb);
  half3 colorMaskG = mask.g * BlendColor(_GreenBlendOp, pixel.rgb, _MaskGreenTint.rgb * SampleTexture(_MaskGreenTex, UVTransform(i.uv, _MaskGreenScale, _MaskGreenVel, _MaskGreenAngle)).rgb);
  half3 colorMaskB = mask.b * BlendColor(_BlueBlendOp,  pixel.rgb, _MaskBlueTint.rgb  * SampleTexture(_MaskBlueTex,  UVTransform(i.uv, _MaskBlueScale,  _MaskBlueVel,  _MaskBlueAngle)).rgb);

  pixel.rgb = lerp(pixel.rgb, colorMaskR + colorMaskG + colorMaskB, (half3)(mask.r + mask.g + mask.b));

  return pixel;
}

#endif
