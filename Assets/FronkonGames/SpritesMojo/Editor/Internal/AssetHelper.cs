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
using System.IO;
using UnityEngine;
using UnityEditor;

namespace FronkonGames.SpritesMojo.Editor
{
  /// <summary>
  /// Utilities for assets.
  /// </summary>
  public static class AssetsHelper
  {
    public static void CreateMaterial(string shader)
    {
      string name = shader.Substring(shader.LastIndexOf('/'));
      Material material = new Material(Shader.Find(shader));
      if (material != null)
      {
        string path = Selection.activeObject == null ? "Assets" : AssetDatabase.GetAssetPath(Selection.activeObject);
        path = path.Replace(Path.GetFileName(path), string.Empty);

        string assetPath = $"{path}/{name}.mat";

        int index = 0;
        while (AssetDatabase.LoadAssetAtPath(assetPath, typeof(Material)) != null)
          assetPath = $"{path}/{name}{index++.ToString("00")}.mat";

        MaterialExtensions.SetBlendMode(material, SpriteMojo.BlendModes.PreMultipliedAlpha);

        AssetDatabase.CreateAsset(material, assetPath);
        AssetDatabase.Refresh();
      }
      else
        SpriteMojo.LogError($"Shader '{shader}' not found.");
    }
  }
}