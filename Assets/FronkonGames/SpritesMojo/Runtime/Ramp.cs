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
  /// <summary> Apply a 5-color gradient based on lightness. </summary>
  public static class Ramp
  {
    /// <summary> First color of the gradient (lower luminosity). </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color0 = new("_RampColor0", Color.white * 0.2f);

    /// <summary> Second color of the gradient. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color1 = new("_RampColor1", Color.white * 0.4f);

    /// <summary> Third color of the gradient. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color2 = new("_RampColor2", Color.white * 0.6f);

    /// <summary> Fourth color of the gradient. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color3 = new("_RampColor3", Color.white * 0.8f);

    /// <summary> Fifth color of the gradient (greater luminosity). </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color4 = new("_RampColor4", Color.white * 1.0f);

    /// <summary> Reverse the order of the colors. </summary>
    /// <returns></returns>
    public static readonly KeywordVariable Invert = new("INVERT_RAMP", false);

    /// <summary> Luminance range that will be used to change colors, default (0.0, 0.25). </summary>
    /// <returns>Value.</returns>
    public static readonly FloatMinMaxVariable Luminance = new("_LumRangeMin", "_LumRangeMax", (0.0f, 0.25f), (0.0f, 1.0f));

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Ramp/RampSprite");
      Reset(material);

      return material;
    }

    /// <summary> Set a new color gradient. </summary>
    /// <param name="material">The material.</param>
    /// <param name="color0">First color of the gradient (lower luminosity).</param>
    /// <param name="color1">Second color of the gradient.</param>
    /// <param name="color2">Third color of the gradient.</param>
    /// <param name="color3">Fourth color of the gradient.</param>
    /// <param name="color4">Fifth color of the gradient (greater luminosity).</param>
    public static void SetRamp(Material material, Color color0, Color color1, Color color2, Color color3, Color color4)
    {
      Color0.Set(material, color0);
      Color1.Set(material, color1);
      Color2.Set(material, color2);
      Color3.Set(material, color3);
      Color4.Set(material, color4);
    }

    /// <summary> Set a new color gradient. </summary>
    /// <param name="material">The material</param>
    /// <param name="colors">Array of five colors.</param>
    public static void SetRamp(Material material, Color[] colors)
    {
      if (colors.Length > 4)
      {
        Color0.Set(material, colors[0]);
        Color1.Set(material, colors[1]);
        Color2.Set(material, colors[2]);
        Color3.Set(material, colors[3]);
        Color4.Set(material, colors[4]);
      }
    }

    /// <summary> Returns the current gradient. </summary>
    /// <param name="material">The material.</param>
    /// <param name="colors">Array of five colors.</param>
    public static void GetRamp(Material material, out Color[] colors)
    {
      colors = new Color[5];

      colors[0] = Color0.Get(material);
      colors[1] = Color1.Get(material);
      colors[2] = Color2.Get(material);
      colors[3] = Color3.Get(material);
      colors[4] = Color4.Get(material);
    }

    /// <summary> Returns the current gradient. </summary>
    /// <param name="material">The material.</param>
    /// <param name="color0">First color of the gradient (lower luminosity).</param>
    /// <param name="color1">Second color of the gradient.</param>
    /// <param name="color2">Third color of the gradient.</param>
    /// <param name="color3">Fourth color of the gradient.</param>
    /// <param name="color4">Fifth color of the gradient (greater luminosity).</param>
    public static void GetRamp(Material material, out Color color0, out Color color1, out Color color2, out Color color3, out Color color4)
    {
      color0 = Color0.Get(material);
      color1 = Color1.Get(material);
      color2 = Color2.Get(material);
      color3 = Color3.Get(material);
      color4 = Color4.Get(material);
    }

    /// <summary> Sort the current gradient according to the luminosity of each color. </summary>
    /// <param name="material">The material.</param>
    public static void SortRampByLuminance(Material material)
    {
      GetRamp(material, out Color[] colors);

      System.Array.Sort(colors, CompareLuminance);

      SetRamp(material, colors);
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
      Color0.Reset(material);
      Color1.Reset(material);
      Color2.Reset(material);
      Color3.Reset(material);
      Color4.Reset(material);
      Invert.Reset(material);
      Luminance.Reset(material);
    }

    private static float GetLuminance(Color color) => (color.r * 0.299f) + (color.g * 0.587f) + (color.b * 0.114f);

    private static int CompareLuminance(Color color1, Color color2)
    {
      float colorLum1 = GetLuminance(color1);
      float colorLum2 = GetLuminance(color2);

      if (colorLum1 > colorLum2)
        return 1;

      if (colorLum2 > colorLum1)
        return -1;

      return 0;
    }
  }
}
