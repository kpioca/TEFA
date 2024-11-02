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
using System;
using UnityEngine;
using UnityEditor;

namespace FronkonGames.SpritesMojo.Editor
{
  /// <summary> Material inspector base. </summary>
  public abstract class SpriteBaseGUI : ShaderGUI
  {
    public const string MenuSprite = "GameObject/2D Object/Sprites Mojo/";
    public const string MenuMaterial = "Assets/Create/2D/Sprites Mojo/";

    private MaterialProperty propertyPixelSnap;
    private MaterialProperty propertyRenderQueue;
    private MaterialProperty propertyCulling;

    protected abstract bool EffectPropertiesGUI(MaterialEditor materialEditor, MaterialProperty[] properties);

    protected virtual void ResetDefaultValues(Material material)
    {
    }

    protected virtual void FindProperties(MaterialProperty[] props)
    {
      propertyPixelSnap = FindProperty("PixelSnap", props);
      propertyRenderQueue = FindProperty("_RenderQueue", props);
      propertyCulling = FindProperty("_Cull", props);
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
      bool hasChanged = false;

      EditorHelper.LabelWidth = 0.0f;

      EditorHelper.Separator();

      bool isSprite = materialEditor.target.name.Contains("UI/Image") == false;
      if (isSprite == true)
        FindProperties(properties);

      hasChanged |= EffectPropertiesGUI(materialEditor, properties);

      hasChanged |= ColorPropertiesGUI(materialEditor);

      if (isSprite == true)
        hasChanged |= SpritePropertiesGUI(materialEditor);

      EditorHelper.Separator();

      EditorHelper.BeginHorizontal();
      {
        EditorHelper.Separator();

        if (GUILayout.Button(new GUIContent("documentation", "Online documentation"), Styles.miniLabelButton) == true)
          Application.OpenURL(SpriteMojo.Documentation);

        if (EditorPrefs.GetBool($"{SpriteMojo.AssemblyName}.Review") == false)
        {
          EditorHelper.Separator();

          if (GUILayout.Button(new GUIContent("write a review <color=#800000>❤️</color>", "Write a review, thanks!"), Styles.miniLabelButton) == true)
          {
            Application.OpenURL(SpriteMojo.Store);

            EditorPrefs.SetBool($"{SpriteMojo.AssemblyName}.Review", true);
          }
        }

        EditorHelper.FlexibleSpace();

        if (EditorHelper.Button("Reset") == true)
          ResetDefaultValues(materialEditor.target as Material);
      }
      EditorHelper.EndHorizontal();

      if (hasChanged == true)
      {
        for (int i = 0; i < materialEditor.targets.Length; ++i)
          SetMaterialKeywords((Material)materialEditor.targets[i]);
      }
    }

    private bool ColorPropertiesGUI(MaterialEditor materialEditor)
    {
      EditorGUI.BeginChangeCheck();

      Material material = materialEditor.target as Material;

      if (EditorHelper.Header("Color", material, SpriteMojo.ColorAdjust.Name) == true)
      {
        EditorHelper.IndentLevel++;

        GUI.enabled = SpriteMojo.ColorAdjust.Get(material);

        SliderEditor("Brightness", "Brightness, default 0.0 [-1.0 - 1.0].", material, SpriteMojo.Brightness);

        SliderEditor("Contrast", "Contrast, default 0.0 [-1.0 - 1.0].", material, SpriteMojo.Contrast);

        SliderEditor("Gamma", "Gamma, default 1.0 [0.1 - 10.0].", material, SpriteMojo.Gamma);

        if (material.shader.name.Contains("FronkonGames/SpritesMojo/BlackAndWhite") == false)
        {
          SliderEditor("Hue", "Hue, default 0.0 [0.0 - 1.0].", material, SpriteMojo.Hue);

          SliderEditor("Saturation", "Saturation, default 1.0 [0.0 - 5.0].", material, SpriteMojo.Saturation);
        }

        SliderEditor("Vibrance", "Color intentity, default 0.0 [-1.0 - 10.0].", material, SpriteMojo.Vibrance);

        GUI.enabled = true;

        EditorHelper.IndentLevel--;
      }

      EditorHelper.Separator();

      return EditorGUI.EndChangeCheck();
    }

    private bool SpritePropertiesGUI(MaterialEditor materialEditor)
    {
      bool hasChanged = false;

      if (EditorHelper.Header("Sprite") == true)
      {
        EditorHelper.IndentLevel++;

        EditorGUI.BeginChangeCheck();

        Material material = materialEditor.target as Material;

        MaterialExtensions.SetBlendMode(material, (SpriteMojo.BlendModes)EditorHelper.EnumPopup("Blend mode", "", MaterialExtensions.GetBlendMode(material), SpriteMojo.BlendModes.PreMultipliedAlpha));

        EditorGUI.showMixedValue = propertyRenderQueue.hasMixedValue;
        int renderQueue = EditorGUILayout.IntSlider("Render queue", (int)propertyRenderQueue.floatValue, 0, 49);
        if (EditorGUI.EndChangeCheck() == true)
        {
          SetInt(materialEditor, "_RenderQueue", renderQueue);

          hasChanged = true;
        }

        EditorGUI.BeginChangeCheck();

        SpriteMojo.Culling culling = (SpriteMojo.Culling)Mathf.RoundToInt(propertyCulling.floatValue);
        EditorGUI.showMixedValue = propertyCulling.hasMixedValue;

        culling = (SpriteMojo.Culling)EditorGUILayout.Popup("Culling", (int)culling, new string[] { "Off", "Front", "Back" });
        if (EditorGUI.EndChangeCheck() == true)
        {
          SetInt(materialEditor, "_Cull", (int)culling);

          hasChanged = true;
        }

        EditorGUI.showMixedValue = false;

        EditorGUI.BeginChangeCheck();

        materialEditor.ShaderProperty(propertyPixelSnap, "Pixel snap");

        hasChanged |= EditorGUI.EndChangeCheck();

        EditorHelper.IndentLevel--;
      }
      
      EditorHelper.Separator();

      return hasChanged;
    }

