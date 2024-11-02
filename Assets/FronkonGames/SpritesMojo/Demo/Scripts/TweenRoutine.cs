using System;
using System.Collections;

using UnityEngine;

/// <summary>
/// See https://easings.net
/// </summary>
public enum Eases
{
  Linear,
  InBack,
  InBounce,
  InCirc,
  InCubic,
  InElastic,
  InExpo,
  InQuad,
  InQuart,
  InQuint,
  InSine,
  OutBack,
  OutBounce,
  OutCirc,
  OutCubic,
  OutElastic,
  OutExpo,
  OutQuad,
  OutQuart,
  OutQuint,
  OutSine,
  InOutBack,
  InOutBounce,
  InOutCirc,
  InOutCubic,
  InOutElastic,
  InOutExpo,
  InOutQuad,
  InOutQuart,
  InOutQuint,
  InOutSine,
}

/// <summary>
/// Tween coroutines.
/// </summary>
public static class TweenRoutine
{
  public delegate float EasingFunction(float t);

  private static float Linear(float t) => t;

  private static float InBack(float t) => t * t * t - t * Mathf.Sin(t * Mathf.PI);

  private static float OutBack(float t) => 1f - InBack(1f - t);

  private static float InOutBack(float t) => t < 0.5f ? 0.5f * InBack(2.0f * t) : 0.5f * OutBack(2.0f * t - 1.0f) + 0.5f;

  private static float InBounce(float t) => 1.0f - OutBounce(1.0f - t);

  private static float OutBounce(float t) =>  t < 4.0f / 11.0f ?
                                                (121.0f * t * t) / 16.0f :
                                              t < 8.0f / 11.0f ?
                                                (363.0f / 40.0f * t * t) - (99.0f / 10.0f * t) + 17.0f / 5.0f :
                                              t < 9.0f / 10.0f ?
                                                (4356.0f / 361.0f * t * t) - (35442.0f / 1805.0f * t) + 16061.0f / 1805.0f :
                                              (54.0f / 5.0f * t * t) - (513.0f / 25.0f * t) + 268.0f / 25.0f;

  private static float InOutBounce(float t) => t < 0.5f ? 0.5f * InBounce(2.0f * t) : 0.5f * OutBounce(2.0f * t - 1.0f) + 0.5f;

  private static float InCirc(float t) => 1.0f - Mathf.Sqrt(1.0f - (t * t));

  private static float OutCirc(float t) => Mathf.Sqrt((2.0f - t) * t);

  private static float InOutCirc(float t) =>  t < 0.5f ? 0.5f * (1.0f - Mathf.Sqrt(1.0f - 4.0f * (t * t))) : 0.5f * (Mathf.Sqrt(-((2.0f * t) - 3.0f) * ((2.0f * t) - 1.0f)) + 1.0f);

  private static float InCubic(float t) => t * t * t;

  private static float OutCubic(float t) => InCubic(t - 1.0f) + 1.0f;

  private static float InOutCubic(float t) => t < 0.5f ? 4.0f * t * t * t : 0.5f * InCubic(2.0f * t - 2.0f) + 1.0f;

  private static float InElastic(float t) => Mathf.Sin(13.0f * (Mathf.PI * 0.5f) * t) * Mathf.Pow(2.0f, 10.0f * (t - 1.0f));

  private static float OutElastic(float t) => Mathf.Sin(-13.0f * (Mathf.PI * 0.5f) * (t + 1.0f)) * Mathf.Pow(2.0f, -10.0f * t) + 1.0f;

  private static float InOutElastic(float t) => t < 0.5f ?
                                                  0.5f * Mathf.Sin(13.0f * (Mathf.PI * 0.5f) * (2.0f * t)) * Mathf.Pow(2.0f, 10.0f * ((2.0f * t) - 1.0f)) :
                                                  0.5f * (Mathf.Sin(-13.0f * (Mathf.PI * 0.5f) * ((2.0f * t - 1.0f) + 1.0f)) * Mathf.Pow(2.0f, -10.0f * (2.0f * t - 1.0f)) + 2.0f);

  private static float InExpo(float t) => Mathf.Approximately(0.0f, t) ? t : Mathf.Pow(2.0f, 10.0f * (t - 1.0f));

  private static float OutExpo(float t) => Mathf.Approximately(1.0f, t) ? t : 1.0f - Mathf.Pow(2.0f, -10.0f * t);

