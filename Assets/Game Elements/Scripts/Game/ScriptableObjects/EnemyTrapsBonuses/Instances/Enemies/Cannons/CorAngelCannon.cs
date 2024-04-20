using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CannonInfo;
using static UnityEngine.Rendering.DebugUI;

public class CorAngelCannon : Cannon
{
    public CorAngelCannon(CorAngelCannonInfo info) : base(info)
    {

    }

    float t;

    public override void Attack(GameObject markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines)
    {
        GameObject bullet;
        bullet = spawnBullet(bulletInfo.Prefab, markGun, null);

        MoveToPos(toUseCoroutines, bullet, target, bulletInfo, platformsSpeed);
    }

    public override void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, BulletInfo bulletInfo, float platformsSpeed)
    {
        toUseCoroutines.StartCoroutine(MovementCoroutine(obj, obj.transform.forward * 20, target, bulletInfo.Speed + platformsSpeed, 25));

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


    public virtual IEnumerator MovementCoroutine(GameObject obj, Vector3 last_pos, Vector3 target, float speed, int n_segments)
    {
        float amplitude = 5; //5
        float frequency = 150/amplitude; //150;


        Vector3 start_pos = obj.transform.position;

        last_pos = new Vector3(last_pos.x, last_pos.y + 5 * 0.2f, last_pos.z);

        Vector3 direction = start_pos - last_pos;

        List<float> distances = new List<float>();
        float distance;
        float runningTime = 0;
        float totalRunningTime = 0;

        int n = n_segments;

        Vector3[] poses = new Vector3[n];

        Vector3 currPos = start_pos;

        int k1 = 2;
        for (int i = 0; i < n; i++)
        {
            if(i > k1)
                poses[i] = new Vector3(start_pos.x + func1(Time.deltaTime, frequency, amplitude, i) - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);
            else if (i <= k1) poses[i] = new Vector3(start_pos.x - (direction.x) * i / n, start_pos.y - (direction.y) * i / n, start_pos.z - (direction.z) * i / n);

            //GameObject obj2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //obj2.transform.position = poses[i];
            distance = Vector3.Distance(currPos, poses[i]);
            distances.Add(distance);
            
            //totalRunningTime += distance / speed;

            currPos = poses[i];

        }

        int min_i = 0;
        float min_range = 100;
        for (int i = 0; i < n; i++)
            if (Vector3.Distance(poses[i], target) <= min_range)
            {
                min_range = Vector3.Distance(poses[i], target);
                min_i = i;
            }

        poses[min_i] = target;
        distances[min_i] = min_range;

        for(int i = 0; i < n; i++)
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

    float func1(float x, float frequency, float amplitude, int iterator)
    {
        return amplitude * Mathf.Sin(frequency * iterator * x);
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
