using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameRoundManager : MonoBehaviour
{
    public static GameRoundManager Instance { get; private set; }

    public int Round { get; private set; } = 0;

    public static event Action OnPlayerMoved;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameManager.OnGameLost += ResetRound;
        GameRoundManager.OnPlayerMoved += AdvanceRound;
    }

    private void OnDisable()
    {
        GameManager.OnGameLost -= ResetRound;
        GameRoundManager.OnPlayerMoved -= AdvanceRound;
    }

    private void AdvanceRound()
    {
        Round++;
    }

    private void ResetRound()
    {
        Round = 0;
    }

    public void RaisePlayerMove()
    {
        OnPlayerMoved?.Invoke();
    }
}
