using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform blendCanvas;

  [SerializeField] private Text textBlendMode;
  [SerializeField] private Button buttonLeftBlend;
  [SerializeField] private Button buttonRightBlend;

  private void ChangeBlend(Material material)
  {
    Blend.Mode.Set(material, NiceRandomBlend());
  }

  private void UpdateCanvasBlend()
  {
    blendCanvas.gameObject.SetActive(true);
    textBlendMode.text = Blend.Mode.Get<BlendFunction>(selectedCard.Material).ToString();

    EnumLeftButton<BlendFunction>(buttonLeftBlend, textBlendMode, selectedCard.Material, Blend.Mode, (int)BlendFunction.Subtract);
    EnumRightButton<BlendFunction>(buttonRightBlend, textBlendMode, selectedCard.Material, Blend.Mode, (int)BlendFunction.Subtract);
  }
}
