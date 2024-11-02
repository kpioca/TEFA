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
  /// <summary> Shift material inspector. </summary>
  [CanEditMultipleObjects]
  public class ShiftSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Shift")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Shift.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Shift material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Shift");

    protected override void ResetDefaultValues(Material material) => Shift.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Shift") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        KeywordsEditor<ShiftMode>("Mode", "Type of displacement, default ShiftMode.Linear.", material, Shift.Mode);

        ShiftMode mode = (ShiftMode)Shift.Mode.Get(material);
        if (mode == ShiftMode.Linear)
        {
          EditorHelper.Label("Shift");

          EditorHelper.IndentLevel++;

          Vector2Editor("Red", "Red channel linear shift, default (0.0, 0.0).", material, Shift.RedShift);
          Vector2Editor("Green", "Green channel linear shift, default (0.0, 0.0).", material, Shift.GreenShift);
          Vector2Editor("Blue", "Blue channel linear shift, default (0.0, 0.0).", material, Shift.BlueShift);

          EditorHelper.IndentLevel--;
        }
        else
          SliderEditor("Shift", "Radial shift, default 0.0 [0.0 - 1.0].", material, Shift.RadialShift);

        EditorHelper.Separator();

        KeywordEditor("Noise", "Add noise to shift intensity, default false.", material, Shift.Noise);

        if (Shift.Noise.Get(material) == true)
        {
          EditorHelper.IndentLevel++;

          SliderEditor("Strength", "Noise strength, default 0.0 [0.0 - 1.0].", material, Shift.NoiseStrength);
          SliderEditor("Speed", "Noise speed, default 0.5 [0.0 - 1.0].", material, Shift.NoiseSpeed);

          EditorHelper.IndentLevel--;
        }

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}