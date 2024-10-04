using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveCan : Cannon
{
    [Range(1, 5)]
    [SerializeField] private protected int increaseSize_Projectile;
    public int increaseSizeProjectile => increaseSize_Projectile;

    public LoveCan(LoveCanInfo info, GameObject instance, Stamp stamp = null) : base(info, instance, stamp)
    {
        increaseSize_Projectile = info.increaseSizeProjectile;
    }

    public override void Attack(GameObject[] markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines = null)
    {
        GameObject bullet;
        bullet = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun[0], null, stamp, out contentBullet[0]);

        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2 + Vector3.up, bulletsInfo[0], platformsSpeed, contentBullet[0]);
    }

    public override void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, BulletInfo bulletInfo, float platformsSpeed, ContentBullet contentBullet)
    {
        float speedMultiplier = stamp == null ? 1 : stamp.getStampValue();
        toUseCoroutines.StartCoroutine(MovementCoroutine(obj, target, bulletInfo.Speed * speedMultiplier + platformsSpeed, contentBullet));

        //projectile_rb = obj.GetComponent<Rigidbody>();
        //projectile_rb.velocity = obj.transform.forward * (bulletInfo.Speed + platformsSpeed);
    }

    private protected override IEnumerator MovementCoroutine(GameObject obj, Vector3 target, float speed, ContentBullet contentBullet)
    {
        Vector3 start_pos = obj.transform.position;
        Vector3 start_scale = obj.transform.localScale;
        Vector3 scale;
        float runningTime = 0;
        float totalRunningTime = Vector3.Distance(start_pos, target) / speed;
        int k = 0;
        float increasing = 0;

        while (runningTime < totalRunningTime)
        {
            k++;
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);

            if ((int)increasing != increaseSizeProjectile)
            {
                increasing = (float)(1 + (0.03 * k));
                scale = start_scale * (increasing);
                obj.transform.localScale = scale;
            }
            yield return 1;
        }
        obj.transform.localScale = start_scale;
        if (contentBullet.gameObject.activeInHierarchy)
            contentBullet.Remove();
    }
}
