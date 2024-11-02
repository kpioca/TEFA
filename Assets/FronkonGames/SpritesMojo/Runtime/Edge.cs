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
  /// <summary> Edge modes. </summary>
  public enum EdgeMode
  {
    Standard,
    Color,
    TrueColor,
  }

  /// <summary> Sobel algorithms. </summary>
  public enum SobelFunction
  {
    Standard,
    Prewitt,
    RobertsCross,
    Scharr,
  }

  /// <summary> Enhance the edges. </summary>
  public static class Edge
  {
    /// <summary> Edge mode, default EdgeMode.Standard. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable Mode = new("_EdgeMode", EdgeMode.Standard, (material, value) =>
    {
      material.DisableKeyword("EDGE_COLOR");
      material.DisableKeyword("EDGE_TRUECOLOR");

      EdgeMode mode = (EdgeMode)value;
      if (mode == EdgeMode.Color)
        material.EnableKeyword("EDGE_COLOR");
      else if (mode == EdgeMode.TrueColor)
        material.EnableKeyword("EDGE_TRUECOLOR");
    });

    /// <summary> Algorithms used to enhance the edges, default SobelFunction.Scharr. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable Sobel = new("_SobelFunction", SobelFunction.Scharr, (material, value) =>
    {
      material.DisableKeyword("SOBEL_PREWITT");
      material.DisableKeyword("SOBEL_ROBERTS_CROSS");
      material.DisableKeyword("SOBEL_SCHARR");

      SobelFunction sobel = (SobelFunction)value;
      if (sobel == SobelFunction.Prewitt)
        material.EnableKeyword("SOBEL_PREWITT");
      else if (sobel == SobelFunction.RobertsCross)
        material.EnableKeyword("SOBEL_ROBERTS_CROSS");
      else if (sobel == SobelFunction.Scharr)
        material.EnableKeyword("SOBEL_SCHARR");
    });

    /// <summary> Color used to enhance the edges. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Tint = new("_EdgeTint", Color.cyan);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Edge/EdgeSprite");
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
      Sobel.Reset(material);
      Tint.Reset(material);
    }
  }
}