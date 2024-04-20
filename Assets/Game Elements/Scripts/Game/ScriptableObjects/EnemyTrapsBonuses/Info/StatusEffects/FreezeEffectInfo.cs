using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEffectInfo", menuName = "LevelProperties/Status Effects/New FreezeEffectInfo")]
public class FreezeEffectInfo : StatusEffectInfo
{
    [Range(0f, 1f)]
    [SerializeField] private float percentSlowdown = 0.5f;

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
            contentPlayer.removeEffect(this);
        }
        effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(gameManager, contentPlayer, duration));
    }

    public override IEnumerator EffectCoroutine(GameManager gameManager, ContentPlayer contentPlayer, int duration)
    {
        float startSpeedMove = gameManager.Speed_playerDash;
        float startSpeedJump = gameManager.Speed_playerJump;

        float startHeightMove = gameManager.Height_playerDash;
        float startHeightJump = gameManager.Height_playerJump;
        float startSpeedRoute = gameManager.SpeedRouteMovement;

        float speedMove;
        float speedJump;

        float heightMove;
        float heightJump;
        float speedRoute;

        speedMove = startSpeedMove *(1 - percentSlowdown);
        speedJump = startSpeedJump * (1 - percentSlowdown);
        heightMove = startHeightMove * (1 - percentSlowdown);
        heightJump = startHeightJump * (1 - percentSlowdown);
        speedRoute = startSpeedRoute * (1 - percentSlowdown);

        gameManager.changeRouteSpeedMovement(speedRoute);
        gameManager.changePlayerMoveParameters(speedMove, heightMove, speedJump, heightJump);

        yield return new WaitForSeconds(duration);

        gameManager.changePlayerMoveParameters(startSpeedMove, startHeightMove, startSpeedJump, startHeightJump);
        gameManager.changeRouteSpeedMovement(startSpeedRoute);
        contentPlayer.removeEffect(this);
    }
}
