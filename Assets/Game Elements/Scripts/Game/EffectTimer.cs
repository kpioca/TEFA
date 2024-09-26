using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectTimer : MonoBehaviour
{
    [SerializeField] ContentPlayer contentPlayer;

    [SerializeField] private TMP_Text effectTimerText;
    private int effectTimerValue = 1;

    [SerializeField] private GameObject panel;

    public Coroutine effectTimerCoroutine;
    public IEnumerator EffectTimerCoroutine()
    {
        effectTimerValue = 1;
        panel.SetActive(true);
        while (effectTimerValue > 0)
        {
            effectTimerValue = contentPlayer.EffectTimer;
            effectTimerText.text = effectTimerValue.ToString();
            effectTimerValue--;
            contentPlayer.EffectTimer = effectTimerValue;

            yield return new WaitForSeconds(1f);
        }
        panel.SetActive(false);
        effectTimerText.text = "";
    }
}
