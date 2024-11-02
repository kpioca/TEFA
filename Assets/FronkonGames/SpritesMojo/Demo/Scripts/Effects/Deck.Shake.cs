using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform shakeCanvas;

  [SerializeField] private Slider sliderShakeAmplitude;
  [SerializeField] private Slider sliderShakeIntensy;
  [SerializeField] private Slider sliderShakeSpeed;

  private Vector2 amplitude, intensity;

  private void ChangeShake(Material material)
  {
    Shake.Amplitude.Set(material, new Vector2(Random.Range(-randomRange, randomRange) * 10.0f, Random.Range(-randomRange, randomRange) * 20.0f), timeToUpdate);
    Shake.Intensity.Set(material, Shake.Intensity.ResetValue + 5.0f * Random.Range(-randomRange, randomRange) * Shake.Intensity.ResetValue, timeToUpdate);
    Shake.Speed.Set(material, Shake.Speed.ResetValue + Shake.Speed.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
  }

  private void UpdateCanvasShake()
  {
    Shake.Amplitude.Set(selectedCard.Material, new Vector2(5.0f, 2.0f));

    sliderAmount.gameObject.SetActive(false);
    shakeCanvas.gameObject.SetActive(true);

    amplitude = Shake.Amplitude.Get(selectedCard.Material);
    intensity = Shake.Intensity.Get(selectedCard.Material);

    sliderShakeAmplitude.onValueChanged.RemoveAllListeners();
    sliderShakeAmplitude.value = 1.0f;
    sliderShakeAmplitude.minValue = -4.0f;
    sliderShakeAmplitude.maxValue = 4.0f;
    sliderShakeAmplitude.onValueChanged.AddListener((value) => { Shake.Amplitude.Set(selectedCard.Material, amplitude * value);  });

    sliderShakeIntensy.onValueChanged.RemoveAllListeners();
    sliderShakeIntensy.value = 1.0f;
    sliderShakeIntensy.minValue = 0.0f;
    sliderShakeIntensy.maxValue = 4.0f;
    sliderShakeIntensy.onValueChanged.AddListener((value) => { Shake.Intensity.Set(selectedCard.Material, intensity * value); });

    UpdateSlider(sliderShakeSpeed, Shake.Speed, 0.0f, 10.0f, selectedCard.Material);
  }
}
