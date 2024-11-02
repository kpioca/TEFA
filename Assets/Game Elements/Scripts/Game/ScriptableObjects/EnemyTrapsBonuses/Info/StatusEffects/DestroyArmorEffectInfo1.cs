using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpEffectInfo", menuName = "LevelProperties/Status Effects/New JumpEffectInfo")]
public class JumpEffectInfo : StatusEffectInfo
{


    [SerializeField] float speed_jump;
    [SerializeField] float speed_jumpFall;
    [SerializeField] float height_jump;
    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration)
    {
        duration = 1;

        gameManager.player_Control.activateJumpEffect(speed_jump, speed_jumpFall, height_jump);
        if (effectCoroutine != null)
        {
            gameManager.StopCoroutine(effectCoroutine);
        }
        effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(contentPlayer, duration));

    }

    public IEnumerator EffectCoroutine(ContentPlayer contentPlayer, int duration)
    {
        contentPlayer.applyEffect(this);
        yield return new WaitForSeconds(duration);
        contentPlayer.removeEffect(this);
        effectCoroutine = null;
    }

}
