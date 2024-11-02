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
  /// <summary>
  /// Texture variable.
  /// </summary>
  public class TextureVariable : MaterialVariable<Texture>
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="variable">Variable name.</param>
    /// <param name="reset">Default value.</param>
    public TextureVariable(string variable) : base(variable, null) { }

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="value">Value.</param>
    /// <param name="duration">Time to change value (no effect).</param>
    public override void Set(Material material, Texture value, float duration = 0.0f) { material.SetTexture(nameID, value); }

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <returns>Value.</returns>
    public override Texture Get(Material material) => material.GetTexture(nameID);
  }
}
