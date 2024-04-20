using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CannonInfo;

public class ContentPlayer : MonoBehaviour
{
    [SerializeField] private StatusEffectInfo deathImmortalEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EffectTimer effect_Timer;

    [SerializeField] private int health = 1;

    public int Health => health;

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

    [SerializeField] private bool isImmortal = false;

    private Dictionary<string, StatusEffectInfo> appliedEffects = new Dictionary<string, StatusEffectInfo>();


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
        if (other.tag == "FishMoney")
        {
            collectMoney(other.gameObject);
        }

        else if(other.tag == "Bonus")
        {
            collectBonus(other.gameObject);

            BonusInfo bonusInfo = other.GetComponent<ContentBonus>().bonusInfo;
            bonusInfo.Action(gameManager, this);
        }

        else if (isImmortal && other.tag == "Trap")
        {
            DestroyableAndCollectable destr = other.gameObject.GetComponent<DestroyableAndCollectable>();
            if (destr != null)
            {
                if (destr.DestroyInPlayerCollision)
                    deleteTrap(other.gameObject, destr);
            }
        }
        else if (!isImmortal)
        {
            if (other.tag == "Bullet")
            {
                BulletInfo bulletInfo = other.GetComponent<ContentBullet>().bulletInfo;

                attackBullet(bulletInfo, other.gameObject);
              
            }
            else if (other.tag == "Trap")
            {
                TrapInfo trapInfo = other.GetComponent<ContentTrap>().trapInfo;
                
                attackTrap(trapInfo, other.gameObject);

            }
        }
    }

    void GameOver()
    {
        this.enabled = false;
    }
    
    void collectMoney(GameObject obj)
    {
        gameManager.AddMoney();
        deleteMisc(obj);
    }

    void collectBonus(GameObject obj)
    {
        deleteBonus(obj);
    }

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

    public void applyEffect(StatusEffectInfo effect)
    {
        if (!appliedEffects.ContainsKey(effect.EffectId))
            appliedEffects[effect.EffectId] = effect;
    }

    public void reapplyEffect(StatusEffectInfo effect)
    {
        if (appliedEffects.ContainsKey(effect.EffectId))
            effectActivate(effect);
    }

    public void effectActivate(StatusEffectInfo effect)
    {
        if (!hasThisEffect(effect))
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
        else reapplyEffect(effect);
    }

    public void removeEffect(StatusEffectInfo effect)
    {
        if (appliedEffects.ContainsKey(effect.EffectId))
            appliedEffects.Remove(effect.EffectId);
    }

    public bool hasThisEffect(StatusEffectInfo effect)
    {
        if (appliedEffects.ContainsKey(effect.EffectId))
            return true;
        else return false;
    }

    public void changeImmortalState(bool state)
    {
        isImmortal = state;
    }

    public void changeCounterHealth(int health)
    {
        gameManager.changeHealth(health);
    }

    public void changeCounterArmor(int armor)
    {
        gameManager.changeArmor(armor);
    }
    private void attackBullet(BulletInfo bullet, GameObject bulletObj)
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
                armor -= damage;
                if (armor > 0) changeCounterArmor(armor);
            }
            if (armor <= 0)
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
                armor -= damage;
                if (armor > 0) changeCounterArmor(armor);
            }
            if (armor <= 0) 
            {
                armor = 0;
                changeCounterArmor(armor);
            }

        }

        DestroyableAndCollectable destr = trapObj.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            if (destr.DestroyInPlayerCollision)
                deleteTrap(trapObj, destr);
        }

        if (health <= 0)
        {
            health = 0;
            changeCounterHealth(health);
            GlobalEventManager.GameOver();
        }
        else if (trap.HasEffect)
            effectActivate(trap.EffectInfo);
    }
}
