using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(BoxCollider))]
public class Card : MonoBehaviour
{
  public enum States
  {
    InDeck,
    Selected,
  }

  public States State { get; private set; } = States.InDeck;

  public Material Material { get; private set; }

  public Vector3 DeckPosition
  {
    get => deckPosition;
    set
    {
      deckPosition = value;
      showPosition = deckPosition + new Vector3(0.0f, 1.25f, 0.0f);
      originalScale = this.transform.localScale;
    }
  }

  private TextMesh label;
  private Collider trigger;

  private Vector3 deckPosition;
  private Vector3 showPosition;
  private Vector3 originalScale;

  private Coroutine coroutine0;
  private Coroutine coroutine1;

  private string effectName;
  private string effectText;

  public void Show(float duration, Eases ease)
  {
    StopCoroutines();

    coroutine0 = StartCoroutine(TweenRoutine.Interpolate(this.transform.localPosition,
                                                          showPosition,
                                                          duration,
                                                          ease,
                                                          (position) => this.transform.localPosition = position));

    coroutine1 = StartCoroutine(TweenRoutine.Interpolate(0.0f, 1.0f, duration * 1.5f, Eases.Linear, (alpha) => label.color = new Color(1.0f, 1.0f, 1.0f, alpha)));
  }

  public void Stay()
  {
  }

  public void Hide(float duration, Eases ease)
  {
    StopCoroutines();

    coroutine0 = StartCoroutine(TweenRoutine.Interpolate(this.transform.localPosition,
                                                          DeckPosition,
                                                          duration,
                                                          ease,
                                                          (position) => this.transform.localPosition = position));

    coroutine1 = StartCoroutine(TweenRoutine.Interpolate(1.0f, 0.0f, duration, Eases.Linear, (alpha) => label.color = new Color(1.0f, 1.0f, 1.0f, alpha)));
  }

  public void Select(Vector3 position, float duration, Eases ease, Text effectName, Text effectText)
  {
    if (State == States.InDeck)
    {
      StopCoroutines();

      State = States.Selected;
      label.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
      label.gameObject.SetActive(false);
      trigger.enabled = false;

      coroutine0 = StartCoroutine(TweenRoutine.Interpolate(this.transform.localPosition,
                                                           position,
                                                           duration,
                                                           ease,
                                                           (position) =>
                                                           {
                                                             this.transform.localPosition = position;
                                                             UpdateText(effectName, effectText);
                                                           }));

      coroutine1 = StartCoroutine(TweenRoutine.Interpolate(1.0f,
                                                           1.7f,
                                                           duration,
                                                           ease,
                                                           (scale) => this.transform.localScale = originalScale * scale));
    }
  }

  public void UnSelect(float duration, Eases ease, Text effectName, Text effectText)
  {
    if (State == States.Selected)
    {
      State = States.InDeck;

      effectName.gameObject.SetActive(false);
      effectText.gameObject.SetActive(false);

      coroutine0 = StartCoroutine(TweenRoutine.Interpolate(this.transform.localPosition,
                                                           deckPosition,
                                                           duration,
                                                           ease,
                                                           (position) => this.transform.localPosition = position,
                                                           () => { coroutine0 = null; trigger.enabled = true; label.gameObject.SetActive(true); }));

      coroutine1 = StartCoroutine(TweenRoutine.Interpolate(1.7f,
                                                           1.0f,
                                                           duration,
                                                           ease,
                                                           (scale) => this.transform.localScale = originalScale * scale));
    }
  }

  private void StopCoroutines()
  {
    if (coroutine0 != null)
    {
      StopCoroutine(coroutine0);
      coroutine0 = null;
    }

    if (coroutine1 != null)
    {
      StopCoroutine(coroutine1);
      coroutine1 = null;
    }
  }

  private void UpdateText(Text name, Text desc)
  {
    name.gameObject.SetActive(true);
    desc.gameObject.SetActive(true);

    name.text = label.text;

    switch (name.text)
    {
      case "Black And White": desc.text = "Black and white effect with controls for each channel"; break;
      case "Black Hole":      desc.text = "Create a 'Black Hole' effect inside the sprite"; break;
      case "Blend":           desc.text = "Photoshop pixel blending modes"; break;
      case "Dissolve":        desc.text = "Dissolves the sprite using patterns"; break;
      case "Dither":          desc.text = "Apply various color reduction algorithms"; break;
      case "Duo Tone":        desc.text = "Apply a two color gradient based on lightness"; break;
      case "Edge":            desc.text = "Enhance the edges"; break;
      case "Glass":           desc.text = "Simulates a glass material"; break;
      case "Hologram":        desc.text = "Simulates a hologram"; break;
      case "Instagram":       desc.text = "Mimics Instagram effects"; break;
      case "Masks":           desc.text = "Apply effects in up to three masks defined by a texture"; break;
      case "Negative":        desc.text = "Change each color to its opposite"; break;
      case "Outline":         desc.text = "Outer edge"; break;
      case "Pinch":           desc.text = "Deforms the sprite by applying a pinching effect"; break;
      case "Ramp":            desc.text = "Apply a 5-color gradient based on lightness"; break;
      case "Retro":           desc.text = "Vintage computer effects"; break;
      case "RGBGlitch":       desc.text = "Simulates a failure in the color channels"; break;
      case "Shake":           desc.text = "Shake the sprite"; break;
      case "Shift":           desc.text = "Shift of RGB channels"; break;
      case "Swirl":           desc.text = "Twist the sprite"; break;
      case "Tremor":          desc.text = "It makes it look like the sprite trembles"; break;
      case "Trio Tone":       desc.text = "Apply a three color gradient based on lightness"; break;

      default: desc.text = string.Empty; break;
    }
  }

  public static string FromCamelCase(string text)
  {
    string humanReadable = System.Text.RegularExpressions.Regex.Replace(text, "^_", "").Trim();
    humanReadable = System.Text.RegularExpressions.Regex.Replace(humanReadable, "([a-z])([A-Z])", "$1 $2").Trim();
    humanReadable = humanReadable.Replace("_", " ");

    return humanReadable;
  }

  private void Awake()
  {
    trigger = this.GetComponentInChildren<Collider>();

    label = this.GetComponentInChildren<TextMesh>();
    label.text = this.GetComponent<SpriteRenderer>().material.name;
    label.text = label.text.Replace(" (Instance)", string.Empty);
    label.text = FromCamelCase(label.text);
    label.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    Material = this.GetComponent<SpriteRenderer>().material;
  }
}
