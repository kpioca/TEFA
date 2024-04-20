using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor_Properties", menuName = "LevelProperties/Bonuses/New Armor_Properties")]
public class ArmorInfo : BonusInfo
{
    [SerializeField] StatusEffectInfo effectInfo;

    [Header("Bonus parameters")]
    [SerializeField] int max_armor = 3;
    [SerializeField] int armor_value = 1;
    public override void Action(GameManager gameManager, ContentPlayer contentPlayer)
    {
        if (contentPlayer.Armor < max_armor)
        {
            contentPlayer.Armor += armor_value;
            contentPlayer.changeCounterArmor(contentPlayer.Armor);
        }
    }
}
