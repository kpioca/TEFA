using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StunEffectInfo", menuName = "LevelProperties/Status Effects/New StunEffectInfo")]
public class StunEffectInfo : StatusEffectInfo
{
    [Range(0f, 1f)]
    [SerializeField] private float percentSlowdown = 0.8f;

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
        //

        startSpeedRoute = gameManager.SpeedRouteMovement;
        float startAnimatorMoveMultiplier = contentPlayer.getAnimatorSpeedMultiplier();

        //change parameters

        speedMove = startSpeedMove * (1 - percentSlowdown);
        heightJump = startHeightJump * (1 - percentSlowdown);
        float animatorMoveMultiplier = startAnimatorMoveMultiplier * (1 - percentSlowdown);
        //

        //calculate decreases
        decreaseSpeedMove = startSpeedMove - speedMove;
        decreaseHeightJump = startHeightJump - heightJump;
        decreaseSpeedRoute = startSpeedRoute;
        float decreaseAnimatorMoveMultiplier = startAnimatorMoveMultiplier - animatorMoveMultiplier;
        //


        //apply effect parameters
        if (startSpeedRoute > 0.000001)
        {
            gameManager.changeRouteSpeedMovement(0);
            gameManager.addDecrementControlValue("speedRoute", effectId, decreaseSpeedRoute);
        }
        gameManager.changePlayerMoveParameters(speedMove, heightJump);
        contentPlayer.setAnimatorSpeedMultiplier(animatorMoveMultiplier);

        gameManager.player_Control.stopFall = true;
        //

        //add decrements to dictionary
        gameManager.addDecrementControlValue("speedMove", effectId, decreaseSpeedMove);
        gameManager.addDecrementControlValue("heightJump", effectId, decreaseHeightJump);
        gameManager.addDecrementControlValue("multiplierAnimator", effectId, decreaseAnimatorMoveMultiplier);
        //

        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration--;
        }


        gameManager.applyLastDecrementControlValues("speedMove", effectId);
        gameManager.applyLastDecrementControlValues("heightJump", effectId);
        gameManager.applyLastDecrementControlValues("speedRoute", effectId);
        gameManager.applyLastDecrementControlValues("multiplierAnimator", effectId);

        decreaseSpeedMove = 0;
        decreaseHeightJump = 0;
        decreaseAnimatorMoveMultiplier = 0;

        contentPlayer.removeEffect(this);
        gameManager.player_Control.stopFall = false;
        effectCoroutine = null;

    }
}
