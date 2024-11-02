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
  /// Bool variable.
  /// </summary>
  public class BoolVariable : MaterialVariable<bool>
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="variable">Variable name.</param>
    /// <param name="reset">Default value.</param>
    public BoolVariable(string variable, bool reset) : base(variable, reset) { }

    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="value">Value.</param>
    /// <param name="duration">No effect.</param>
    public override void Set(Material material, bool value, float duration = 0.0f) => material.SetInt(nameID, value == true ? 1 : 0);

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <returns>Value.</returns>
    public override bool Get(Material material) => material.GetInt(nameID) == 1;
  }
}
