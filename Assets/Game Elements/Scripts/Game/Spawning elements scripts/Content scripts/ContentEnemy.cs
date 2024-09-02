using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnemy : MonoBehaviour
{
    Cannon cannon;
    Mage mage;

    [SerializeField] GameManager gameManager;
    public GameManager game_Manager
    {
        get {  return gameManager; }
        set { gameManager = value; }
    }

    [SerializeField] GameObject visionZoneObj = null;
    public VisionZone visionZone;

    BoxCollider visionBox;
    [SerializeField] GameObject attackZoneObj = null;
    public AttackZone attackZone;
    public AttackMageZone attackMageZone;

    BoxCollider attackBox;

    InfoPieceOfPath info_PieceOfPath;
    public InfoPieceOfPath infoPieceOfPath => info_PieceOfPath;



    [SerializeField] float visionZoneX = -1;
    [SerializeField] float visionZoneY = -1;
    [SerializeField] float visionZoneZ = -1;

    [SerializeField] float attackZoneX = -1;
    [SerializeField] float attackZoneY = -1;
    [SerializeField] float attackZoneZ = -1;

    [SerializeField]  private GameObject element1;
    [SerializeField]  private GameObject element2;

    [Header("Parts of enemy")]
    [SerializeField] private GameObject[] enemy_Parts;
    public GameObject[] enemyParts => enemy_Parts;

    private float intervalBetweenShots = 0;
    public float IntervalBetweenShots => intervalBetweenShots;

    private int n_shots = 1;
    public int N_shots => n_shots;


    public void Start()
    {
        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnUnSubscribe += unSubscribe;
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnUnSubscribe -= unSubscribe;
    }
    public void setVisionZone(Enemy enemy)
    {
        visionZoneX = enemy.VisionZoneX;
        visionZoneY = enemy.VisionZoneY;
        visionZoneZ = enemy.VisionZoneZ;

        visionBox.size = new Vector3(visionZoneX, visionZoneY, visionZoneZ);
        visionZone = visionZoneObj.GetComponent<VisionZone>();
    }

    public void setAttackCannonZone(Cannon cannon)
    {
        attackZoneX = cannon.AttackZoneX;
        attackZoneY = cannon.AttackZoneY;
        attackZoneZ = cannon.AttackZoneZ;

        attackBox.size = new Vector3(attackZoneX, attackZoneY, attackZoneZ);
        attackZone = attackZoneObj.GetComponent<AttackZone>();
        attackZone.game_Manager = game_Manager;
    }

    public void setAttackMageZone(Mage mage)
    {
        attackZoneX = mage.AttackZoneX;
        attackZoneY = mage.AttackZoneY;
        attackZoneZ = mage.AttackZoneZ;

        attackBox.size = new Vector3(attackZoneX, attackZoneY, attackZoneZ);
        attackMageZone = attackZoneObj.GetComponent<AttackMageZone>();
        attackMageZone.game_Manager = game_Manager;
    }

    public void setInfoSpawnElement(EnemyInfo element, InfoPieceOfPath infoPieceOfPath, Stamp stamp = null)
    {
        info_PieceOfPath = infoPieceOfPath;

        if(element is CannonInfo c)
        {

            switch (element)
            {
                case RainbowCannonInfo:
                    element2.SetActive(false);
                    cannon = new RainbowCannon((RainbowCannonInfo)c, element2, out intervalBetweenShots, out n_shots, gameObject, stamp);
                    break;
                case StandartDoubleCannonInfo:
                    cannon = new StandartDoubleCannon((StandartDoubleCannonInfo)c, out intervalBetweenShots, gameObject, stamp);
                    n_shots = 2;
                    break;
                case StandartCannonInfo:
                    cannon = new StandartCannon((StandartCannonInfo)c, gameObject, stamp);
                    break;
                case FrostyCannonInfo:
                    cannon = new FrostyCannon((FrostyCannonInfo)c, gameObject, stamp);
                    break;
                case SniperCannonInfo:
                    cannon = new SniperCannon((SniperCannonInfo)c, gameObject, stamp);
                    break;
                case BigCannonInfo:
                    cannon = new BigCannon((BigCannonInfo)c, gameObject, stamp);
                    break;
                case CorAngelCannonInfo:
                    cannon = new CorAngelCannon((CorAngelCannonInfo)c, gameObject, stamp);
                    break;
                case StunAngelCannonInfo:
                    cannon = new StunAngelCannon((StunAngelCannonInfo)c, gameObject, stamp);
                    break;
                case ChthAngelCanInfo:
                    cannon = new ChthAngelCan((ChthAngelCanInfo)c, gameObject, stamp);
                    break;
                case CarrotCanInfo:
                    cannon = new CarrotCan((CarrotCanInfo)c, gameObject, stamp);
                    break;
                case LoveCanInfo:
                    cannon = new LoveCan((LoveCanInfo)c, gameObject, stamp);
                    break;
            }

            visionBox = visionZoneObj.GetComponent<BoxCollider>();
            attackBox = attackZoneObj.GetComponent<BoxCollider>();

            if (visionZoneX == -1 && visionZoneY == -1 && visionZoneZ == -1)
                setVisionZone(cannon);
            if (attackZoneX == -1 && attackZoneY == -1 && attackZoneZ == -1)
                setAttackCannonZone(cannon);
            attackZone.IsAttacked = false;
        }

        else if (element is MageInfo M)
        {

            switch (element)
            {
                case MageTransformAngelInfo:
                    mage = new MageTransformAngel((MageTransformAngelInfo)M, gameObject);
                    break;
                case MageBattleAngelInfo:
                    mage = new MageBattleAngel((MageBattleAngelInfo)M, gameObject);
                    break;
            }

            visionBox = visionZoneObj.GetComponent<BoxCollider>();
            attackBox = attackZoneObj.GetComponent<BoxCollider>();

            if (visionZoneX == -1 && visionZoneY == -1 && visionZoneZ == -1)
                setVisionZone(mage);
            if (attackZoneX == -1 && attackZoneY == -1 && attackZoneZ == -1)
                setAttackMageZone(mage);
            attackMageZone.IsAttacked = false;
        }

    }

    public Cannon getCannon()
    {
        return cannon;
    }

    public Mage getMage()
    {
        return mage;
    }

    void GameOver()
    {
        if (gameObject.activeInHierarchy)
        {
            visionZone.enabled = false;
            if(attackZone != null)
                attackZone.enabled = false;
            else if(attackMageZone != null)
                attackMageZone.enabled = false;
        }
    }
}
