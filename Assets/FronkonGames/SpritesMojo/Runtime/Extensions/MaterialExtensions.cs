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

namespace FronkonGames.SpritesMojo
{
  /// <summary>
  /// Extensions for Material.
  /// </summary>
  public static class MaterialExtensions
  {
    /// <summary>
    /// Set the blend mode.
    /// </summary>
    /// <param name="blendMode">See BlendModes.cs</param>
    public static void SetBlendMode(Material material, SpriteMojo.BlendModes blendMode)
    {
      material.SetKeyword("_ALPHAPREMULTIPLY_ON", blendMode == SpriteMojo.BlendModes.PreMultipliedAlpha);
      material.SetKeyword("_MULTIPLYBLEND", blendMode == SpriteMojo.BlendModes.Multiply);
      material.SetKeyword("_MULTIPLYBLEND_X2", blendMode == SpriteMojo.BlendModes.Multiplyx2);
      material.SetKeyword("_ADDITIVEBLEND", blendMode == SpriteMojo.BlendModes.Additive);
      material.SetKeyword("_ADDITIVEBLEND_SOFT", blendMode == SpriteMojo.BlendModes.SoftAdditive);

      switch (blendMode)
      {
        case SpriteMojo.BlendModes.Opaque:
        {
          material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
          material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
          material.SetRenderType("Opaque");
        }
        break;

        case SpriteMojo.BlendModes.Additive:
        {
          material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
          material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);

          bool zWrite = material.GetFloat("_ZWrite") > 0.0f;
          material.SetRenderType(zWrite == true ? "TransparentCutout" : "Transparent");
        }
        break;

        case SpriteMojo.BlendModes.SoftAdditive:
        {
          material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
          material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcColor);

          bool zWrite = material.GetFloat("_ZWrite") > 0.0f;
          material.SetRenderType(zWrite ? "TransparentCutout" : "Transparent");
        }
        break;

        case SpriteMojo.BlendModes.Multiply:
        {
          material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
          material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.SrcColor);

          bool zWrite = material.GetFloat("_ZWrite") > 0.0f;
          material.SetRenderType(zWrite == true ? "TransparentCutout" : "Transparent");
        }
        break;

        case SpriteMojo.BlendModes.Multiplyx2:
        {
          material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
          material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.SrcColor);

          bool zWrite = material.GetFloat("_ZWrite") > 0.0f;
          material.SetRenderType(zWrite == true ? "TransparentCutout" : "Transparent");
        }
        break;

        default:
        {
          material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
          material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

          if (material.HasProperty("_ZWrite") == true)
            material.SetRenderType(material.GetFloat("_ZWrite") > 0.0f ? "TransparentCutout" : "Transparent");
        }
        break;
      }

      material.SetOverrideTag("IgnoreProjector", blendMode == SpriteMojo.BlendModes.Opaque ? "False" : "True");
    }

    /// <summary>
    /// Gets the blend mode.
    /// </summary>
    /// <returns>SpritesMojo.BlendModes</returns>
    public static SpriteMojo.BlendModes GetBlendMode(Material material)
    {
      if (material.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON") == true)
        return SpriteMojo.BlendModes.PreMultipliedAlpha;

      if (material.IsKeywordEnabled("_MULTIPLYBLEND") == true)
        return SpriteMojo.BlendModes.Multiply;

      if (material.IsKeywordEnabled("_MULTIPLYBLEND_X2") == true)
        return SpriteMojo.BlendModes.Multiplyx2;

      if (material.IsKeywordEnabled("_ADDITIVEBLEND") == true)
        return SpriteMojo.BlendModes.Additive;

      if (material.IsKeywordEnabled("_ADDITIVEBLEND_SOFT") == true)
        return SpriteMojo.BlendModes.SoftAdditive;

      return SpriteMojo.BlendModes.Opaque;
    }

    /// <summary>
    /// Calculates the luminance range used for the effect.
    /// </summary>
    public static void AutoLuminance(Texture texture, out float minLuminance, out float maxLuminance)
    {
      minLuminance = maxLuminance = 0.0f;

      if (texture != null)
      {
        RenderTexture tmp = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(texture, tmp);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmp;

        Texture2D readableTexture = new Texture2D(texture.width, texture.height);
        readableTexture.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmp);

        Color[] colors = readableTexture.GetPixels();

        for (int i = 0; i < colors.Length; ++i)
        {
          if (colors[i].a > 0.0f)
          {
            float lum = GetLuminance(colors[i]);

            minLuminance = lum < minLuminance ? lum : minLuminance;
            maxLuminance = lum > maxLuminance ? lum : maxLuminance;
          }
        }

        readableTexture = null;
      }
    }

    /// <summary>
    /// Set the type of culling.
    /// </summary>
    /// <param name="culling">See CullModes.cs</param>
    public static void SetCulling(Material material, SpriteMojo.Culling culling) => material.SetInt("_Cull", (int)culling);

    /// <summary>
    /// Gets the type of culling.
    /// </summary>
    /// <returns>See CullModes.cs</returns>
    public static SpriteMojo.Culling GetCulling(Material material) => (SpriteMojo.Culling)material.GetInt("_Cull");

    /// <summary>
    /// Activate or deactivate a keyword.
    /// </summary>
    /// <param name="self">The material.</param>
    /// <param name="keyword">Keyword name.</param>
    /// <param name="value">Activate or deactivate.</param>
    public static void SetKeyword(this Material self, string keyword, bool value)
    {
      if (value == true)
        self.EnableKeyword(keyword);
      else
        self.DisableKeyword(keyword);
    }

    /// <summary>
    /// Gets the state of a keyword.
    /// </summary>
    /// <param name="self">The material.</param>
    /// <param name="keyword">Keyword name.</param>
    /// <returns></returns>
    public static bool GetKeyword(this Material self, string keyword) => Array.IndexOf(self.shaderKeywords, keyword) != -1;

    /// <summary>
    /// Internal use.
    /// </summary>
    /// <param name="renderType"></param>
    internal static void SetRenderType(this Material self, string renderType)
    {
      bool zWrite = self.GetFloat("_ZWrite") > 0.0f;

      self.SetOverrideTag("AlphaDepth", zWrite == true ? "False" : "True");
      self.SetOverrideTag("RenderType", renderType);
    }

    internal static float GetLuminance(Color color) => (color.r * 0.2126729f + color.g * 0.7151522f + color.b * 0.072175f) * color.a;
  }
}
