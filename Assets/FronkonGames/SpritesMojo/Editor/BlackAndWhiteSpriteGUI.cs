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
  /// <summary> Black And White material inspector. </summary>
  [CanEditMultipleObjects]
  public class BlackAndWhiteSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Black and White")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      BlackAndWhite.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Black and White material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/BlackAndWhite");

    protected override void ResetDefaultValues(Material material) => BlackAndWhite.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Black and White") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        SliderEditor("Threshold", "The threshold between black and white, default 0.5 [0.0 - 1.0].", material, BlackAndWhite.Threshold);

        SliderEditor("Softness", "Smooth transition between black and white, default 0.25 [0.0 - 1.0].", material, BlackAndWhite.Softness);

        SliderEditor("Exposure", "The amount of light, default 1.0 [0.0 - 5.0].", material, BlackAndWhite.Exposure, 5.0f);

        EditorHelper.Separator();

        EditorHelper.Label("Channels factor");

        EditorHelper.IndentLevel++;

        SliderEditor("Red", "Strength of the effect in the red channel, default 1.0 [0.0 - 1.0].", material, BlackAndWhite.Red);
        SliderEditor("Green", "Strength of the effect in the green channel, default 1.0 [0.0 - 1.0].", material, BlackAndWhite.Green);
        SliderEditor("Blue", "Strength of the effect in the blue channel, default 1.0 [0.0 - 1.0].", material, BlackAndWhite.Blue);

        EditorHelper.IndentLevel--;

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}