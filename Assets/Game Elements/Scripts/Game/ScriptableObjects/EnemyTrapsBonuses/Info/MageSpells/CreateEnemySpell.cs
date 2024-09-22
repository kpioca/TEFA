using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CreateEnemySpellInfo", menuName = "LevelProperties/Enemy/Mage Spells/New CreateEnemySpellInfo")]
public class CreateEnemySpellInfo : MageSpellInfo
{
    [Header("Enemy Pull")]
    [SerializeField] EnemyInfo[] enemyPull;

    public override void ActivateSpell(GameManager gameManager, GameObject player, InfoPieceOfPath infoPieceOfPath)
    {
        createEnemy(gameManager, player, infoPieceOfPath);
    }

    private void createEnemy(GameManager gameManager, GameObject player, InfoPieceOfPath infoPieceOfPath)
    {
        GameObject road = infoPieceOfPath.gameObject;
        GameObject enemyObj;
        List<SpawnPlace> spawnPlaces = infoPieceOfPath.Enemies;
        Mark enemyMark;

        int n1 = spawnPlaces.Count;
        int k1 = Random.Range(0, n1);
        
        int n2 = enemyPull.Length;
        int k2 = Random.Range(0, n2);

        ContentEnemy contentEnemy = spawnPlaces[k1].obj.GetComponent<ContentEnemy>();

        if (contentEnemy.attackZone != null)
        {
            contentEnemy.attackZone.IsAttacked = false;
            contentEnemy.attackZone.animatorProperty.Rebind();
            contentEnemy.attackZone.animatorProperty.enabled = false;
        }
        else if (contentEnemy.attackMageZone != null)
        {
            contentEnemy.attackMageZone.IsAttacked = false;
            contentEnemy.attackMageZone.animatorProperty.Rebind();
            contentEnemy.attackMageZone.animatorProperty.enabled = false;
        }

        KhtPool.ReturnObject(spawnPlaces[k1].obj);
        enemyMark = spawnPlaces[k1].mark;
        infoPieceOfPath.deleteEnemyElement(spawnPlaces[k1].num);


        enemyObj = spawnEnemy(player, infoPieceOfPath, road, enemyPull[k2], enemyMark, gameManager);
        SpawnParticles(enemyObj.transform, gameManager);


        //if (contentEnemy.attackZone != null)
        //    contentEnemy.attackZone.animatorProperty.Play("Attack", -1, 0.0f);
        //contentEnemy.attackZone.animatorProperty.SetBool("isStop", false);
        //else if (contentEnemy.attackMageZone != null)
        //    contentEnemy.attackMageZone.animatorProperty.Play("Attack", -1, 0.0f);
        //contentEnemy.attackMageZone.animatorProperty.SetBool("isStop", false);

    }

    private GameObject spawnEnemy(GameObject player, InfoPieceOfPath infoPieceOfPath, GameObject road, EnemyInfo enemy, Mark enemyMark, GameManager gameManager)
    {
        ContentEnemy contentEnemy;
        GameObject enemyObj = spawnObjectWithPrefabParameters(player, enemy.Prefab, enemyMark.obj.transform.position, enemyMark.obj.transform.rotation, road.transform);

        contentEnemy = enemyObj.GetComponent<ContentEnemy>();
        contentEnemy.game_Manager = gameManager;
        contentEnemy.visionZone.turnToInitialState();

        int num2 = infoPieceOfPath.Enemies.Count;
        SpawnPlace spawnPlace = new SpawnPlace(enemyObj, enemyMark, num2);
        infoPieceOfPath.Enemies.Add(spawnPlace);
        enemyMark.isTaken = true;
        enemyMark.spawnPlace = spawnPlace;
        contentEnemy.setInfoSpawnElement(enemy, infoPieceOfPath);

        GameObject movable = contentEnemy.visionZone.MovablePart;
        Vector3 direction = player.transform.position + new Vector3(0, 0, 0.3f) - movable.transform.position;
        Quaternion rotation2 = Quaternion.LookRotation(direction);
        movable.transform.rotation = rotation2;

        return enemyObj;
    }
    private GameObject spawnObjectWithPrefabParameters(GameObject player, GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        Transform tempTrans = temp.transform;

        tempTrans.position = pos;
        tempTrans.rotation = rotation;

        tempTrans.SetParent(parent);
        temp.SetActive(true);

        return temp;
    }

    public void ResetAni(Animation ani, string name)
    {
        AnimationState state = ani[name];
        ani.Play(name);
        state.time = 0;
        ani.Sample();
        state.enabled = false;
    }


}
