using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;
using Outline = FronkonGames.SpritesMojo.Outline;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform outlineCanvas;

  [SerializeField] private Slider sliderOutlineSize;

  private void ChangeOutline(Material material)
  {
        FronkonGames.SpritesMojo.Outline.Size.Set(material, 30, timeToUpdate);
        FronkonGames.SpritesMojo.Outline.Mode.Set(material, (OutlineMode)Random.Range(0, 3));

    float hue = Random.Range(0.0f, 1.0f);
        FronkonGames.SpritesMojo.Outline.Color0.Set(material, Color.HSVToRGB(hue, 1.0f, 1.0f), timeToUpdate);
        FronkonGames.SpritesMojo.Outline.Color1.Set(material, Color.HSVToRGB(1.0f - hue, 1.0f, 1.0f), timeToUpdate);

    if (patterns.Length > 0)
            FronkonGames.SpritesMojo.Outline.Texture.Set(material, patterns[Random.Range(0, patterns.Length)]);

        FronkonGames.SpritesMojo.Outline.TextureVelocity.Set(material, new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange)) * 0.01f, timeToUpdate);
        FronkonGames.SpritesMojo.Outline.Vertical.Set(material, Random.Range(0.0f, 1.0f) >= 0.5f);
  }

  private void UpdateCanvasOutline()
  {
    sliderAmount.gameObject.SetActive(false);
    outlineCanvas.gameObject.SetActive(true);

    UpdateSlider(sliderOutlineSize, FronkonGames.SpritesMojo.Outline.Size, 30, selectedCard.Material);
  }
}
