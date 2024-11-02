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
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FronkonGames.SpritesMojo.Editor
{
  /// <summary>
  /// Color palette search tool on colorlovers.com.
  /// </summary>
  public class ColourLoversSearch : EditorWindow
  {
    private string searchText;
    private string previusSearchText;

    private List<Palette> colorEntries = new List<Palette>();

    private int pageCurrent = 0;
    private int pageCount = 0;

    [MenuItem("Window/Sprites Mojo/Ramp Color Search", false, 1000)]
    public static void ShowTool()
    {
      EditorWindow window = EditorWindow.GetWindow(typeof(ColourLoversSearch), false, "Ramp Color Search");
      window.minSize = new Vector2(800.0f, 600.0f);
    }

    private void SearchGUI()
    {
      EditorHelper.Separator();

      GUILayout.BeginHorizontal(GUILayout.Height(20));
      {
        EditorHelper.Separator(10);

        searchText = GUILayout.TextField(searchText, GUILayout.ExpandWidth(true), GUILayout.Height(18));

        if (Event.current.isKey == true && Event.current.keyCode == KeyCode.Return && string.IsNullOrEmpty(searchText) == false)
        {
          SearchColors();

          this.Repaint();
        }

        EditorHelper.Separator(5);

        if (GUILayout.Button(EditorGUIUtility.IconContent("d_Search Icon"), GUILayout.Width(55), GUILayout.Height(18)) == true)
          SearchColors();

        EditorHelper.Separator(5);

        if (GUILayout.Button("random", GUILayout.Width(55), GUILayout.Height(18)) == true)
        {
          colorEntries.Clear();
          pageCurrent = pageCount = 0;

          for (int i = 0; i < 5; ++i)
            colorEntries.Add(ColourLovers.Random());
        }

        EditorHelper.Separator(10);
      }
      GUILayout.EndHorizontal();
    }

    private void ColorGUI()
    {
      GUILayout.BeginVertical("box", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
      {
        if (colorEntries.Count == 0)
          EditorHelper.FlexibleSpace();
        else
        {
          for (int i = 0; i < colorEntries.Count; ++i)
          {
            if (colorEntries[i].colors != null && colorEntries[i].colors.Length == 5)
              ColorEntryGUI(colorEntries[i]);
          }
        }
      }
      GUILayout.EndVertical();
    }

    private void ColorEntryGUI(Palette colorEntry)
    {
      GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true), GUILayout.MaxHeight(200));
      {
        GUILayout.BeginVertical();
        {
          GUILayout.Label($"'{colorEntry.tittle}' by {colorEntry.author}");

          GUILayout.BeginHorizontal();
          {
            for (int i = 0; i < 5; ++i)
            {
              GUILayout.BeginVertical();
              {
                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                {
                }
                GUILayout.EndHorizontal();

                Rect rect = GUILayoutUtility.GetLastRect();
                GUI.DrawTexture(rect, MakeTexture((int)rect.width, (int)rect.height, colorEntry.colors[i]));

                GUILayout.Label($"#{ColorUtility.ToHtmlStringRGB(colorEntry.colors[i])}");
              }
              GUILayout.EndVertical();
            }
          }
          GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        EditorHelper.Separator();

        GUILayout.BeginVertical();
        {
          EditorHelper.FlexibleSpace();

          List<SpriteRenderer> sprites = new List<SpriteRenderer>();

          if (Selection.objects.Length > 0)
          {
            for (int i = 0; i < Selection.objects.Length; ++i)
            {
              GameObject go = Selection.objects[i] as GameObject;
              if (go != null)
              {
                SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
                if (sprite != null && sprite.sharedMaterial.shader.name.Contains("FronkonGames/SpritesMojo/Ramp") == true)
                  sprites.Add(sprite);
              }
            }
          }

          GUI.enabled = sprites.Count > 0;

          if (GUILayout.Button("Use") == true)
          {
            for (int i = 0; i < sprites.Count; ++i)
            {
              Ramp.SetRamp(sprites[i].sharedMaterial, colorEntry.colors[0],
                                                      colorEntry.colors[1],
                                                      colorEntry.colors[2],
                                                      colorEntry.colors[3],
                                                      colorEntry.colors[4]);

              Ramp.SortRampByLuminance(sprites[i].sharedMaterial);

              EditorHelper.SetDirty(sprites[i]);
            }

            sprites.Clear();
          }

          GUI.enabled = true;

          if (GUILayout.Button("Copy") == true)
            EditorGUIUtility.systemCopyBuffer = JsonUtility.ToJson(colorEntry);

          EditorHelper.Separator(20);
        }
        GUILayout.EndVertical();
      }
      GUILayout.EndHorizontal();
    }

    private void CommandGUI()
    {
      GUILayout.BeginHorizontal();
      {
        EditorHelper.Label("powered by ColourLovers.com");

        EditorHelper.FlexibleSpace();

        if (pageCount > 1)
        {
          if (GUILayout.Button(EditorGUIUtility.IconContent("Animation.FirstKey"), GUILayout.Height(16)) == true)
          {
            pageCurrent = 0;

            SearchColors();
          }

          if (GUILayout.Button(EditorGUIUtility.IconContent("Profiler.PrevFrame"), GUILayout.Height(16)) == true && pageCurrent > 0)
          {
            pageCurrent--;

            SearchColors();
          }

          EditorHelper.Separator();

          GUILayout.Label($"{pageCurrent} / {pageCount}");

          EditorHelper.Separator();

          if (GUILayout.Button(EditorGUIUtility.IconContent("Profiler.NextFrame"), GUILayout.Height(16)) == true && pageCurrent < pageCount)
          {
            pageCurrent++;

            SearchColors();
          }

          if (GUILayout.Button(EditorGUIUtility.IconContent("Animation.LastKey"), GUILayout.Height(16)) == true)
          {
            pageCurrent = pageCount;

            SearchColors();
          }
        }

        EditorHelper.Separator();
      }
      GUILayout.EndHorizontal();

      EditorHelper.Separator();
    }

    private void OnGUI()
    {
      GUILayout.BeginVertical();
      {
        SearchGUI();

        ColorGUI();

        CommandGUI();
      }
      GUILayout.EndVertical();
    }

    private void SearchColors()
    {
      colorEntries.Clear();

      int totalResults = 0;

      if (previusSearchText != searchText)
      {
        pageCurrent = 0;
        previusSearchText = searchText;
      }

      colorEntries.AddRange(ColourLovers.Search(pageCurrent, searchText, out totalResults));

      pageCount = totalResults / 5;
    }

    private Texture2D MakeTexture(int width, int height, Color col)
    {
      Color[] pix = new Color[width * height];

      for (int i = 0; i < pix.Length; ++i)
        pix[i] = col;

      Texture2D result = new Texture2D(width, height, TextureFormat.RGB24, false);
      result.SetPixels(pix);
      result.Apply();

      return result;
    }
  }
}