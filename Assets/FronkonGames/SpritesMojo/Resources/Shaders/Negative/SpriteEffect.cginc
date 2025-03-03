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
#ifndef NEGATIVE_INC
#define NEGATIVE_INC

half3 _ColorChannels;
half3 _HSL;

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  pixel.rgb = abs(_ColorChannels - pixel.rgb);
  pixel.rgb = abs(_HSL - FG_RGBtoHSL(pixel.rgb));
  pixel.rgb = FG_HSLtoRGB(pixel.rgb);

  return pixel;
}

#endif
