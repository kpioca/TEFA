using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageInfo : EnemyInfo
{
    [Header("Spell Pull")]
    [SerializeField] MageSpellInfo[] spellPull;
    public MageSpellInfo[] SpellPull => spellPull;

    public override Enemy createEnemy(GameObject mageObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        return null;
    }
    public override void setAttackEnemyZone(Enemy enemy, BoxCollider attackBox, GameObject attackZoneObj, ContentEnemy contentEnemy)
    {
        attackZoneX = enemy.AttackZoneX;
        attackZoneY = enemy.AttackZoneY;
        attackZoneZ = enemy.AttackZoneZ;

        attackBox.size = new Vector3(attackZoneX, attackZoneY, attackZoneZ);
        AttackMageZone attackMageZone = attackZoneObj.GetComponent<AttackMageZone>();
        contentEnemy.attackMageZone = attackMageZone;
        attackMageZone.game_Manager = contentEnemy.game_Manager;
        attackMageZone.IsAttacked = false;
    }


}
