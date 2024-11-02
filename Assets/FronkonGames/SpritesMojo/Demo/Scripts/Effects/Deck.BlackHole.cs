using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform blackHoleCanvas;

  [SerializeField] private Slider sliderRadius;
  [SerializeField] private Slider sliderDistortion;
  [SerializeField] private Slider sliderX;
  [SerializeField] private Slider sliderY;

  private void ChangeBlackHole(Material material)
  {
    BlackHole.Color.Set(material, Demo.TransparentColor);
    BlackHole.Radius.Set(material, BlackHole.Radius.ResetValue + BlackHole.Radius.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    BlackHole.Center.Set(material, BlackHole.Center.ResetValue + BlackHole.Center.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    BlackHole.Distortion.Set(material, BlackHole.Distortion.ResetValue + BlackHole.Distortion.ResetValue * Random.Range(-randomRange, randomRange), timeToUpdate);
    BlackHole.Color.Set(material, Demo.TransparentColor);
  }

  private void UpdateCanvasBlackHole()
  {
    blackHoleCanvas.gameObject.SetActive(true);
    sliderAmount.gameObject.SetActive(false);

    UpdateSlider(sliderRadius, BlackHole.Radius, selectedCard.Material);
    UpdateSlider(sliderDistortion, BlackHole.Distortion, selectedCard.Material);
    UpdateSlider(sliderX, BlackHole.Center.Get(selectedCard.Material).x, selectedCard.Material, (value) => BlackHole.Center.Set(selectedCard.Material, new Vector2(value, BlackHole.Center.Get(selectedCard.Material).y)));
    UpdateSlider(sliderY, BlackHole.Center.Get(selectedCard.Material).y, selectedCard.Material, (value) => BlackHole.Center.Set(selectedCard.Material, new Vector2(BlackHole.Center.Get(selectedCard.Material).x, value)));
  }
}
