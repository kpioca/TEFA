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
  /// <summary> Black Hole material inspector. </summary>
  [CanEditMultipleObjects]
  public class BlackHoleSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "BlackHole")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      BlackHole.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Black Hole material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/BlackHole");

    protected override void ResetDefaultValues(Material material) => BlackHole.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Black Hole") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        SliderEditor("Radius", "Effect radius, default 0.1 [0.0 - 1.0].", material, BlackHole.Radius);
        Vector2Editor("Center", "Effect center.", material, BlackHole.Center);
        SliderEditor("Distortion", "Effect distortion, default 0.1 [0.0 - 1.0].", material, BlackHole.Distortion);
        ColorEditor("Color", "Effect color.", material, BlackHole.Color);

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}