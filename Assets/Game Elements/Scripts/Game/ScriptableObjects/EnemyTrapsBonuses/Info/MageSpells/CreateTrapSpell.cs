using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CreateTrapSpellInfo", menuName = "LevelProperties/Enemy/Mage Spells/New CreateTrapSpellInfo")]
public class CreateTrapSpellInfo : MageSpellInfo
{
    [Header("Trap Pull")]
    [SerializeField] TrapInfo[] trapPull;

    public override void ActivateSpell(GameManager gameManager, GameObject player, InfoPieceOfPath infoPieceOfPath)
    {
        createTrap(infoPieceOfPath);
    }

    private void createTrap(InfoPieceOfPath infoPieceOfPath)
    {
        GameObject road = infoPieceOfPath.gameObject;
        Mark[] trap_marks = infoPieceOfPath.Marks_traps;
        Mark[] holesInstances = infoPieceOfPath.HolesInstances;

        int n1 = trap_marks.Length;
        int k1 = Random.Range(0, n1);
        
        int n2 = trapPull.Length;
        int k2 = Random.Range(0, n2);
        if (trap_marks[k1].isTaken == true) {

            KhtPool.ReturnObject(trap_marks[k1].spawnPlace.obj);
            infoPieceOfPath.deleteTrapElement(trap_marks[k1].spawnPlace.num);
            trap_marks[k1].isTaken = false;
            trap_marks[k1].spawnPlace = null;

            if (holesInstances[k1].isTaken == true)
            {
                holesInstances[k1].isTaken = false;
                holesInstances[k1].spawnPlace.obj.SetActive(true);
                holesInstances[k1].spawnPlace = null;
            }
            spawnTrap(infoPieceOfPath, road, trapPull[k2], trap_marks, holesInstances, k1);
        }
        else
        {
            spawnTrap(infoPieceOfPath, road, trapPull[k2], trap_marks, holesInstances, k1);
        }

    }

    private GameObject spawnTrap(InfoPieceOfPath infoPieceOfPath, GameObject road, TrapInfo trap, Mark[] trap_marks, Mark[] holesInstances, int num_mark)
    {
        GameObject trapObj = spawnObjectWithPrefabParameters(trap.Prefab, trap_marks[num_mark].obj.transform.position, road.transform);
        Mark hole;

        DestroyableAndCollectable destr;

        destr = trapObj.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            destr.num = infoPieceOfPath.Traps.Count;
            destr.info = infoPieceOfPath;
            destr.type = "trap";
        }

        int num2 = infoPieceOfPath.Traps.Count;
        SpawnPlace spawnPlace = new SpawnPlace(trapObj, trap_marks[num_mark], num2);
        infoPieceOfPath.Traps.Add(spawnPlace);
        trap_marks[num_mark].isTaken = true;
        trap_marks[num_mark].spawnPlace = spawnPlace;

        if (trap.DoHoleInRoad)
        {
            hole = holesInstances[num_mark];
            num2 = infoPieceOfPath.CurrHoles.Count;
            spawnPlace = new SpawnPlace(hole.obj, hole, num2);
            infoPieceOfPath.CurrHoles.Add(spawnPlace);
            hole.isTaken = true;
            hole.spawnPlace = spawnPlace;
            hole.obj.SetActive(false);
        }

        return trapObj;
    }
    private GameObject spawnObjectWithPrefabParameters(GameObject prefab, Vector3 pos, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        Transform tempTrans = temp.transform;

        tempTrans.position = new Vector3(pos.x, tempTrans.position.y, pos.z);
        tempTrans.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

    
}