    private void SetMaterialKeywords(Material material)
    {
      SpriteMojo.BlendModes blendMode = MaterialExtensions.GetBlendMode(material);
      MaterialExtensions.SetBlendMode(material, blendMode);

      if (material.HasProperty("_ZWrite") == true)
      {
        bool zWrite = material.GetFloat("_ZWrite") > 0.0f;
        bool clipAlpha = zWrite && blendMode != SpriteMojo.BlendModes.Opaque && material.GetFloat("_Cutoff") > 0.0f;
        SetKeyword(material, "_ALPHA_CLIP", clipAlpha);
      }
    }

    private void SetInt(MaterialEditor materialEditor, string propertyName, int value)
    {
      for (int i = 0; i < materialEditor.targets.Length; ++i)
        ((Material)materialEditor.targets[i]).SetInt(propertyName, value);
    }

    private void SetKeyword(MaterialEditor materialEditor, string keyword, bool state)
    {
      foreach (Material material in materialEditor.targets)
        SetKeyword(material, keyword, state);
    }

    private void SetKeyword(Material material, string keyword, bool state)
    {
      if (state == true)
        material.EnableKeyword(keyword);
      else
        material.DisableKeyword(keyword);
    }

    protected bool ToggleKeyword(MaterialEditor materialEditor, string label, string keyword)
    {
      bool enabled = false;

      Material material = materialEditor.target as Material;
      if (material != null)
      {
        enabled = Array.IndexOf(material.shaderKeywords, keyword) != -1;
        enabled = EditorHelper.Toggle(label, enabled);

        if (enabled == true)
          material.EnableKeyword(keyword);
        else
          material.DisableKeyword(keyword);
      }

      return enabled;
    }

    protected void BoolEditor(string label, string comment, Material material, BoolVariable variable)
    {
      variable.Set(material, EditorHelper.Toggle(label, comment, variable.Get(material), variable.ResetValue));
    }

    protected void KeywordEditor(string label, string comment, Material material, KeywordVariable variable)
    {
      variable.Set(material, EditorHelper.Toggle(label, comment, variable.Get(material), variable.ResetValue));
    }

    protected void KeywordsEditor<T>(string label, string comment, Material material, KeywordsVariable variable) where T : struct, IConvertible
    {
      variable.Set(material, Convert.ToInt32(EditorHelper.EnumPopup(label, comment, (T)(object)variable.Get(material) as Enum, (T)(object)variable.ResetValue as Enum)));
    }

    protected void EnumEditor<T>(string label, string comment, Material material, EnumVariable variable)
    {
      variable.Set(material, EditorHelper.EnumPopup(label, comment, variable.Get<T>(material) as Enum, variable.ResetValue));
    }

    protected void SliderEditor(string label, string comment, Material material, IntRangeVariable variable)
    {
      variable.Set(material, EditorHelper.Slider(label, comment, variable.Get(material), variable.Min, variable.Max, variable.ResetValue));
    }

    protected void SliderEditor(string label, string comment, Material material, IntPositiveVariable variable, int max)
    {
      variable.Set(material, EditorHelper.Slider(label, comment, variable.Get(material), 0, max, variable.ResetValue));
    }

    protected void SliderEditor(string label, string comment, Material material, FloatRangeVariable variable)
    {
      variable.Set(material, EditorHelper.Slider(label, comment, variable.Get(material), variable.Min, variable.Max, variable.ResetValue));
    }

    protected void SliderEditor(string label, string comment, Material material, FloatVariable variable, float min, float max)
    {
      variable.Set(material, EditorHelper.Slider(label, comment, variable.Get(material), min, max, variable.ResetValue));
    }

    protected void SliderEditor(string label, string comment, Material material, FloatPositiveVariable variable, float max)
    {
      variable.Set(material, EditorHelper.Slider(label, comment, variable.Get(material), 0.0f, max, variable.ResetValue));
    }

    protected void SliderEditor(string label, string comment, Material material, IntVariable variable, int min, int max)
    {
      variable.Set(material, EditorHelper.Slider(label, comment, variable.Get(material), min, max, variable.ResetValue));
    }

    protected void MinMaxEditor(string label, string comment, Material material, FloatMinMaxVariable variable)
    {
      var value = variable.Get(material);

      EditorGUILayout.MinMaxSlider(label, ref value.Item1, ref value.Item2, variable.Limit.Item1, variable.Limit.Item2);

      variable.Set(material, value);
    }

    protected void Vector2Editor(string label, string comment, Material material, VectorVariable variable)
    {
      variable.Set(material, EditorHelper.Vector2(label, comment, variable.Get(material), variable.ResetValue));
    }

    protected void Vector3Editor(string label, string comment, Material material, VectorVariable variable)
    {
      variable.Set(material, EditorHelper.Vector3(label, comment, variable.Get(material), variable.ResetValue));
    }

    protected void ColorEditor(string label, string comment, Material material, ColorVariable variable)
    {
      variable.Set(material, EditorHelper.Color(label, comment, variable.Get(material), variable.ResetValue));
    }

    protected void TextureEditor(string label, string comment, Material material, TextureVariable variable)
    {
      variable.Set(material, EditorHelper.Texture(label, comment, variable.Get(material)));
    }
  }
}