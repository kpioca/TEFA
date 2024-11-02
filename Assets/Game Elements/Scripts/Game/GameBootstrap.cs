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
        InitializeData();
        InitializeResultMenu();
        InitializePlayer();
        InitializeGeneratorLevel();

        InitializeGameManager();
        InitializePathCounter();
        InitializePlayerControl();

    }

    private void InitializeData()
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);
        LoadDataOrInit();
    }

    private void InitializeGeneratorLevel()
    {
        _generatorLevel.Initialize();
    }

    private void InitializeResultMenu()
    {
        _gameManager.resultMenu.Initialize(_dataProvider, _persistentData);
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

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentData.saveData = new SaveData();
    }
}
