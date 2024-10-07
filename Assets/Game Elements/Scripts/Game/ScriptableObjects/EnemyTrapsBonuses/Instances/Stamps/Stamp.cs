using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp
{
    [SerializeField] private protected Material enemy_Material;
    public Material enemyMaterial => enemy_Material;

    [SerializeField] private protected Material bullet_Material;
    public Material bulletMaterial => bullet_Material;

    [SerializeField] private protected float fishMultiplier = 0;
    public float FishMultiplier => fishMultiplier;

    [Header("Instance Parameters")]
    [SerializeField] private protected CannonInfo cannonInfo_;
    public CannonInfo cannonInfo
    {
        get { return cannonInfo_; }
        set {  cannonInfo_ = value; }
    }
    public Stamp(StampInfo info)
    {
        enemy_Material = info.enemyMaterial;
        bullet_Material = info.bulletMaterial;
        fishMultiplier = info.Multiplier;
    }

    public static Stamp createStampFromInfo(StampInfo stampInfo)
    {
        switch (stampInfo)
        {
            case DamageStampInfo:
                return new DamageStamp((DamageStampInfo)stampInfo);
            case SpeedStampInfo:
                return new SpeedStamp((SpeedStampInfo)stampInfo);
            case SizeStampInfo:
                return new SizeStamp((SizeStampInfo)stampInfo);
            case RainbowStampInfo:
                return new RainbowStamp((RainbowStampInfo)stampInfo);
            default: 
                return null;
        }
    }

    public virtual void applyStampEffectOnCannon(Cannon cannon)
    {

    }

    public virtual float getStampValue(float defaultValue = 1)
    {
        return defaultValue;
    }

    public virtual GameObject spawnBulletWithStamp(GameObject prefab, GameObject markGun, Transform parent, BulletInfo bulletInfo, out ContentBullet contentBullet)
    {
        contentBullet = null;
        return null;
    }

    public virtual GameObject spawnBulletWithStamp(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent, BulletInfo bulletInfo, out ContentBullet contentBullet)
    {
        contentBullet = null;
        return null;
    }

}
