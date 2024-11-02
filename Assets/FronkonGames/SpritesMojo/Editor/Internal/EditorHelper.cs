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
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace FronkonGames.SpritesMojo.Editor
{
  /// <summary>
  /// Utilities for the Editor.
  /// </summary>
  public static class EditorHelper
  {
    /// <summary>
    /// Indent level.
    /// </summary>
    public static int IndentLevel
    {
      get { return EditorGUI.indentLevel; }
      set { EditorGUI.indentLevel = value; }
    }

    /// <summary>
    /// Label width.
    /// </summary>
    public static float LabelWidth
    {
      get { return EditorGUIUtility.labelWidth; }
      set { EditorGUIUtility.labelWidth = value; }
    }

    /// <summary>
    /// Field width.
    /// </summary>
    public static float FieldWidth
    {
      get { return EditorGUIUtility.fieldWidth; }
      set { EditorGUIUtility.fieldWidth = value; }
    }

    /// <summary>
    /// GUI enabled?
    /// </summary>
    public static bool Enabled
    {
      get { return GUI.enabled; }
      set { GUI.enabled = value; }
    }

    /// <summary>
    /// GUI changed?
    /// </summary>
    public static bool Changed
    {
      get { return GUI.changed; }
      set { GUI.changed = value; }
    }

    private static readonly Dictionary<string, GUIContent> GUIContentCache;

    static EditorHelper()
    {
      GUIContentCache = new Dictionary<string, GUIContent>();
    }

    /// <summary>
    /// Reset some GUI variables.
    /// </summary>
    public static void Reset(int indentLevel = 0, float labelWidth = 0.0f, float fieldWidth = 0.0f, bool guiEnabled = true)
    {
      EditorGUI.indentLevel = 0;
      EditorGUIUtility.labelWidth = 0.0f;
      EditorGUIUtility.fieldWidth = 0.0f;
      GUI.enabled = true;
    }

    /// <summary>
    /// Line separator.
    /// </summary>
    public static void Line()
    {
      EditorGUILayout.Separator();

      GUILayout.Box(string.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(1.0f));

      EditorGUILayout.Separator();
    }

    /// <summary>
    /// Separator.
    /// </summary>
    public static void Separator(float space = 0.0f)
    {
      if (space <= 0.0f)
        EditorGUILayout.Separator();
      else
        GUILayout.Space(space);
    }

    /// <summary>
    /// Expand width.
    /// </summary>
    public static void ExpandWidth(bool expand = true)
    {
      GUILayout.ExpandWidth(expand);
    }

    /// <summary>
    /// Expand width.
    /// </summary>
    public static void ExpandHeight(bool expand = true)
    {
      GUILayout.ExpandHeight(expand);
    }

    /// <summary>
    /// BeginChangeCheck.
    /// </summary>
    public static void BeginChangeCheck()
    {
      EditorGUI.BeginChangeCheck();
    }

    /// <summary>
    /// EndChangeCheck.
    /// </summary>
    public static bool EndChangeCheck()
    {
      return EditorGUI.EndChangeCheck();
    }

    /// <summary>
    /// Begin vertical.
    /// </summary>
    public static void BeginVertical()
    {
      EditorGUILayout.BeginVertical();
    }

    /// <summary>
    /// End vertical.
    /// </summary>
    public static void EndVertical()
    {
      EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// Begin horizontal.
    /// </summary>
    public static void BeginHorizontal()
    {
      EditorGUILayout.BeginHorizontal();
    }

    /// <summary>
    /// End horizontal.
    /// </summary>
    public static void EndHorizontal()
    {
      EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Flexible space.
    /// </summary>
    public static void FlexibleSpace()
    {
      GUILayout.FlexibleSpace();
    }

    /// <summary>
    /// Label.
    /// </summary>
    public static void Label(string label, string tooltip = default)
    {
      EditorGUILayout.LabelField(new GUIContent(label, tooltip));
    }

    /// <summary>
    /// Button.
    /// </summary>
    public static bool Button(string label, string tooltip = default, GUIStyle style = null)
    {
      return GUILayout.Button(new GUIContent(label, tooltip), style ?? GUI.skin.button);
    }

    /// <summary>
    /// Toggle with reset.
    /// </summary>
    public static bool Toggle(string label, string tooltip, bool value, bool resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Toggle(new GUIContent(label, tooltip), value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Toggle.
    /// </summary>
    public static bool Toggle(string label, bool value)
    {
      return EditorGUILayout.Toggle(label, value);
    }

    /// <summary>
    /// Toggle.
    /// </summary>
    public static bool Toggle(string label, string tooltip, bool value)
    {
      return EditorGUILayout.Toggle(new GUIContent(label, tooltip), value);
    }

    /// <summary>
    /// Toggle with reset.
    /// </summary>
    public static bool Toggle(string label, bool value, bool resetValue)
    {
      return Toggle(label, string.Empty, value, resetValue);
    }

    /// <summary>
    /// Enum popup with reset.
    /// </summary>
    public static Enum EnumPopup(string label, string tooltip, Enum selected, Enum resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        selected = EditorGUILayout.EnumPopup(new GUIContent(label, tooltip), selected);

        if (ResetButton(resetValue) == true)
          selected = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return selected;
    }

    /// <summary>
    /// Enum popup with reset.
    /// </summary>
    public static Enum EnumPopup(string label, Enum selected, Enum resetValue)
    {
      return EnumPopup(label, string.Empty, selected, resetValue);
    }

    /// <summary>
    /// Slider with reset.
    /// </summary>
    public static float Slider(string label, string tooltip, float value, float minValue, float maxValue, float resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Slider(new GUIContent(label, tooltip), value, minValue, maxValue);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Slider with reset.
    /// </summary>
    public static float Slider(string label, float value, float minValue, float maxValue, float resetValue)
    {
      return Slider(label, string.Empty, value, minValue, maxValue, resetValue);
    }

    /// <summary>
    /// Float field with reset.
    /// </summary>
    public static float Float(string label, string tooltip, float value, float resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.FloatField(new GUIContent(label, tooltip), value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Float field with reset.
    /// </summary>
    public static float Float(string label, float value, float resetValue)
    {
      return Float(label, string.Empty, value, resetValue);
    }

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int Slider(string label, string tooltip, int value, int minValue, int maxValue, int resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntSlider(new GUIContent(label, tooltip), value, minValue, maxValue);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int Slider(string label, int value, int minValue, int maxValue, int resetValue)
    {
      return Slider(label, string.Empty, value, minValue, maxValue, resetValue);
    }

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int Int(string label, string tooltip, int value, int resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntField(new GUIContent(label, tooltip), value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int Int(string label, int value, int resetValue)
    {
      return Int(label, string.Empty, value, resetValue);
    }

    /// <summary>
    /// Int popup field with reset.
    /// </summary>
    public static int IntPopup(string label, int value, string[] names, int[] values, int resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntPopup(label, value, names, values);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Min-max slider with reset.
    /// </summary>
    public static void MinMaxSlider(string label, string tooltip, ref float minValue, ref float maxValue, float minLimit, float maxLimit, float defaultMinLimit, float defaultMaxLimit)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.MinMaxSlider(new GUIContent(label, tooltip), ref minValue, ref maxValue, minLimit, maxLimit);

        if (GUILayout.Button("R", GUILayout.Width(18.0f), GUILayout.Height(17.0f)) == true)
        {
          minValue = defaultMinLimit;
          maxValue = defaultMaxLimit;
        }
      }
      EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Min-max slider with reset.
    /// </summary>
    public static void MinMaxSlider(string label, ref float minValue, ref float maxValue, float minLimit, float maxLimit, float defaultMinLimit, float defaultMaxLimit)
    {
      MinMaxSlider(label, string.Empty, ref minValue, ref maxValue, minLimit, maxLimit, defaultMinLimit, defaultMaxLimit);
    }

    /// <summary>
    /// Color field with reset.
    /// </summary>
    public static Color Color(string label, string tooltip, Color value, Color resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.ColorField(new GUIContent(label, tooltip), value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Color field with reset.
    /// </summary>
    public static Color Color(string label, Color value, Color resetValue)
    {
      return Color(label, string.Empty, value, resetValue);
    }

    /// <summary>
    /// Animation curve.
    /// </summary>
    public static AnimationCurve Curve(string label, string tooltip, AnimationCurve value)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.CurveField(new GUIContent(label, tooltip), value);

        if (ResetButton() == true)
          value = new AnimationCurve(new Keyframe(1.0f, 0.0f, 0.0f, 0.0f), new Keyframe(0.0f, 1.0f, 0.0f, 0.0f));
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Animation curve.
    /// </summary>
    public static AnimationCurve Curve(string label, AnimationCurve curve)
    {
      return Curve(label, string.Empty, curve);
    }

    /// <summary>
    /// Vector2 field with reset.
    /// </summary>
    public static Vector2 Vector2(string label, string tooltip, Vector2 value, Vector2 resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Vector2Field(new GUIContent(label, tooltip), value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Vector2 field with reset.
    /// </summary>
    public static Vector2 Vector2(string label, Vector2 value, Vector2 resetValue)
    {
      return Vector2(label, string.Empty, value, resetValue);
    }

    /// <summary>
    /// Vector3 field with reset.
    /// </summary>
    public static Vector3 Vector3(string label, string tooltip, Vector3 value, Vector3 resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Vector3Field(new GUIContent(label, tooltip), value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Vector3 field with reset.
    /// </summary>
    public static Vector3 Vector3(string label, Vector3 value, Vector3 resetValue)
    {
      return Vector3(label, string.Empty, value, resetValue);
    }

    /// <summary>
    /// Texture field.
    /// </summary>
    public static Texture Texture(string label, Texture value)
    {
      return Texture(label, string.Empty, value);
    }

    /// <summary>
    /// Texture field.
    /// </summary>
    public static Texture Texture(string label, string tooltip, Texture value)
    {
      return EditorGUILayout.ObjectField(new GUIContent(label, tooltip), value, typeof(Texture), false) as Texture;
    }

    /// <summary>
    /// Layermask field with reset.
    /// </summary>
    public static LayerMask LayerMask(string label, LayerMask layerMask, int resetValue)
    {
      List<string> layers = new List<string>();
      List<int> layerNumbers = new List<int>();

      for (int i = 0; i < 32; ++i)
      {
        string layerName = UnityEngine.LayerMask.LayerToName(i);
        if (string.IsNullOrEmpty(layerName) == false)
        {
          layers.Add(layerName);
          layerNumbers.Add(i);
        }
      }

      int maskWithoutEmpty = 0;
      for (int i = 0; i < layerNumbers.Count; ++i)
      {
        if (((1 << layerNumbers[i]) & layerMask.value) > 0)
          maskWithoutEmpty |= (1 << i);
      }

      EditorGUILayout.BeginHorizontal();
      {
        maskWithoutEmpty = EditorGUILayout.MaskField(label, maskWithoutEmpty, layers.ToArray());
        int mask = 0;
        for (int i = 0; i < layerNumbers.Count; ++i)
        {
          if ((maskWithoutEmpty & (1 << i)) > 0)
            mask |= (1 << layerNumbers[i]);
        }

        layerMask.value = mask;

        if (ResetButton(resetValue) == true)
          layerMask.value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return layerMask;
    }

    /// <summary>
    /// Creates a texture for use in the Editor.
    /// </summary>
    public static Texture2D MakeTexture(int width, int height, Color color)
    {
      Color[] pixels = new Color[width * height];

      for (int i = 0; i < pixels.Length; ++i)
        pixels[i] = color;

      Texture2D result = new Texture2D(width, height);
      result.SetPixels(pixels);
      result.Apply();

      return result;
    }

    /// <summary>
    /// Marks object target as dirty.
    /// </summary>
    public static void SetDirty(UnityEngine.Object target)
    {
      EditorUtility.SetDirty(target);
    }

    /// <summary>
    /// Draws a UI box with a description and a "Fix Me" button next to it.
    /// </summary>
    /// <param name="text">The description</param>
    /// <param name="action">The action to execute when the button is clicked</param>
    public static void FixMeBox(string text, Action action)
    {
      EditorGUILayout.HelpBox(text, MessageType.Warning);

      GUILayout.Space(-32);
      using (new EditorGUILayout.HorizontalScope())
      {
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Fix", GUILayout.Width(60)) == true)
          action();

        GUILayout.Space(8);
      }
      GUILayout.Space(11);
    }

    /// <summary>
    /// Draws a horizontal split line.
    /// </summary>
    public static void Splitter()
    {
      Rect rect = GUILayoutUtility.GetRect(1f, 1f);

      rect.xMin = 0f;
      rect.width += 4f;

      if (Event.current.type != EventType.Repaint)
        return;

      EditorGUI.DrawRect(rect, Styles.Splitter);
    }

    /// <summary>
    /// Draws a header label.
    /// </summary>
    /// <param name="title">The label to display as a header</param>
    public static void HeaderLabel(string title)
    {
      EditorGUILayout.LabelField(title, Styles.headerLabel);
    }

    public static bool Header(string title, Material material = null, string shaderKeyword = "")
    {
      string key = string.Format("FronkonGames.SpritesMojo.{0}", title);

      bool previousState = EditorPrefs.GetBool(key, true);

      bool state = Header(title, previousState, material, shaderKeyword);

      if (previousState != state)
        EditorPrefs.SetBool(key, state);

      return state;
    }

    public static bool Header(string title, bool state, Material material = null, string shaderKeyword = "")
    {
      Rect backgroundRect = GUILayoutUtility.GetRect(1.0f, 17.0f);

      Rect labelRect = backgroundRect;
      labelRect.xMin += 16.0f;
      labelRect.xMax -= 20.0f;

      Rect foldoutRect = backgroundRect;
      foldoutRect.y += 1.0f;
      foldoutRect.width = 13.0f;
      foldoutRect.height = 13.0f;

      backgroundRect.xMin = 0.0f;
      backgroundRect.width += 4.0f;

      EditorGUI.DrawRect(backgroundRect, Styles.HeaderBackground);

      EditorGUI.LabelField(labelRect, GetContent(title), EditorStyles.boldLabel);

      state = GUI.Toggle(foldoutRect, state, GUIContent.none, EditorStyles.foldout);

      if (material != null && string.IsNullOrEmpty(shaderKeyword) == false)
      {
        Rect toggleRect = backgroundRect;
        toggleRect.x = backgroundRect.xMax - 14.0f;
        toggleRect.y += 2.0f;
        toggleRect.width = 13.0f;
        toggleRect.height = 13.0f;

        material.SetKeyword(shaderKeyword, GUI.Toggle(toggleRect, material.GetKeyword(shaderKeyword), GUIContent.none, Styles.smallTickbox));
      }

      Event e = Event.current;
      if (e.type == EventType.MouseDown && backgroundRect.Contains(e.mousePosition) && e.button == 0)
      {
        state = !state;
        e.Use();
      }

      return state;
    }

    public static bool MiniButton(string label)
    {
      return GUILayout.Button(label, Styles.miniLabelButton);
    }

    public static bool ResetButton()
    {
      return GUILayout.Button("R", Styles.miniLabelButton, GUILayout.Width(10.0f), GUILayout.Height(14.0f));
    }

    public static bool ResetButton<T>(T resetValue)
    {
      return GUILayout.Button(new GUIContent("R", string.Format("Reset to '{0}'.", resetValue)), Styles.miniLabelButton, GUILayout.Width(10.0f), GUILayout.Height(14.0f));
    }

    private static GUIContent GetContent(string textAndTooltip)
    {
      if (string.IsNullOrEmpty(textAndTooltip))
        return GUIContent.none;

      GUIContent content;

      if (GUIContentCache.TryGetValue(textAndTooltip, out content) == false)
      {
        string[] s = textAndTooltip.Split('|');
        content = new GUIContent(s[0]);

        if (s.Length > 1 && !string.IsNullOrEmpty(s[1]))
          content.tooltip = s[1];

        GUIContentCache.Add(textAndTooltip, content);
      }

      return content;
    }
  }
}