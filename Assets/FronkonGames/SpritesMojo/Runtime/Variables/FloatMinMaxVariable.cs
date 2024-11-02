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
  /// Min/Max range float variable.
  /// </summary>
  public class FloatMinMaxVariable : MaterialVariable<ValueTuple<float, float>>
  {
    /// <summary>
    /// Range of values.
    /// </summary>
    /// <value>Tuple.</value>
    public ValueTuple<float, float> Limit { get { return limit; } }

    private string variableMax;

    private ValueTuple<float, float> value = new ValueTuple<float, float>(0.0f, 0.0f);

    private ValueTuple<float, float> limit = new ValueTuple<float, float>(0.0f, 0.0f);

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="variableMin">Min variable name.</param>
    /// <param name="variableMax">Min variable name.</param>
    /// <param name="reset">Default value.</param>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    public FloatMinMaxVariable(string variableMin, string variableMax, ValueTuple<float, float> reset, ValueTuple<float, float> limit) : base(variableMin, reset)
    {
      this.variableMax = variableMax;
      this.limit = limit;
    }

    /// <summary>
    /// Use Set(Material material, float min, float max).
    /// </summary>
    public override void Set(Material material, ValueTuple<float, float> value, float duration = 0.0f)
    {
      material.SetFloat(nameID, Mathf.Clamp(value.Item1, limit.Item1, limit.Item2));
      material.SetFloat(variableMax, Mathf.Clamp(value.Item2, limit.Item1, limit.Item2));
    }

    /// <summary>
    /// Gets the min / max values.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <returns>Min / max values.</returns>
    public override ValueTuple<float, float> Get(Material material)
    {
      value.Item1 = Mathf.Clamp(material.GetFloat(nameID), limit.Item1, limit.Item2);
      value.Item2 = Mathf.Clamp(material.GetFloat(variableMax), limit.Item1, limit.Item2);

      return value;
    }
  }
}
