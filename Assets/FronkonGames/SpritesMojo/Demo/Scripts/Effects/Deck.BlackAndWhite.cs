using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform blackAndWhiteCanvas;

  [SerializeField] private Slider sliderThreshold;
  [SerializeField] private Slider sliderSoftness;
  [SerializeField] private Slider sliderExposure;
  [SerializeField] private Slider sliderRed;
  [SerializeField] private Slider sliderGreen;
  [SerializeField] private Slider sliderBlue;

  private void ChangeBlackAndWhite(Material material)
  {
    BlackAndWhite.Threshold.Set(material, BlackAndWhite.Threshold.ResetValue + BlackAndWhite.Threshold.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    BlackAndWhite.Softness.Set(material, BlackAndWhite.Softness.ResetValue + BlackAndWhite.Softness.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    BlackAndWhite.Exposure.Set(material, BlackAndWhite.Exposure.ResetValue + BlackAndWhite.Exposure.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
  }

  private void UpdateCanvasBlackAndWhite()
  {
    blackAndWhiteCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderThreshold, BlackAndWhite.Threshold, selectedCard.Material);
    UpdateSlider(sliderSoftness, BlackAndWhite.Softness, selectedCard.Material);
    UpdateSlider(sliderExposure, BlackAndWhite.Exposure, 10.0f, selectedCard.Material);
    UpdateSlider(sliderRed, BlackAndWhite.Red, selectedCard.Material);
    UpdateSlider(sliderGreen, BlackAndWhite.Green, selectedCard.Material);
    UpdateSlider(sliderBlue, BlackAndWhite.Blue, selectedCard.Material);
  }
}
