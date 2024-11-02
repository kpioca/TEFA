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
  /// <summary> Mode used for the effect. </summary>
  public enum DissolveMode
  {
    /// <summary> Normal dissolve effect. </summary>
    Normal,

    /// <summary> Dissolve effect with border color. </summary>
    Color,

    /// <summary> Dissolve effect with border texture. </summary>
    Texture,
  }

  /// <summary> Dissolve texture type. If you want to use your own, set 'Custom' and change 'borderTexture'. </summary>
  public enum DissolveShape
  {
    BarrRipple_1,
    BarrRipple_2,
    BlindsInToOut,
    BlindsSliding,
    Board_1,
    Board_2,
    Boxes_1,
    Boxes_2,
    Clock,
    Cross,
    CrossedBarr,
    CrossLeftBar,
    CrossRightBar,
    Deform,
    Dissolve,
    Flower_1,
    Flower_2,
    Flower_3,
    FoggySpiral,
    Fogg_1,
    Fogg_2,
    Frame,
    FrameCrossLeftBarr,
    Hatched_1,
    Hatched_2,
    Horizontal,
    HorizontalBarr,
    Hourglass,
    LateralLeftTriangle,
    LateralRightTriangle,
    LittleRipplingLeft,
    Losange,
    LuminousFrame_1,
    LuminousFrame_2,
    LuminousSpiral_1,
    LuminousSpiral_2,
    MiddleCrossLeftBarr,
    MiddleCrossRightBarr,
    MiddleLeftInspiration,
    MiddleLosange,
    MiddleRightInspiration,
    Mosaic_1,
    Mosaic_2,
    Mosaic_3,
    Mosaic_4,
    Postime,
    Puzzle,
    RectangleOutToIn,
    Spiral_1,
    Spiral_2,
    Spiral_3,
    Spiral_4,
    Spiral_5,
    Spots,
    SquareMiddleLeftBarr,
    Star_1,
    Star_2,
    Sunlight_1,
    Sunlight_2,
    Triangle_1,
    Triangle_2,
    Vertical,

    Custom,
  }

  /// <summary> Dissolves the sprite using patterns. </summary>
  public static class Dissolve
  {
    /// <summary> Dissolution amount, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Slide = new("_DissolveSlide", 1.0f, 0.0f, 1.0f);

    /// <summary> Dissolve modes, default DissolveMode.Color. </summary>
    /// <returns>Value.</returns>
    public static readonly KeywordsVariable Mode = new(new string[] { "DISSOLVE_NORMAL", "DISSOLVE_COLOR", "DISSOLVE_TEXTURE" }, 0);

    /// <summary> Border size, default 0.5 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable BorderSize = new("_DissolveBorderSize", 0.05f, 0.0f, 1.0f);

    /// <summary> Inner border color, default white. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable ColorInside = new("_DissolveBorderColorInside", Color.white);

    /// <summary> Color of the outer border, default white. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable ColorOutside = new("_DissolveBorderColorOutside", Color.white);

    /// <summary> Edge texture. </summary>
    /// <returns>Value.</returns>
    public static readonly TextureVariable BorderTexture = new("_DissolveBorderTex");

    /// <summary> Edge texture scale, default 1. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatVariable BorderTextureUVScale = new("_DissolveBorderTexUVScale", 1.0f);

    /// <summary> How to dissolve, default DissolveShape.Horizontal. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable Shape = new("_DissolveShape", DissolveShape.Horizontal, (material, value) =>
    {
      DissolveShape shape = (DissolveShape)value;
      if (shape != DissolveShape.Custom)
      {
        string texturePath = string.Format("Textures/Dissolve/{0}", shape.ToString());

        Texture texture = Resources.Load<Texture>(texturePath);
        if (texture != null)
          DissolveTexture.Set(material, texture);
        else
          SpriteMojo.LogError($"Texture '{texturePath}' not found.");
      }
    });

    /// <summary> Custom texture when Shape == DissolveShape.Custom. </summary>
    /// <returns>Value.</returns>
    public static readonly TextureVariable DissolveTexture = new("_DissolveTex");

    /// <summary> Scale shape used to dissolve, default 1.0 [0.01 - 5.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable UVScale = new("_DissolveUVScale", 1.0f, 0.01f, 5.0f);

    /// <summary> Reverse the direction of dissolving, default false. </summary>
    /// <returns>Value.</returns>
    public static readonly BoolVariable Invert = new("_DissolveInverse", false);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Dissolve/DissolveSprite");
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
      Slide.Reset(material);
      Mode.Reset(material);
      BorderSize.Reset(material);
      ColorInside.Reset(material);
      ColorOutside.Reset(material);
      BorderTexture.Reset(material);
      BorderTextureUVScale.Reset(material);
      Shape.Reset(material);
      DissolveTexture.Reset(material);
      UVScale.Reset(material);
      Invert.Reset(material);
    }    
  }
}