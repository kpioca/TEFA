using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static CannonInfo;

public class ContentPlayer : MonoBehaviour
{
    [SerializeField] private StatusEffectInfo deathImmortalEffect;
    [SerializeField] private StatusEffectInfo destroyArmorEffect;
    [SerializeField] private GameManager gameManager;

    public GameManager game_Manager => gameManager;
    [SerializeField] private EffectTimer effect_Timer;

    [SerializeField] private int health = 1;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    [SerializeField] private int armor = 0;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    [SerializeField] private int effectTimer = 0;
    public int EffectTimer
    {
        get { return effectTimer; }
        set {
            effectTimer = value;
        }
    }


    private bool isImmortal = false;
    [SerializeField] private bool isAdminImmortal = false;
    public bool IsAdminImmortal => isAdminImmortal;

    public Dictionary<string, StatusEffectInfo> appliedEffects = new Dictionary<string, StatusEffectInfo>();
    public List<StatusEffectInfo> effectsWithAppliedShaders;
    Material[] baseMaterials;

    [Header("For shaders")]
    [SerializeField] private GameObject[] objectParts;

    private void Start()
    {
        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnUnSubscribe += unSubscribe;
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnUnSubscribe -= unSubscribe;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ContentMisc>(out ContentMisc contentMisc))
        {
            collectMisc(contentMisc, other.gameObject);
        }

        else if (other.tag == "Bonus")
        {
            if (isAdminImmortal == false)
            {
                collectBonus(other.gameObject);

                BonusInfo bonusInfo = other.GetComponent<ContentBonus>().bonusInfo;
                bonusInfo.Action(gameManager, this);
            }
        }

