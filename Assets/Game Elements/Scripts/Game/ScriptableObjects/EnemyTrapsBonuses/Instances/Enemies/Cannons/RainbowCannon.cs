using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RainbowCannon : Cannon
{

    [SerializeField] float intervalBetweenShots = 0.5f;
    public float IntervalBetweenShots => intervalBetweenShots;

    //private GameObject charge1;
    private GameObject charge2;

    private int n_charges = -1;

    public RainbowCannon(RainbowCannonInfo info, GameObject charge2, out float intervalBetweenShots, out int n_shots, GameObject instance, Stamp stamp = null) : base(info,instance, stamp)
    {
        intervalBetweenShots = info.IntervalBetweenShots;
        this.charge2 = charge2;
        intervalBetweenShots = this.intervalBetweenShots;
        setUpElement(charge2, out n_charges);
        n_shots = n_charges;
    }

    public void setUpElement(GameObject charge2, out int n_charges)
    {
        int k = Random.Range(1, 3);
        charge2.SetActive(false);
        if (k == 2)
            charge2.SetActive(true);
        n_charges = k;

        //Debug.Log($"INIT {n_charges} : charge2 : {charge2.activeInHierarchy}");

    }

    public void setUpElement(GameObject charge2, int n_charges)
    {
        charge2.SetActive(false);
        if (n_charges == 2)
            charge2.SetActive(true);
    }

    public void setUpNumCharges(GameObject charge2, out int n_charges)
    {
        if (charge2.activeInHierarchy == true)
            n_charges = 2;
        else n_charges = 1;

    }

    private List<BulletInfo> getRandomBulletsFromListBullets(List<BulletInfo> list, int amount)
    {
        List<BulletInfo> all_projectiles = new List<BulletInfo>(list);

        List<BulletInfo> sorted_projectiles = new List<BulletInfo>();

        int n = list.Count;
        int k = 0;

        for(int i = 0; i < amount; i++)
        {
            n = all_projectiles.Count;
            k = Random.Range(0, n);

            sorted_projectiles.Add(all_projectiles[k]);
        }

        sorted_projectiles.Sort((a, b) => b.Speed.CompareTo(a.Speed));

        return sorted_projectiles;
    }



    public override void Attack(GameObject[] markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines)
    {
        List<BulletInfo> projectiles = new List<BulletInfo>();

        setUpNumCharges(charge2, out n_charges);

        projectiles = getRandomBulletsFromListBullets(new List<BulletInfo>(bulletsInfo), n_charges);

        toUseCoroutines.StartCoroutine(AttackCoroutine(markGun[0], target, platformsSpeed, projectiles, n_charges, toUseCoroutines));
    }

    private IEnumerator AttackCoroutine(GameObject markGun, Vector3 target, float platformsSpeed, List<BulletInfo> projectiles, int n_charges, MonoBehaviour toUseCoroutines)
    {
        GameObject bullet;

        for(int i = 0; i < n_charges; i++)
        {
            bullet = projectiles[i].spawnBullet(projectiles[i].Prefab, markGun, null, stamp);
            MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, projectiles[i].Speed, platformsSpeed);

            yield return new WaitForSeconds(intervalBetweenShots);
        }
       
    }

    public virtual void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, float speed, float platformsSpeed)
    {
        float speedMultiplier = stamp == null ? 1 : stamp.getStampValue();
        toUseCoroutines.StartCoroutine(MovementCoroutine(obj, target, speed * speedMultiplier + platformsSpeed));

        //projectile_rb = obj.GetComponent<Rigidbody>();
        //projectile_rb.velocity = obj.transform.forward * (bulletInfo.Speed + platformsSpeed);
    }

}
