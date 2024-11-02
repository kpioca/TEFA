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
#ifndef GLASS_INC
#define GLASS_INC

half _Distortion;
half _Refraction;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  float2 bump = UnpackNormal(SampleSprite(i.uv)).rb;
  float2 offset = bump * _Distortion * _GrabTexture_TexelSize.xy * 2.0;

  i.grabPos.xy = offset * i.grabPos.z + i.grabPos.xy;

  pixel.r = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.grabPos.x + _Refraction * _GrabTexture_TexelSize.x * bump.x * 2.0,
                                                            i.grabPos.y + _Refraction * _GrabTexture_TexelSize.y * bump.y * 2.0,
                                                            i.grabPos.z,
                                                            i.grabPos.w))).r;

  pixel.g = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabPos)).g;

  pixel.b = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.grabPos.x - _Refraction * _GrabTexture_TexelSize.x * bump.x * 2.0,
                                                            i.grabPos.y - _Refraction * _GrabTexture_TexelSize.y * bump.y * 2.0,
                                                            i.grabPos.z,
                                                            i.grabPos.w))).b;

  return pixel;
}

#endif
