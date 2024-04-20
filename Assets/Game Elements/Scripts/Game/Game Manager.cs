using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private TMP_Text moneyCounterText;
    [SerializeField] private TMP_Text healthCounterText;
    [SerializeField] private TMP_Text armorCounterText;

    [SerializeField] private PathCounter pathCounter;
    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private ContentPlayer contentPlayer;

    [SerializeField] private GameObject deathPanel;

    [Header("Player parameters")]
    [SerializeField] private float speed_playerDash = 7;
    public float Speed_playerDash => speed_playerDash;

    [SerializeField] private float height_playerDash = 0.3f;

    public float Height_playerDash => height_playerDash;

    [SerializeField] private float speed_playerJump = 5;

    [SerializeField] private int health;
    [SerializeField] private int armor;

    public float Speed_playerJump => speed_playerJump;
    [SerializeField] private float height_playerJump = 2.5f;
    public float Height_playerJump => height_playerJump;

    [Header("Route parameters")]
    [SerializeField] private float speedRouteMovement = 6f;

    [SerializeField] private float start_speedRouteMovement = 6f;
    public float SpeedRouteMovement => speedRouteMovement;

    private float speedCount;
    public float SpeedCount => speedCount;


    [Header("Game")]
    [SerializeField] private bool isGameOver = false;

    [Header("Collectables")]
    [SerializeField] private int fishMoney = 0;
    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
    void Awake()
    {
        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnUnSubscribe += unSubscribe;
        speedCount = speedRouteMovement / (start_speedRouteMovement - 1);

        moneyCounterText.text = fishMoney.ToString();

        changeHealth(contentPlayer.Health);
        changeArmor(contentPlayer.Armor);
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnUnSubscribe -= unSubscribe;
    }

    void GameOver()
    {
        isGameOver = true;
        playerControl.enabled = false;
        deathPanel.SetActive(true);
    }

    public void RestartGame()
    {
        GlobalEventManager.UnSubscribe();
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int amount = 1)
    {
        fishMoney += amount;
        moneyCounterText.text = fishMoney.ToString();
    }

    public void changeHealth(int value)
    {
        health = value;
        healthCounterText.text = health.ToString();
    }

    public void changeArmor(int value)
    {
        armor = value;
        armorCounterText.text = armor.ToString();
    }

    public void changeRouteSpeedMovement(float speedMovement)
    {
        float newSpeedCount = speedMovement / (start_speedRouteMovement - 1);
        pathCounter.changeSpeedCount(newSpeedCount);
        GlobalEventManager.ChangeSpeedRouteMovement(speedMovement);
    }

    public void changePlayerMoveParameters(float speedMove, float heightMove, float speedJump, float heightJump)
    {
        playerControl.changePlayerParameters(speedMove, heightMove, speedJump, heightJump);
    }
}
