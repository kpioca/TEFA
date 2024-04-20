using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Immortality_Properties", menuName = "LevelProperties/Bonuses/New Immortality_Properties")]
public class ImmortalityInfo : BonusInfo
{
    [SerializeField] StatusEffectInfo effectInfo;
    public override void Action(GameManager gameManager, ContentPlayer contentPlayer)
    {
        contentPlayer.effectActivate(effectInfo);
    }
}
