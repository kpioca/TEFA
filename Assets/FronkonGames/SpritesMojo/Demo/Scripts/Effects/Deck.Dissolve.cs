using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform dissolveCanvas;

  [SerializeField] private Slider sliderSlide;
  [SerializeField] private Text textDissolveShape;
  [SerializeField] private Button buttonLeftShape;
  [SerializeField] private Button buttonRightShape;

  private void ChangeDissolve(Material material)
  {
    Dissolve.Slide.Set(material, sign ? 0 : 1, timeToUpdate);
    Dissolve.Shape.Set(material, (DissolveShape)Random.Range(0, (int)DissolveShape.Custom - 1));
  }

  private void UpdateCanvasDissolve()
  {
    Dissolve.Slide.Set(selectedCard.Material, 0.25f);
    dissolveCanvas.gameObject.SetActive(true);
    textDissolveShape.text = Card.FromCamelCase(Dissolve.Shape.Get<DissolveShape>(selectedCard.Material).ToString());
    sliderAmount.gameObject.SetActive(false);

    UpdateSlider(sliderSlide, Dissolve.Slide, selectedCard.Material);
    EnumLeftButton<DissolveShape>(buttonLeftShape, textDissolveShape, selectedCard.Material, Dissolve.Shape, (int)DissolveShape.Vertical);
    EnumRightButton<DissolveShape>(buttonRightShape, textDissolveShape, selectedCard.Material, Dissolve.Shape, (int)DissolveShape.Vertical);
  }
}
