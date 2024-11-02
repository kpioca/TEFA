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
  /// Base class to access material shader variables.
  /// </summary>
  public abstract class MaterialVariable<T>
  {
    protected readonly int nameID;

    protected readonly T reset;

    /// <summary>
    /// Variable name hashed.
    /// </summary>
    /// <value>Value.</value>
    public int NameID { get { return nameID; } }

    /// <summary>
    /// Returns the default value.
    /// </summary>
    /// <value>Value.</value>
    public T ResetValue { get { return reset; } }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="variable">Variable name.</param>
    /// <param name="reset">Default value.</param>
    public MaterialVariable(string variable, T reset)
    {
      this.nameID = Shader.PropertyToID(variable);
      this.reset = reset;
    }

    /// <summary>
    /// Reset the variable.
    /// </summary>
    /// <param name="sprite">Sprite.</param>
    public void Reset(SpriteRenderer sprite) => Reset(sprite.material);

    /// <summary>
    /// Reset the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    public void Reset(Material material) => Set(material, reset);

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="sprite">Sprite.</param>
    /// <param name="value">Value.</param>
    /// <param name="duration">Time to change value.</param>
    public void Set(SpriteRenderer sprite, T value, float duration = 0.0f) => Set(sprite.material, value, duration);

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="value">Value.</param>
    /// <param name="duration">Time to change value.</param>
    public abstract void Set(Material material, T value, float duration = 0.0f);

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <param name="sprite">Sprite.</param>
    /// <returns>Value.</returns>
    public T Get(SpriteRenderer sprite) => Get(sprite.material);

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <returns>Value.</returns>
    public abstract T Get(Material material);
  }
}
