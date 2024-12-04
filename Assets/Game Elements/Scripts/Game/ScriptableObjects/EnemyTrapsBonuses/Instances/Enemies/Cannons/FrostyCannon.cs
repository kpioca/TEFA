using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostyCannon : Cannon
{
    public FrostyCannon(FrostyCannonInfo info, GameObject instance, Stamp stamp = null) : base(info, instance, stamp)
    {
    }

    public override void Attack(GameObject[] markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines = null)
    {
        GameObject bullet;
        bullet = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun[0], null, stamp, out contentBullet[0]);

        //
        //bullet.transform.parent = instance.transform;
        //platformsSpeed = 6;
        //

        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2 + Vector3.up, bulletsInfo[0], platformsSpeed, contentBullet[0]);
    }

    public override void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, BulletInfo bulletInfo, float platformsSpeed, ContentBullet contentBullet)
    {
        float speedMultiplier = stamp == null ? 1 : stamp.getStampValue();
        contentBullet.SetBulletCoroutine(toUseCoroutines.StartCoroutine(MovementCoroutine(obj, target, bulletInfo.Speed * speedMultiplier + platformsSpeed, contentBullet)), toUseCoroutines);

        //projectile_rb = obj.GetComponent<Rigidbody>();
        //projectile_rb.velocity = obj.transform.forward * (bulletInfo.Speed + platformsSpeed);
    }

    private protected override IEnumerator MovementCoroutine(GameObject obj, Vector3 target, float speed, ContentBullet contentBullet)
    {
        Vector3 start_pos = obj.transform.position;

        float runningTime = 0;
        float totalRunningTime = Vector3.Distance(start_pos, target) / speed;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 1;
        }
        if (contentBullet.gameObject.activeInHierarchy)
            contentBullet.Remove();
    }
}
