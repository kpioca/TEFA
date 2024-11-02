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
using UnityEditor;

namespace FronkonGames.SpritesMojo.Editor
{
  /// <summary> Masks material inspector. </summary>
  [CanEditMultipleObjects]
  public class MasksSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Masks")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Masks.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Masks material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Masks");

    protected override void ResetDefaultValues(Material material) => Masks.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Masks") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        TextureEditor("Mask", "Mask texture. Each channel (RGB) will be a mask.", material, Masks.Mask);

        EditorHelper.Separator();

        EditorGUILayout.LabelField("Red channel");
        {
          EditorHelper.IndentLevel++;

          SliderEditor("Strength", "Strength of the effect on the red channel mask, default 1.0 [0.0 - 1.0].", material, Masks.RedStrength);

          EnumEditor<BlendFunction>("Blend", "Type of color mixing in the red channel mask, default BlendColorOps.Multiply.", material, Masks.RedBlend);
          ColorEditor("Tint", "Color that is applied to the red mask.", material, Masks.RedTint);

          TextureEditor("Texture", "Texture that is applied on the red mask.", material, Masks.RedTexture);

          if (Masks.RedTexture != null)
          {
            SliderEditor("Scale", "Scale of the texture applied to the red mask, default 1.0 [-5.0 - 5.0].", material, Masks.RedTextureScale);
            SliderEditor("Angle", "Angle of the texture applied to the red mask, default 0.0 [0.0 - 180.0].", material, Masks.RedTextureAngle);
            Vector2Editor("Velocity", "Velocity of the texture applied to the red mask, default Vector2.zero.", material, Masks.RedTextureVelocity);
          }

          EditorHelper.IndentLevel--;
        }

        EditorHelper.Separator();

        EditorGUILayout.LabelField("Green channel");
        {
          EditorHelper.IndentLevel++;

          SliderEditor("Strength", "Strength of the effect on the green channel mask, default 1.0 [0.0 - 1.0].", material, Masks.GreenStrength);

          EnumEditor<BlendFunction>("Blend", "Type of color mixing in the green channel mask, default BlendColorOps.Multiply.", material, Masks.GreenBlend);
          ColorEditor("Tint", "Color that is applied to the green mask.", material, Masks.GreenTint);

          TextureEditor("Texture", "Texture that is applied on the green mask.", material, Masks.GreenTexture);
          if (Masks.GreenTexture != null)
          {
            SliderEditor("Scale", "Scale of the texture applied to the green mask, default 1.0 [-5.0 - 5.0].", material, Masks.GreenTextureScale);
            SliderEditor("Angle", "Angle of the texture applied to the green mask, default 0.0 [0.0 - 180.0].", material, Masks.GreenTextureAngle);
            Vector2Editor("Velocity", "Velocity of the texture applied to the green mask, default Vector2.zero.", material, Masks.GreenTextureVelocity);
          }

          EditorHelper.IndentLevel--;
        }

        EditorHelper.Separator();

        EditorGUILayout.LabelField("Blue channel");
        {
          EditorHelper.IndentLevel++;
          
          SliderEditor("Strength", "Strength of the effect on the blue channel mask, default 1.0 [0.0 - 1.0].", material, Masks.BlueStrength);

          EnumEditor<BlendFunction>("Blend", "Type of color mixing in the blue channel mask, default BlendColorOps.Multiply.", material, Masks.BlueBlend);
          ColorEditor("Tint", "Color that is applied to the blue mask.", material, Masks.BlueTint);

          TextureEditor("Texture", "Texture that is applied on the blue mask.", material, Masks.BlueTexture);
          if (Masks.BlueTexture != null)
          {
            SliderEditor("Scale", "Scale of the texture applied to the blue mask, default 1.0 [-5.0 - 5.0].", material, Masks.BlueTextureScale);
            SliderEditor("Angle", "Angle of the texture applied to the blue mask, default 0.0 [0.0 - 180.0].", material, Masks.BlueTextureAngle);
            Vector2Editor("Velocity", "Velocity of the texture applied to the blue mask, default Vector2.zero.", material, Masks.BlueTextureVelocity);
          }

          EditorHelper.IndentLevel--;
        }

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}