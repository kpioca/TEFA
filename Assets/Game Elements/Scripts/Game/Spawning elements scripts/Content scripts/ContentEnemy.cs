using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnemy : MonoBehaviour
{
    Enemy enemy;

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

    [SerializeField]  private GameObject[] objParameters;

    [Header("Parts of enemy")]
    [SerializeField] private GameObject[] enemy_Parts;
    public GameObject[] enemyParts => enemy_Parts;

    private Dictionary<string, float> numParameters = new Dictionary<string, float>();
    public Dictionary<string, float> NumParameters => numParameters;


    public void Start()
    {
        GlobalEventManager.OnGameOver += GameOver;
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
    }
    public void setVisionZone(Enemy enemy)
    {
        visionZoneX = enemy.VisionZoneX;
        visionZoneY = enemy.VisionZoneY;
        visionZoneZ = enemy.VisionZoneZ;

        visionBox.size = new Vector3(visionZoneX, visionZoneY, visionZoneZ);
        visionZone = visionZoneObj.GetComponent<VisionZone>();
    }


    public void setInfoSpawnElement(EnemyInfo element, InfoPieceOfPath infoPieceOfPath, Stamp stamp = null)
    {
        info_PieceOfPath = infoPieceOfPath;

        enemy = element.createEnemy(gameObject, stamp, objParameters, out numParameters);

        visionBox = visionZoneObj.GetComponent<BoxCollider>();
        attackBox = attackZoneObj.GetComponent<BoxCollider>();

        if (visionZoneX == -1 && visionZoneY == -1 && visionZoneZ == -1)
            setVisionZone(enemy);
        if (attackZoneX == -1 && attackZoneY == -1 && attackZoneZ == -1)
            element.setAttackEnemyZone(enemy, attackBox, attackZoneObj, this);

    }

    public Cannon getCannon()
    {
        return (Cannon)enemy;
    }

    public Mage getMage()
    {
        return (Mage)enemy;
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
        unSubscribe();
    }
}
