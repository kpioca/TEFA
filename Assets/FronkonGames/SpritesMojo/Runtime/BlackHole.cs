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
  /// <summary> Create a 'Black Hole' effect inside the sprite. </summary>
  public static class BlackHole
  {
    /// <summary> Effect radius, default 0.1 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Radius = new("_Radius", 0.1f, 0.0f, 1.0f);

    /// <summary> Effect center. </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable Center = new("_Center", new Vector2(0.5f, 0.5f));

    /// <summary> Effect distortion, default 0.1 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Distortion = new("_Distortion", 0.1f, 0.0f, 1.0f);

    /// <summary> Effect color. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Color = new("_BlackHoleColor", new Color(0.0f, 0.0f, 0.0f, 0.8f));

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/BlackHole/BlackHoleSprite");
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
      Radius.Reset(material);
      Center.Reset(material);
      Distortion.Reset(material);
      Color.Reset(material);
    }
  }
}