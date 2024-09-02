using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DamageStamp : Stamp
{
    [Header("Features")]
    [Range(1, 5)]
    [SerializeField] int damage_Increase;
    public int damageIncrease => damage_Increase;
    public DamageStamp(DamageStampInfo info) : base(info)
    {
        damage_Increase = info.damageIncrease;
    }

    public override void applyStampEffectOnCannon(Cannon cannon)
    {
        BulletInfo[] bulletsInfo = cannon.bulletsInfo;
        GameObject[] objectParts = cannon.instance.GetComponent<ContentEnemy>().enemyParts;

        int n = objectParts.Length;
        int m = bulletsInfo.Length;


        for(int i = 0; i < n; i++)
        {
            Material[] materials = objectParts[i].GetComponent<MeshRenderer>().materials;
            List<Material> materialsList = new List<Material>(materials);
            if (materialsList.Count == 1)
            {
                materialsList.Add(enemyMaterial);
                objectParts[i].GetComponent<MeshRenderer>().SetMaterials(materialsList);
            }
            else break;
        }

    }

    public override GameObject spawnBulletWithStamp(GameObject prefab, GameObject markGun, Transform parent, BulletInfo bulletInfo, out ContentBullet contentBullet)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        temp.transform.position = markGun.transform.position;
        temp.transform.rotation = markGun.transform.rotation;
        contentBullet = temp.GetComponent<ContentBullet>();
        contentBullet.bulletInstance = new Bullet(bulletInfo, damageIncrease);
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

    public override GameObject spawnBulletWithStamp(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent, BulletInfo bulletInfo, out ContentBullet contentBullet)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        temp.transform.position = pos;
        temp.transform.rotation = rotation;
        contentBullet = temp.GetComponent<ContentBullet>();
        contentBullet.bulletInstance = new Bullet(bulletInfo, damageIncrease);
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }
}
