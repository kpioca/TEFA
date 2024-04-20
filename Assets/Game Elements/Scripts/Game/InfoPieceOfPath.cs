using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mark
{
    public GameObject obj;
    public bool isTaken;
    public int num;
    public SpawnPlace spawnPlace;

    public Mark(GameObject obj, int num, SpawnPlace spawnPlace, bool isTaken = false)
    {
        this.obj = obj;
        this.isTaken = isTaken;
        this.num = num;
        this.spawnPlace = spawnPlace;
    }
}

public class SpawnPlace
{
    public GameObject obj;
    public Mark mark;
    public int num;

    public SpawnPlace(GameObject obj, Mark mark, int num)
    {
        this.obj = obj;
        this.mark = mark;
        this.num = num;
    }
}


public class InfoPieceOfPath : MonoBehaviour
{
    
    public GameObject road_obj;

    [Header("Marks islands")]
    [SerializeField] Mark[] marks_islandsSmall = new Mark[2];
    public Mark[] Marks_islandsSmall => marks_islandsSmall;

    [SerializeField] Mark[] marks_islandsBig = new Mark[2];
    public Mark[] Marks_islandsBig => marks_islandsBig;

    [SerializeField] Mark[] marks_islandsTall = new Mark[2];
    public Mark[] Marks_islandsTall => marks_islandsTall;

    [Header("Marks enemy")]

    [SerializeField] Mark[] marks_enemyIslSmall = new Mark[2];
    public Mark[] Marks_enemyIslSmall => marks_enemyIslSmall;

    [SerializeField] Mark[] marks_enemyIslBig = new Mark[2];
    public Mark[] Marks_enemyIslBig => marks_enemyIslBig;

    [SerializeField] Mark[] marks_enemyIslTall = new Mark[2];
    public Mark[] Marks_enemyIslTall => marks_enemyIslTall;

    [Header("Marks traps")]

    [SerializeField] Mark[] marks_traps = new Mark[8];
    public Mark[] Marks_traps => marks_traps;


    [Header("Holes instances")]
    [SerializeField] Mark[] holesInstances = new Mark[8];
    public Mark[] HolesInstances => holesInstances;


    [Header("Marks bonuses")]

    [SerializeField] Mark[] marks_bonuses = new Mark[3];
    public Mark[] Marks_bonuses => marks_bonuses;

    [Header("Marks misc")]

    [SerializeField] Mark[] marks_misc = new Mark[12];
    public Mark[] Marks_misc => marks_misc;

    [Header("Marks extra")]
    [SerializeField] Mark[] marks_extraIslandsSmall = new Mark[2];
    public Mark[] Marks_extraIslandsSmall => marks_extraIslandsSmall;

    [SerializeField] Mark[] marks_extraIslandsBig = new Mark[2];
    public Mark[] Marks_extraIslandsBig => marks_extraIslandsBig;

    [SerializeField] Mark[] marks_extraIslandsTall = new Mark[2];
    public Mark[] Marks_extraIslandsTall => marks_extraIslandsTall;

    [Header("Enemies")]
    [SerializeField] List<SpawnPlace> enemies = new List<SpawnPlace>();
    public List<SpawnPlace> Enemies => enemies;

    [Header("Traps")]
    [SerializeField] List<SpawnPlace> traps = new List<SpawnPlace>();
    public List<SpawnPlace> Traps => traps;

    [Header("Holes")]
    [SerializeField] List<SpawnPlace> currHoles = new List<SpawnPlace>();
    public List<SpawnPlace> CurrHoles => currHoles;

    [Header("Bonuses")]
    [SerializeField] List<SpawnPlace> bonuses = new List<SpawnPlace>();
    public List<SpawnPlace> Bonuses => bonuses;

    [Header("Islands")]
    [SerializeField] List<SpawnPlace> islands = new List<SpawnPlace>();
    public List<SpawnPlace> Islands => islands;

    [Header("Misc")]
    [SerializeField] List<SpawnPlace> misc = new List<SpawnPlace>();
    public List<SpawnPlace> Misc => misc;

    public void deleteTrapElement(int num)
    {
        Traps[num].mark.isTaken = false;
        Traps[num].mark.spawnPlace = null;
        Traps.RemoveAt(num);
        recalculateNumbers(Traps);
    }

    public void deleteBonusElement(int num)
    {
        Bonuses[num].mark.isTaken = false;
        Bonuses[num].mark.spawnPlace = null;
        Bonuses.RemoveAt(num);
        recalculateNumbers(Bonuses);
    }

    public void deleteMiscElement(int num)
    {
        Misc[num].mark.isTaken = false;
        Misc[num].mark.spawnPlace = null;
        Misc.RemoveAt(num);
        recalculateNumbers(Misc);
    }

    public void deleteEnemyElement(int num)
    {
        Enemies[num].mark.isTaken = false;
        Enemies[num].mark.spawnPlace = null;
        Enemies.RemoveAt(num);
        recalculateNumbers(Enemies);
    }
    public void recalculateNumbers(List<SpawnPlace> list)
    {
        int n = list.Count;
        DestroyableAndCollectable temp;
        for (int i = 0; i < n; i++)
        {
            temp = list[i].obj.GetComponent<DestroyableAndCollectable>();
            list[i].num = i;
            if (temp != null)
                list[i].obj.GetComponent<DestroyableAndCollectable>().num = i;
        }
    }

}
