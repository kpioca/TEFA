using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform glassCanvas;

  [SerializeField] private Slider sliderGlassDistortion;
  [SerializeField] private Slider sliderGlassRefraction;

  private void ChangeGlass(Material material)
  {
    SpriteMojo.Amount.Set(material, 0.8f);
    Glass.Distortion.Set(material, Random.Range(5.0f, 20.0f), timeToUpdate);
    Glass.Refraction.Set(material, Random.Range(50.0f, 75.0f), timeToUpdate);
  }

  private void UpdateCanvasGlass()
  {
    glassCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderGlassDistortion, Glass.Distortion, 20.0f, selectedCard.Material);
    UpdateSlider(sliderGlassRefraction, Glass.Refraction, 75.0f, selectedCard.Material);
  }
}
