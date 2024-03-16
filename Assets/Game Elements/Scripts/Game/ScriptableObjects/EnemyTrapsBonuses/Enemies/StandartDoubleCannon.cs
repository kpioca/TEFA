using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandartDoubleCannon_Properties", menuName = "LevelProperties/Enemy/New StandartDoubleCannon_Properties")]
public class StandartDoubleCannon : Cannon
{
    [SerializeField] float intervalBetweenShots = 1f;

    public override void Attack(GameObject markGun, float platformsSpeed, MonoBehaviour toUseCoroutines, int parameter = 0)
    {
        toUseCoroutines.StartCoroutine(AttackCoroutine(markGun, platformsSpeed));
    }
    public void test()
    {
        Debug.Log("pizda");
    }

    public IEnumerator AttackCoroutine(GameObject markGun, float platformsSpeed)
    {
        GameObject bullet;

        bullet = spawnBullet(prefabProjectile, markGun, null);
        projectile_rb = bullet.GetComponent<Rigidbody>();
        projectile_rb.velocity = bullet.transform.forward * (projectilePower + platformsSpeed);

        yield return new WaitForSeconds(intervalBetweenShots);

        bullet = spawnBullet(prefabProjectile, markGun, null);
        projectile_rb = bullet.GetComponent<Rigidbody>();
        projectile_rb.velocity = bullet.transform.forward * (projectilePower + platformsSpeed);
    }
}
