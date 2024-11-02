using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform retroCanvas;

  [SerializeField] private Text textRetro;
  [SerializeField] private Button buttonLeftRetro;
  [SerializeField] private Button buttonRightRetro;
  [SerializeField] private Slider sliderRetroPixelation;

  private void ChangeRetro(Material material)
  {
    Retro.Emulation.Set(material, (Retro.Emulations)Random.Range(0, 8));
    Retro.Pixelation.Set(material, Random.Range(5, 20));
  }

  private void UpdateCanvasRetro()
  {
    retroCanvas.gameObject.SetActive(true);

    textRetro.text = Card.FromCamelCase(Retro.Emulation.Get<Retro.Emulations>(selectedCard.Material).ToString());
    EnumLeftButton<Retro.Emulations>(buttonLeftRetro, textRetro, selectedCard.Material, Retro.Emulation, (int)Retro.Emulations.Z80);
    EnumRightButton<Retro.Emulations>(buttonRightRetro, textRetro, selectedCard.Material, Retro.Emulation, (int)Retro.Emulations.Z80);
    UpdateSlider(sliderRetroPixelation, Retro.Pixelation, 1, 20, selectedCard.Material);
  }
}
