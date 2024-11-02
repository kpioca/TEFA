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
    private GameManager gameManager;

    public GameManager game_Manager => gameManager;
    [SerializeField] private Animator playerAnimator;
    public Animator PlayerAnimator => playerAnimator;
    private EffectTimer effect_Timer;

    [SerializeField] private int health = 1;


    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth {get;set;}
    public int MaxArmor { get; set; }

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

    private Dictionary<string, StatusEffectInfo> appliedImmortalEffects = new Dictionary<string, StatusEffectInfo>();

    private Dictionary<string, StatusEffectInfo> appliedEffects = new Dictionary<string, StatusEffectInfo>();
    [SerializeField] private List<StatusEffectInfo> effectsWithAppliedShaders = new List<StatusEffectInfo>();
    [SerializeField] private List<Material> shaderMaterials = new List<Material>();

    [Header("For shaders")]
    [SerializeField] private GameObject[] objectParts;

    public void Initialize(GameManager gameManager, EffectTimer effectTimer, IPersistentData persistentData)
    {
        this.gameManager = gameManager;
        effect_Timer = effectTimer;
        effectTimer.Initialize(this);

        Health = persistentData.saveData.Health;
        MaxHealth = persistentData.saveData.Health;
        Armor = persistentData.saveData.Armor;
        MaxArmor = persistentData.saveData.Armor;
    }

    private void OnEnable()
    {
        GlobalEventManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnGameOver -= GameOver;
    }
    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
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

    public float getAnimatorSpeedMultiplier()
    {
        return playerAnimator.GetFloat("SpeedMultiplier");
    }

    public void setAnimatorSpeedMultiplier(float value)
    {
        playerAnimator.SetFloat("SpeedMultiplier", value);
    }
    void GameOver()
    {
        unSubscribe();
        clearEffects();
        this.enabled = false;
    }
    
    void clearEffects()
    {
        foreach(var item in appliedEffects)
            item.Value.ClearCoroutine();
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
            if (!effectsWithAppliedShaders.Any(eff => eff.EffectId == effect.EffectId))
            {
                if (applyShader(effect))
                    effectsWithAppliedShaders.Add(effect);
            }
            appliedEffects[effect.EffectId] = effect;
        }
    }

    public void addImmortalEffectToDictionary(StatusEffectInfo effect)
    {
        string id = effect.EffectId;
        if (!appliedImmortalEffects.ContainsKey(id))
            appliedImmortalEffects.Add(id, effect);
    }

    public void removeImmortalEffectFromDictionary(StatusEffectInfo effect)
    {
        string id = effect.EffectId;
        if (appliedImmortalEffects.ContainsKey(id))
            appliedImmortalEffects.Remove(id);
    }




    private void applyBaseMaterialss(Material[] baseMaterials)
    {
        int n = objectParts.Length;
        for (int i = 0; i < n; i++)
            objectParts[i].GetComponent<MeshRenderer>().SetMaterials(new List<Material> { baseMaterials[i] });
    }

    private void applyMaterialsToPlayer()
    {
        List<Material> materialsList;
        Material baseMaterial;
        int n = objectParts.Length;
        for (int i = 0; i < n; i++)
        {
            baseMaterial = objectParts[i].GetComponent<MeshRenderer>().materials[0];

            materialsList = new List<Material>(shaderMaterials);
            materialsList.Insert(0, baseMaterial);

            objectParts[i].GetComponent<MeshRenderer>().SetMaterials(materialsList);
        }
    }
    private bool applyShader(StatusEffectInfo effect)
    {
        int n = objectParts.Length;
        Material shaderMaterial = effect.EffectMaterial;

        if (shaderMaterial != null)
        {
            shaderMaterials.Add(shaderMaterial);
            applyMaterialsToPlayer();
            return true;
        }
        else return false;
    }

    //ÁÀÃ
    private void deleteMaterial(int numMaterial)
    {
        shaderMaterials.RemoveAt(numMaterial);
        applyMaterialsToPlayer();
               
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

    //ÁÀÃ
    public void removeEffect(StatusEffectInfo effect)
    {
        int num;
        if (appliedEffects.ContainsKey(effect.EffectId))
        {
            num = effectsWithAppliedShaders.FindIndex(eff => eff.EffectId == effect.EffectId);
            //Debug.Log($"{num} - {effect.EffectId}");
            if (num >= 0)
            {
                //Debug.Log("OK");
                effectsWithAppliedShaders.RemoveAt(num);
                deleteMaterial(num);
            }
            appliedEffects.Remove(effect.EffectId);
        }
    }

    public void changeImmortalState(bool state)
    {
        if (state == false) {
            if (appliedImmortalEffects.Count == 0)
                isImmortal = state;
        }
        else isImmortal = state;
    }

    public void changeCounterHealth(int health)
    {
        Health = health;
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
                Health -= damage;
                if(Health > 0)
                    changeCounterHealth(Health);
                else if (Health <= 0)
                {
                    Health = 0;
                    changeCounterHealth(Health);
                    GlobalEventManager.GameOver();
                    return;
                }
                effectActivate(deathImmortalEffect);
            }
            else if (armor > 0)
            {
                effectActivateWithParameters(destroyArmorEffect, new int[1] { damage });
            }
            else if (armor <= 0)
            {
                armor = 0;
                changeCounterArmor(armor);
            }
        }

        if (bullet.IsBreakInCollision)
            KhtPool.ReturnObject(bulletObj);
        if (bullet.HasEffect)
           effectActivate(bullet.EffectInfo);

    }

    private void attackTrap(TrapInfo trap, GameObject trapObj)
    {
        int damage = trap.AttackDamage;

        if (damage > 0)
        {
            if (trap.IgnoreArmor || armor == 0)
            {
                Health -= damage;
                if (Health > 0)
                    changeCounterHealth(Health);
                else if (Health <= 0)
                {
                    Health = 0;
                    changeCounterHealth(Health);
                    GlobalEventManager.GameOver();
                    return;
                }
                effectActivate(deathImmortalEffect);
            }
            else if (armor > 0)
            {
                effectActivateWithParameters(destroyArmorEffect, new int[1] { damage });
            }
            else if (armor <= 0) 
            {
                armor = 0;
                changeCounterArmor(armor);
            }

        }

        DestroyTrap(trapObj);
        if (trap.HasEffect)
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

    private void debugInfo(string str)
    {
        int n = appliedEffects.Count;

        string info = str + "\n";
        info += "------\n";
        foreach(var element in appliedEffects)
        {
            info += $"id: {element.Key}, effect: {element.Value.EffectName}\n";
        }
        info += "------";

        Debug.Log(info);

    }
}
