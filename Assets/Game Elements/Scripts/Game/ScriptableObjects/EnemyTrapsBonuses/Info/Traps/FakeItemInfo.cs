using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FakeItem_Properties", menuName = "LevelProperties/Traps/New FakeItem_Properties")]
public class FakeItem : TrapInfo
{
    [Header("Features")]
    [SerializeField] StatusEffectInfo[] effectsForUse;
    [SerializeField] bool isUsedRandomEffect;

    public override StatusEffectInfo EffectInfo
    {
        get {
            if (isUsedRandomEffect)
                return getRandomEffect(effectsForUse);
            else return effectInfo;
            }
    }

    StatusEffectInfo getRandomEffect(StatusEffectInfo[] effects)
    {
        int n = effects.Length;
        int k = Random.Range(0, n);

        return effects[k];
    }
}