  private static float InOutExpo(float v) => Mathf.Approximately(0.0f, v) || Mathf.Approximately(1.0f, v) ?
                                              v :
                                                v < 0.5f ?
                                                  0.5f * Mathf.Pow(2.0f, (20.0f * v) - 10.0f) :
                                                -0.5f * Mathf.Pow(2.0f, (-20.0f * v) + 10.0f) + 1.0f;

  private static float InQuad(float t) => t * t;

  private static float OutQuad(float t) => -t * (t - 2.0f);

  private static float InOutQuad(float t) => t < 0.5f ? 2.0f * t * t : -2.0f * t * t + 4.0f * t - 1.0f;

  private static float InQuart(float t) => t * t * t * t;

  private static float OutQuart(float t)
  {
    float u = t - 1.0f;
    return u * u * u * (1.0f - t) + 1.0f;
  }

  private static float InOutQuart(float t) => t < 0.5f ? 8.0f * InQuart(t) : -8.0f * InQuart(t - 1.0f) + 1.0f;

  private static float InQuint(float t) => t * t * t * t * t;

  private static float OutQuint(float t) => InQuint(t - 1.0f) + 1.0f;

  private static float InOutQuint(float t) => t < 0.5f ? 16.0f * InQuint(t) : 0.5f * InQuint(2.0f * t - 2.0f) + 1.0f;

  private static float InSine(float t) => Mathf.Sin((t - 1.0f) * (Mathf.PI * 0.5f)) + 1.0f;

  private static float OutSine(float t) => Mathf.Sin(t * (Mathf.PI * 0.5f));

  private static float InOutSine(float t) => 0.5f * (1.0f - Mathf.Cos(t * Mathf.PI));

  public static IEnumerator Interpolate(float from, float to, float duration, Eases ease, Action<float> onProgress, Action onEnd = null)
  {
    EasingFunction function = GetEasingFunction(ease);

    float time = Time.deltaTime;
    for (; time < duration; time += Time.deltaTime)
    {
      float t = time / duration;

      onProgress(Mathf.Lerp(from, to, function(t)));

      yield return null;
    }

    onProgress(to);

    onEnd?.Invoke();
  }

  public static IEnumerator Interpolate(Vector3 from, Vector3 to, float duration, Eases ease, Action<Vector3> onProgress, Action onEnd = null)
  {
    EasingFunction function = GetEasingFunction(ease);

    float time = Time.deltaTime;
    for (; time < duration; time += Time.deltaTime)
    {
      float t = time / duration;

      onProgress(Vector3.Lerp(from, to, function(t)));

      yield return null;
    }

    onProgress(to);

    onEnd?.Invoke();
  }

  private static EasingFunction GetEasingFunction(Eases ease)
  {
    switch (ease)
    {
      case Eases.Linear:        return Linear;
      case Eases.InBack:        return InBack;
      case Eases.InBounce:      return InBounce;
      case Eases.InCirc:        return InCirc;
      case Eases.InCubic:       return InCubic;
      case Eases.InElastic:     return InElastic;
      case Eases.InExpo:        return InExpo;
      case Eases.InQuad:        return InQuad;
      case Eases.InQuart:       return InQuart;
      case Eases.InQuint:       return InQuint;
      case Eases.InSine:        return InSine;
      case Eases.OutBack:       return OutBack;
      case Eases.OutBounce:     return OutBounce;
      case Eases.OutCirc:       return OutCirc;
      case Eases.OutCubic:      return OutCubic;
      case Eases.OutElastic:    return OutElastic;
      case Eases.OutExpo:       return OutExpo;
      case Eases.OutQuad:       return OutQuad;
      case Eases.OutQuart:      return OutQuart;
      case Eases.OutQuint:      return OutQuint;
      case Eases.OutSine:       return OutSine;
      case Eases.InOutBack:     return InOutBack;
      case Eases.InOutBounce:   return InOutBounce;
      case Eases.InOutCirc:     return InOutCirc;
      case Eases.InOutCubic:    return InOutCubic;
      case Eases.InOutElastic:  return InOutElastic;
      case Eases.InOutExpo:     return InOutExpo;
      case Eases.InOutQuad:     return InOutQuad;
      case Eases.InOutQuart:    return InOutQuart;
      case Eases.InOutQuint:    return InOutQuint;
      case Eases.InOutSine:     return InOutSine;

      default: return Linear;
    }
  }
}
