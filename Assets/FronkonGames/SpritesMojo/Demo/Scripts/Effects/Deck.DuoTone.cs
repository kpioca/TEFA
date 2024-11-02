using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform duoToneCanvas;

  [SerializeField] private Slider sliderDuoToneThreshold;
  [SerializeField] private Slider sliderDuoToneSoftness;

  private void ChangeDuoTone(Material material)
  {
    int palette = Random.Range(0, palettes.Length);

    Color[] colors = palettes[palette].colors;
    System.Array.Sort(colors, CompareLuminance);

    DuoTone.Luminance.Set(material, (0.05f, 0.5f));
    DuoTone.BrightColor.Set(material, colors[3], timeToUpdate);
    DuoTone.DarkColor.Set(material, colors[0] * 0.5f, timeToUpdate);

    if (selectedCard != null && selectedCard.Material != material)
    {
      DuoTone.Threshold.Set(material, DuoTone.Threshold.ResetValue + DuoTone.Threshold.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
      DuoTone.Softness.Set(material, DuoTone.Softness.ResetValue + DuoTone.Softness.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    }
  }

  private void UpdateCanvasDuoTone()
  {
    duoToneCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderDuoToneThreshold, DuoTone.Threshold, selectedCard.Material);
    UpdateSlider(sliderDuoToneSoftness, DuoTone.Softness, selectedCard.Material);
  }
}
