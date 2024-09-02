using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StandartDoubleCannon : Cannon
{
    [SerializeField] float intervalBetweenShots = 1f;

    public StandartDoubleCannon(StandartDoubleCannonInfo info, out float intervalBetweenShots, GameObject instance, Stamp stamp = null) : base(info, instance, stamp)
    {
        this.intervalBetweenShots = info.IntervalBetweenShots;
        intervalBetweenShots = this.intervalBetweenShots;
    }

    public override void Attack(GameObject[] markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines)
    {
        toUseCoroutines.StartCoroutine(AttackCoroutine(markGun[0], target, platformsSpeed, toUseCoroutines));
    }

    
    public IEnumerator AttackCoroutine(GameObject markGun, Vector3 target, float platformsSpeed, MonoBehaviour toUseCoroutines)
    {
        GameObject bullet;
        bullet = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun, null, stamp);
        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, bulletsInfo[0], platformsSpeed);

        yield return new WaitForSeconds(intervalBetweenShots);

        bullet = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun, null, stamp);
        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, bulletsInfo[0], platformsSpeed);
    }
}
