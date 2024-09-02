using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CannonInfo;
using static UnityEngine.GraphicsBuffer;

public class CarrotCan : Cannon
{
    [Range(0f, 1f)]
    [SerializeField] private protected float chance_bulletCollapse;
    public float chance_bullet_Collapse => chance_bulletCollapse;

    public CarrotCan(CarrotCanInfo info, GameObject instance, Stamp stamp = null) : base(info, instance, stamp)
    {
        chance_bulletCollapse = info.chance_bullet_Collapse;
    }

    float t;

    public override void Attack(GameObject[] markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines)
    {
        GameObject bullet1, bullet2;
        bullet1 = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun[0], null, stamp);
        bullet2 = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun[1], null, stamp);

        MoveToPos(toUseCoroutines, bullet1, bullet2, target + (bullet1.transform.forward + bullet2.transform.forward)/2 + (Vector3.up) + Vector3.back, bulletsInfo[0], bulletsInfo[1], platformsSpeed);
    }

    public void MoveToPos(MonoBehaviour toUseCoroutines, GameObject bullet1, GameObject bullet2, Vector3 target, BulletInfo bulletInfo, BulletInfo extra_bulletInfo, float platformsSpeed)
    {
        float speedMultiplier = stamp == null ? 1 : stamp.getStampValue();
        toUseCoroutines.StartCoroutine(MovementCoroutine(bullet1, bullet2, target, bulletInfo.Speed * speedMultiplier + platformsSpeed, extra_bulletInfo.Speed * speedMultiplier + platformsSpeed, extra_bulletInfo, toUseCoroutines));
        
    }


    public virtual IEnumerator MovementCoroutine(GameObject bulletMain1, GameObject bulletMain2, Vector3 target, float speedMain, float speedExtra, BulletInfo extra_bulletInfo, MonoBehaviour toUseCoroutines)
    {

        Vector3 start_pos1 = bulletMain1.transform.position;
        Vector3 start_pos2 = bulletMain2.transform.position;
        Vector3 target1 = new Vector3(target.x - 0.5f, target.y, target.z);
        Vector3 target2 = new Vector3(target.x + 0.5f, target.y, target.z);

        Vector3 pos1, pos11, pos12, pos13;
        Quaternion rotation1;
        Vector3 pos2, pos21, pos22, pos23;
        Quaternion rotation2;
        bool isActiveBullet1 = true;
        bool isActiveBullet2 = true;

        float runningTime = 0;
        float totalRunningTime = Mathf.Max(Vector3.Distance(start_pos1, target1) / speedMain, Vector3.Distance(start_pos2, target2) / speedMain);

        bool isBulletsCollapsed = probabilityFunc(chance_bulletCollapse);

        if (!isBulletsCollapsed)
        {
            while (runningTime < totalRunningTime)
            {
                runningTime += Time.deltaTime;

                if(bulletMain1.activeInHierarchy == false)
                    isActiveBullet1 = false;
                if(bulletMain2.activeInHierarchy == false)
                    isActiveBullet2 = false;

                if(isActiveBullet1)
                    bulletMain1.transform.position = Vector3.Lerp(start_pos1, target1, runningTime / totalRunningTime);
                if(isActiveBullet2)
                    bulletMain2.transform.position = Vector3.Lerp(start_pos2, target2, runningTime / totalRunningTime);
                yield return 1;
            }
        }
        else
        {
            GameObject extraBullet1_1, extraBullet1_2, extraBullet1_3;
            GameObject extraBullet2_1, extraBullet2_2, extraBullet2_3;

            while (runningTime < totalRunningTime/3)
            {
                runningTime += Time.deltaTime;

                if (bulletMain1.activeInHierarchy == false)
                    isActiveBullet1 = false;
                if (bulletMain2.activeInHierarchy == false)
                    isActiveBullet2 = false;

                if (isActiveBullet1)
                    bulletMain1.transform.position = Vector3.Lerp(start_pos1, target1, runningTime / totalRunningTime);
                if (isActiveBullet2)
                    bulletMain2.transform.position = Vector3.Lerp(start_pos2, target2, runningTime / totalRunningTime);
                yield return 1;
            }

            if (isActiveBullet1 || isActiveBullet2)
            {
                if (isActiveBullet1)
                {
                    pos1 = bulletMain1.transform.position;
                    pos11 = pos1 + Vector3.up;
                    pos12 = pos1 + Vector3.right;
                    pos13 = pos1 + Vector3.left;

                    rotation1 = bulletMain1.transform.rotation;

                    KhtPool.ReturnObject(bulletMain1);

                    extraBullet1_1 = bulletsInfo[1].spawnBullet(extra_bulletInfo.Prefab, pos11, rotation1, null, stamp);
                    extraBullet1_2 = bulletsInfo[1].spawnBullet(extra_bulletInfo.Prefab, pos12, rotation1, null, stamp);
                    extraBullet1_3 = bulletsInfo[1].spawnBullet(extra_bulletInfo.Prefab, pos13, rotation1, null, stamp);


                    toUseCoroutines.StartCoroutine(MovementExtraCoroutine(extraBullet1_1, target, speedExtra, Vector3.up));
                    toUseCoroutines.StartCoroutine(MovementExtraCoroutine(extraBullet1_2, target, speedExtra, Vector3.right));
                    toUseCoroutines.StartCoroutine(MovementExtraCoroutine(extraBullet1_3, target, speedExtra, Vector3.left));

                }
                if (isActiveBullet2)
                {
                    pos2 = bulletMain2.transform.position;
                    pos21 = pos2 + Vector3.up;
                    pos22 = pos2 + Vector3.left;
                    pos23 = pos2 + Vector3.right;

                    rotation2 = bulletMain2.transform.rotation;

                    KhtPool.ReturnObject(bulletMain2);

                    extraBullet2_1 = bulletsInfo[1].spawnBullet(extra_bulletInfo.Prefab, pos21, rotation2, null, stamp);
                    extraBullet2_2 = bulletsInfo[1].spawnBullet(extra_bulletInfo.Prefab, pos22, rotation2, null, stamp);
                    extraBullet2_3 = bulletsInfo[1].spawnBullet(extra_bulletInfo.Prefab, pos23, rotation2, null, stamp);


                    toUseCoroutines.StartCoroutine(MovementExtraCoroutine(extraBullet2_1, target, speedExtra, Vector3.up));
                    toUseCoroutines.StartCoroutine(MovementExtraCoroutine(extraBullet2_2, target, speedExtra, Vector3.left));
                    toUseCoroutines.StartCoroutine(MovementExtraCoroutine(extraBullet2_3, target, speedExtra, Vector3.right));
                }
            }
        }
    }

    private bool probabilityFunc(float chance) //return true if event happened, else - false
    {
        if (UnityEngine.Random.value <= chance) return true;
        else return false;
    }

    private IEnumerator MovementExtraCoroutine(GameObject obj, Vector3 target, float speed, Vector3 dirToCurvature, float MaxOffset = 5, int n_segments = 10)
    {
        Vector3 start_pos = obj.transform.position;
        Vector3 last_pos;

        last_pos = new Vector3(target.x, -1, target.z+2);

        Vector3 direction = start_pos - target;

        List<float> distances = new List<float>();
        float distance;
        float runningTime = 0;
        float totalRunningTime = 0;

        int n = n_segments;

        Vector3[] poses = new Vector3[n];

        Vector3 currPos = start_pos;

        int k1 = n / 2 + 2;

        if (dirToCurvature == Vector3.up)
        {
            for (int i = 0; i < n; i++)
            {
                if (i == k1)
                    poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y + MaxOffset - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);
                else if (i < k1) poses[i] = new Vector3(start_pos.x - (direction.x) * i / (n * 1.5f), start_pos.y - (direction.y) * i / (n * 1.5f), start_pos.z - (direction.z) * i / (n * 1.5f));
                else if (i == n - 1)
                    poses[i] = last_pos;
                else if (i == n - 2)
                    poses[i] = new Vector3(last_pos.x, target.y, last_pos.z);
                else if (i == n - 3)
                    poses[i] = new Vector3(last_pos.x, target.y + 0.6f, last_pos.z);

                else poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);


                distance = Vector3.Distance(currPos, poses[i]);
                distances.Add(distance);

                currPos = poses[i];

            }
        }
        else if (dirToCurvature == Vector3.down)
        {
            for (int i = 0; i < n; i++)
            {
                if (i == k1)
                    poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y - MaxOffset - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);
                else if (i < k1) poses[i] = new Vector3(start_pos.x - (direction.x) * i / (n * 1.5f), start_pos.y - (direction.y) * i / (n * 1.5f), start_pos.z - (direction.z) * i / (n * 1.5f));
                else if (i == n - 1)
                    poses[i] = last_pos;
                else if (i == n - 2)
                    poses[i] = new Vector3(last_pos.x, target.y, last_pos.z);
                else if (i == n - 3)
                    poses[i] = new Vector3(last_pos.x, target.y + 0.6f, last_pos.z);

                else poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);


                distance = Vector3.Distance(currPos, poses[i]);
                distances.Add(distance);

                currPos = poses[i];

            }
        }
        else if(dirToCurvature == Vector3.right)
        {
            for (int i = 0; i < n; i++)
            {
                if (i == k1)
                    poses[i] = new Vector3(start_pos.x - MaxOffset - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);
                else if (i < k1) poses[i] = new Vector3(start_pos.x - (direction.x) * i / (n * 1.5f), start_pos.y - (direction.y) * i / (n * 1.5f), start_pos.z - (direction.z) * i / (n * 1.5f));
                else if (i == n - 1)
                    poses[i] = last_pos;
                else if (i == n - 2)
                    poses[i] = new Vector3(last_pos.x, target.y, last_pos.z);
                else if (i == n - 3)
                    poses[i] = new Vector3(last_pos.x, target.y + 0.6f, last_pos.z);

                else poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);


                distance = Vector3.Distance(currPos, poses[i]);
                distances.Add(distance);

                currPos = poses[i];

            }
        }
        else if (dirToCurvature == Vector3.left)
        {
            for (int i = 0; i < n; i++)
            {
                if (i == k1)
                    poses[i] = new Vector3(start_pos.x + MaxOffset - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);
                else if (i < k1) poses[i] = new Vector3(start_pos.x - (direction.x) * i / (n * 1.5f), start_pos.y - (direction.y) * i / (n * 1.5f), start_pos.z - (direction.z) * i / (n * 1.5f));
                else if (i == n - 1)
                    poses[i] = last_pos;
                else if (i == n - 2)
                    poses[i] = new Vector3(last_pos.x, target.y, last_pos.z);
                else if (i == n - 3)
                    poses[i] = new Vector3(last_pos.x, target.y + 0.6f, last_pos.z);

                else poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);


                distance = Vector3.Distance(currPos, poses[i]);
                distances.Add(distance);

                currPos = poses[i];

            }
        }

        for (int i = 0; i < n; i++)
            totalRunningTime += distances[i] / speed;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            t = runningTime / totalRunningTime;
            LerpLine3(poses, obj, speed);
            //obj.transform.localPosition = Vector3.Lerp(start_pos, nextPos, runningTime / totalRunningTime);
            yield return 1;
        }
        runningTime = 0;
    }

    void LerpLine3(Vector3[] poses, GameObject obj, float speed)
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 1; i < poses.Length - 1; i++)
        {
            list.Add(Vector3.Lerp(poses[i], poses[i + 1], t));
        }
        Lerp2Line3(list, obj, speed);
    }
    void Lerp2Line3(List<Vector3> list2, GameObject obj, float speed)
    {
        Vector3 direction;
        Quaternion rotation;
        if (list2.Count > 2)
        {
            List<Vector3> list = new List<Vector3>();
            for (int i = 0; i < list2.Count - 1; i++)
            {
                list.Add(Vector3.Lerp(list2[i], list2[i + 1], t));
            }
            Lerp2Line3(list, obj, speed);
        }
        else
        {
            obj.transform.position = Vector3.Lerp(list2[0], list2[1], t);
            direction = list2[0] - list2[1];
            rotation = Quaternion.LookRotation(direction);
            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, rotation, speed * Time.deltaTime);
        }
    }
}
