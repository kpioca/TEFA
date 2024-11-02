using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectTimer : MonoBehaviour
{
    ContentPlayer _contentPlayer;

    [SerializeField] private TMP_Text effectTimerText;
    private int effectTimerValue = 1;

    [SerializeField] private GameObject panel;

    public Coroutine effectTimerCoroutine;

    public void Initialize(ContentPlayer contentPlayer)
    {
        _contentPlayer = contentPlayer;
    }
    public IEnumerator EffectTimerCoroutine()
    {
        effectTimerValue = 1;
        panel.SetActive(true);
        while (effectTimerValue > 0)
        {
            effectTimerValue = _contentPlayer.EffectTimer;
            effectTimerText.text = effectTimerValue.ToString();
            effectTimerValue--;
            _contentPlayer.EffectTimer = effectTimerValue;

            yield return new WaitForSeconds(1f);
        }
        panel.SetActive(false);
        effectTimerText.text = "";
    }
}
