using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSet", menuName = "LevelProperties/New GameSet")]
public class GameSet : ScriptableObject
{
    [SerializeField] private string nameSet = "";
    public string NameSet => nameSet;
    [SerializeField] private int levelOfCoolness = 1;
    public int LevelOfCoolness => levelOfCoolness;

    [SerializeField] private List<EnemyInfo> enemies;
    public List<EnemyInfo> Enemies => enemies;
    [SerializeField] private List<TrapInfo> traps;
    public List <TrapInfo> Traps => traps;
    [SerializeField] private List<BonusInfo> bonuses;
    public List<BonusInfo> Bonuses => bonuses;

    [Header("For Fish Multiplier")]
    [Range(0f, 10f)]
    [SerializeField] private protected float multiplier = 1;
    public float Multiplier => multiplier;
}
