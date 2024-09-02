using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CannonInfo;
using static UnityEngine.Rendering.DebugUI;

public class StunAngelCannon : Cannon
{
    public StunAngelCannon(StunAngelCannonInfo info, GameObject instance, Stamp stamp = null) : base(info, instance, stamp)
    {

    }

    float t;

    public override void Attack(GameObject[] markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines)
    {
        GameObject bullet;
        bullet = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun[0], null, stamp);

        MoveToPos(toUseCoroutines, bullet, target, bulletsInfo[0], platformsSpeed);
    }

    public override void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, BulletInfo bulletInfo, float platformsSpeed)
    {
        float speedMultiplier = stamp == null ? 1 : stamp.getStampValue();
        toUseCoroutines.StartCoroutine(MovementCoroutine(obj, target, bulletInfo.Speed * speedMultiplier + platformsSpeed, 10));

        //projectile_rb = obj.GetComponent<Rigidbody>();
        //projectile_rb.velocity = obj.transform.forward * (bulletInfo.Speed + platformsSpeed);
    }

    //public virtual IEnumerator MovementCoroutine(GameObject obj, Vector3 target, float speed, int n_segments)
    //{
    //    Vector3 start_pos = obj.transform.position;

    //    target = new Vector3(target.x, target.y + 5 * 0.2f, target.z);

    //    float runningTime = 0;
    //    float totalRunningTime = 0;
    //    Vector3 direction = start_pos - target;

    //    int n = n_segments;

    //    Vector3[] poses = new Vector3[n];
    //    Vector3 nextPos;
    //    Quaternion rotation;

    //    for (int i = 0; i < n; i++)
    //    {
    //        poses[i] = new Vector3(start_pos.x + 0.8f * Mathf.Sin(100 * i * Time.deltaTime) - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);
    //    }

    //    for (int i = 0; i < n; i++)
    //    {
    //        if (i != n - 1)
    //            nextPos = poses[i];
    //        else nextPos = target;

    //        totalRunningTime = Vector3.Distance(start_pos, nextPos) / speed;

    //        direction = start_pos - nextPos;
    //        rotation = Quaternion.LookRotation(direction);

    //        obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, rotation, speed * Time.deltaTime);

    //        while (runningTime < totalRunningTime)
    //        {
    //            runningTime += Time.deltaTime;
    //            obj.transform.localPosition = Vector3.Lerp(start_pos, nextPos, runningTime / totalRunningTime);
    //            yield return 1;
    //        }
    //        start_pos = nextPos;
    //        runningTime = 0;

    //    }
    //}


    public virtual IEnumerator MovementCoroutine(GameObject obj,Vector3 target, float speed, int n_segments, float yMaxOffset = 10)
    {


        Vector3 start_pos = obj.transform.position;
        Vector3 last_pos;

        last_pos = new Vector3(target.x, -1, target.z);

        Vector3 direction = start_pos - target;

        List<float> distances = new List<float>();
        float distance;
        float runningTime = 0;
        float totalRunningTime = 0;

        int n = n_segments;

        Vector3[] poses = new Vector3[n];

        Vector3 currPos = start_pos;

        int k1 = n/2 + 2;

        for (int i = 0; i < n; i++)
        {
            if (i == k1)
                poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y + yMaxOffset - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);
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
