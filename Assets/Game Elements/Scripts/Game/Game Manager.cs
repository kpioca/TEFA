using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private TMP_Text moneyCounterText;
    [SerializeField] private TMP_Text foodCounterText;
    [SerializeField] private TMP_Text healthCounterText;
    [SerializeField] private TMP_Text armorCounterText;

    [SerializeField] public PathCounter pathCounter;
    [SerializeField] private PlayerControl playerControl;
    public PlayerControl player_Control => playerControl;
    private ContentPlayer _contentPlayer;
    public ContentPlayer contentPlayer => _contentPlayer;

    [SerializeField] private GameObject deathPanel;
    [SerializeField] public ResultMenu resultMenu;
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

    bool isRestart = false;
    public int FishMultiplier { get; set; }


    IPersistentData _persistentData;
    IDataProvider _dataProvider;

    [Header("Collectables")]
    [SerializeField] private int fishMoney = 0;
    public int FishMoney => fishMoney;
    [SerializeField] private int food = 0;
    public int Food => food;

    public float getPlatformSpeedForBullets()
    {
        return (speedRouteMovement < 0.5f * start_speedRouteMovement) ? 0.5f * start_speedRouteMovement : speedRouteMovement;
    }
    public Dictionary<string, Dictionary<string, List<float>>> decrementsControlValues = new Dictionary<string, Dictionary<string, List<float>>>()
    {
        {"speedMove",  new Dictionary<string, List<float>>()},
        {"heightJump",  new Dictionary<string, List<float>>()},
        {"speedRoute",  new Dictionary<string, List<float>>()},
        {"multiplierAnimator", new Dictionary<string, List<float>>() }
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
                case "multiplierAnimator":
                    value = _contentPlayer.getAnimatorSpeedMultiplier() + value;
                    _contentPlayer.setAnimatorSpeedMultiplier(value);
                    break;
            }
            list.RemoveAt(n - 1);
            if (list.Count == 0)
                decrementsControlValues[keyValue].Remove(keyEffect);

        }
    }
    public void Initialize(IPersistentData persistentData, IDataProvider dataProvider, ContentPlayer contentPlayer)
    {

        speedRouteMovement = start_speedRouteMovement;
        speedCount = speedRouteMovement / (start_speedRouteMovement - 1);

        moneyCounterText.text = fishMoney.ToString();

        _persistentData = persistentData;
        _dataProvider = dataProvider;
        _contentPlayer = contentPlayer;

        changeHealth(_persistentData.saveData.Health);
        changeArmor(_persistentData.saveData.Armor);

        roads = generatorLevel.ready_partsOfPath;
    }

    private void OnEnable()
    {
        GlobalEventManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnGameOver -= GameOver;
    }


    public void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
    }

    void GameOver()
    {
        playerControl.enabled = false;
        Animator animator = _contentPlayer.gameObject.GetComponent<Animator>();
        animator.enabled = false;
        animator.Rebind();
        if (!isRestart)
        {
            resultMenu.setResult(pathCounter.PathScore, pathCounter.currentStageDistance.Value[1], fishMoney, food, FishMultiplier);
            deathPanel.SetActive(true);
            StopAllCoroutines();
        }
        unSubscribe();
    }

    

    public void DisablePlatformMovement()
    {
        int n = roads.Count;
        for (int i = 0; i < n; i++)
            roads[i].GetComponent<PlatformMovement>().enabled = false;
    }

    public void allEnable()
    {
        playerControl.gameObject.SetActive(true);
        int n = roads.Count;
        for (int i = 0; i < n; i++)
            roads[i].GetComponent<PlatformMovement>().enabled = true;
        pathCounter.gameObject.SetActive(true);
        pathCounter.startPathCounter();
        _contentPlayer.gameObject.GetComponent<Animator>().enabled = true;
    }

    public void RestartBeforeGame()
    {
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        isRestart = true;
        GlobalEventManager.GameOver();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(1);
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
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
