using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyStandartCan : Cannon
{
    [SerializeField] private float spinningSpeed = 10;
    public float SpinningSpeed => spinningSpeed;

    [SerializeField] private float smooth = 0.05f;
    public float Smooth => smooth;

    public FlyStandartCan(FlyStandartCanInfo info, GameObject instance, Stamp stamp = null) : base(info, instance, stamp)
    {
        spinningSpeed = info.SpinningSpeed;
        smooth = info.Smooth;
    }

    public override void Attack(GameObject[] markGun, float platformsSpeed, Vector3 target, MonoBehaviour toUseCoroutines)
    {
        GameObject bullet;
        bullet = bulletsInfo[0].spawnBullet(bulletsInfo[0].Prefab, markGun[0], null, stamp, out contentBullet[0]);

        MoveToPos(toUseCoroutines, bullet, target + bullet.transform.forward * 2, bulletsInfo[0], platformsSpeed, contentBullet[0]);
    }

    public override void MoveToPos(MonoBehaviour toUseCoroutines, GameObject obj, Vector3 target, BulletInfo bulletInfo, float platformsSpeed, ContentBullet contentBullet)
    {
        float speedMultiplier = stamp == null ? 1 : stamp.getStampValue();
        toUseCoroutines.StartCoroutine(MovementCoroutine(obj, target, bulletInfo.Speed * speedMultiplier + platformsSpeed, toUseCoroutines, contentBullet));
    }

    private protected IEnumerator MovementCoroutine(GameObject obj, Vector3 target, float speed, MonoBehaviour toUseCoroutines, ContentBullet contentBullet)
    {
        Coroutine spiningCoroutine = toUseCoroutines.StartCoroutine(SpinningCoroutine(obj, spinningSpeed, smooth));
        Vector3 start_pos = obj.transform.position;

        float runningTime = 0;
        float totalRunningTime = Vector3.Distance(start_pos, target) / speed;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 1;
        }
        toUseCoroutines.StopCoroutine(spiningCoroutine);
        if (contentBullet.gameObject.activeInHierarchy)
            contentBullet.Remove();
    }

    private IEnumerator SpinningCoroutine(GameObject bullet, float speed = 10, float smooth = 0.05f)
    {
        while (true)
        {
            //currentAngle = bullet.transform.rotation.eulerAngles.y;
            bullet.transform.eulerAngles += new Vector3(speed, 0.0f, 0.0f);
            //bullet.transform.eulerAngles -= new Vector3(speed/2f, 0.0f, 0.0f);
            yield return new WaitForSeconds(smooth);
        }
    }
}
