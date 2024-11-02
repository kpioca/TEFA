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
  /// <summary> Outline material inspector. </summary>
  [CanEditMultipleObjects]
  public class OutlineSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Outline")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Outline.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Outline material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Outline");

    protected override void ResetDefaultValues(Material material) => Outline.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Outline") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        SliderEditor("Size", "Border size, default 5 [0 - 50].", material, Outline.Size, 50);

        EnumEditor<OutlineMode>("Mode", "Border type, default OutlineMode.Solid.", material, Outline.Mode);

        OutlineMode outlineMode = Outline.Mode.Get<OutlineMode>(material);

        EditorHelper.IndentLevel++;

        switch (outlineMode)
        {
          case OutlineMode.Solid:
            ColorEditor("Color", "Border color.", material, Outline.Color0);
          break;

          case OutlineMode.Gradient:
          {
            ColorEditor("Color 1", "Main color.", material, Outline.Color0);
            ColorEditor("Color 2", "Secondary color.", material, Outline.Color1);

            SliderEditor("Scale", "Gradient scale, valid for OutlineMode.Gradient, default 1.0 [-10.0 - 10.0].", material, Outline.GradientScale);
            SliderEditor("Offset", "Gradient offset, valid for OutlineMode.Gradient, default 0.0 [-10.0 - 10.0].", material, Outline.GradientOffset);

            KeywordEditor("Vertical", "Vertical gradient, default false.", material, Outline.Vertical);
          }
          break;

          case OutlineMode.Texture:
          {
            TextureEditor("Texture", "Texture border, valid for OutlineMode.Texture.", material, Outline.Texture);

            ColorEditor("Color", "Texture color.", material, Outline.Color0);

            SliderEditor("Scale", "Texture border scale, default 1.0 [0.0 - 5.0].", material, Outline.TextureScale);
            SliderEditor("Angle", "Texture border angle, default 0.0 [0.0 - 180.0].", material, Outline.TextureAngle);

            Vector2Editor("Velocity", "Texture border velocity, default Vector2.zero.", material, Outline.TextureVelocity);
          }
          break;
        }

        EditorHelper.IndentLevel--;

        SliderEditor("Threshold", "Sensitivity to the alpha channel, default 0.0 [0.0 - 0.9999].", material, Outline.Threshold);

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}