using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private TMP_Text moneyCounterText;
    [SerializeField] private TMP_Text foodCounterText;
    [SerializeField] private TMP_Text healthCounterText;
    [SerializeField] private TMP_Text armorCounterText;

    [SerializeField] private PathCounter pathCounter;
    [SerializeField] private PlayerControl playerControl;
    public PlayerControl player_Control => playerControl;
    [SerializeField] private ContentPlayer contentPlayer;

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private ResultMenu resultMenu;
    [SerializeField] private GameDataManager gameDataManager;
    [SerializeField] private GeneratorLevel generator_Level;
    public GeneratorLevel generatorLevel => generator_Level;

    public List<GameObject> roads;

    [Header("Player parameters")]
    [SerializeField] private float speed_playerDash = 7;
    public float Speed_playerDash => speed_playerDash;

    [SerializeField] private float speed_playerJump = 5;

    [SerializeField] private int health;
    [SerializeField] private int armor;

    public float Speed_playerJump => speed_playerJump;


    [SerializeField] private float height_playerJump = 2.5f;
    public float Height_playerJump => height_playerJump;

    [SerializeField] private float speed_fall;
    public float SpeedFall => speed_fall;

    [Header("Route parameters")]
    [SerializeField] private float speedRouteMovement = 6f;

    [SerializeField] private float start_speedRouteMovement = 6f;
    public float SpeedRouteMovement => speedRouteMovement;

    public float speedCount;
    public float SpeedCount => speedCount;



    //[Header("Game")]
    //[SerializeField] private bool isGameOver = false;

    [Header("Collectables")]
    [SerializeField] private int fishMoney = 0;
    [SerializeField] private int food = 0;
    //public bool IsGameOver
    //{
    //    get { return isGameOver; }
    //    set { isGameOver = value; }
    //}

    public float getPlatformSpeedForBullets()
    {
        return (speedRouteMovement < 0.5f * start_speedRouteMovement) ? 0.5f * start_speedRouteMovement : speedRouteMovement;
    }
    public Dictionary<string, Dictionary<string, List<float>>> decrementsControlValues = new Dictionary<string, Dictionary<string, List<float>>>()
    {
        {"speedMove",  new Dictionary<string, List<float>>()},
        {"heightJump",  new Dictionary<string, List<float>>()},
        {"speedRoute",  new Dictionary<string, List<float>>()}
    };

    public void addDecrementControlValue(string keyValue, string keyEffect, float value)
    {
        if (decrementsControlValues[keyValue].ContainsKey(keyEffect))
            decrementsControlValues[keyValue][keyEffect].Add(value);
        else
        {
            decrementsControlValues[keyValue][keyEffect] = new List<float> { value };
        }
    }

    public void applyAllDecrementControlValues(string keyValue, string keyEffect)
    {
        if (decrementsControlValues[keyValue].ContainsKey(keyEffect))
        {
            List<float> list = decrementsControlValues[keyValue][keyEffect];
            int n = list.Count;
            float value = list.Sum();

            switch (keyValue)
            {
                case "speedMove":
                    value = getSpeedMove() + value;
                    changeSpeedMove(value);
                    break;
                case "heightJump":
                    value = getHeightJump() + value;
                    changeHeightJump(value); 
                    break;
                case "speedRoute":
                    value = speedRouteMovement + value;
                    changeRouteSpeedMovement(value);
                    break;
            }
            list.Clear();
            decrementsControlValues[keyValue].Remove(keyEffect);
        }
    }

    public void applyLastDecrementControlValues(string keyValue, string keyEffect)
    {
        if (decrementsControlValues[keyValue].ContainsKey(keyEffect))
        {
            List<float> list = decrementsControlValues[keyValue][keyEffect];
            int n = list.Count;
            float value = list[n - 1];

            switch (keyValue)
            {
                case "speedMove":
                    value = getSpeedMove() + value;
                    changeSpeedMove(value);
                    break;
                case "heightJump":
                    value = getHeightJump() + value;
                    changeHeightJump(value);
                    break;
                case "speedRoute":
                    value = speedRouteMovement + value;
                    changeRouteSpeedMovement(value);
                    break;
            }
            list.RemoveAt(n - 1);
            if (list.Count == 0)
                decrementsControlValues[keyValue].Remove(keyEffect);

        }
    }
    void Awake()
    {
        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnUnSubscribe += unSubscribe;

        speedRouteMovement = start_speedRouteMovement;
        speedCount = speedRouteMovement / (start_speedRouteMovement - 1);

        moneyCounterText.text = fishMoney.ToString();

        changeHealth(contentPlayer.Health);
        changeArmor(contentPlayer.Armor);
    }

    private void Start()
    {
        roads = generatorLevel.ready_partsOfPath;
        allDisable();
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnUnSubscribe -= unSubscribe;
    }

    void GameOver()
    {
        //isGameOver = true;
        playerControl.enabled = false;
        Animator animator = contentPlayer.gameObject.GetComponent<Animator>();
        animator.enabled = false;
        animator.Rebind();
        resultMenu.setResult(pathCounter.PathScore, fishMoney, food, gameDataManager);
        deathPanel.SetActive(true);
    }

    public void allDisable()
    {
        playerControl.enabled = false;
        int n = roads.Count;
        for(int i = 0; i < n; i++)
            roads[i].GetComponent<PlatformMovement>().enabled = false;
        pathCounter.stopPathCounter();
    }

    public void allEnable()
    {
        playerControl.enabled = true;
        int n = roads.Count;
        for (int i = 0; i < n; i++)
            roads[i].GetComponent<PlatformMovement>().enabled = true;
        pathCounter.startPathCounter();
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

    public void AddFood(int amount = 1)
    {
        food += amount;
        foodCounterText.text = food.ToString();
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

        speedRouteMovement = speedMovement;
        float newSpeedCount = speedMovement / (start_speedRouteMovement - 1);
        pathCounter.changeSpeedCount(newSpeedCount);
        GlobalEventManager.ChangeSpeedRouteMovement(speedMovement);
    }


    public void getCurrentPlayerMoveParameters(out float speedMove, out float heightJump)
    {
        speedMove = playerControl.SpeedMove;
        heightJump = playerControl.HeightJump;
    }

    public float getSpeedMove()
    {
        return playerControl.SpeedMove;
    }

    public float getHeightJump()
    {
        return playerControl.HeightJump;
    }

    public void changeSpeedMove(float speedMove)
    {
        playerControl.changeSpeedMove(speedMove);
    }

    public void changeHeightJump(float heightJump)
    {
        playerControl.changeHeightJump(heightJump);
    }

    public void changePlayerMoveParameters(float speedMove, float heightJump)
    {
        playerControl.changePlayerParameters(speedMove, heightJump);
    }
}
