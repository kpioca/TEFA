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
#ifndef RETRO_INC
#define RETRO_INC

int _Emulation;
half _Pixelation;
half _Luminance;

inline fixed4 CustomPixel(VertexOutput i)
{
  half2 fragCoord = i.uv / _MainTex_TexelSize.xy;
  half2 uvPixellated = floor(fragCoord / _Pixelation) * _Pixelation;

  return SampleSprite(uvPixellated * _MainTex_TexelSize.xy);
}

inline half3 Retro(half3 pixel, half2 uv)
{
  half3 final = half3(100.0 * 255.0, 100.0 * 255.0, 100.0 * 255.0);

#define TRY_COLOR(color) final = lerp(color, final, step(length(final - pixel), length(color - pixel)));

  switch (_Emulation)
  {
    // Two BGS.
    case 0:
      TRY_COLOR(half3(000.0, 000.0, 000.0));
      TRY_COLOR(half3(103.0, 103.0, 103.0));
      TRY_COLOR(half3(184.0, 184.0, 184.0));
      TRY_COLOR(half3(255.0, 255.0, 255.0));
    break;

    // Black and White.
    case 1:
      TRY_COLOR(half3(000.0, 000.0, 000.0));
      TRY_COLOR(half3(255.0, 255.0, 255.0));
    break;

    // NES.
    case 2:
      TRY_COLOR(half3(000.0, 000.0, 000.0));
      TRY_COLOR(half3(080.0, 048.0, 000.0));
      TRY_COLOR(half3(000.0, 104.0, 000.0));
      TRY_COLOR(half3(000.0, 064.0, 088.0));
      TRY_COLOR(half3(000.0, 120.0, 000.0));
      TRY_COLOR(half3(136.0, 020.0, 000.0));
      TRY_COLOR(half3(000.0, 168.0, 000.0));
      TRY_COLOR(half3(168.0, 016.0, 000.0));
      TRY_COLOR(half3(168.0, 000.0, 032.0));
      TRY_COLOR(half3(000.0, 168.0, 068.0));
      TRY_COLOR(half3(000.0, 184.0, 000.0));
      TRY_COLOR(half3(000.0, 000.0, 188.0));
      TRY_COLOR(half3(000.0, 136.0, 136.0));
      TRY_COLOR(half3(148.0, 000.0, 132.0));
      TRY_COLOR(half3(068.0, 040.0, 188.0));
      TRY_COLOR(half3(120.0, 120.0, 120.0));
      TRY_COLOR(half3(172.0, 124.0, 000.0));
      TRY_COLOR(half3(124.0, 124.0, 124.0));
      TRY_COLOR(half3(228.0, 000.0, 088.0));
      TRY_COLOR(half3(228.0, 092.0, 016.0));
      TRY_COLOR(half3(088.0, 216.0, 084.0));
      TRY_COLOR(half3(000.0, 000.0, 252.0));
      TRY_COLOR(half3(248.0, 056.0, 000.0));
      TRY_COLOR(half3(000.0, 088.0, 248.0));
      TRY_COLOR(half3(000.0, 120.0, 248.0));
      TRY_COLOR(half3(104.0, 068.0, 252.0));
      TRY_COLOR(half3(248.0, 120.0, 088.0));
      TRY_COLOR(half3(216.0, 000.0, 204.0));
      TRY_COLOR(half3(088.0, 248.0, 152.0));
      TRY_COLOR(half3(248.0, 088.0, 152.0));
      TRY_COLOR(half3(104.0, 136.0, 252.0));
      TRY_COLOR(half3(252.0, 160.0, 068.0));
      TRY_COLOR(half3(248.0, 184.0, 000.0));
      TRY_COLOR(half3(184.0, 248.0, 024.0));
      TRY_COLOR(half3(152.0, 120.0, 248.0));
      TRY_COLOR(half3(000.0, 232.0, 216.0));
      TRY_COLOR(half3(060.0, 188.0, 252.0));
      TRY_COLOR(half3(188.0, 188.0, 188.0));
      TRY_COLOR(half3(216.0, 248.0, 120.0));
      TRY_COLOR(half3(248.0, 216.0, 120.0));
      TRY_COLOR(half3(248.0, 164.0, 192.0));
      TRY_COLOR(half3(000.0, 252.0, 252.0));
      TRY_COLOR(half3(184.0, 184.0, 248.0));
      TRY_COLOR(half3(184.0, 248.0, 184.0));
      TRY_COLOR(half3(240.0, 208.0, 176.0));
      TRY_COLOR(half3(248.0, 120.0, 248.0));
      TRY_COLOR(half3(252.0, 224.0, 168.0));
      TRY_COLOR(half3(184.0, 248.0, 216.0));
      TRY_COLOR(half3(216.0, 184.0, 248.0));
      TRY_COLOR(half3(164.0, 228.0, 252.0));
      TRY_COLOR(half3(248.0, 184.0, 248.0));
      TRY_COLOR(half3(248.0, 216.0, 248.0));
      TRY_COLOR(half3(248.0, 248.0, 248.0));
      TRY_COLOR(half3(252.0, 252.0, 252.0));
    break;

    // EGA.
    case 3:
      TRY_COLOR(half3(000.0, 000.0, 000.0));
      TRY_COLOR(half3(255.0, 255.0, 255.0));
      TRY_COLOR(half3(255.0, 000.0, 000.0));
      TRY_COLOR(half3(000.0, 255.0, 000.0));
      TRY_COLOR(half3(000.0, 000.0, 255.0));
      TRY_COLOR(half3(255.0, 255.0, 000.0));
      TRY_COLOR(half3(000.0, 255.0, 255.0));
      TRY_COLOR(half3(255.0, 000.0, 255.0));
      TRY_COLOR(half3(128.0, 000.0, 000.0));
      TRY_COLOR(half3(000.0, 128.0, 000.0));
      TRY_COLOR(half3(000.0, 000.0, 128.0));
      TRY_COLOR(half3(128.0, 128.0, 000.0));
      TRY_COLOR(half3(000.0, 128.0, 128.0));
      TRY_COLOR(half3(128.0, 000.0, 128.0));
      TRY_COLOR(half3(128.0, 128.0, 128.0));
      TRY_COLOR(half3(255.0, 128.0, 128.0));
    break;

    // CPC.
    case 4:
      TRY_COLOR(half3(000.0, 000.0, 000.0));
      TRY_COLOR(half3(000.0, 000.0, 128.0));
      TRY_COLOR(half3(000.0, 000.0, 255.0));
      TRY_COLOR(half3(128.0, 000.0, 000.0));
      TRY_COLOR(half3(128.0, 000.0, 128.0));
      TRY_COLOR(half3(128.0, 000.0, 255.0));
      TRY_COLOR(half3(255.0, 000.0, 000.0));
      TRY_COLOR(half3(255.0, 000.0, 128.0));
      TRY_COLOR(half3(255.0, 000.0, 255.0));
      TRY_COLOR(half3(000.0, 128.0, 000.0));
      TRY_COLOR(half3(000.0, 128.0, 128.0));
      TRY_COLOR(half3(000.0, 128.0, 255.0));
      TRY_COLOR(half3(128.0, 128.0, 000.0));
      TRY_COLOR(half3(128.0, 128.0, 128.0));
      TRY_COLOR(half3(128.0, 128.0, 255.0));
      TRY_COLOR(half3(255.0, 128.0, 000.0));
      TRY_COLOR(half3(255.0, 128.0, 128.0));
      TRY_COLOR(half3(255.0, 128.0, 255.0));
      TRY_COLOR(half3(000.0, 255.0, 000.0));
      TRY_COLOR(half3(000.0, 255.0, 128.0));
      TRY_COLOR(half3(000.0, 255.0, 255.0));
      TRY_COLOR(half3(128.0, 255.0, 000.0));
      TRY_COLOR(half3(128.0, 255.0, 128.0));
      TRY_COLOR(half3(128.0, 255.0, 255.0));
      TRY_COLOR(half3(255.0, 255.0, 000.0));
      TRY_COLOR(half3(255.0, 255.0, 128.0));
      TRY_COLOR(half3(255.0, 255.0, 255.0));
    break;

    // CGA.
    case 5:
      TRY_COLOR(half3(000.0, 000.0, 000.0));
      TRY_COLOR(half3(000.0, 170.0, 170.0));
      TRY_COLOR(half3(170.0, 000.0, 170.0));
      TRY_COLOR(half3(170.0, 170.0, 170.0));
    break;

    // Gameboy.
    case 6:
      TRY_COLOR(half3(156.0, 189.0, 015.0));
      TRY_COLOR(half3(140.0, 173.0, 015.0));
      TRY_COLOR(half3(048.0, 098.0, 048.0));
      TRY_COLOR(half3(000.0, 000.0, 000.0)); // TRY_COLOR(half3((015.0, 056.0, 015.0));
    break;

    // Teletext.
    case 7:
      TRY_COLOR(half3(000.0, 000.0, 000.0));
      TRY_COLOR(half3(255.0, 000.0, 000.0));
      TRY_COLOR(half3(000.0, 255.0, 000.0));
      TRY_COLOR(half3(000.0, 000.0, 255.0));
      TRY_COLOR(half3(000.0, 255.0, 255.0));
      TRY_COLOR(half3(255.0, 000.0, 255.0));
      TRY_COLOR(half3(255.0, 255.0, 000.0));
      TRY_COLOR(half3(255.0, 255.0, 255.0));
    break;

    // Commodore 64.
    case 8:
      TRY_COLOR(half3(0,0,0)*255.0);
      TRY_COLOR(half3(1,1,1)*255.0);
      TRY_COLOR(half3(0.41,0.22,0.17)*255.0);
      TRY_COLOR(half3(0.44,0.64,0.70)*255.0);
      TRY_COLOR(half3(0.44,0.24,0.53)*255.0);
      TRY_COLOR(half3(0.35,0.55,0.26)*255.0);
      TRY_COLOR(half3(0.21,0.16,0.47)*255.0);
      TRY_COLOR(half3(0.72,0.78,0.44)*255.0);
      TRY_COLOR(half3(0.44,0.31,0.15)*255.0);
      TRY_COLOR(half3(0.26,0.22,0.00)*255.0); 
      TRY_COLOR(half3(0.60,0.40,0.35)*255.0);
      TRY_COLOR(half3(0.27,0.27,0.27)*255.0);
      TRY_COLOR(half3(0.42,0.42,0.42)*255.0);
      TRY_COLOR(half3(0.60,0.82,0.52)*255.0);
      TRY_COLOR(half3(0.42,0.37,0.71)*255.0);
      TRY_COLOR(half3(0.58,0.58,0.58)*255.0);    
    break;

    // Z80.
    case 9:
      TRY_COLOR(half3(0,0,0));
      TRY_COLOR(half3(0,0,255.0));
      TRY_COLOR(half3(0,0,192.0));
      TRY_COLOR(half3(255.0,0,0));
      TRY_COLOR(half3(192.0,0,0));
      TRY_COLOR(half3(255.0,0,255.0));
      TRY_COLOR(half3(192.0,0,192.0));	
      TRY_COLOR(half3(0,255.0,0));
      TRY_COLOR(half3(0,192.0,0));
      TRY_COLOR(half3(0,255.0,255.0));
      TRY_COLOR(half3(0,192.0,192.0));
      TRY_COLOR(half3(255.0,255.0,0));
      TRY_COLOR(half3(192.0,192.0,0));
      TRY_COLOR(half3(255.0,255.0,255.0));
      TRY_COLOR(half3(192.0,192.0,192.0));    
    break;
  }

#undef TRY_COLOR

  return final / 255.0;
}

inline half4 SpriteEffect(half4 pixel, VertexOutput i)
{
  pixel.rgb = Retro(pixel.rgb * 255.0 * _Luminance, i.uv);

  return pixel;
}

#endif
