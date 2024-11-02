using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyArmorEffectInfo", menuName = "LevelProperties/Status Effects/New DestroyArmorEffectInfo")]
public class DestroyArmorEffectInfo : StatusEffectInfo
{

    [SerializeField] int defaultDecreaseArmor = 1;

    int duration;

    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration)
    {
        duration = durationSec;
        this.duration = duration;

        if (effectCoroutine == null)
        {
            int armor = contentPlayer.Armor;
            if (armor > 0)
            {
                armor -= defaultDecreaseArmor;
                if (armor > 0)
                    contentPlayer.changeCounterArmor(armor);
                else contentPlayer.changeCounterArmor(0);

                effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(contentPlayer));
                contentPlayer.applyEffect(this);
            }
        }

    }
    public override void ApplyEffect(GameManager gameManager, ContentPlayer contentPlayer, out int duration, int[] decreaseArmor = null)
    {
        duration = durationSec;
        this.duration = duration;

        if (effectCoroutine == null)
        {
            int decrease;
            if (decreaseArmor == null)
                decrease = defaultDecreaseArmor;
            else decrease = decreaseArmor[0];


            int armor = contentPlayer.Armor;
            if (armor > 0)
            {
                armor -= decrease;
                if (armor > 0)
                    contentPlayer.changeCounterArmor(armor);
                else contentPlayer.changeCounterArmor(0);
            }

            effectCoroutine = gameManager.StartCoroutine(EffectCoroutine(contentPlayer));
        }

    }

    public IEnumerator EffectCoroutine(ContentPlayer contentPlayer)
    {
        contentPlayer.applyEffect(this);

        contentPlayer.addImmortalEffectToDictionary(this);
        contentPlayer.changeImmortalState(true);


        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration--;
        }

        contentPlayer.removeImmortalEffectFromDictionary(this);
        contentPlayer.changeImmortalState(false);

        contentPlayer.removeEffect(this);
        effectCoroutine = null;
    }

}
