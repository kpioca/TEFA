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
  /// Variable interpolator.
  /// </summary>
  public sealed class VariableInterpolator : MonoBehaviour
  {
    private static VariableInterpolator instance;

    private List<VariableEntry<int>> intVariablesToUpdate;
    private List<VariableEntry<float>> floatVariablesToUpdate;
    private List<VariableEntry<Vector4>> vectorVariablesToUpdate;
    private List<VariableEntry<Color>> colorVariablesToUpdate;

    private struct VariableEntry<T>
    {
      public Material material;
      public int nameID;
      public T start;
      public T end;
      public float time;
      public float duration;
    }

    /// <summary>
    /// Singleton instance.
    /// </summary>
    public static VariableInterpolator Instance
    {
      get
      {
        if (instance == null)
        {
          GameObject go = new GameObject("Variable Interpolator");
          instance = go.AddComponent<VariableInterpolator>();
          go.hideFlags = HideFlags.HideInHierarchy;
        }

        return instance;
      }
    }

    /// <summary>
    /// Adds a material variable to interpolate.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="nameID">Variable names hashed.</param>
    /// <param name="start">Initial value.</param>
    /// <param name="end">Target value.</param>
    /// <param name="duration">Time in seconds to interpolate.</param>
    public void Add(Material material, int nameID, int start, int end, float duration)
    {
      Debug.Assert(duration > 0.0f, "The duration must be greater than zero.");

      intVariablesToUpdate.Add(new VariableEntry<int>()
      {
        material = material,
        nameID = nameID,
        time = Time.time,
        start = start,
        end = end,
        duration = duration
      });
    }

    /// <summary>
    /// Adds a material variable to interpolate.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="nameID">Variable names hashed.</param>
    /// <param name="start">Initial value.</param>
    /// <param name="end">Target value.</param>
    /// <param name="duration">Time in seconds to interpolate.</param>
    public void Add(Material material, int nameID, float start, float end, float duration)
    {
      Debug.Assert(duration > 0.0f, "The duration must be greater than zero.");

      floatVariablesToUpdate.Add(new VariableEntry<float>()
      {
        material = material,
        nameID = nameID,
        time = Time.time,
        start = start,
        end = end,
        duration = duration
      });
    }

    /// <summary>
    /// Adds a material variable to interpolate.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="nameID">Variable names hashed.</param>
    /// <param name="start">Initial value.</param>
    /// <param name="end">Target value.</param>
    /// <param name="duration">Time in seconds to interpolate.</param>
    public void Add(Material material, int nameID, Vector4 start, Vector4 end, float duration)
    {
      Debug.Assert(duration > 0.0f, "The duration must be greater than zero.");

      vectorVariablesToUpdate.Add(new VariableEntry<Vector4>()
      {
        material = material,
        nameID = nameID,
        time = Time.time,
        start = start,
        end = end,
        duration = duration
      });
    }

    /// <summary>
    /// Adds a material variable to interpolate.
    /// </summary>
    /// <param name="material">Material.</param>
    /// <param name="nameID">Variable names hashed.</param>
    /// <param name="start">Initial value.</param>
    /// <param name="end">Target value.</param>
    /// <param name="duration">Time in seconds to interpolate.</param>
    public void Add(Material material, int nameID, Color start, Color end, float duration)
    {
      Debug.Assert(duration > 0.0f, "The duration must be greater than zero.");

      colorVariablesToUpdate.Add(new VariableEntry<Color>()
      {
        material = material,
        nameID = nameID,
        time = Time.time,
        start = start,
        end = end,
        duration = duration
      });
    }

    private void Awake()
    {
      intVariablesToUpdate = new List<VariableEntry<int>>();
      floatVariablesToUpdate = new List<VariableEntry<float>>();
      colorVariablesToUpdate = new List<VariableEntry<Color>>();
      vectorVariablesToUpdate = new List<VariableEntry<Vector4>>();
    }

    private void Update()
    {
      for (int i = intVariablesToUpdate.Count - 1; i >= 0; --i)
      {
        VariableEntry<int> variable = intVariablesToUpdate[i];
        float elapsed = Time.time - variable.time;

        if (elapsed < variable.duration && variable.start != variable.end)
          variable.material.SetInt(variable.nameID, (int)Mathf.Lerp(variable.start, variable.end, elapsed / variable.duration));
        else
        {
          variable.material.SetInt(variable.nameID, variable.end);

          intVariablesToUpdate.RemoveAt(i);
        }
      }

      for (int i = floatVariablesToUpdate.Count - 1; i >= 0; --i)
      {
        VariableEntry<float> variable = floatVariablesToUpdate[i];
        float elapsed = Time.time - variable.time;

        if (elapsed < variable.duration)
          variable.material.SetFloat(variable.nameID, Mathf.Lerp(variable.start, variable.end, elapsed / variable.duration));
        else
        {
          variable.material.SetFloat(variable.nameID, variable.end);

          floatVariablesToUpdate.RemoveAt(i);
        }
      }

      for (int i = vectorVariablesToUpdate.Count - 1; i >= 0; --i)
      {
        VariableEntry<Vector4> variable = vectorVariablesToUpdate[i];
        float elapsed = Time.time - variable.time;

        if (elapsed < variable.duration)
          variable.material.SetVector(variable.nameID, Vector4.Lerp(variable.start, variable.end, elapsed / variable.duration));
        else
        {
          variable.material.SetVector(variable.nameID, variable.end);

          vectorVariablesToUpdate.RemoveAt(i);
        }
      }

      for (int i = colorVariablesToUpdate.Count - 1; i >= 0; --i)
      {
        VariableEntry<Color> variable = colorVariablesToUpdate[i];
        float elapsed = Time.time - variable.time;

        if (elapsed < variable.duration)
          variable.material.SetColor(variable.nameID, Color.Lerp(variable.start, variable.end, elapsed / variable.duration));
        else
        {
          variable.material.SetColor(variable.nameID, variable.end);

          colorVariablesToUpdate.RemoveAt(i);
        }
      }
    }

    private void OnDestroy()
    {
      intVariablesToUpdate = null;
      floatVariablesToUpdate = null;
      colorVariablesToUpdate = null;
      vectorVariablesToUpdate = null;
    }
  }
}
