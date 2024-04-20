using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectTimer : MonoBehaviour
{
    [SerializeField] ContentPlayer contentPlayer;

    [SerializeField] private TMP_Text effectTimerText;
    [SerializeField] private int effectTimerValue = 1;

    public Coroutine effectTimerCoroutine;
    public IEnumerator EffectTimerCoroutine()
    {
        effectTimerValue = 1;
        while (effectTimerValue > 0)
        {
            effectTimerValue = contentPlayer.EffectTimer;
            effectTimerText.text = effectTimerValue.ToString();
            effectTimerValue--;
            contentPlayer.EffectTimer = effectTimerValue;

            yield return new WaitForSeconds(1f);
        }
        effectTimerText.text = "";
    }
}
