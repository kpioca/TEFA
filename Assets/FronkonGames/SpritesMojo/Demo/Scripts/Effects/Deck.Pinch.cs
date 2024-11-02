using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform pinchCanvas;

  [SerializeField] private Slider sliderPinchStrength;

  private void ChangePinch(Material material)
  {
    Pinch.Center.Set(material, Pinch.Center.ResetValue + Pinch.Center.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);

    if (selectedCard != null && selectedCard.Material != material)
      Pinch.Strength.Set(material, Pinch.Strength.ResetValue + Pinch.Strength.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
  }

  private void UpdateCanvasPinch()
  {
    sliderAmount.gameObject.SetActive(false);
    pinchCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderPinchStrength, Pinch.Strength, -2.0f, 2.0f, selectedCard.Material);
  }
}
