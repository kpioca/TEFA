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
  /// <summary> Duo Tone material inspector. </summary>
  [CanEditMultipleObjects]
  public class DuoToneSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Duo Tone")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      DuoTone.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Duo Tone material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/DuoTone");

    protected override void ResetDefaultValues(Material material) => DuoTone.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Duo Tone") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        ColorEditor("Bright color", "First color, the brightest, default White.", material, DuoTone.BrightColor);

        ColorEditor("Dark color", "Second color, darker, default black.", material, DuoTone.DarkColor);

        SliderEditor("Threshold", "Threshold between the two colors, default 0.25 [0.0 - 1.0].", material, DuoTone.Threshold);

        SliderEditor("Softness", "Smoothness between the two colors, default 0.25 [0.0 - 1.0].", material, DuoTone.Softness);

        GUILayout.BeginHorizontal();
        {
          var value = DuoTone.Luminance.Get(material);

          EditorGUILayout.MinMaxSlider("Luminance", ref value.Item1, ref value.Item2, DuoTone.Luminance.Limit.Item1, DuoTone.Luminance.Limit.Item2);

          DuoTone.Luminance.Set(material, value);

          if (GUILayout.Button("Auto", GUILayout.Width(50.0f), GUILayout.Height(17.0f)) == true)
            DuoTone.AutoLuminance(material, FindProperty("_MainTex", properties).textureValue);

          if (EditorHelper.ResetButton() == true)
            DuoTone.Luminance.Reset(material);
        }
        EditorHelper.EndHorizontal();

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}