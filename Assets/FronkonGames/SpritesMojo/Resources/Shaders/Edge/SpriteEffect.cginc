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
#ifndef EDGE_INC
#define EDGE_INC

int _EdgeMode;
half _SobelFunction;
int _Blend;
half4 _EdgeTint;

inline half Convolve(half3x3 kernel, half3x3 image)
{
  half result = 0.0;
  for (int i = 0; i < 3; ++i)
    for (int j = 0; j < 3; ++j)
      result += kernel[i][j] * image[i][j];

  return result;
}

inline half3 Edge(half stepx, half stepy, half2 center, half3x3 kernelX, half3x3 kernelY)
{
  half3x3 image = half3x3(length(SampleSprite(center + half2(-stepx, stepy)).rgb),
                          length(SampleSprite(center + half2(0.0, stepy)).rgb),
                          length(SampleSprite(center + half2(stepx, stepy)).rgb),
                          length(SampleSprite(center + half2(-stepx, 0.0)).rgb),
                          length(SampleSprite(center).rgb),
                          length(SampleSprite(center + half2(stepx, 0.0)).rgb),
                          length(SampleSprite(center + half2(-stepx, -stepy)).rgb),
                          length(SampleSprite(center + half2(0.0, -stepy)).rgb),
                          length(SampleSprite(center + half2(stepx, -stepy)).rgb));
  
  half2 result = half2(Convolve(kernelX, image), Convolve(kernelY, image));

  half color = clamp(length(result), 0.0, 1.0);

  return (half3)color;
}    

inline half ConvolveComponent(half3x3 kernelX, half3x3 kernelY, half3x3 image)
{
  half2 result;
  result.x = Convolve(kernelX, image);
  result.y = Convolve(kernelY, image);

  return clamp(length(result), 0.0, 1.0);
}

inline half3 ColorEdge(half stepx, half stepy, half2 center, half3x3 kernelX, half3x3 kernelY)
{
  half3 colors[9];
  colors[0] = tex2D(_MainTex, center + half2(-stepx, stepy)).rgb;
  colors[1] = tex2D(_MainTex, center + half2(0.0, stepy)).rgb;
  colors[2] = tex2D(_MainTex, center + half2(stepx, stepy)).rgb;
  colors[3] = tex2D(_MainTex, center + half2(-stepx, 0.0)).rgb;
  colors[4] = tex2D(_MainTex, center).rgb;
  colors[5] = tex2D(_MainTex, center + half2(stepx, 0.0)).rgb;
  colors[6] = tex2D(_MainTex, center + half2(-stepx, -stepy)).rgb;
  colors[7] = tex2D(_MainTex, center + half2(0.0, -stepy)).rgb;
  colors[8] = tex2D(_MainTex, center + half2(stepx, -stepy)).rgb;

  half3x3 imageR, imageG, imageB;
  for (int i = 0; i < 3; ++i)
  {
    for (int j = 0; j < 3; ++j)
    {
      imageR[i][j] = colors[i * 3 + j].r;
      imageG[i][j] = colors[i * 3 + j].g;
      imageB[i][j] = colors[i * 3 + j].b;
    }
  }

  half3 color;
  color.r = ConvolveComponent(kernelX, kernelY, imageR);
  color.g = ConvolveComponent(kernelX, kernelY, imageG);
  color.b = ConvolveComponent(kernelX, kernelY, imageB);

  return color;
}

inline half3 TrueColorEdge(half stepx, half stepy, half2 center, half3x3 kernelX, half3x3 kernelY)
{
  half3 edgeVal = Edge(stepx, stepy, center, kernelX, kernelY);
  
  return edgeVal * tex2D(_MainTex, center);
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  half _Step = 1.0;

#ifdef SOBEL_PREWITT
  // Prewitt masks (see http://en.wikipedia.org/wiki/Prewitt_operator)
  const half3x3 sobelKernelX = half3x3(-1.0, 0.0, 1.0,
                                       -1.0, 0.0, 1.0,
                                       -1.0, 0.0, 1.0);

  const half3x3 sobelKernelY = half3x3(1.0,  1.0,  1.0,
                                       0.0,  0.0,  0.0,
                                      -1.0, -1.0, -1.0);
#elif SOBEL_ROBERTS_CROSS
  // Roberts Cross masks (see http://en.wikipedia.org/wiki/Roberts_cross)
  const half3x3 sobelKernelX = half3x3(1.0,  0.0, 0.0,
                                       0.0, -1.0, 0.0,
                                       0.0,  0.0, 0.0);

  const half3x3 sobelKernelY = half3x3(0.0, 1.0, 0.0,
                                      -1.0, 0.0, 0.0,
                                       0.0, 0.0, 0.0);
#elif SOBEL_SCHARR
  // Scharr masks (see http://en.wikipedia.org/wiki/Sobel_operator#Alternative_operators)
  const half3x3 sobelKernelX = half3x3(3.0, 10.0,   3.0,
                                       0.0,  0.0,   0.0,
                                      -3.0, -10.0, -3.0);

  const half3x3 sobelKernelY = half3x3(3.0, 0.0, -3.0,
                                      10.0, 0.0, -10.0,
                                       3.0, 0.0, -3.0);
  _Step = 0.15;
#else
  // Sobel masks (see http://en.wikipedia.org/wiki/Sobel_operator)
  const half3x3 sobelKernelX = half3x3(1.0, 0.0, -1.0,
                                       2.0, 0.0, -2.0,
                                       1.0, 0.0, -1.0);

  const half3x3 sobelKernelY = half3x3(-1.0, -2.0, -1.0,
                                        0.0, 0.0, 0.0,
                                        1.0, 2.0, 1.0);
#endif

  half3 edgeColor = _EdgeTint;

  if (_EdgeMode == 0)
    edgeColor *= Edge(_Step * _MainTex_TexelSize.x, _Step * _MainTex_TexelSize.y, i.uv, sobelKernelX, sobelKernelY);
  else if (_EdgeMode == 1)
    edgeColor *= ColorEdge(_Step * _MainTex_TexelSize.x, _Step * _MainTex_TexelSize.y, i.uv, sobelKernelX, sobelKernelY);
  else
    edgeColor *= TrueColorEdge(_Step * _MainTex_TexelSize.x, _Step * _MainTex_TexelSize.y, i.uv, sobelKernelX, sobelKernelY);

  pixel.rgb = BlendColor(_Blend, pixel.rgb, edgeColor);

  return pixel;
}

#endif
