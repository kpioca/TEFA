using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform rampCanvas;

  [SerializeField] private InputField rampInput;
  [SerializeField] private Button rampButton;

  private string lastSearch;

  private void ChangeRamp(Material material)
  {
    int palette = Random.Range(0, palettes.Length);

    Color[] colors = palettes[palette].colors;
    System.Array.Sort(colors, CompareLuminance);

    Ramp.Luminance.Set(material, (0.05f, 0.5f));
    Ramp.Color0.Set(material, colors[0], timeToUpdate);
    Ramp.Color1.Set(material, colors[1], timeToUpdate);
    Ramp.Color2.Set(material, colors[2], timeToUpdate);
    Ramp.Color3.Set(material, colors[3], timeToUpdate);
    Ramp.Color4.Set(material, colors[4], timeToUpdate);
  }

  private void UpdateCanvasRamp()
  {
#if UNITY_WEBGL
    rampCanvas.gameObject.SetActive(false);
#else
    rampCanvas.gameObject.SetActive(true);

    rampButton.onClick.RemoveAllListeners();
    rampButton.onClick.AddListener(() =>
    {
      if (string.IsNullOrEmpty(rampInput.text) == false && rampInput.text.Length > 3)
      {
        if (lastSearch != rampInput.text)
        {
          int pages = 0;

          Palette[] palettes = ColourLovers.Search(0, rampInput.text.Trim(), out pages);
          if (palettes.Length > 0)
          {
            Color[] colors = palettes[0].colors;

            System.Array.Sort(colors, CompareLuminance);

            Ramp.Color0.Set(selectedCard.Material, colors[0], 0.5f);
            Ramp.Color1.Set(selectedCard.Material, colors[1], 0.5f);
            Ramp.Color2.Set(selectedCard.Material, colors[2], 0.5f);
            Ramp.Color3.Set(selectedCard.Material, colors[3], 0.5f);
            Ramp.Color4.Set(selectedCard.Material, colors[4], 0.5f);
          }

          lastSearch = rampInput.text;
        }
      }

      rampInput.text = string.Empty;
    });
#endif
  }
}
