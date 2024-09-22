using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableAndCollectable : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public InfoPieceOfPath info;
    private BulletInfo bulletInfo;


    [HideInInspector] public int num = 0;
    [HideInInspector] public string type = "";

    [SerializeField] bool destroyInEnemyAttackCollision = true;
    [SerializeField] bool destroyInPlayerCollision = true;

    [SerializeField] public GameObject destructionParticles;

    private GameObject destructionParticlesInstance;
    public bool DestroyInPlayerCollision => destroyInPlayerCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (destroyInEnemyAttackCollision)
        {
            if (gameObject.activeInHierarchy == true)
            {
                if (other.gameObject.TryGetComponent(out ContentBullet contentBullet))
                {
                    bulletInfo = contentBullet.bulletInfo;
                    switch (type)
                    {
                        case "trap":
                            info.deleteTrapElement(num);
                            break;
                        case "misc":
                            info.deleteMiscElement(num);
                            break;
                        case "bonus":
                            info.deleteBonusElement(num);
                            break;
                    }

                    if (bulletInfo.IsBreakInCollision)
                        contentBullet.Remove();
                    SpawnParticles(gameObject.transform.position, gameManager);
                    KhtPool.ReturnObject(gameObject);
                }
            }
        }
    }

    public bool SpawnParticles(Vector3 position, MonoBehaviour forCoroutine)
    {
        if (destructionParticlesInstance != null)
        {
            if (destructionParticlesInstance.activeInHierarchy != true)
            {
                forCoroutine.StartCoroutine(particlesCoroutine(destructionParticles, position));
                return true;
            }
            else return false;
        }
        else
        {
            forCoroutine.StartCoroutine(particlesCoroutine(destructionParticles, position));
            return true;
        }
    }
    public IEnumerator particlesCoroutine(GameObject destructionParticlesPrefab, Vector3 position)
    {
        destructionParticlesInstance = KhtPool.GetObject(destructionParticlesPrefab);
        destructionParticlesInstance.SetActive(true);
        destructionParticlesInstance.transform.SetParent(null);
        destructionParticlesInstance.transform.position = new Vector3(position.x, position.y + 0.7f, position.z);
        yield return new WaitForSeconds(0.5f);
        KhtPool.ReturnObject(destructionParticlesInstance);
        destructionParticlesInstance = null;
    }
}
