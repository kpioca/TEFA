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
  /// <summary> Highlight material inspector. </summary>
  [CanEditMultipleObjects]
  public class HighlightSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Highlight")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Highlight.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Highlight material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Highlight");

    protected override void ResetDefaultValues(Material material) => Highlight.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Highlight") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}