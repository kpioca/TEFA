using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform trioToneCanvas;

  [SerializeField] private Slider sliderTrioToneThreshold;
  [SerializeField] private Slider sliderTrioToneSoftness;

  private void ChangeTrioTone(Material material)
  {
    int palette = Random.Range(0, palettes.Length);

    Color[] colors = palettes[palette].colors;
    System.Array.Sort(colors, CompareLuminance);

    TrioTone.Luminance.Set(material, (0.01f, 0.5f));
    TrioTone.BrightColor.Set(material, colors[4], timeToUpdate);
    TrioTone.MiddleColor.Set(material, colors[2], timeToUpdate);
    TrioTone.DarkColor.Set(material, colors[0], timeToUpdate);

    if (selectedCard != null && selectedCard.Material != material)
    {
      TrioTone.Threshold.Set(material, TrioTone.Threshold.ResetValue + TrioTone.Threshold.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
      TrioTone.Softness.Set(material, TrioTone.Softness.ResetValue + TrioTone.Softness.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    }
  }

  private void UpdateCanvasTrioTone()
  {
    trioToneCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderTrioToneThreshold, TrioTone.Threshold, selectedCard.Material);
    UpdateSlider(sliderTrioToneSoftness, TrioTone.Softness, selectedCard.Material);
  }
}
