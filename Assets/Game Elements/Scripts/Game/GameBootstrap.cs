using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{

    [SerializeField] private GeneratorLevel _generatorLevel;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private EffectTimer _effectTimer;
    [SerializeField] private PlayerLoad _playerLoad;

    private ContentPlayer _contentPlayer;

    IDataProvider _dataProvider;
    IPersistentData _persistentData;

    private void Start()
    {
        InitializeData((string callback) => { });
        InitializeResultMenu();
        InitializePlayer();
        InitializeGeneratorLevel();

        InitializeGameManager();
        InitializePathCounter();
        InitializePlayerControl();

    }

    private void InitializeData(Action<string> callback)
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);
        LoadDataOrInit(callback);
    }

    private void InitializeGeneratorLevel()
    {
        //
        if (_generatorLevel._rankingIsActive)
        {
            int seed = _persistentData.saveData.RankingSeed;
            _generatorLevel.Initialize(seed);
        }
        else _generatorLevel.Initialize();

    }

    private void InitializeResultMenu()
    {
        _gameManager.resultMenu.Initialize(_dataProvider, _persistentData, _generatorLevel._rankingIsActive);
    }

    private void InitializePathCounter()
    {
        _gameManager.pathCounter.Initialize();
    }

    private void InitializePlayerControl()
    {
        _gameManager.player_Control.Initialize();
    }

    private void InitializeGameManager()
    {
        _gameManager.Initialize(_persistentData, _dataProvider, _contentPlayer);
    }

    private void InitializePlayer()
    {
        _playerLoad.Inititialize(_persistentData, out _contentPlayer);
        _contentPlayer.Initialize(_gameManager, _effectTimer, _persistentData);
    }

    private void LoadDataOrInit(Action<string> callback)
    {
        _dataProvider.TryLoad(callback);
    }
}
