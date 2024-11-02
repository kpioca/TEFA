using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [SerializeField]
  private Vector3 selectedCardPosition;

  [SerializeField]
  private float width = 10.0f;

  [SerializeField]
  private float showTime = 0.25f; // Its showtime!!

  [SerializeField]
  private Eases showEase = Eases.Linear;

  [SerializeField]
  private Eases hideEase = Eases.Linear;

  [Space(10.0f), SerializeField]
  private UnityEngine.UI.Text effectText;

  [SerializeField]
  private UnityEngine.UI.Text effectNameText;

  [SerializeField]
  private bool showEffectsCanvas = true;

  [Space(10.0f), SerializeField, Range(0.0f, 1.0f)]
  private float randomRange = 0.25f;

  [SerializeField, Range(0.0f, 10.0f)]
  private float timeToUpdate = 2.0f;

  [Space(10.0f), SerializeField]
  private Texture[] patterns;

  [SerializeField]
  private RectTransform effectsCanvas;

  [SerializeField]
  private Slider sliderAmount;

  private const int rays = 5;

  private float time;

  private RaycastHit[] raycastHit = new RaycastHit[rays];

  private Card[] cards = new Card[] { };
  private List<Material> materials = new List<Material>();

  private Card showCard;
  private Card selectedCard;

  private bool sign;
  private int cardIndex = 0;

  private class PaletteFive
  {
    public Color[] colors = new Color[5];

    public PaletteFive(Color[] colors) { this.colors = colors; }
  };

  /// <summary>
  /// Nice looking palletes.
  /// </summary>
  static PaletteFive[] palettes = new PaletteFive[]
  {
    new PaletteFive(new Color[] { FromHex("16C1C8"), FromHex("49CCCC"), FromHex("7CD7CF"), FromHex("AEE1D3"), FromHex("E1ECD6") }),
    new PaletteFive(new Color[] { FromHex("B1103C"), FromHex("781E36"), FromHex("4A2932"), FromHex("575C5D"), FromHex("667172") }),
    new PaletteFive(new Color[] { FromHex("3E4147"), FromHex("FFFEDF"), FromHex("DFBA69"), FromHex("5A2E2E"), FromHex("2A2C31") }),
    new PaletteFive(new Color[] { FromHex("F2502C"), FromHex("CAD17A"), FromHex("FCF59B"), FromHex("91C494"), FromHex("C42311") }),
    new PaletteFive(new Color[] { FromHex("000000"), FromHex("F75E11"), FromHex("FFFFFF"), FromHex("97B8B6"), FromHex("CCDEDD") }),
    new PaletteFive(new Color[] { FromHex("046D8B"), FromHex("309292"), FromHex("2FB8AC"), FromHex("93A42A"), FromHex("ECBE13") }),
    new PaletteFive(new Color[] { FromHex("CAFF42"), FromHex("EBF7F8"), FromHex("D0E0EB"), FromHex("88ABC2"), FromHex("49708A") }),
    new PaletteFive(new Color[] { FromHex("1B676B"), FromHex("519548"), FromHex("88C425"), FromHex("BEF202"), FromHex("EAFDE6") }),
    new PaletteFive(new Color[] { FromHex("655643"), FromHex("80BCA3"), FromHex("F6F7BD"), FromHex("E6AC27"), FromHex("BF4D28") }),
    new PaletteFive(new Color[] { FromHex("F04330"), FromHex("C9746B"), FromHex("89615D"), FromHex("583733"), FromHex("271310") }),
    new PaletteFive(new Color[] { FromHex("F1EAD1"), FromHex("84B5A2"), FromHex("2D8B92"), FromHex("254971"), FromHex("1E0741") }),
    new PaletteFive(new Color[] { FromHex("DAE0F0"), FromHex("A2A0B0"), FromHex("3B3D3C"), FromHex("47449E"), FromHex("A69EF7") }),
    new PaletteFive(new Color[] { FromHex("ED7E2F"), FromHex("3D3127"), FromHex("629C7C"), FromHex("A3CE96"), FromHex("E3FFB0") }),
    new PaletteFive(new Color[] { FromHex("E2E8CA"), FromHex("D5DE95"), FromHex("8EBA5F"), FromHex("528F47"), FromHex("1F5C45") }),
    new PaletteFive(new Color[] { FromHex("364C4A"), FromHex("A7B1B0"), FromHex("DFD7CC"), FromHex("F4EDDD"), FromHex("FFFEF7") }),
    new PaletteFive(new Color[] { FromHex("DED9D6"), FromHex("8CFF02"), FromHex("648A64"), FromHex("1F4E53"), FromHex("050235") }),
    new PaletteFive(new Color[] { FromHex("5E3F61"), FromHex("92637A"), FromHex("CC8E8B"), FromHex("EAC99E"), FromHex("F2ECB7") }),
    new PaletteFive(new Color[] { FromHex("792427"), FromHex("545058"), FromHex("2B818E"), FromHex("80A098"), FromHex("D1BDA2") }),
    new PaletteFive(new Color[] { FromHex("020314"), FromHex("5932E6"), FromHex("8632E6"), FromHex("B332E6"), FromHex("E032E6") }),
    new PaletteFive(new Color[] { FromHex("FF1298"), FromHex("D31CFC"), FromHex("41ACF2"), FromHex("4FE3C3"), FromHex("6FEDAE") }),
  };

  private static Color FromHex(string hex)
  {
    return new Color(int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f,
                     int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f,
                     int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255.0f);
  }

  private void ChangeEffects()
  {
    for (int i = 0; i < materials.Count; ++i)
    {
      if (showEffectsCanvas == true)
      if (selectedCard != null &&
          selectedCard.Material != null &&
          selectedCard.Material == materials[i] &&
          materials[i].name != "DuoTone" &&
          materials[i].name != "Edge" &&
          materials[i].name != "Masks" &&
          materials[i].name != "Outline" &&
          materials[i].name != "Swirl" &&
          materials[i].name != "Pinch" &&
          materials[i].name != "Ramp" &&
          materials[i].name != "TrioTone")
        continue;

      switch (materials[i].name)
      {
        case "BlackAndWhite": ChangeBlackAndWhite(materials[i]); break;
        case "BlackHole":     ChangeBlackHole(materials[i]); break;
        case "Blend":         ChangeBlend(materials[i]); break;
        case "Dissolve":      ChangeDissolve(materials[i]); break;
        case "Dither":        ChangeDither(materials[i]); break;
        case "DuoTone":       ChangeDuoTone(materials[i]); break;
        case "Edge":          ChangeEdge(materials[i]); break;
        case "Glass":         ChangeGlass(materials[i]); break;
        case "Hologram":      ChangeHologram(materials[i]); break;
        case "Instagram":     ChangeInstagram(materials[i]); break;
        case "Masks":         ChangeMasks(materials[i]); break;
        case "Negative":      ChangeNegative(materials[i]); break;
        case "Outline":       ChangeOutline(materials[i]); break;
        case "Pinch":         ChangePinch(materials[i]); break;
        case "Ramp":          ChangeRamp(materials[i]); break;
        case "Retro":         ChangeRetro(materials[i]); break;
        case "RGBGlitch":     ChangeRGBGlitch(materials[i]); break;
        case "Shake":         ChangeShake(materials[i]); break;
        case "Shift":         ChangeShift(materials[i]); break;
        case "Swirl":         ChangeSwirl(materials[i]); break;
        case "Tremor":        ChangeTremor(materials[i]); break;
        case "TrioTone":      ChangeTrioTone(materials[i]); break;
      }
    }

    sign = !sign;
    time = 0.0f;
  }

  private void UpdateCanvasEffects()
  {
    sliderAmount.gameObject.SetActive(true);
    blackAndWhiteCanvas.gameObject.SetActive(false);
    blackHoleCanvas.gameObject.SetActive(false);
    blendCanvas.gameObject.SetActive(false);
    dissolveCanvas.gameObject.SetActive(false);
    ditherCanvas.gameObject.SetActive(false);
    duoToneCanvas.gameObject.SetActive(false);
    edgeCanvas.gameObject.SetActive(false);
    glassCanvas.gameObject.SetActive(false);
    hologramCanvas.gameObject.SetActive(false);
    instagramCanvas.gameObject.SetActive(false);
    masksCanvas.gameObject.SetActive(false);
    negativeCanvas.gameObject.SetActive(false);
    outlineCanvas.gameObject.SetActive(false);
    pinchCanvas.gameObject.SetActive(false);
    rampCanvas.gameObject.SetActive(false);
    retroCanvas.gameObject.SetActive(false);
    rgbGlitchCanvas.gameObject.SetActive(false);
    shakeCanvas.gameObject.SetActive(false);
    shiftCanvas.gameObject.SetActive(false);
    swirlCanvas.gameObject.SetActive(false);
    tremorCanvas.gameObject.SetActive(false);
    trioToneCanvas.gameObject.SetActive(false);

    if (showEffectsCanvas == true)
    {
      if (selectedCard != null && selectedCard.Material != null)
      {
        switch (selectedCard.Material.name)
        {
          case "BlackAndWhite": UpdateCanvasBlackAndWhite(); break;
          case "BlackHole":     UpdateCanvasBlackHole(); break;
          case "Blend":         UpdateCanvasBlend(); break;
          case "Dissolve":      UpdateCanvasDissolve(); break;
          case "Dither":        UpdateCanvasDither(); break;
          case "DuoTone":       UpdateCanvasDuoTone(); break;
          case "Edge":          UpdateCanvasEdge(); break;
          case "Glass":         UpdateCanvasGlass(); break;
          case "Hologram":      UpdateCanvasHologram(); break;
          case "Instagram":     UpdateCanvasInstagram(); break;
          case "Masks":         UpdateCanvasMasks(); break;
          case "Negative":      UpdateCanvasNegative(); break;
          case "Outline":       UpdateCanvasOutline(); break;
          case "Pinch":         UpdateCanvasPinch(); break;
          case "Ramp":          UpdateCanvasRamp(); break;
          case "Retro":         UpdateCanvasRetro(); break;
          case "RGBGlitch":     UpdateCanvasRGBGlitch(); break;
          case "Shake":         UpdateCanvasShake(); break;
          case "Shift":         UpdateCanvasShift(); break;
          case "Swirl":         UpdateCanvasSwirl(); break;
          case "Tremor":        UpdateCanvasTremor(); break;
          case "TrioTone":      UpdateCanvasTrioTone(); break;
        }
      }
    }
    else
      sliderAmount.gameObject.SetActive(false);
  }

  private void EnumLeftButton<T>(Button button, Text label, Material material, EnumVariable enumVariable, int max) where T : struct, IComparable
  {
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() =>
    {
      int mode = Convert.ToInt32(enumVariable.Get<T>(material));
      if (mode > 0)
        enumVariable.Set(material, mode - 1);
      else
        enumVariable.Set(material, max);

      label.text = Card.FromCamelCase(Enum.GetNames(typeof(T)).GetValue(material.GetInt(enumVariable.Variable)).ToString());
    });
  }

  private void EnumRightButton<T>(Button button, Text label, Material material, EnumVariable enumVariable, int max) where T : struct, IComparable
  {
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() =>
    {
      int mode = Convert.ToInt32(enumVariable.Get<T>(material));
      if (mode < max)
        enumVariable.Set(material, mode + 1);
      else
        enumVariable.Set(material, 0);

      label.text = Card.FromCamelCase(Enum.GetNames(typeof(T)).GetValue(material.GetInt(enumVariable.Variable)).ToString());
    });
  }

  private void EnumLeftButton<T>(Button button, Text label, Material material, KeywordsVariable keywordsVariable, int max) where T : struct, IComparable
  {
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() =>
    {
      int mode = keywordsVariable.Get(material);
      if (mode > 0)
        keywordsVariable.Set(material, mode - 1);
      else
        keywordsVariable.Set(material, max);

      label.text = Card.FromCamelCase(keywordsVariable.Names[keywordsVariable.Get(material)]);
    });
  }

  private void EnumRightButton<T>(Button button, Text label, Material material, KeywordsVariable keywordsVariable, int max) where T : struct, IComparable
  {
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() =>
    {
      int mode = keywordsVariable.Get(material);
      if (mode < max)
        keywordsVariable.Set(material, mode + 1);
      else
        keywordsVariable.Set(material, 0);

      label.text = Card.FromCamelCase(keywordsVariable.Names[keywordsVariable.Get(material)]);
    });
  }

  private void UpdateSlider(Slider slider, float value, Material material, UnityAction<float> onValueChanged)
  {
    slider.onValueChanged.RemoveAllListeners();
    slider.value = value;
    slider.onValueChanged.AddListener(onValueChanged);
  }

  private void UpdateSlider(Slider slider, FloatVariable range, float min, float max, Material material)
  {
    slider.onValueChanged.RemoveAllListeners();
    slider.value = range.Get(material);
    slider.maxValue = max;
    slider.minValue = min;
    slider.onValueChanged.AddListener((value) => range.Set(material, value));
  }

  private void UpdateSlider(Slider slider, FloatRangeVariable range, Material material)
  {
    slider.onValueChanged.RemoveAllListeners();
    slider.value = range.Get(material);
    slider.maxValue = range.Max;
    slider.minValue = range.Min;
    slider.onValueChanged.AddListener((value) => range.Set(material, value));
  }

  private void UpdateSlider(Slider slider, IntPositiveVariable range, int max, Material material)
  {
    slider.onValueChanged.RemoveAllListeners();
    slider.value = range.Get(material);
    slider.maxValue = max;
    slider.minValue = 0.0f;
    slider.onValueChanged.AddListener((value) => range.Set(material, (int)value));
  }

  private void UpdateSlider(Slider slider, IntRangeVariable range, Material material)
  {
    slider.onValueChanged.RemoveAllListeners();
    slider.value = range.Get(material);
    slider.maxValue = range.Max;
    slider.minValue = range.Min;
    slider.onValueChanged.AddListener((value) => range.Set(material, (int)value));
  }

  private void UpdateSlider(Slider slider, IntVariable range, int min, int max, Material material)
  {
    slider.onValueChanged.RemoveAllListeners();
    slider.value = range.Get(material);
    slider.maxValue = max;
    slider.minValue = min;
    slider.onValueChanged.AddListener((value) => range.Set(material, (int)value));
  }

  private void UpdateSlider(Slider slider, FloatPositiveVariable range, float max, Material material)
  {
    slider.onValueChanged.RemoveAllListeners();
    slider.value = range.Get(material);
    slider.maxValue = max;
    slider.minValue = 0.0f;
    slider.onValueChanged.AddListener((value) => range.Set(material, value));
  }

  private static int CompareLuminance(Color color1, Color color2)
  {
    float colorLum1 = color1.r * 0.299f + color1.g * 0.587f + color1.b * 0.114f;
    float colorLum2 = color2.r * 0.299f + color2.g * 0.587f + color2.b * 0.114f;

    if (colorLum1 > colorLum2)
      return 1;

    if (colorLum2 > colorLum1)
      return -1;

    return 0;
  }

  static BlendFunction[] functions =
  {
    BlendFunction.Additive,
    BlendFunction.Average,
    BlendFunction.ColorDodge,
    BlendFunction.Darken,
    BlendFunction.Difference,
    BlendFunction.Exclusion,
    BlendFunction.Reflect,
    BlendFunction.Glow,
    BlendFunction.Overlay,
    BlendFunction.HardLight,
    BlendFunction.Multiply,
    BlendFunction.PinLight,
    BlendFunction.Screen,
    BlendFunction.SoftLight
  };

  private BlendFunction NiceRandomBlend() => functions[UnityEngine.Random.Range(0, functions.Length)];

  private void Awake()
  {
    effectsCanvas.gameObject.SetActive(false);

    sliderAmount.onValueChanged.AddListener((value) =>
    {
      if (selectedCard != null && selectedCard.Material != null)
        SpriteMojo.Amount.Set(selectedCard.Material, value);
    });

    cards = this.GetComponentsInChildren<Card>();

    this.enabled = cards.Length > 0;
  }

  private void OnEnable()
  {
    float offset = width / cards.Length;
    for (int i = 0; i < cards.Length; ++i)
    {
      cards[i].transform.localPosition = new Vector3(-width * 0.5f + (offset * i), 0.0f, i);
      cards[i].DeckPosition = cards[i].transform.localPosition;

      Material material = cards[i].GetComponent<SpriteRenderer>().material;
      material.name = material.name.Replace(" (Instance)", string.Empty);
      materials.Add(material);
    }
  }

  private void OnDestroy()
  {
    sliderAmount.onValueChanged.RemoveAllListeners();
  }

  private void Update()
  {
    time += Time.deltaTime;

    effectsCanvas.gameObject.SetActive(effectNameText.IsActive());

    for (int i = 0; i < materials.Count; ++i)
    {
      if (selectedCard != null && selectedCard.Material != null && selectedCard.Material == materials[i])
        continue;

      switch (materials[i].name)
      {
        case "Dissolve":
          Dissolve.Slide.Set(materials[i], sign == true ? time / (timeToUpdate - 0.0f) : 1.0f - (time / (timeToUpdate - 0.0f)));
          break;
      }
    }

    if (time > timeToUpdate)
      ChangeEffects();

    Card card = null;
    int hits = Physics.RaycastNonAlloc(Camera.main.ScreenPointToRay(Input.mousePosition), raycastHit, Mathf.Infinity);
    if (hits > 0)
    {
      for (int i = 0; i < hits; ++i)
      {
        Card cardOnTop = raycastHit[i].collider.gameObject.GetComponent<Card>();
        if (cardOnTop != null && (card == null || cardOnTop.transform.position.z < card.transform.position.z))
          card = cardOnTop;
      }
    }

    if (showEffectsCanvas == false && Input.GetKeyUp(KeyCode.Space) == true)
    {
      if (cardIndex < cards.Length)
      {
        card = cards[cardIndex];

        cardIndex = cardIndex + 1;
      }
      else
      {
        if (selectedCard != null)
          selectedCard.UnSelect(showTime, Eases.Linear, effectNameText, effectText);

        selectedCard = showCard = card = null;
      }
    }

    if (card == null)
    {
      if (showCard != null)
        showCard.Hide(showTime, hideEase);
    }
    else if (showCard == card)
      card.Stay();
    else if (showCard != null)
    {
      showCard.Hide(showTime, hideEase);
      card.Show(showTime, showEase);
    }
    else
      card.Show(showTime, showEase);

    showCard = card;

    if (showCard != null && (Input.GetMouseButtonUp(0) == true || Input.GetKeyUp(KeyCode.Space) == true) && (selectedCard == null || selectedCard.State == Card.States.Selected))
    {
      sliderAmount.value = 1.0f;

      if (selectedCard != null)
        selectedCard.UnSelect(showTime, Eases.Linear, effectNameText, effectText);

      showCard.Select(selectedCardPosition, showTime, Eases.Linear, effectNameText, effectText);

      selectedCard = showCard;
      showCard = null;

      UpdateCanvasEffects();
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.gray;
    
    Gizmos.DrawWireCube(this.transform.position, new Vector3(width, 1.0f, 0.0f));

    Gizmos.DrawWireSphere(this.transform.position + selectedCardPosition, 1.0f);
  }
}
