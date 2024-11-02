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
  /// <summary> Negative material inspector. </summary>
  [CanEditMultipleObjects]
  public class NegativeSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Negative")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Negative.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Negative material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Negative");

    protected override void ResetDefaultValues(Material material) => Negative.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Negative") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        Vector3 channels = Negative.ColorChannels.Get(material);

        EditorHelper.Label("Color channels");

        EditorHelper.IndentLevel++;

        channels.x = EditorHelper.Slider("Red", channels.x, 0.0f, 1.0f, 1.0f);
        channels.y = EditorHelper.Slider("Green", channels.y, 0.0f, 1.0f, 1.0f);
        channels.z = EditorHelper.Slider("Blue", channels.z, 0.0f, 1.0f, 1.0f);

        EditorHelper.IndentLevel--;

        Negative.ColorChannels.Set(material, channels);

        EditorHelper.Separator();

        Vector3 hsl = Negative.HSL.Get(material);

        EditorHelper.Label("Hue / Saturation / Luminance");

        EditorHelper.IndentLevel++;

        hsl.x = EditorHelper.Slider("Hue", hsl.x, 0.0f, 1.0f, 0.0f);
        hsl.y = EditorHelper.Slider("Saturation", hsl.y, 0.0f, 1.0f, 0.0f);
        hsl.z = EditorHelper.Slider("Luminance", hsl.z, 0.0f, 1.0f, 0.0f);

        EditorHelper.IndentLevel--;

        Negative.HSL.Set(material, hsl);

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}