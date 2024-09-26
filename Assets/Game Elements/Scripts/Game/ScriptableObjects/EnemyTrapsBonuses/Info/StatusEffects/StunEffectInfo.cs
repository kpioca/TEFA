using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StunEffectInfo", menuName = "LevelProperties/Status Effects/New StunEffectInfo")]
public class StunEffectInfo : StatusEffectInfo
{
    [Range(0f, 1f)]
    [SerializeField] private float percentSlowdown = 0.8f;

    private Coroutine effectCoroutine;
    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration)
    {
        duration = durationSec;
        if (effectCoroutine != null)
        {
            gameManager.StopCoroutine(effectCoroutine);

            float startSpeedMove = gameManager.Speed_playerDash;
            float startSpeedJump = gameManager.Speed_playerJump;

            float startHeightMove = gameManager.Height_playerDash;
            float startHeightJump = gameManager.Height_playerJump;

            gameManager.changePlayerMoveParameters(startSpeedMove, startHeightMove, startSpeedJump, startHeightJump);
            gameManager.changeRouteSpeedMovement(gameManager.SpeedRouteMovement);
            //contentPlayer.removeEffect(this);
        }
        effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(gameManager, contentPlayer, duration));
        contentPlayer.applyEffect(this);
    }

    public override IEnumerator EffectCoroutine(GameManager gameManager, ContentPlayer contentPlayer, int duration)
    {
        float startSpeedMove = gameManager.Speed_playerDash;
        float startSpeedJump = gameManager.Speed_playerJump;

        float startHeightMove = gameManager.Height_playerDash;
        float startHeightJump = gameManager.Height_playerJump;

        float speedMove = gameManager.Speed_playerDash;
        float speedJump = gameManager.Speed_playerJump;

        float heightMove = gameManager.Height_playerDash;
        float heightJump = gameManager.Height_playerJump;


        speedMove = startSpeedMove *(1 - percentSlowdown);
        speedJump = startSpeedJump * (1 - percentSlowdown);
        heightMove = startHeightMove * (1 - percentSlowdown);
        heightJump = startHeightJump * (1 - percentSlowdown);

        gameManager.changeRouteSpeedMovement(0);
        gameManager.changePlayerMoveParameters(speedMove, heightMove, speedJump, heightJump);

        yield return new WaitForSeconds((float)duration / 4);

        int n = 4;

        for (int i = 3; i >= 1; i--)
        {
            speedMove = startSpeedMove * (1 - i * percentSlowdown / n);
            speedJump = startSpeedJump * (1 - i * percentSlowdown / n);
            heightMove = startHeightMove * (1 - i * percentSlowdown / n);
            heightJump = startHeightJump * (1 - i * percentSlowdown / n);

            gameManager.changePlayerMoveParameters(speedMove, heightMove, speedJump, heightJump);
            yield return new WaitForSeconds((float)duration / 4);
        }

        gameManager.changePlayerMoveParameters(startSpeedMove, startHeightMove, startSpeedJump, startHeightJump);
        gameManager.changeRouteSpeedMovement(gameManager.SpeedRouteMovement);
        contentPlayer.removeEffect(this);
    }
}
