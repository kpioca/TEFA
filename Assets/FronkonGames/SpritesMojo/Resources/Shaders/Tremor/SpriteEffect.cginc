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
#ifndef TREMOR_INC
#define TREMOR_INC

half _Velocity;
half _NoiseScale;
half _NoiseSnap;

inline half4 CustomVert(half4 vertex)
{
  float time = _Time.y * _Velocity * 25.0;
  if (_NoiseSnap > 0.0)
    time = round(time / _NoiseSnap) * _NoiseSnap * 2.0;

  half2 noise = (half2)Rand3(vertex.xyz + half3(time, 0.0, 0.0)) * _NoiseScale * 2.0;
  vertex.xy += noise;

  return vertex;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  return pixel;
}

#endif
