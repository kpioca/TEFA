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
  /// <summary> Vintage computer effects. </summary>
  public static class Retro
  {
    public enum Emulations
    {
      TwoBGS          = 0,
      BlackAndWhite   = 1,
      NES             = 2,
      EGA             = 3,
      CPC             = 4,
      CGA             = 5,
      Gameboy         = 6,
      Teletext        = 7,
      Commodore64     = 8,
      Z80             = 9
    }

    /// <summary> Retro effect type, default Emulations.Gameboy. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable Emulation = new("_Emulation", Emulations.Gameboy);

    /// <summary> Pixel size, default 4 [1 - 10]. </summary>
    /// <returns>Value.</returns>
    public static readonly IntVariable Pixelation = new("_Pixelation", 4);

    /// <summary> Adjust in luminance, default 3.0 [0.0 - 10.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatVariable Luminance = new("_Luminance", 3.0f);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Retro/RetroSprite");
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
      Emulation.Reset(material);
      Pixelation.Reset(material);
      Luminance.Reset(material);
    }
  }
}