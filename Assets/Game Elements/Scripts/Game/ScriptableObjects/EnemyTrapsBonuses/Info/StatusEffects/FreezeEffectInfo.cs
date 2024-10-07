using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEffectInfo", menuName = "LevelProperties/Status Effects/New FreezeEffectInfo")]
public class FreezeEffectInfo : StatusEffectInfo
{
    [Range(0f, 1f)]
    [SerializeField] private float percentSlowdown = 0.5f;

    private Coroutine effectCoroutine;

    float startSpeedMove;

    float startHeightJump;
    float startSpeedRoute;

    float decreaseSpeedMove = 0;

    float decreaseHeightJump = 0;
    float decreaseSpeedRoute = 0;

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
        //get current parameters
        gameManager.getCurrentPlayerMoveParameters(out float speedMove, out float heightJump);

        startSpeedMove = speedMove;
        startHeightJump = heightJump;
        startSpeedRoute = gameManager.SpeedRouteMovement;
        //


        float speedRoute = startSpeedRoute;

        //change parameters

        speedMove = speedMove * (1 - percentSlowdown);
        heightJump = heightJump * (1 - percentSlowdown);
        speedRoute = speedRoute * (1 - percentSlowdown);
        //

        //calculate decrements
        decreaseSpeedMove = startSpeedMove - speedMove;
        decreaseHeightJump = startHeightJump - heightJump;
        decreaseSpeedRoute = startSpeedRoute - speedRoute;
        //


        //apply effect parameters
        if (startSpeedRoute > 0.00001)
        {
            gameManager.changeRouteSpeedMovement(speedRoute);
            gameManager.addDecrementControlValue("speedRoute", effectId, decreaseSpeedRoute);
        }
        gameManager.changePlayerMoveParameters(speedMove, heightJump);

        gameManager.player_Control.stopFall = true;
        //

        //add decrements to dictionary
        gameManager.addDecrementControlValue("speedMove", effectId, decreaseSpeedMove);
        gameManager.addDecrementControlValue("heightJump", effectId, decreaseHeightJump);
        //


        while (duration>0)
        {
            yield return new WaitForSeconds(1f);
            duration--;
        }

        gameManager.applyLastDecrementControlValues("speedMove", effectId);
        gameManager.applyLastDecrementControlValues("heightJump", effectId);
        gameManager.applyLastDecrementControlValues("speedRoute", effectId);

        decreaseSpeedMove = 0;
        decreaseHeightJump = 0;
        decreaseSpeedRoute = 0;

        contentPlayer.removeEffect(this);
        gameManager.player_Control.stopFall = false;
        effectCoroutine = null;
    }
}
