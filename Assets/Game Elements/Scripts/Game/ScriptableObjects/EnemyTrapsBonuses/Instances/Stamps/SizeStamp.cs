using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeStamp : Stamp
{
    [Header("Features")]
    [Range(1f, 5f)]
    [SerializeField] float size_Multiplier = 1.5f;
    public float sizeMultiplier => size_Multiplier;

    private Dictionary<string, Vector3> sizes = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> Sizes
    {
        get { return sizes; }
        set { sizes = value; }

    }
    public SizeStamp(SizeStampInfo info) : base(info)
    {
        size_Multiplier = info.sizeMultiplier;
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
        
        for(int i = 0; i < m;i++)
            sizes[bulletsInfo[i].Id] = sizeMultiplier * bulletsInfo[i].Prefab.gameObject.transform.localScale;

    }

    public override GameObject spawnBulletWithStamp(GameObject prefab, GameObject markGun, Transform parent, BulletInfo bulletInfo, out ContentBullet contentBullet)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        temp.transform.position = markGun.transform.position;
        temp.transform.rotation = markGun.transform.rotation;
        temp.transform.localScale = sizes[bulletInfo.Id];
        contentBullet = temp.GetComponent<ContentBullet>();
        contentBullet.bulletInstance = new Bullet(bulletInfo);
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

    public override GameObject spawnBulletWithStamp(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent, BulletInfo bulletInfo, out ContentBullet contentBullet)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        temp.transform.position = pos;
        temp.transform.rotation = rotation;
        temp.transform.localScale = sizes[bulletInfo.Id];
        contentBullet = temp.GetComponent<ContentBullet>();
        contentBullet.bulletInstance = new Bullet(bulletInfo);
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }
}
