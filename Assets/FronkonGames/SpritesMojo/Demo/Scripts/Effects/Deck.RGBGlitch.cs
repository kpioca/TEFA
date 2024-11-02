using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform rgbGlitchCanvas;

  [SerializeField] private Slider sliderRGBGlitchAmplitude;
  [SerializeField] private Slider sliderRGBGlitchSpeed;

  private void ChangeRGBGlitch(Material material)
  {
    RGBGlitch.Amplitude.Set(material, RGBGlitch.Amplitude.ResetValue + RGBGlitch.Amplitude.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    RGBGlitch.Speed.Set(material, RGBGlitch.Speed.ResetValue + RGBGlitch.Speed.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
  }

  private void UpdateCanvasRGBGlitch()
  {
    sliderAmount.gameObject.SetActive(false);
    rgbGlitchCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderRGBGlitchAmplitude, RGBGlitch.Amplitude, selectedCard.Material);
    UpdateSlider(sliderRGBGlitchSpeed, RGBGlitch.Speed, 2.0f, selectedCard.Material);
  }
}
