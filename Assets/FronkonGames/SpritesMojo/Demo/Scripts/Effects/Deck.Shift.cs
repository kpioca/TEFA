using UnityEngine;
using UnityEngine.UI;

using FronkonGames.SpritesMojo;

public partial class Deck : MonoBehaviour
{
  [Space(10.0f), SerializeField]
  private RectTransform shiftCanvas;

  [SerializeField] private Text textShift;
  [SerializeField] private Button buttonLeftShift;
  [SerializeField] private Button buttonRightShift;

  [SerializeField] private Slider sliderShift;
  [SerializeField] private Slider sliderShiftNoiseStrength;
  [SerializeField] private Slider sliderShiftNoiseSpeed;

  private Vector2 redShift, greenShift, blueShift;
  private float radial;

  private void ChangeShift(Material material)
  {
    Shift.Mode.Set(material, Random.Range(0, 2));
    Shift.RedShift.Set(material, 50.0f * Random.Range(-randomRange, randomRange) * Vector2.one, timeToUpdate);
    Shift.GreenShift.Set(material, 50.0f * Random.Range(-randomRange, randomRange) * Vector2.one, timeToUpdate);
    Shift.BlueShift.Set(material, 50.0f * Random.Range(-randomRange, randomRange) * Vector2.one, timeToUpdate);
    Shift.RadialShift.Set(material, Random.Range(randomRange * 0.1f, randomRange * 0.5f), timeToUpdate);
    Shift.NoiseStrength.Set(material, Random.Range(randomRange * 0.1f, randomRange), timeToUpdate);
    Shift.NoiseSpeed.Set(material, Random.Range(0.1f, randomRange), timeToUpdate);
  }

  private void UpdateCanvasShift()
  {
    sliderAmount.gameObject.SetActive(false);
    shiftCanvas.gameObject.SetActive(true);

    textShift.text = ((ShiftMode)Shift.Mode.Get(selectedCard.Material)).ToString();
    EnumLeftButton<BlendFunction>(buttonLeftShift, textShift, selectedCard.Material, Shift.Mode, 1);
    EnumRightButton<BlendFunction>(buttonRightShift, textShift, selectedCard.Material, Shift.Mode, 1);

    UpdateSlider(sliderShiftNoiseStrength, Shift.NoiseStrength, selectedCard.Material);
    UpdateSlider(sliderShiftNoiseSpeed, Shift.NoiseSpeed, selectedCard.Material);

    redShift = 50.0f * Random.Range(-randomRange, randomRange) * Vector2.one;
    greenShift = 50.0f * Random.Range(-randomRange, randomRange) * Vector2.one;
    blueShift = 50.0f * Random.Range(-randomRange, randomRange) * Vector2.one;

    Shift.RedShift.Set(selectedCard.Material, redShift);
    Shift.GreenShift.Set(selectedCard.Material, greenShift);
    Shift.BlueShift.Set(selectedCard.Material, blueShift);

    radial = Random.Range(randomRange * 0.1f, randomRange * 0.5f);

    Shift.RadialShift.Set(selectedCard.Material, radial);

    sliderShift.onValueChanged.RemoveAllListeners();
    sliderShift.value = 1.0f;
    sliderShift.minValue = 0.0f;
    sliderShift.maxValue = 2.0f;
    sliderShift.onValueChanged.AddListener((value) =>
    {
      Shift.RadialShift.Set(selectedCard.Material, value);

      Shift.RedShift.Set(selectedCard.Material, redShift * value * 2.0f);
      Shift.GreenShift.Set(selectedCard.Material, greenShift * value * 2.0f);
      Shift.BlueShift.Set(selectedCard.Material, blueShift * value * 2.0f);
    });
  }
}
