using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImmortalEffectInfo", menuName = "LevelProperties/Status Effects/New ImmortalEffectInfo")]
public class ImmortalEffectInfo : StatusEffectInfo
{

    private Coroutine effectCoroutine;
    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration)
    {
        duration = durationSec;
        if (effectCoroutine != null)
        {
            gameManager.StopCoroutine(effectCoroutine);

            contentPlayer.changeImmortalState(false);
            contentPlayer.removeEffect(this);
        }
        effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(gameManager, contentPlayer, duration));
    }

    public override IEnumerator EffectCoroutine(GameManager gameManager, ContentPlayer contentPlayer, int duration)
    {
        contentPlayer.changeImmortalState(true);

        yield return new WaitForSeconds(duration);

        contentPlayer.changeImmortalState(false);

        contentPlayer.removeEffect(this);
    }
}
