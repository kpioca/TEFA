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

namespace FronkonGames.SpritesMojo
{
  /// <summary>
  /// Keywords variable.
  /// </summary>
  public class KeywordsVariable : MaterialVariable<int>
  {
    private readonly List<string> keywords = new List<string>();

    /// <summary>
    /// Keyword name.
    /// </summary>
    /// <value>Value.</value>
    public List<string> Names { get { return keywords; } }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="variable">Variable name.</param>
    /// <param name="reset">Default value.</param>
    public KeywordsVariable(string[] keywords, int reset) : base(string.Empty, reset) { this.keywords.AddRange(keywords); }

    /// <summary>
    /// Enable a keyword.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="index">Keyword index to enable.</param>
    /// <param name="duration">Time to change value (no effect).</param>
    public override void Set(Material material, int index, float duration = 0.0f)
    {
      for (int i = 0; i < keywords.Count; ++i)
      {
        if (i == index)
          material.EnableKeyword(keywords[i]);
        else
          material.DisableKeyword(keywords[i]);
      }
    }

    /// <summary>
    /// Keyword enabled.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <returns>Keyword index enabled or -1.</returns>
    public override int Get(Material material)
    {
      for (int i = 0; i < keywords.Count; ++i)
      {
        if (material.IsKeywordEnabled(keywords[i]) == true)
          return i;
      }

      return -1;
    }
  }
}
