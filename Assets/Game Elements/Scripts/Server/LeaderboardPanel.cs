
using System;
using UnityEngine;
using LootLocker.Requests;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class LeaderboardPanel : MonoBehaviour
{
    string _leaderboardKey = "record_path_lb";
    private int seedForRanking = -1;
    IDataProvider _dataProvider;
    IPersistentData _persistentData;
    public void Initialize(IDataProvider dataProvider, IPersistentData persistentData)
    {
        GetSeedForRankingGame();
        _persistentData = persistentData;
        _dataProvider = dataProvider;
    }

    private int GetSeedFromResetDate(DateTime date)
    {
        return (int)date.Ticks;
    }

    

    public void GetSeedForRankingGame()
    {
        LootLockerSDKManager.GetLeaderboardData(_leaderboardKey, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error of getting leaderboard data");
                seedForRanking = -1;
                _persistentData.saveData.RankingSeed = seedForRanking;
                _dataProvider.Save();
                return;
            }
            Debug.Log("success of getting leaderboard data");
            string timeReset = response.schedule.next_run;
            seedForRanking = GetSeedFromResetDate(DateTime.Parse(timeReset));
            Debug.Log($"seed is {seedForRanking}");
            _persistentData.saveData.RankingSeed = seedForRanking;
            _dataProvider.Save();
        });
    }
}
