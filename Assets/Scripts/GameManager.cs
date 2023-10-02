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
    public bool StoryIsTold { get; private set; } = false;

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

    public void SetStoryTold()
    {
        StoryIsTold = true;
    }

    #endregion
}
