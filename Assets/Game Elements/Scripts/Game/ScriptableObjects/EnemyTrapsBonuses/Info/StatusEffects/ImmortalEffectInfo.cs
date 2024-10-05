using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImmortalEffectInfo", menuName = "LevelProperties/Status Effects/New ImmortalEffectInfo")]
public class ImmortalEffectInfo : StatusEffectInfo
{

    private Coroutine effectCoroutine;
    int duration;
    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration)
    {
        duration = durationSec;
        this.duration = durationSec;

        if (effectCoroutine == null)
        {
            effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(gameManager, contentPlayer));
            contentPlayer.applyEffect(this);
        }
    }

    public override IEnumerator EffectCoroutine(GameManager gameManager, ContentPlayer contentPlayer)
    {
        contentPlayer.changeImmortalState(true);


        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration--;
        }

        contentPlayer.changeImmortalState(false);

        contentPlayer.removeEffect(this);
        effectCoroutine = null;
    }
}
