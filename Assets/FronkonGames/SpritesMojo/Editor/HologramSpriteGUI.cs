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
  /// <summary> Hologram material inspector. </summary>
  [CanEditMultipleObjects]
  public class HologramSpriteGUI : SpriteBaseGUI
  {
    [MenuItem(MenuSprite + "Hologram")]
    private static void CreateSprite(MenuCommand menuCommand)
    {
      GameObject gameObject = menuCommand.context as GameObject;

      Hologram.CreateSprite(string.Empty, gameObject != null ? gameObject.transform : null);
    }

    [MenuItem(MenuMaterial + "Hologram material", false, 301)]
    private static void CreateMaterial(MenuCommand menuCommand) => AssetsHelper.CreateMaterial("FronkonGames/SpritesMojo/Hologram");

    protected override void ResetDefaultValues(Material material) => Hologram.Reset(material);

    protected override bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;
      if (material != null && EditorHelper.Header("Hologram") == true)
      {
        EditorHelper.IndentLevel++;

        SliderEditor("Amount", "Effect strength, default 1.0 [0.0 - 1.0].", material, SpriteMojo.Amount);

        EditorHelper.Separator();

        SliderEditor("Distortion", "Hologram distortion, default 5.0 [0.0 - 10.0].", material, Hologram.Distortion, 10.0f);

        EditorHelper.Separator();

        SliderEditor("Blink strength", "Hologram strength, default 0.03 [0.0 - 1.0].", material, Hologram.BlinkStrength);
        SliderEditor("Blink speed", "Hologram speed, default 50.0 [0.0 - 100.0].", material, Hologram.BlinkSpeed, 100.0f);

        EditorHelper.Separator();

        SliderEditor("Scanline strength", "Scanlines strength, default 0.1 [0.0 - 1.0].", material, Hologram.ScanlineStrength);
        SliderEditor("Scanline count", "Number of lines, default 10.0 [0.0 - 20.0].", material, Hologram.ScanlineCount, 20.0f);
        SliderEditor("Scanline speed", "Scanline speed, default 10.0 [-50.0 - 50.0].", material, Hologram.ScanlineSpeed, -50.0f, 50.0f);

        EditorHelper.Separator();

        ColorEditor("Tint", "Hologram color.", material, Hologram.Tint);

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }
  }
}