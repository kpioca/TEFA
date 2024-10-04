using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletInfo_Properties", menuName = "LevelProperties/Enemy/Bullets/New BulletInfo_Properties")]
public class BulletInfo : ScriptableObject
{
    [SerializeField] private protected string id;
    public string Id => id;

    [Header("Reference")]
    [SerializeField] private protected GameObject prefab;
    public GameObject Prefab => prefab;

    [Header("Projectile Info")]
    [SerializeField] private protected int speed = 3;
    public int Speed => speed;

    [SerializeField] private protected int damage = 1;
    public int Damage => damage;

    [SerializeField] private protected bool hasEffect = false;

    public bool HasEffect => hasEffect;

    [SerializeField] private protected bool isBreakInCollision = false;

    public bool IsBreakInCollision => isBreakInCollision;

    [SerializeField] private protected bool ignoreArmor = false;

    public bool IgnoreArmor => ignoreArmor;

    [SerializeField] private protected StatusEffectInfo effectInfo = null;

    public StatusEffectInfo EffectInfo => effectInfo;


    public GameObject spawnBullet(GameObject prefab, GameObject markGun, Transform parent, Stamp stamp, out ContentBullet contentBullet)
    {
        if(stamp == null)
        {
            GameObject temp = KhtPool.GetObject(prefab);
            temp.transform.position = markGun.transform.position;
            temp.transform.rotation = markGun.transform.rotation;
            contentBullet = temp.GetComponent<ContentBullet>();
            contentBullet.bulletInstance = new Bullet(this);
            temp.transform.SetParent(parent);
            temp.SetActive(true);
            return temp;
        }
        else
        {
            GameObject temp = stamp.spawnBulletWithStamp(prefab, markGun, parent, this, out contentBullet);
            applyStampMaterial(stamp, contentBullet.BulletMainObject);
            return temp;
        }
        
    }

    public GameObject spawnBullet(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent, Stamp stamp, out ContentBullet contentBullet)
    {
        if (stamp == null)
        {
            GameObject temp = KhtPool.GetObject(prefab);
            temp.transform.position = pos;
            temp.transform.rotation = rotation;
            contentBullet = temp.GetComponent<ContentBullet>();
            contentBullet.bulletInstance = new Bullet(this);
            temp.transform.SetParent(parent);
            temp.SetActive(true);
            return temp;
        }
        else
        {
            GameObject temp = stamp.spawnBulletWithStamp(prefab, pos, rotation, parent, this, out contentBullet);
            applyStampMaterial(stamp, contentBullet.BulletMainObject);
            return temp;
        }
    }

    private void applyStampMaterial(Stamp stamp, GameObject bulletMainObject)
    {
        if(stamp != null)
        {
            Material[] materials = bulletMainObject.GetComponent<MeshRenderer>().materials;
            List<Material> materialsList = new List<Material>(materials);
            if (materialsList.Count == 1)
            {
                materialsList.Add(stamp.bulletMaterial);
                bulletMainObject.GetComponent<MeshRenderer>().SetMaterials(materialsList);
            }
        }
    }
}
