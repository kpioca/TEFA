using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyArmorEffectInfo", menuName = "LevelProperties/Status Effects/New DestroyArmorEffectInfo")]
public class DestroyArmorEffectInfo : StatusEffectInfo
{
    private Coroutine effectCoroutine;

    [SerializeField] int defaultDecreaseArmor = 1;
    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration, int[] decreaseArmor = null)
    {
        int decrease;
        if (decreaseArmor == null)
            decrease = defaultDecreaseArmor;
        else decrease = decreaseArmor[0];

        duration = durationSec;
        int armor = contentPlayer.Armor;
        if (armor > 0)
        {
            armor -= decrease;
            if(armor > 0)
                contentPlayer.changeCounterArmor(armor);
            else contentPlayer.changeCounterArmor(0);
        }

        if (effectCoroutine != null)
        {
            contentPlayer.changeImmortalState(false);
            gameManager.StopCoroutine(effectCoroutine);
        }
        effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(contentPlayer, duration));
        contentPlayer.applyEffect(this);

    }

    public IEnumerator EffectCoroutine(ContentPlayer contentPlayer, int duration)
    {
        contentPlayer.changeImmortalState(true);
        yield return new WaitForSeconds(duration);
        contentPlayer.changeImmortalState(false);
        contentPlayer.removeEffect(this);
        effectCoroutine = null;
    }

}
