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
  /// <summary> Apply a two color gradient based on lightness. </summary>
  public static class DuoTone
  {
    /// <summary> First color, the brightest, default White. </summary>
    /// <returns>Value.</returns>
    public static ColorVariable BrightColor = new("_BrightColor", Color.white);

    /// <summary> Second color, darker, default black. </summary>
    /// <returns>Value.</returns>
    public static ColorVariable DarkColor = new("_DarkColor", Color.black);

    /// <summary> Threshold between the two colors, default 0.25 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static FloatRangeVariable Threshold = new("_Threshold", 0.25f, 0.0f, 1.0f);

    /// <summary> Smoothness between the two colors, default 0.25 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static FloatRangeVariable Softness = new("_Softness", 0.25f, 0.0f, 1.0f);

    /// <summary> Luminance range that will be used to change colors, default (0.0, 0.25). </summary>
    /// <returns>Value.</returns>
    public static FloatMinMaxVariable Luminance = new("_LumRangeMin", "_LumRangeMax", (0.0f, 0.25f), (0.0f, 1.0f));

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/DuoTone/DuoToneSprite");
      Reset(material);

      return material;
    }

    /// <summary> Calculates the luminance range used for the effect. </summary>
    public static void AutoLuminance(Material material, Texture texture)
    {
      MaterialExtensions.AutoLuminance(texture, out float minLuminance, out float maxLuminance);

      Luminance.Set(material, (minLuminance, maxLuminance));
    }

    /// <summary> Reset the effect values of a sprite. </summary>
    /// <param name="sprite">Sprite.</param>
    public static void Reset(SpriteRenderer sprite) => Reset(sprite.material);

    /// <summary> Reset the effect values of a material. </summary>
    /// <param name="material">Material.</param>
    public static void Reset(Material material)
    {
      BrightColor.Reset(material);
      DarkColor.Reset(material);
      Threshold.Reset(material);
      Softness.Reset(material);
      Luminance.Reset(material);
    }
  }
}