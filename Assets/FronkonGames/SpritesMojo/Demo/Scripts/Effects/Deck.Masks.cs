using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform masksCanvas;

  [SerializeField] private Text textRed;
  [SerializeField] private Button buttonLeftRed;
  [SerializeField] private Button buttonRightRed;

  [SerializeField] private Text textGreen;
  [SerializeField] private Button buttonLeftGreen;
  [SerializeField] private Button buttonRightGreen;

  [SerializeField] private Text textBlue;
  [SerializeField] private Button buttonLeftBlue;
  [SerializeField] private Button buttonRightBlue;

  private void ChangeMasks(Material material)
  {
    if (patterns.Length > 0)
    {
      int palette = Random.Range(0, palettes.Length);

      Color[] colors = palettes[palette].colors;

      Masks.RedTexture.Set(material, patterns[Random.Range(0, patterns.Length)]);
      Masks.RedTint.Set(material, colors[2]);
      Masks.RedTextureVelocity.Set(material, new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange)) * 0.01f, timeToUpdate);

      Masks.GreenTexture.Set(material, patterns[Random.Range(0, patterns.Length)]);
      Masks.GreenTint.Set(material, colors[1]);
      Masks.GreenTextureVelocity.Set(material, new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange)) * 0.01f, timeToUpdate);

      Masks.BlueTexture.Set(material, patterns[Random.Range(0, patterns.Length)]);
      Masks.BlueTint.Set(material, colors[4]);
      Masks.BlueTextureVelocity.Set(material, new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange)) * 0.01f, timeToUpdate);

      if (selectedCard != null && selectedCard.Material != material)
      {
        Masks.RedBlend.Set(material, NiceRandomBlend());
        Masks.GreenBlend.Set(material, NiceRandomBlend());
        Masks.BlueBlend.Set(material, NiceRandomBlend());
      }
    }
  }

  private void UpdateCanvasMasks()
  {
    masksCanvas.gameObject.SetActive(true);

    textRed.text = Masks.RedBlend.Get<BlendFunction>(selectedCard.Material).ToString();
    EnumLeftButton<BlendFunction>(buttonLeftRed, textRed, selectedCard.Material, Masks.RedBlend, (int)BlendFunction.Subtract);
    EnumRightButton<BlendFunction>(buttonRightRed, textRed, selectedCard.Material, Masks.RedBlend, (int)BlendFunction.Subtract);

    textGreen.text = Masks.GreenBlend.Get<BlendFunction>(selectedCard.Material).ToString();
    EnumLeftButton<BlendFunction>(buttonLeftGreen, textGreen, selectedCard.Material, Masks.GreenBlend, (int)BlendFunction.Subtract);
    EnumRightButton<BlendFunction>(buttonRightGreen, textGreen, selectedCard.Material, Masks.GreenBlend, (int)BlendFunction.Subtract);

    textBlue.text = Masks.BlueBlend.Get<BlendFunction>(selectedCard.Material).ToString();
    EnumLeftButton<BlendFunction>(buttonLeftBlue, textBlue, selectedCard.Material, Masks.BlueBlend, (int)BlendFunction.Subtract);
    EnumRightButton<BlendFunction>(buttonRightBlue, textBlue, selectedCard.Material, Masks.BlueBlend, (int)BlendFunction.Subtract);
  }
}
