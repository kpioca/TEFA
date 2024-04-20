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

                    KhtPool.ReturnObject(gameObject);
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
                        KhtPool.ReturnObject(other.gameObject);
                }
            }
        }
    }
}
