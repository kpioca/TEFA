using System;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Async tasks enqueued.
/// </summary>
public class TaskQueue
{
  private struct Step
  {
    public float time;
    public Action onStart;
    public Action<float> onProgress;
    public Action onEnd;
  }

  private Queue<Step> steps = new Queue<Step>();
  private bool nextSteps = true;

  public void Enqueue(float time, Action onStart = null, Action<float> onProgress = null, Action onEnd = null) =>
    steps.Enqueue(new Step() { time = time, onStart = onStart, onProgress = onProgress, onEnd = onEnd });

  public void Update(float deltaTime)
  {
    if (nextSteps == true && steps.Count > 0)
      ExecuteStep(steps.Dequeue(), deltaTime);
  }

  private async void ExecuteStep(Step step, float deltaTime)
  {
    nextSteps = false;

    step.onStart?.Invoke();

    float delta = 0.0f;
    while ((delta += deltaTime) < step.time)
    {
      step.onProgress?.Invoke(delta / step.time);

      await Task.Yield();
    }

    step.onEnd?.Invoke();

    nextSteps = true;
  }
}
