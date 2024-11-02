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
  /// <summary> Ramp material inspector. </summary>
  [CanEditMultipleObjects]
  public class RampSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Ramp")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Ramp.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Ramp material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Ramp");

    protected override void ResetDefaultValues(Material material) => Ramp.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Ramp") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        ColorEditor("Color 1", "First color of the gradient (lower luminosity).", material, Ramp.Color0);
        ColorEditor("Color 2", "Second color of the gradient.", material, Ramp.Color1);
        ColorEditor("Color 3", "Third color of the gradient.", material, Ramp.Color2);
        ColorEditor("Color 4", "Fourth color of the gradient.", material, Ramp.Color3);
        ColorEditor("Color 5", "Fifth color of the gradient (greater luminosity).", material, Ramp.Color4);

        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        {
          EditorHelper.FlexibleSpace();

          if (EditorHelper.MiniButton("search") == true)
            ColourLoversSearch.ShowTool();

          if (EditorHelper.MiniButton("random") == true)
          {
            Palette colorEntry = ColourLovers.Random();

            Ramp.SetRamp(material, colorEntry.colors[0], colorEntry.colors[1], colorEntry.colors[2], colorEntry.colors[3], colorEntry.colors[4]);
            Ramp.SortRampByLuminance(material);

            EditorHelper.SetDirty(materialEditor.target);
          }

          EditorHelper.Separator();

          if (EditorHelper.MiniButton("sort") == true)
            Ramp.SortRampByLuminance(material);

          if (EditorHelper.MiniButton("copy") == true)
            CopyColors(properties);

          if (EditorHelper.MiniButton("paste") == true)
          {
            PasteColors(properties);

            Ramp.SortRampByLuminance(material);
          }

          EditorHelper.Separator();
        }
        EditorHelper.EndHorizontal();

        EditorHelper.Separator();

        KeywordEditor("Invert", "", material, Ramp.Invert);

        GUILayout.BeginHorizontal();
        {
          var value = Ramp.Luminance.Get(material);

          EditorGUILayout.MinMaxSlider("Luminance", ref value.Item1, ref value.Item2, Ramp.Luminance.Limit.Item1, Ramp.Luminance.Limit.Item2);

          Ramp.Luminance.Set(material, value);

          if (GUILayout.Button("Auto", GUILayout.Width(50.0f), GUILayout.Height(17.0f)) == true)
            Ramp.AutoLuminance(material, FindProperty("_MainTex", properties).textureValue);

          if (EditorHelper.ResetButton() == true)
            Ramp.Luminance.Reset(material);
        }
        EditorHelper.EndHorizontal();

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }

    private void CopyColors(MaterialProperty[] properties)
    {
      Palette colorEntry = new Palette();

      colorEntry.colors = new Color[5];
      colorEntry.colors[0] = FindProperty("_RampColor0", properties).colorValue;
      colorEntry.colors[1] = FindProperty("_RampColor1", properties).colorValue;
      colorEntry.colors[2] = FindProperty("_RampColor2", properties).colorValue;
      colorEntry.colors[3] = FindProperty("_RampColor3", properties).colorValue;
      colorEntry.colors[4] = FindProperty("_RampColor4", properties).colorValue;

      EditorGUIUtility.systemCopyBuffer = JsonUtility.ToJson(colorEntry);
    }

    private void PasteColors(MaterialProperty[] properties)
    {
      string json = EditorGUIUtility.systemCopyBuffer;
      if (string.IsNullOrEmpty(json) == false)
      {
        Palette colorEntry = JsonUtility.FromJson<Palette>(json);
        FindProperty("_RampColor0", properties).colorValue = colorEntry.colors[0];
        FindProperty("_RampColor1", properties).colorValue = colorEntry.colors[1];
        FindProperty("_RampColor2", properties).colorValue = colorEntry.colors[2];
        FindProperty("_RampColor3", properties).colorValue = colorEntry.colors[3];
        FindProperty("_RampColor4", properties).colorValue = colorEntry.colors[4];
      }
    }
  }
}