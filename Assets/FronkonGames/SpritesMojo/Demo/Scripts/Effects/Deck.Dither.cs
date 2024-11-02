using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform ditherCanvas;

  [SerializeField] private Text textDitherMode;
  [SerializeField] private Button buttonLeftDither;
  [SerializeField] private Button buttonRightDither;
  [SerializeField] private Slider sliderDitherColors;
  [SerializeField] private Slider sliderDitherPixelation;

  private void ChangeDither(Material material)
  {
    Dither.Mode.Set(material, (DitherMode)Random.Range(0, 3));
    Dither.ColorReduction.Set(material, Dither.ColorReduction.ResetValue + Dither.ColorReduction.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    Dither.PixelSize.Set(material, Random.Range(5, 10), timeToUpdate);
  }

  private void UpdateCanvasDither()
  {
    ditherCanvas.gameObject.SetActive(true);
    textDitherMode.text = Card.FromCamelCase(Dither.Mode.Get<DitherMode>(selectedCard.Material).ToString());

    EnumLeftButton<DitherMode>(buttonLeftDither, textDitherMode, selectedCard.Material, Dither.Mode, (int)DitherMode.AnimNoise);
    EnumRightButton<DitherMode>(buttonRightDither, textDitherMode, selectedCard.Material, Dither.Mode, (int)DitherMode.AnimNoise);
    UpdateSlider(sliderDitherColors, Dither.ColorReduction, selectedCard.Material);
    UpdateSlider(sliderDitherPixelation, Dither.PixelSize, selectedCard.Material);
  }
}
