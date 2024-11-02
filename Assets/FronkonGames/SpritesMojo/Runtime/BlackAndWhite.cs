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
  /// <summary> Black and white effect with controls for each channel. </summary>
  public static class BlackAndWhite
  {
    /// <summary> The threshold between black and white, default 0.5 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Threshold = new("_Threshold", 0.5f, 0.0f, 1.0f);

    /// <summary> Smooth transition between black and white, default 0.25 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Softness = new("_Softness", 0.25f, 0.0f, 1.0f);

    /// <summary> The amount of light, default 5.0 [0.0 - 5.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatPositiveVariable Exposure = new("_Exposure", 1.0f);

    /// <summary> Strength of the effect in the red channel, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Red = new("_Red", 1.0f, 0.0f, 1.0f);

    /// <summary> Strength of the effect in the green channel, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Green = new("_Green", 1.0f, 0.0f, 1.0f);

    /// <summary> Strength of the effect in the blue channel, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Blue = new("_Blue", 1.0f, 0.0f, 1.0f);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/BlackAndWhite/BlackAndWhiteSprite");
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
      Threshold.Reset(material);
      Softness.Reset(material);
      Exposure.Reset(material);
      Red.Reset(material);
      Green.Reset(material);
      Blue.Reset(material);
    }    
  }
}