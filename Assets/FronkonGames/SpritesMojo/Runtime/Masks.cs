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
  /// <summary> Apply effects in up to three masks defined by a texture. </summary>
  public static class Masks
  {
    /// <summary> Mask texture. Each channel (RGB) will be a mask. </summary>
    /// <returns>Value.</returns>
    public static readonly TextureVariable Mask = new("_MaskTex");

    /// <summary> Strength of the effect on the red channel mask, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable RedStrength = new("_MaskRedStrength", 1.0f, 0.0f, 1.0f);

    /// <summary> Type of color mixing in the red channel mask, default BlendColorOps.Multiply. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable RedBlend = new("_RedBlendOp", BlendFunction.Multiply);

    /// <summary> Color that is applied to the red mask. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable RedTint = new("_MaskRedTint", Color.white);

    /// <summary> Texture that is applied on the red mask. </summary>
    /// <returns>Value.</returns>
    public static readonly TextureVariable RedTexture = new("_MaskRedTex");

    /// <summary> Scale of the texture applied to the red mask, default 1.0 [-5.0 - 5.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable RedTextureScale = new("_MaskRedScale", 1.0f, -5.0f, 5.0f);

    /// <summary> Angle of the texture applied to the red mask, default 0.0 [0.0 - 180.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable RedTextureAngle = new("_MaskRedAngle", 0.0f, 0.0f, 180.0f);

    /// <summary> Velocity of the texture applied to the red mask, default Vector2.zero. </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable RedTextureVelocity = new("_MaskRedVel", Vector2.zero);

    /// <summary> Strength of the effect on the green channel mask, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable GreenStrength = new("_MaskGreenStrength", 1.0f, 0.0f, 1.0f);

    /// <summary> Type of color mixing in the green channel mask, default BlendColorOps.Multiply. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable GreenBlend = new("_GreenBlendOp", BlendFunction.Multiply);

    /// <summary> Color that is applied to the green mask. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable GreenTint = new("_MaskGreenTint", Color.white);

    /// <summary> Texture that is applied on the green mask. </summary>
    /// <returns>Value.</returns>
    public static readonly TextureVariable GreenTexture = new("_MaskGreenTex");

    /// <summary> Scale of the texture applied to the green mask, default 1.0 [-5.0 - 5.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable GreenTextureScale = new("_MaskGreenScale", 1.0f, -5.0f, 5.0f);

    /// <summary> Angle of the texture applied to the green mask, default 0.0 [0.0 - 180.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable GreenTextureAngle = new("_MaskGreenAngle", 0.0f, 0.0f, 180.0f);

    /// <summary> Velocity of the texture applied to the green mask, default Vector2.zero. </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable GreenTextureVelocity = new("_MaskGreenVel", Vector2.zero);

    /// <summary> Strength of the effect on the blue channel mask, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable BlueStrength = new("_MaskBlueStrength", 1.0f, 0.0f, 1.0f);

    /// <summary> Type of color mixing in the blue channel mask, default BlendColorOps.Multiply. </summary>
    /// <returns>Value.</returns>
    public static readonly EnumVariable BlueBlend = new("_BlueBlendOp", BlendFunction.Multiply);

    /// <summary> Color that is applied to the blue mask. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable BlueTint = new("_MaskBlueTint", Color.white);

    /// <summary> Texture that is applied on the blue mask. </summary>
    /// <returns>Value.</returns>
    public static readonly TextureVariable BlueTexture = new("_MaskBlueTex");

    /// <summary> Scale of the texture applied to the blue mask, default 1.0 [-5.0 - 5.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable BlueTextureScale = new("_MaskBlueScale", 1.0f, -5.0f, 5.0f);

    /// <summary> Angle of the texture applied to the blue mask, default 0.0 [0.0 - 180.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable BlueTextureAngle = new("_MaskBlueAngle", 0.0f, 0.0f, 180.0f);

    /// <summary> Velocity of the texture applied to the blue mask, default Vector2.zero. </summary>
    /// <returns>Value.</returns>
    public static readonly VectorVariable BlueTextureVelocity = new("_MaskBlueVel", Vector2.zero);

    /// <summary> Create a sprite with a NEW material with the effect. </summary>
    /// <param name="name">Sprite name.</param>
    /// <param name="parent">Sprite parent.</param>
    /// <returns>New GameObject.</returns>
    public static GameObject CreateSprite(string name = default, Transform parent = null) => SpriteMojo.CreateSprite(CreateMaterial(), name, parent);

    /// <summary> Create a NEW material with the effect. </summary>
    /// <returns>New material.</returns>
    public static Material CreateMaterial()
    {
      Material material = SpriteMojo.CreateMaterial("Shaders/Masks/MasksSprite");
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
      Mask.Reset(material);

      RedStrength.Reset(material);
      RedBlend.Reset(material);
      RedTint.Reset(material);
      RedTexture.Reset(material);
      RedTextureScale.Reset(material);
      RedTextureAngle.Reset(material);
      RedTextureVelocity.Reset(material);

      GreenStrength.Reset(material);
      GreenBlend.Reset(material);
      GreenTint.Reset(material);
      GreenTexture.Reset(material);
      GreenTextureScale.Reset(material);
      GreenTextureAngle.Reset(material);
      GreenTextureVelocity.Reset(material);

      BlueStrength.Reset(material);
      BlueBlend.Reset(material);
      BlueTint.Reset(material);
      BlueTexture.Reset(material);
      BlueTextureScale.Reset(material);
      BlueTextureAngle.Reset(material);
      BlueTextureVelocity.Reset(material);
    }    
  }
}