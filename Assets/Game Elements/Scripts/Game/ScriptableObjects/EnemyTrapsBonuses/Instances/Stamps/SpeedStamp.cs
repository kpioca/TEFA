using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStamp : Stamp
{
    [Header("Features")]
    [Range(1f, 5f)]
    [SerializeField] float speed_Multiplier = 1.5f;
    public float speedMultiplier => speed_Multiplier;

    public SpeedStamp(SpeedStampInfo info) : base(info)
    {
        speed_Multiplier = info.speedMultiplier;
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
        contentBullet.bulletInstance = new Bullet(bulletInfo, 0, speedMultiplier);
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
        contentBullet.bulletInstance = new Bullet(bulletInfo, 0, speedMultiplier);
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

    public override float getStampValue(float defaultValue = 1)
    {
        return speedMultiplier;
    }
}
