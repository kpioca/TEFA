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
  /// <summary> Types of displacement. </summary>
  public enum ShiftMode
  {
    /// <summary> Linear displacement. </summary>
    Linear,

    /// <summary> Radial based. </summary>
    Radial,
  }

  /// <summary> Shift of RGB channels. </summary>
  public static class Shift
  {
    /// <summary> Type of displacement, default ShiftMode.Linear. </summary>
    /// <returns>Value.</returns>
    public static readonly KeywordsVariable Mode = new(new string[] { "LINEAR", "RADIAL" }, 0);

    /// <summary> Red channel linear shift, default (0.0, 0.0). </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable RedShift = new("_RedShift", Vector2.zero);

    /// <summary> Green channel linear shift, default (0.0, 0.0). </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable GreenShift = new("_GreenShift", Vector2.zero);

    /// <summary> Blue channel linear shift, default (0.0, 0.0). </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable BlueShift = new("_BlueShift", Vector2.zero);

    /// <summary> Radial shift, default 0.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable RadialShift = new("_RadialShift", 0.0f, 0.0f, 1.0f);

    /// <summary> Add noise to shift intensity, default false. </summary>
    /// <returns>Value.</returns>
    public static readonly KeywordVariable Noise = new("NOISE_ON", false);

    /// <summary> Noise strength, default 0.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable NoiseStrength = new("_NoiseStrength", 0.0f, 0.0f, 1.0f);

    /// <summary> Noise speed, default 0.5 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable NoiseSpeed = new("_NoiseSpeed", 0.5f, 0.0f, 1.0f);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Shift/ShiftSprite");
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
      RedShift.Reset(material);
      GreenShift.Reset(material);
      BlueShift.Reset(material);
      RadialShift.Reset(material);
      Noise.Reset(material);
      NoiseStrength.Reset(material);
      NoiseSpeed.Reset(material);
    }
  }
}