using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform tremorCanvas;

  [SerializeField] private Slider sliderTremorVelocity;
  [SerializeField] private Slider sliderTremorNoiseScale;
  [SerializeField] private Slider sliderTremorNoiseSnap;

  private void ChangeTremor(Material material)
  {
    Tremor.Velocity.Set(material, Random.Range(0.4f, 0.6f), timeToUpdate);
    Tremor.NoiseScale.Set(material, Random.Range(0.2f, 0.5f), timeToUpdate);
    Tremor.NoiseSnap.Set(material, Random.Range(0.2f, 2.0f), timeToUpdate);
  }

  private void UpdateCanvasTremor()
  {
    sliderAmount.gameObject.SetActive(false);
    tremorCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderTremorVelocity, Tremor.Velocity, selectedCard.Material);
    UpdateSlider(sliderTremorNoiseScale, Tremor.NoiseScale, selectedCard.Material);
    UpdateSlider(sliderTremorNoiseSnap, Tremor.NoiseSnap, selectedCard.Material);
  }
}
