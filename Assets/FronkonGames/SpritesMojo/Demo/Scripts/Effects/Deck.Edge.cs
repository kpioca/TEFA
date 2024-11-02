using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform edgeCanvas;

  [SerializeField] private Text textEdgeMode;
  [SerializeField] private Button buttonLeftEdgeMode;
  [SerializeField] private Button buttonRightEdgeMode;
  [SerializeField] private Text textEdgeSobel;
  [SerializeField] private Button buttonLeftEdgeSobel;
  [SerializeField] private Button buttonRightEdgeSobel;

  private void ChangeEdge(Material material)
  {
    Edge.Tint.Set(material, Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f), timeToUpdate);

    if (selectedCard != null && selectedCard.Material != material)
    {
      Edge.Mode.Set(material, (EdgeMode)Random.Range(0, 2));
      Edge.Sobel.Set(material, (SobelFunction)Random.Range(0, 4));
    }
  }

  private void UpdateCanvasEdge()
  {
    Edge.Reset(selectedCard.Material);
    edgeCanvas.gameObject.SetActive(true);

    textEdgeMode.text = Card.FromCamelCase(Edge.Mode.Get<EdgeMode>(selectedCard.Material).ToString());
    EnumLeftButton<EdgeMode>(buttonLeftEdgeMode, textEdgeMode, selectedCard.Material, Edge.Mode, (int)EdgeMode.TrueColor);
    EnumRightButton<EdgeMode>(buttonRightEdgeMode, textEdgeMode, selectedCard.Material, Edge.Mode, (int)EdgeMode.TrueColor);

    textEdgeSobel.text = Card.FromCamelCase(Edge.Sobel.Get<SobelFunction>(selectedCard.Material).ToString());
    EnumLeftButton<SobelFunction>(buttonLeftEdgeSobel, textEdgeSobel, selectedCard.Material, Edge.Sobel, (int)SobelFunction.Scharr);
    EnumRightButton<SobelFunction>(buttonRightEdgeSobel, textEdgeSobel, selectedCard.Material, Edge.Sobel, (int)SobelFunction.Scharr);
  }
}
