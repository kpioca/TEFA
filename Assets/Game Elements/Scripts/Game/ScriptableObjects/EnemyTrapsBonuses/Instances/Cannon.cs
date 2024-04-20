using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static CannonInfo;
using static UnityEngine.GraphicsBuffer;

public class Cannon : Enemy
{

    [Header("Projectile Reference")]
    [SerializeField] private BulletInfo bullet_Info;
    public BulletInfo bulletInfo => bullet_Info;

    [Header("Projectile Pooling")]
    [SerializeField] private protected int sizeOfProjectilePool = 12;
    public int SizeOfProjectilePool => sizeOfProjectilePool;


    private protected Rigidbody projectile_rb;

    public Cannon(CannonInfo info) : base(info)
    {
        this.bullet_Info = info.bulletInfo;
        this.sizeOfProjectilePool = info.SizeOfProjectilePool;

    }

    public virtual void Attack(GameObject markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines = null)
    {
        GameObject bullet;
        bullet = spawnBullet(bulletInfo.Prefab, markGun, null);

        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, bulletInfo, platformsSpeed);
    }

    public virtual void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, BulletInfo bulletInfo, float platformsSpeed)
    {
        toUseCoroutines.StartCoroutine(MovementCoroutine(obj, target, bulletInfo.Speed + platformsSpeed));

        //projectile_rb = obj.GetComponent<Rigidbody>();
        //projectile_rb.velocity = obj.transform.forward * (bulletInfo.Speed + platformsSpeed);
    }

    private protected virtual IEnumerator MovementCoroutine(GameObject obj, Vector3 target, float speed)
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
    }


    private protected GameObject spawnBullet(GameObject prefab, GameObject markGun, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        temp.transform.position = markGun.transform.position;
        temp.transform.rotation = markGun.transform.rotation;
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }
}
