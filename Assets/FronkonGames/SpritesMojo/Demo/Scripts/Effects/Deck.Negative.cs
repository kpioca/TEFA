using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform negativeCanvas;

  [SerializeField] private Slider sliderNegativeRed;
  [SerializeField] private Slider sliderNegativeGreen;
  [SerializeField] private Slider sliderNegativeBlue;

  [SerializeField] private Slider sliderNegativeHue;
  [SerializeField] private Slider sliderNegativeSaturation;
  [SerializeField] private Slider sliderNegativeLuminance;

  private Vector4 channels, hsl;

  private void ChangeNegative(Material material)
  {
    Negative.ColorChannels.Set(material, Random.ColorHSV(0.5f - randomRange, 0.5f + randomRange), timeToUpdate);
  }

  private void UpdateCanvasNegative()
  {
    negativeCanvas.gameObject.SetActive(true);

    channels = Negative.ColorChannels.Get(selectedCard.Material);

    sliderNegativeRed.onValueChanged.RemoveAllListeners();
    sliderNegativeRed.value = channels.x;
    sliderNegativeRed.onValueChanged.AddListener((value) => { channels.x = value; Negative.ColorChannels.Set(selectedCard.Material, channels); });

    sliderNegativeGreen.onValueChanged.RemoveAllListeners();
    sliderNegativeGreen.value = channels.y;
    sliderNegativeGreen.onValueChanged.AddListener((value) => { channels.y = value; Negative.ColorChannels.Set(selectedCard.Material, channels); });

    sliderNegativeBlue.onValueChanged.RemoveAllListeners();
    sliderNegativeBlue.value = channels.z;
    sliderNegativeBlue.onValueChanged.AddListener((value) => { channels.z = value; Negative.ColorChannels.Set(selectedCard.Material, channels); });

    hsl = Negative.HSL.Get(selectedCard.Material);

    sliderNegativeHue.onValueChanged.RemoveAllListeners();
    sliderNegativeHue.value = hsl.x;
    sliderNegativeHue.onValueChanged.AddListener((value) => { hsl.x = value; Negative.HSL.Set(selectedCard.Material, hsl); });

    sliderNegativeSaturation.onValueChanged.RemoveAllListeners();
    sliderNegativeSaturation.value = hsl.y;
    sliderNegativeSaturation.onValueChanged.AddListener((value) => { hsl.y = value; Negative.HSL.Set(selectedCard.Material, hsl); });

    sliderNegativeLuminance.onValueChanged.RemoveAllListeners();
    sliderNegativeLuminance.value = hsl.z;
    sliderNegativeLuminance.onValueChanged.AddListener((value) => { hsl.z = value; Negative.HSL.Set(selectedCard.Material, hsl); });
  }
}