        else if ((isImmortal || isAdminImmortal) && other.tag == "Trap")
        {
            DestroyTrap(other.gameObject);
        }
        else if (!(isImmortal || isAdminImmortal))
        {
            if (other.tag == "Bullet")
            {
                Bullet bullet = other.GetComponent<ContentBullet>().bulletInstance;
                
                if(bullet != null)
                attackBullet(bullet, other.gameObject);
              
            }
            else if (other.tag == "Trap")
            {
                TrapInfo trapInfo = other.GetComponent<ContentTrap>().trapInfo;
                
                if(trapInfo != null)
                attackTrap(trapInfo, other.gameObject);

            }
        }
    }

    void GameOver()
    {
        this.enabled = false;
    }
    
    void collectMisc(ContentMisc content, GameObject obj)
    {
        switch (content.Misc_Info.Id)
        {
            case "money_fish":
                gameManager.AddMoney();
                break;
            case "food":
                gameManager.AddFood();
                break;
            default:
                break;
        }
        DestroyMisc(obj);
    }

    void collectBonus(GameObject obj)
    {
        DestroyBonus(obj);
    }

    void deleteTrap(GameObject obj, DestroyableAndCollectable destr)
    {
        InfoPieceOfPath info = destr.info;
        int num = destr.num;

        KhtPool.ReturnObject(obj);
        info.deleteTrapElement(num);
    }

    void deleteMisc(GameObject obj, DestroyableAndCollectable destr)
    {
        InfoPieceOfPath info = destr.info;
        int num = destr.num;

        KhtPool.ReturnObject(obj);
        info.deleteMiscElement(num);
    }

    void deleteBonus(GameObject obj, DestroyableAndCollectable destr)
    {
        InfoPieceOfPath info = destr.info;
        int num = destr.num;

        KhtPool.ReturnObject(obj);
        info.deleteBonusElement(num);
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

    public void applyEffect(StatusEffectInfo effect)
    {
        if (!appliedEffects.ContainsKey(effect.EffectId))
        {
            Material[] tempBaseMaterials;
            if (!effectsWithAppliedShaders.Any(eff => eff.EffectId == effect.EffectId))
            {
                if (applyShader(effect, out tempBaseMaterials))
                    effectsWithAppliedShaders.Add(effect);
                if (tempBaseMaterials != null && baseMaterials == null)
                    baseMaterials = tempBaseMaterials;
            }
            appliedEffects[effect.EffectId] = effect;
        }
    }

    private bool applyShader(StatusEffectInfo effect, out Material[] baseMaterials)
    {
        int n = objectParts.Length;
        Material shaderMaterial = effect.EffectMaterial;
        baseMaterials = null;
        if (shaderMaterial != null)
        {
            baseMaterials = new Material[n];
            for (int i = 0; i < n; i++)
            {
                Material[] materials = objectParts[i].GetComponent<MeshRenderer>().materials;
                baseMaterials[i] = materials[0];
                List<Material> materialsList = new List<Material>(materials) { shaderMaterial };
                objectParts[i].GetComponent<MeshRenderer>().SetMaterials(materialsList);
            }
            return true;
        }
        else return false;
    }

    private void applyBaseMaterialss(Material[] baseMaterials)
    {
        int n = objectParts.Length;
        for (int i = 0; i < n; i++)
            objectParts[i].GetComponent<MeshRenderer>().SetMaterials(new List<Material> { baseMaterials[i] });
    }

    //¡¿√
    private void deleteMaterial(int numMaterial)
    {
        numMaterial++;
        int n = objectParts.Length;
        for (int i = 0; i < n; i++) {
            List<Material> materials = new List<Material>(objectParts[i].GetComponent<MeshRenderer>().materials);
            if (numMaterial < materials.Count)
            {
                materials.RemoveAt(numMaterial);
                objectParts[i].GetComponent<MeshRenderer>().SetMaterials(materials);
            }
        }
               
    }
    public void effectActivate(StatusEffectInfo effect)
    {
        //if (!hasThisEffect(effect))
        {
            int duration = 0;
            effect.ApplyEffect(gameManager, this, out duration);

            if (effect.HaveTimer)
            {
                EffectTimer = duration;
                if (effect_Timer.effectTimerCoroutine != null)
                    StopCoroutine(effect_Timer.effectTimerCoroutine);
                effect_Timer.effectTimerCoroutine = StartCoroutine(effect_Timer.EffectTimerCoroutine());
            }
        }
    }

    public void effectActivateWithParameters(StatusEffectInfo effect, int[] parameters)
    {
        //if (!hasThisEffect(effect))
        {
            int duration = 0;
            effect.ApplyEffect(gameManager, this, out duration, parameters);

            if (effect.HaveTimer)
            {
                EffectTimer = duration;
                if (effect_Timer.effectTimerCoroutine != null)
                    StopCoroutine(effect_Timer.effectTimerCoroutine);
                effect_Timer.effectTimerCoroutine = StartCoroutine(effect_Timer.EffectTimerCoroutine());
            }
        }
    }

    //¡¿√
    public void removeEffect(StatusEffectInfo effect)
    {
        if (appliedEffects.ContainsKey(effect.EffectId))
        {
            int num = effectsWithAppliedShaders.FindIndex(eff => eff.EffectId == effect.EffectId);
            if (num != -1)
            {
                effectsWithAppliedShaders.RemoveAt(num);
                deleteMaterial(num);
            }
            appliedEffects.Remove(effect.EffectId);
        }
    }

    public void changeImmortalState(bool state)
    {
        if (state == false) {
            if (!((appliedEffects.ContainsKey("eff_im") && appliedEffects.ContainsKey("eff_d_ar"))
                || (appliedEffects.ContainsKey("eff_im") && appliedEffects.ContainsKey("eff_im_d"))))
                isImmortal = state;
        }
        else isImmortal = state;
    }

    public void changeCounterHealth(int health)
    {
        gameManager.changeHealth(health);
    }

    public void changeCounterArmor(int armor)
    {
        this.armor = armor;
        gameManager.changeArmor(armor);
    }
    private void attackBullet(Bullet bullet, GameObject bulletObj)
    {
        int damage = bullet.Damage;

        if (damage > 0)
        {
            if (bullet.IgnoreArmor || armor == 0)
            {
                health -= damage;
                if(health > 0)
                    changeCounterHealth(health);
                effectActivate(deathImmortalEffect);
            }
            else if (armor > 0)
            {
                effectActivateWithParameters(destroyArmorEffect, new int[1] { damage });
            }
            if (armor < 0)
            {
                armor = 0;
                changeCounterArmor(armor);
            }
        }

        if (bullet.IsBreakInCollision)
            KhtPool.ReturnObject(bulletObj);

        if (health <= 0)
        {
            health = 0;
            changeCounterHealth(health);
            GlobalEventManager.GameOver();
        }
        else if (bullet.HasEffect)
           effectActivate(bullet.EffectInfo);

    }

    private void attackTrap(TrapInfo trap, GameObject trapObj)
    {
        int damage = trap.AttackDamage;

        if (damage > 0)
        {
            if (trap.IgnoreArmor || armor == 0)
            {
                health -= damage;
                if (health > 0)
                    changeCounterHealth(health);
                effectActivate(deathImmortalEffect);
            }
            else if (armor > 0)
            {
                effectActivateWithParameters(destroyArmorEffect, new int[1] { damage });
            }
            if (armor <= 0) 
            {
                armor = 0;
                changeCounterArmor(armor);
            }

        }

        DestroyTrap(trapObj);

        if (health <= 0)
        {
            health = 0;
            changeCounterHealth(health);
            GlobalEventManager.GameOver();
        }
        else if (trap.HasEffect)
            effectActivate(trap.EffectInfo);
    }

    private void DestroyTrap(GameObject gameObject)
    {
        DestroyableAndCollectable destr = gameObject.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            if (destr.DestroyInPlayerCollision)
            {
                if (destr.SpawnParticles(gameObject.transform.position, this))
                    deleteTrap(gameObject, destr);
            }
        }
    }

    private void DestroyBonus(GameObject gameObject)
    {
        DestroyableAndCollectable destr = gameObject.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            if (destr.DestroyInPlayerCollision)
            {
                if (destr.SpawnParticles(gameObject.transform.position, this))
                    deleteBonus(gameObject, destr);
            }
        }
    }
    private void DestroyMisc(GameObject gameObject)
    {
        DestroyableAndCollectable destr = gameObject.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            if (destr.DestroyInPlayerCollision)
            {
                if (destr.SpawnParticles(gameObject.transform.position, this))
                    deleteMisc(gameObject, destr);
            }
        }
    }
}
