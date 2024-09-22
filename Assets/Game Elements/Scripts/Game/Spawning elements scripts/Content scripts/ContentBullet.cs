using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ContentBullet : MonoBehaviour, IRemovable
{
    [SerializeField] private BulletInfo bullet_Info;
    public BulletInfo bulletInfo => bullet_Info;

    public Bullet bulletInstance { get; set; }

    [SerializeField] private GameObject bulletMainObject;
    public GameObject BulletMainObject => bulletMainObject;

    public void Remove()
    {
        StartCoroutine(RemoveCoroutine());
    }

    public IEnumerator RemoveCoroutine()
    {
        ContentBullet contentBullet = gameObject.GetComponent<ContentBullet>();
        contentBullet.bulletInstance = null;

        Transform transform = gameObject.transform;
        Vector3 startScale = gameObject.transform.localScale;
        Vector3 endScale = new Vector3(0, 0, 0);

        float runningTime = 0;
        float totalRunningTime = 0.25f;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, runningTime / totalRunningTime);
            yield return 1;
        }

        transform.localScale = contentBullet.bulletInfo.Prefab.transform.localScale;
        transform.position = new Vector3(100, 0, 0);
        KhtPool.ReturnObject(gameObject);
    }

}
