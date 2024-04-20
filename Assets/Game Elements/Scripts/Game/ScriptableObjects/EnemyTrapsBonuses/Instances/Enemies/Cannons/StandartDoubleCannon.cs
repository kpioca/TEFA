using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StandartDoubleCannon : Cannon
{
    [SerializeField] float intervalBetweenShots = 1f;

    public StandartDoubleCannon(StandartDoubleCannonInfo info, out float intervalBetweenShots) : base(info)
    {
        this.intervalBetweenShots = info.IntervalBetweenShots;
        intervalBetweenShots = this.intervalBetweenShots;
    }

    public override void Attack(GameObject markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines)
    {
        toUseCoroutines.StartCoroutine(AttackCoroutine(markGun, target, platformsSpeed, toUseCoroutines));
    }

    
    public IEnumerator AttackCoroutine(GameObject markGun, Vector3 target, float platformsSpeed, MonoBehaviour toUseCoroutines)
    {
        GameObject bullet;
        bullet = spawnBullet(bulletInfo.Prefab, markGun, null);
        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, bulletInfo, platformsSpeed);

        yield return new WaitForSeconds(intervalBetweenShots);

        bullet = spawnBullet(bulletInfo.Prefab, markGun, null);
        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, bulletInfo, platformsSpeed);
    }
}
