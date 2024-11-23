using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReverseEffectInfo", menuName = "LevelProperties/Status Effects/New ReverseEffectInfo")]
public class ReverseEffectInfo : StatusEffectInfo
{

    int duration;
    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration)
    {
        duration = durationSec;
        this.duration = durationSec;

        if (effectCoroutine == null)
        {
            effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(gameManager, contentPlayer));
        }
    }

    public override IEnumerator EffectCoroutine(GameManager gameManager, ContentPlayer contentPlayer)
    {
        contentPlayer.applyEffect(this);

        gameManager.player_Control.ReverseControls(true);


        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration--;
        }

        gameManager.player_Control.ReverseControls(false);

        contentPlayer.removeEffect(this);
        effectCoroutine = null;
    }
}
