using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RSet", menuName = "LevelProperties/Generation/New RoadGenerationSet")]

public class RoadGenerationSet : ScriptableObject
{
    [Serializable]
    public class SetElement<T> where T : SpawnElementInfo
    {
        [Range(1, 12)]
        [SerializeField] int numSpawnPlace;
        public int NumSpawnPlace => numSpawnPlace;

        [SerializeField] T spawnElement;
        public T SpawnElement => spawnElement;

        public SetElement(int numSpawnPlace, T spawnElement)
        {
            this.numSpawnPlace = numSpawnPlace;
            this.spawnElement = spawnElement;
        }


    }
    [SerializeField] string id;
    public string Id => id;


    [Header("Types of Traps")]
    [SerializeField] TrapInfo[] trapTypes;
    public TrapInfo[] TrapTypes => trapTypes;

    [Header("Traps")]
    [SerializeField] SetElement<TrapInfo>[] trapElements;

    public SetElement<TrapInfo>[] TrapElements => trapElements;

    [Header("Bonuses")]
    [Range(1, 12)]
    [SerializeField] int[] bonusNumSpawnPlaces;

    public int[] BonusNumSpawnPlaces => bonusNumSpawnPlaces;


}
