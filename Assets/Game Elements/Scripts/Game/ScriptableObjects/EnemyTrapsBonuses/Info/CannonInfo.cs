using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonInfo : EnemyInfo
{

    [Header("Projectile Reference")]
    [SerializeField] private protected BulletInfo[] bullets_Info;
    public BulletInfo[] bulletsInfo => bullets_Info;

    [Header("Projectile Pooling")]
    [SerializeField] private protected int sizeOfProjectilePool = 12;
    public int SizeOfProjectilePool => sizeOfProjectilePool;

    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        return null;
    }

    public override void setEnemyInContent(GameObject cannonObject, Stamp stamp, ContentEnemy contentEnemy)
    {
    }

    public override void setAttackEnemyZone(Enemy enemy, BoxCollider attackBox, GameObject attackZoneObj, ContentEnemy contentEnemy)
    {
        attackZoneX = enemy.AttackZoneX;
        attackZoneY = enemy.AttackZoneY;
        attackZoneZ = enemy.AttackZoneZ;

        attackBox.size = new Vector3(attackZoneX, attackZoneY, attackZoneZ);
        AttackZone attackZone = attackZoneObj.GetComponent<AttackZone>();
        contentEnemy.attackZone = attackZone;
        attackZone.game_Manager = contentEnemy.game_Manager;
        attackZone.IsAttacked = false;
    }


}
