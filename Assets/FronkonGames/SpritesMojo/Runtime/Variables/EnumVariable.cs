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
  /// Enum material variable.
  /// </summary>
  public class EnumVariable
  {
    /// <summary>
    /// Returns the default value.
    /// </summary>
    /// <value>Value.</value>
    public Enum ResetValue { get { return reset; } }

    /// <summary>
    /// Material variable hash.
    /// </summary>
    /// <value>Variable</value>
    public int Variable { get { return variable; } }

    protected readonly int variable;

    private readonly Enum reset;

    private readonly Action<Material, int> onSet;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="variable">Variable name.</param>
    /// <param name="reset">Default value.</param>
    /// <param name="onSet">Action when the value is changed.</param>
    public EnumVariable(string variable, Enum reset, Action<Material, int> onSet = null)
    {
      this.variable = Shader.PropertyToID(variable);
      this.reset = reset;
      this.onSet = onSet;
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
    public void Set(SpriteRenderer sprite, Enum value) => Set(sprite.material, value);

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="value">Value.</param>
    public virtual void Set(Material material, Enum value)
    {
      int intValue = Convert.ToInt32(value);

      material.SetInt(variable, intValue);

      onSet?.Invoke(material, intValue);
    }

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="value">Value.</param>
    public virtual void Set(Material material, int value)
    {
      material.SetInt(variable, value);

      onSet?.Invoke(material, value);
    }

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <param name="sprite">Sprite.</param>
    /// <typeparam name="T">Enum type.</typeparam>
    /// <returns>Value.</returns>
    public T Get<T>(SpriteRenderer sprite) => Get<T>(sprite.material);

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <typeparam name="T">Enum type.</typeparam>
    /// <returns>Value.</returns>
    public T Get<T>(Material material) => (T)Enum.Parse(typeof(T), material.GetInt(variable).ToString());
  }
}
