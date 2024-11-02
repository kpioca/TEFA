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
  /// <summary> Dither methods. </summary>
  public enum DitherMode
  {
    Bayer4x4,
    Bayer8x8,
    Noise,
    AnimNoise,
  }

  /// <summary> Apply various color reduction algorithms. </summary>
  public static class Dither
  {
    /// <summary> Dithering algorithms, default DitherMode.Bayer4x4. </summary>
    /// <returns>Value.</returns>
    public static EnumVariable Mode = new("_Dithering", DitherMode.Bayer4x4, (material, value) =>
    {
      material.DisableKeyword("DITHERING_BAYER4x4");
      material.DisableKeyword("DITHERING_BAYER8x8");
      material.DisableKeyword("DITHERING_NOISE");

      DitherMode mode = (DitherMode)value;
      if (mode == DitherMode.Bayer4x4)
      {
        material.EnableKeyword("DITHERING_BAYER4x4");

        if (material.GetTexture("_DitheringTex") == null)
          material.SetTexture("_DitheringTex", Resources.Load<Texture>("Textures/Bayer4x4"));
      }
      else if (mode == DitherMode.Bayer8x8)
        material.EnableKeyword("DITHERING_BAYER8x8");
      else if (mode == DitherMode.Noise)
        material.EnableKeyword("DITHERING_NOISE");
    });

    /// <summary> Amount of color reduction, default 0.125 [0.001 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static FloatRangeVariable ColorReduction = new("_PaletteSpace", 0.125f, 0.001f, 1.0f);

    /// <summary> Pixelation, default 4 [1 - 10]. </summary>
    /// <returns>Value.</returns>
    public static IntRangeVariable PixelSize = new("_Pixelation", 4, 1, 10);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Dither/DitherSprite");
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
      Mode.Reset(material);
      ColorReduction.Reset(material);
      PixelSize.Reset(material);
    }    
  }
}