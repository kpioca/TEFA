using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSet", menuName = "LevelProperties/New GameSet")]
public class GameSet : ScriptableObject
{
    public string nameSet = "";
    public int levelOfCoolness = 1;

    public List<EnemyInfo> enemies;
    public List<TrapInfo> traps;
    public List<BonusInfo> bonuses;

    public GameSet(string nameSet, int levelOfCoolness, List<EnemyInfo> enemies, List<TrapInfo> traps, List<BonusInfo> bonuses)
    {
        this.nameSet = nameSet;
        this.levelOfCoolness = levelOfCoolness;
        this.enemies = enemies;
        this.traps = traps;
        this.bonuses = bonuses;
    }
}
