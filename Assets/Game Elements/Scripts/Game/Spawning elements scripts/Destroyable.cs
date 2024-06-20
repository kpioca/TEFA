using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableAndCollectable : MonoBehaviour
{
    [HideInInspector] public InfoPieceOfPath info;
    private BulletInfo bulletInfo;

    [HideInInspector] public int num = 0;
    [HideInInspector] public string type = "";

    [SerializeField] bool destroyInEnemyAttackCollision = true;
    [SerializeField] bool destroyInPlayerCollision = true;

    [SerializeField] public GameObject destructionParticles;
    public bool DestroyInPlayerCollision => destroyInPlayerCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (destroyInEnemyAttackCollision)
        {
            if (gameObject.activeInHierarchy == true)
            {
                if (other.tag == "Bullet")
                {
                    bulletInfo = other.gameObject.GetComponent<ContentBullet>().bulletInfo;
                    switch (type)
                    {
                        case "trap":
                            info.deleteTrapElement(num);
                            StartCoroutine(destruction(destructionParticles, gameObject.transform.position));
                            break;
                        case "misc":
                            info.deleteMiscElement(num);
                            break;
                        case "bonus":
                            info.deleteBonusElement(num);
                            break;
                    }
                    KhtPool.ReturnObject(gameObject);

                    if (bulletInfo.IsBreakInCollision)
                        KhtPool.ReturnObject(other.gameObject);
                }
            }
        }
    }

    public IEnumerator destruction(GameObject destructionParticlesPrefab, Vector3 position)
    {
        GameObject destructionParticlesObj;
        destructionParticlesObj = KhtPool.GetObject(destructionParticlesPrefab);
        destructionParticlesObj.transform.position = new Vector3(position.x, position.y + 0.7f, position.z);
        Debug.Log(destructionParticlesObj.transform.position);
        yield return new WaitForSeconds(1f);
        KhtPool.ReturnObject(destructionParticlesObj);
    }
}
