using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform instagramCanvas;

  [SerializeField] private Text textInstagram;
  [SerializeField] private Button buttonLeftInstagram;
  [SerializeField] private Button buttonRightInstagram;

  private void ChangeInstagram(Material material)
  {
    Instagram.Filter.Set(material, (InstagramFilter)Random.Range(0, 14));
  }

  private void UpdateCanvasInstagram()
  {
    instagramCanvas.gameObject.SetActive(true);

    textInstagram.text = Card.FromCamelCase(Instagram.Filter.Get<InstagramFilter>(selectedCard.Material).ToString());
    EnumLeftButton<InstagramFilter>(buttonLeftInstagram, textInstagram, selectedCard.Material, Instagram.Filter, (int)InstagramFilter.XProII);
    EnumRightButton<InstagramFilter>(buttonRightInstagram, textInstagram, selectedCard.Material, Instagram.Filter, (int)InstagramFilter.XProII);
  }
}
