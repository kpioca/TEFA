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
  /// <summary> Dissolve material inspector. </summary>
  [CanEditMultipleObjects]
  public class DissolveSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Dissolve")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Dissolve.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Dissolve material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Dissolve");

    protected override void ResetDefaultValues(Material material) => Dissolve.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Dissolve") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Slide", "Dissolution amount, default 1.0 [0.0 - 1.0].", material, Dissolve.Slide);

        EditorHelper.Separator();

        KeywordsEditor<DissolveMode>("Mode", "Dissolve modes, default DissolveMode.Color", material, Dissolve.Mode);

        EditorHelper.IndentLevel++;

        DissolveMode dissolveMode = (DissolveMode)Dissolve.Mode.Get(material);
        if (dissolveMode != DissolveMode.Normal)
        {
          SliderEditor("Size", "Border size, default 0.5 [0.0 - 1.0].", material, Dissolve.BorderSize);

          ColorEditor("Inside", "Inner border color, default white.", material, Dissolve.ColorInside);

          ColorEditor("Outside", "Color of the outer border., default white.", material, Dissolve.ColorOutside);

          if (dissolveMode == DissolveMode.Texture)
          {
            TextureEditor("Border texture", "Edge texture.", material, Dissolve.BorderTexture);

            SliderEditor("Scale", "Edge texture scale, default 1.", material, Dissolve.BorderTextureUVScale, -10.0f, 10.0f);
          }
        }

        EditorHelper.IndentLevel--;

        EnumEditor<DissolveShape>("Shape", "How to dissolve, default DissolveShape.Horizontal.", material, Dissolve.Shape);

        DissolveShape dissolveShape = Dissolve.Shape.Get<DissolveShape>(material);
        if (dissolveShape == DissolveShape.Custom)
        {
          EditorHelper.IndentLevel++;

          TextureEditor("Custom texture", "Custom texture when Shape == DissolveShape.Custom.", material, Dissolve.DissolveTexture);

          EditorHelper.IndentLevel--;
        }

        SliderEditor("Shape scale", "Scale shape used to dissolve, default 1.0 [0.01 - 5.0].", material, Dissolve.UVScale);

        BoolEditor("Invert", "Reverse the direction of dissolving, default false.", material, Dissolve.Invert);

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}