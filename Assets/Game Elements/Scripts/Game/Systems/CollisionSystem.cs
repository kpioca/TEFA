using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSystem : MonoBehaviour
{
    void deleteTrap(GameObject obj, DestroyableAndCollectable destr)
    {
        InfoPieceOfPath info = destr.info;
        int num = destr.num;

        KhtPool.ReturnObject(obj);
        info.deleteTrapElement(num);
    }

    void deleteMisc(GameObject obj)
    {
        DestroyableAndCollectable destr = obj.GetComponent<DestroyableAndCollectable>();
        InfoPieceOfPath info = destr.info;
        int num = destr.num;

        KhtPool.ReturnObject(obj);
        info.deleteMiscElement(num);
    }

    void deleteBonus(GameObject obj)
    {
        DestroyableAndCollectable destr = obj.GetComponent<DestroyableAndCollectable>();
        InfoPieceOfPath info = destr.info;
        int num = destr.num;

        KhtPool.ReturnObject(obj);
        info.deleteBonusElement(num);
    }


}
