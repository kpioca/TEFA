using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverBullets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
            despawnBullet(other.gameObject);
    }

    private void despawnBullet(GameObject gameObject)
    {
        ContentBullet contentBullet = gameObject.GetComponent<ContentBullet>();
        gameObject.transform.localScale = contentBullet.bulletInfo.Prefab.transform.localScale;
        gameObject.transform.position = new Vector3(100, 0, 0);
        contentBullet.bulletInstance = null;
        KhtPool.ReturnObject(gameObject);
    }
}
