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
  /// <summary> Simulates a hologram. </summary>
  public static class Hologram
  {
    /// <summary> Hologram distortion, default 5.0 [0.0 - 10.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatPositiveVariable Distortion = new("_Distortion", 5.0f);

    /// <summary> Hologram strength, default 0.03 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable BlinkStrength = new("_BlinkStrength", 0.03f, 0.0f, 1.0f);

    /// <summary> Hologram speed, default 50.0 [0.0 - 100.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatPositiveVariable BlinkSpeed = new("_BlinkSpeed", 50.0f);

    /// <summary> Scanlines strength, default 0.1 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable ScanlineStrength = new("_ScanlineStrength", 0.1f, 0.0f, 1.0f);

    /// <summary> Number of lines, default 10.0 [0.0 - 20.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatVariable ScanlineSpeed = new("_ScanlineSpeed", 10.0f);

    /// <summary> Scanline speed, default 10.0 [-50.0 - 50.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatPositiveVariable ScanlineCount = new("_ScanlineCount", 10.0f);

    /// <summary> Hologram color. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Tint = new("_Tint", new Color(0.95f, 1.05f, 0.95f, 1.0f));

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Hologram/HologramSprite");
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
      Distortion.Reset(material);
      BlinkStrength.Reset(material);
      BlinkSpeed.Reset(material);
      ScanlineStrength.Reset(material);
      ScanlineSpeed.Reset(material);
      ScanlineCount.Reset(material);
      Tint.Reset(material);
    }
  }
}