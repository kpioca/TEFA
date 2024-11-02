using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform hologramCanvas;

  [SerializeField] private Slider sliderHologramDistortion;
  [SerializeField] private Slider sliderHologramBlinkStrength;
  [SerializeField] private Slider sliderHologramBlinkSpeed;
  [SerializeField] private Slider sliderHologramScanlineStrength;
  [SerializeField] private Slider sliderHologramScanlineCount;
  [SerializeField] private Slider sliderHologramScanlineSpeed;

  private void ChangeHologram(Material material)
  {
    Hologram.Distortion.Set(material, Hologram.Distortion.ResetValue + Hologram.Distortion.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    Hologram.ScanlineCount.Set(material, Random.Range(2, 5), timeToUpdate);
    Hologram.ScanlineSpeed.Set(material, Hologram.ScanlineSpeed.ResetValue + Hologram.ScanlineSpeed.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
  }

  private void UpdateCanvasHologram()
  {
    Hologram.Reset(selectedCard.Material);
    hologramCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderHologramDistortion, Hologram.Distortion, 10.0f, selectedCard.Material);
    UpdateSlider(sliderHologramBlinkStrength, Hologram.BlinkStrength, selectedCard.Material);
    UpdateSlider(sliderHologramBlinkSpeed, Hologram.BlinkSpeed, 100.0f, selectedCard.Material);
    UpdateSlider(sliderHologramScanlineStrength, Hologram.ScanlineStrength, selectedCard.Material);
    UpdateSlider(sliderHologramScanlineCount, Hologram.ScanlineCount, 50.0f, selectedCard.Material);
    UpdateSlider(sliderHologramScanlineSpeed, Hologram.ScanlineSpeed, 0.0f, 20.0f, selectedCard.Material);
  }
}
