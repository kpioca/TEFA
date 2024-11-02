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
using UnityEngine;

namespace FronkonGames.SpritesMojo
{
  /// <summary> Instagram filters. </summary>
  public enum InstagramFilter
  {
    _1977,
    Apollo,
    Brannan,
    EarlyBird,
    Gotham,
    Hefe,
    Inkwell,
    Lily,
    LomoFi,
    LordKevin,
    Nashville,
    Poprocket,
    Sutro,
    Walden,
    XProII,
  }

  /// <summary> Mimics Instagram effects. </summary>
  public static class Instagram
  {
    /// <summary> Effect type, default InstagramFilter._1977. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable Filter = new("_Filter", InstagramFilter._1977, (material, value) =>
    {
      InstagramFilter filter = (InstagramFilter)value;

      Vector3 slope = Vector3.zero;
      Vector3 offset = Vector3.zero;
      Vector3 power = Vector3.zero;
      float saturation = 1.0f;

      switch (value)
      {
        case (int)InstagramFilter._1977:
          slope = new Vector3(1.002344f, 1.002344f, 1.002344f);
          offset = new Vector3(0.177295f, 0.102685f, 0.124902f);
          power = new Vector3(1.271409f, 1.412865f, 1.441414f);
          break;

        case (int)InstagramFilter.Apollo:
          slope = new Vector3(1.027020f, 1.019360f, 0.997714f);
          offset = new Vector3(-0.012891f, 0.139844f, 0.139844f);
          power = new Vector3(1.186510f, 1.859080f, 1.896160f);
          saturation = 0.600441f;
          break;

        case (int)InstagramFilter.Brannan:
          slope = new Vector3(0.842500f, 0.956808f, 0.805664f);
          offset = new Vector3(-0.035071f, -0.052115f, 0.172788f);
          power = new Vector3(0.985901f, 1.188610f, 2.283580f);
          saturation = 0.607811f;
          break;

        case (int)InstagramFilter.EarlyBird:
          slope = new Vector3(1.050910f, 0.806908f, 0.758130f);
          offset = new Vector3(-0.052488f, -0.022408f, -0.043116f);
          power = new Vector3(1.294280f, 1.117030f, 1.186460f);
          saturation = 1.019480f;
          break;

        case (int)InstagramFilter.Gotham:
          slope = new Vector3(1.002633f, 1.283138f, -1.022549f);
          offset = new Vector3(-0.894868f, -0.192727f, 1.629342f);
          power = new Vector3(0.694179f, 1.469600f, 0.0f);
          saturation = 0.026953f;
          break;

        case (int)InstagramFilter.Hefe:
          slope = new Vector3(1.086620f, 1.086620f, 1.086620f);
          offset = new Vector3(0.123243f, -0.034278f, 0.024562f);
          power = new Vector3(1.501950f, 1.312590f, 2.0f);
          saturation = 0.776757f;
          break;

        case (int)InstagramFilter.Inkwell:
          slope = new Vector3(-1.956830f, 1.401900f, 3.810730f);
          offset = new Vector3(0.013761f, -0.143172f, -21.281799f);
          power = new Vector3(1.497810f, 1.277210f, 1.985840f);
          saturation = 0.0f;
          break;

        case (int)InstagramFilter.Lily:
          slope = new Vector3(0.723593f, 0.715874f, 0.6668720f);
          offset = new Vector3(0.094940f, 0.141620f, 0.097916f);
          power = new Vector3(0.910115f, 1.177830f, 1.714090f);
          saturation = 0.754297f;
          break;

        case (int)InstagramFilter.LomoFi:
          slope = new Vector3(1.435720f, 1.250320f, 1.186830f);
          offset = new Vector3(-0.279101f, -0.226710f, -0.081201f);
          power = new Vector3(1.178660f, 0.889618f, 1.169710f);
          break;

        case (int)InstagramFilter.LordKevin:
          slope = new Vector3(0.649603f, 0.769153f, 0.752337f);
          offset = new Vector3(0.364704f, 0.213678f, 0.031597f);
          power = new Vector3(1.096905f, 1.688936f, 1.286792f);
          saturation = 0.743945f;
          break;

        case (int)InstagramFilter.Nashville:
          slope = new Vector3(1.24203f, 0.87111f, 0.52847f);
          offset = new Vector3(0.01127f, 0.21499f, 0.48637f);
          power = new Vector3(1.58391f, 1.92828f, 2.36919f);
          saturation = 0.56308f;
          break;

        case (int)InstagramFilter.Poprocket:
          slope = new Vector3(0.511196f, 0.754472f, 1.069880f);
          offset = new Vector3(0.349511f, -0.022850f, 0.128856f);
          power = new Vector3(0.995112f, 1.195600f, 1.462390f);
          saturation = 0.510202f;
          break;

        case (int)InstagramFilter.Sutro:
          slope = new Vector3(1.310010f, 1.216570f, 1.286590f);
          offset = new Vector3(-0.068845f, -0.278657f, -0.001999f);
          power = new Vector3(0.507390f, 0.963987f, 1.188950f);
          saturation = 0.328368f;
          break;

        case (int)InstagramFilter.Walden:
          slope = new Vector3(1.077660f, 1.197070f, 1.15260f);
          offset = new Vector3(-0.087942f, -0.051414f, 0.026514f);
          power = new Vector3(1.264750f, 1.251250f, 1.247840f);
          saturation = 0.547076f;
          break;

        case (int)InstagramFilter.XProII:
          slope = new Vector3(1.047730f, 1.109550f, 1.226080f);
          offset = new Vector3(-0.122850f, -0.088573f, -0.113134f);
          power = new Vector3(1.214190f, 1.361770f, 0.992396f);
          break;
      }

      material.SetVector("_Slope", slope);
      material.SetVector("_Offset", offset);
      material.SetVector("_Power", power);
      material.SetFloat("_BW", saturation);      
    });

    /// <summary> Use a contrast effect used in films, default false. </summary>
    /// <returns>Value.</returns>
    public static readonly BoolVariable FilmContrast = new("_FilmContrast", false);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Instagram/InstagramSprite");
      Reset(material);

      return material;
    }

    /// <summary> Reset the effect values of a sprite. </summary>
    /// <param name="sprite">Sprite.</param>
    public static void Reset(SpriteRenderer sprite) => Reset(sprite.material);

    /// <summary> Reset the effect values of a material. </summary>
    /// <param name="material">Material.</param>
    public static void Reset(Material material)
    {
      Filter.Reset(material);
      FilmContrast.Reset(material);
    }
  }
}