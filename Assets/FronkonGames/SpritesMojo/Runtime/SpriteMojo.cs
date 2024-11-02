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

namespace FronkonGames.SpritesMojo
{
  /// <summary> Utilities and basic properties for all efects. </summary>
  public static class SpriteMojo
  {
    /// <summary> Cull modes. </summary>
    public enum Culling
    {
      Off = 0,
      Front = 1,
      Back = 2,
    };

    /// <summary> Blend modes. </summary>
    public enum BlendModes
    {
      PreMultipliedAlpha,
      Opaque,
      Additive,
      SoftAdditive,
      Multiply,
      Multiplyx2,
    };

    /// <summary> Effect strength, default 1.0 [0.0 - 1.0]. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Amount = new("_Amount", 1.0f, 0.0f, 1.0f);

    /// <summary> Activate color controls, default false. </summary>
    /// <returns>Value.</returns>
    public static readonly KeywordVariable ColorAdjust = new("_COLOR_ADJUST", false);

    /// <summary> Brightness, default 0.0 [-1.0 - 1.0]. Requires ColorAdjust to be set to true. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Brightness = new("_Brightness", 0.0f, -1.0f, 1.0f);

    /// <summary> Contrast, default 0.0 [-1.0 - 1.0]. Requires ColorAdjust to be set to true. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Contrast = new("_Contrast", 0.0f, -1.0f, 1.0f);

    /// <summary> Gamma, default 1.0 [0.1 - 10.0]. Requires ColorAdjust to be set to true. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Gamma = new("_Gamma", 1.0f, 0.1f, 10.0f);

    /// <summary> Hue, default 0.0 [0.0 - 1.0]. Requires ColorAdjust to be set to true. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Hue = new("_Hue", 0.0f, 0.0f, 1.0f);

    /// <summary> Saturation, default 1.0 [0.0 - 5.0]. Requires ColorAdjust to be set to true. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Saturation = new("_Saturation", 1.0f, 0.0f, 5.0f);

    /// <summary> Color intentity, default 0.0 [-1.0 - 10.0]. Requires ColorAdjust to be set to true. </summary>
    /// <returns>Value.</returns>
    public static readonly FloatRangeVariable Vibrance = new("_Vibrance", 0.0f, -1.0f, 10.0f);

    /// <summary> Color tint, default white. </summary>
    /// <returns>Value.</returns>
    public static readonly ColorVariable Tint = new("_Color", Color.white);

    public static readonly string Documentation = "https://fronkongames.github.io/store/sprites-mojo/";
    public static readonly string Store = "https://assetstore.unity.com/packages/vfx/shaders/sprites-mojo-214468";
    public static readonly string AssemblyName = "FronkonGames.SpritesMojo";

    private static Shader defaultSpriteShader;

    internal static Shader DefaultSpriteShader
    {
      get
      {
        if (defaultSpriteShader == null)
          defaultSpriteShader = Shader.Find("Sprites/Default");

        return defaultSpriteShader;
      }
    }

    /// <summary> Internal use. </summary>
    internal static GameObject CreateSprite(Material material, string name = default, Transform parent = null)
    {
      GameObject gameObject = new GameObject();

      if (string.IsNullOrEmpty(name) == false)
        gameObject.name = name;

      if (parent != null)
        gameObject.transform.parent = parent;

      gameObject.AddComponent<SpriteRenderer>().material = material;
#if UNITY_EDITOR
      UnityEditor.Selection.activeGameObject = gameObject;
#endif
      return gameObject;
    }

    /// <summary> Internal use. </summary>
    internal static Material CreateMaterial(string shaderPath)
    {
      Shader shader = Resources.Load<Shader>(shaderPath);
      if (shader == null)
        LogError($"Shader 'FronkonGames/SpritesMojo/Resources/{shaderPath}' not found.");
      else if (shader.isSupported == false)
      {
        shader = null;

        LogError($"'{shaderPath}' shader not supported.");
      }

      Material material = null;
      if (shader != null)
      {
        material = new Material(shader);
        MaterialExtensions.SetBlendMode(material, BlendModes.PreMultipliedAlpha);
      }
      else
        material = new Material(shaderPath.Contains("Sprite") == true ? DefaultSpriteShader : DefaultSpriteShader);

      return material;
    }

    /// <summary> Error message. </summary>
    public static void LogError(string error) => Debug.LogError($"[FronkonGames.SpritesMojo] {error}. Please contact with 'frokongames@gmail.com' and send the log file.");
  }
}
