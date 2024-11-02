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
  /// <summary> Outline mode. </summary>
  public enum OutlineMode
  {
    /// <summary> One solid color. </summary>
    Solid,

    /// <summary> Two gradient color. </summary>
    Gradient,

    /// <summary> Texture. </summary>
    Texture,
  }

  /// <summary> Outer edge. </summary>
  public static class Outline
  {
    /// <summary> Border size, default 5 [0 - 50]. </summary>
    /// <returns>Value.</returns>
    public static readonly IntPositiveVariable Size = new("_OutlineSize", 5);

    /// <summary> Border type, default OutlineMode.Solid. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable Mode = new("_OutlineMode", OutlineMode.Solid, (material, mode) =>
    {
      material.DisableKeyword("OUTLINE_SOLID");
      material.DisableKeyword("OUTLINE_GRADIENT");
      material.DisableKeyword("OUTLINE_TEXTURE");

      switch (mode)
      {
        case (int)OutlineMode.Solid:     material.EnableKeyword("OUTLINE_SOLID"); break;
        case (int)OutlineMode.Gradient:  material.EnableKeyword("OUTLINE_GRADIENT"); break;
        case (int)OutlineMode.Texture:   material.EnableKeyword("OUTLINE_TEXTURE"); break;
      }
    });

    /// <summary> Main color, valid for all modes. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color0 = new("_OutlineColor0", Color.white);

    /// <summary> Secondary color, valid for OutlineMode.Gradient. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color1 = new("_OutlineColor1", Color.white);

    /// <summary> Gradient scale, valid for OutlineMode.Gradient, default 1.0 [-10.0 - 10.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable GradientScale = new("_GradientScale", 1.0f, -10.0f, 10.0f);

    /// <summary> Gradient offset, valid for OutlineMode.Gradient, default 0.0 [-10.0 - 10.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable GradientOffset = new("_GradientOffset", 0.0f, -10.0f, 10.0f);

    /// <summary> Vertical gradient, default false. </summary>
    /// <returns>Value.</returns>
    public static readonly KeywordVariable Vertical = new("GRADIENT_VERTICAL", false);

    /// <summary> Texture border, valid for OutlineMode.Texture. </summary>
    /// <returns>Value.</returns>
    public static readonly TextureVariable Texture = new("_OutlineTex");

    /// <summary> Texture border scale, default 1.0 [0.0 - 5.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable TextureScale = new("_OutlineTexScale", 1.0f, 0.0f, 5.0f);

    /// <summary> Texture border angle, default 0.0 [0.0 - 180.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable TextureAngle = new("_OutlineTexAngle", 0.0f, 0.0f, 180.0f);

    /// <summary> Texture border velocity, default Vector2.zero. </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable TextureVelocity = new("_OutlineTexVel", Vector2.zero);

    /// <summary> Sensitivity to the alpha channel, default 0.0 [0.0 - 0.9999]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Threshold = new("_AlphaThreshold", 0.0f, 0.0f, 0.9999f);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Outline/OutlineSprite");
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
      Size.Reset(material);
      Mode.Reset(material);
    }    
  }
}