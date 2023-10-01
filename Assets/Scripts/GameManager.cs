using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields and Properties

    public static GameManager Instance { get; private set; }
    public static event Action OnGameReset;
    public static event Action OnGameWon;
    public static event Action OnGameLost;
    public static event Action OnLevelWon;

    public int Score { get; private set; } = 0;

    #endregion

    #region Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        OnGameReset += ResetScore;
    }

    private void OnDisable()
    {
        OnGameReset -= ResetScore;    
    }

    private void Start()
    {
        //Logs in the player with a guest id for the global leaderboard
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                return;
            }
            Debug.Log("sucessfully started LootLocker session");
        });
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    private void ResetScore()
    { 
        Score = 0; 
    }

    public void RaiseGameWon()
    {
        OnGameWon?.Invoke();
    }

    public void RaiseGameLost()
    {
        OnGameLost?.Invoke();
    }

    public void RaiseLevelWon()
    {
        OnLevelWon?.Invoke();
    }

    #endregion
}
