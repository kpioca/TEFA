using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPieceOfPath : MonoBehaviour
{
    [Header("Marks islands")]
    [SerializeField] GameObject[] marks_islandsSmall = new GameObject[2];
    public GameObject[] Marks_islandsSmall => marks_islandsSmall;

    [SerializeField] GameObject[] marks_islandsBig = new GameObject[2];
    public GameObject[] Marks_islandsBig => marks_islandsBig;

    [SerializeField] GameObject[] marks_islandsTall = new GameObject[2];
    public GameObject[] Marks_islandsTall => marks_islandsTall;

    [Header("Marks enemy")]

    [SerializeField] GameObject[] marks_enemyIslSmall = new GameObject[2];
    public GameObject[] Marks_enemyIslSmall => marks_enemyIslSmall;

    [SerializeField] GameObject[] marks_enemyIslBig = new GameObject[2];
    public GameObject[] Marks_enemyIslBig => marks_enemyIslBig;

    [SerializeField] GameObject[] marks_enemyIslTall = new GameObject[2];
    public GameObject[] Marks_enemyIslTall => marks_enemyIslTall;

    [Header("Marks traps")]

    [SerializeField] GameObject[] marks_traps = new GameObject[9];
    public GameObject[] Marks_traps => marks_traps;

    [Header("Marks bonuses")]

    [SerializeField] GameObject[] marks_bonuses = new GameObject[3];
    public GameObject[] Marks_bonuses => marks_bonuses;

    [Header("Marks misc")]

    [SerializeField] GameObject[] marks_misc = new GameObject[12];
    public GameObject[] Marks_misc => marks_misc;

    [Header("Marks extra")]
    [SerializeField] GameObject[] marks_extraIslandsSmall = new GameObject[2];
    public GameObject[] Marks_extraIslandsSmall => marks_extraIslandsSmall;

    [SerializeField] GameObject[] marks_extraIslandsBig = new GameObject[2];
    public GameObject[] Marks_extraIslandsBig => marks_extraIslandsBig;

    [SerializeField] GameObject[] marks_extraIslandsTall = new GameObject[2];
    public GameObject[] Marks_extraIslandsTall => marks_extraIslandsTall;

    [Header("Enemies")]
    [SerializeField] List<GameObject> enemies;
    public List<GameObject> Enemies => enemies;

    [Header("Traps")]
    [SerializeField] List<GameObject> traps;
    public List<GameObject> Traps => traps;

    [Header("Bonuses")]
    [SerializeField] List<GameObject> bonuses;
    public List<GameObject> Bonuses => bonuses;

    [Header("Islands")]
    [SerializeField] List<GameObject> islands;
    public List<GameObject> Islands => islands;

    [Header("Misc")]
    [SerializeField] List<GameObject> misc;
    public List<GameObject> Misc => misc;

}
