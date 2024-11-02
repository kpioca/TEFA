using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform swirlCanvas;

  [SerializeField] private Slider sliderSwirlStrength;
  [SerializeField] private Slider sliderSwirlTorsion;

  private void ChangeSwirl(Material material)
  {
    if (selectedCard != null && selectedCard.Material != material)
    {
      Swirl.Strength.Set(material, Random.Range(0.1f, 0.1f + randomRange), timeToUpdate);
      Swirl.Torsion.Set(material, Random.Range(0.05f, 0.05f + randomRange), timeToUpdate);
    }

    Swirl.Center.Set(material, new Vector2(0.5f + Random.Range(-randomRange, randomRange) * 0.5f, 0.5f + Random.Range(-randomRange, randomRange) * 0.5f), timeToUpdate);
  }

  private void UpdateCanvasSwirl()
  {
    sliderAmount.gameObject.SetActive(false);
    swirlCanvas.gameObject.SetActive(true);

    Swirl.Strength.Set(selectedCard.Material, Random.Range(0.1f, 0.1f + randomRange), timeToUpdate);
    Swirl.Torsion.Set(selectedCard.Material, Random.Range(0.05f, 0.05f + randomRange), timeToUpdate);

    UpdateSlider(sliderSwirlStrength, Swirl.Strength, selectedCard.Material);
    UpdateSlider(sliderSwirlTorsion, Swirl.Torsion, -5.0f, 5.0f, selectedCard.Material);
  }
}
