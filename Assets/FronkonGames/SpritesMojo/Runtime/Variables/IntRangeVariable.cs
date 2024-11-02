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
  /// Int range variable.
  /// </summary>
  public class IntRangeVariable : MaterialVariable<int>
  {
    /// <summary>
    /// Minimum value.
    /// </summary>
    /// <value>Value.</value>
    public int Min { get { return range.Item1; } }

    /// <summary>
    /// Maximum value.
    /// </summary>
    /// <value>Value.</value>
    public int Max { get { return range.Item2; } }

    private ValueTuple<int, int> range = new ValueTuple<int, int>(0, 0);

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="variable">Variable name.</param>
    /// <param name="reset">Default value.</param>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    public IntRangeVariable(string variable, int reset, int min, int max) : base(variable, (int)Mathf.Clamp(reset, min, max))
    {
      range.Item1 = min;
      range.Item2 = max;
    }

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="value">Value.</param>
    /// <param name="duration">Time to change value.</param>
    public override void Set(Material material, int value, float duration = 0.0f)
    {
      value = Mathf.Clamp(value, range.Item1, range.Item2);

      if (duration <= 0.0f)
        material.SetInt(nameID, value);
      else
        VariableInterpolator.Instance.Add(material, nameID, material.GetInt(nameID), value, duration);
    }

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <returns>Value.</returns>
    public override int Get(Material material) => Mathf.Clamp(material.GetInt(nameID), range.Item1, range.Item2);
  }
}
