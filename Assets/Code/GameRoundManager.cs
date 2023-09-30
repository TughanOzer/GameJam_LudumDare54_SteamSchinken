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
    public GameObject player;
    public TextMeshProUGUI roundUI;
    public TextMeshProUGUI turnTextUI;
    PlayerController playerController;
    public int Round { get; private set; }
    public bool IsOngoingRoundPlayer { get; private set; } = true;
    public bool IsOngoingRoundEnemy { get; private set; } = false;

    public static event Action OnEnemyTurnStarted;
    public static event Action OnEnemyTurnFinished;
    public static event Action OnPlayerTurnStarted;
    public static event Action OnPlayerTurnFinished;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        playerController = player.GetComponent<PlayerController>();
    }

    private void FixedUpdate() 
    {
        if (IsOngoingRoundEnemy ) { 
            turnTextUI.text = "Enemy Turn!"; 
        }
        else if (IsOngoingRoundPlayer ) {
            turnTextUI.text = "Player Turn!"; 
        }
        else {
            turnTextUI.text = "Error!";
        }
    }

    public void FinishEnemyTurn()
    {
        SetPlayerTurn(); //Das ist nur vorübergehend!
        OnEnemyTurnFinished?.Invoke();
        OnPlayerTurnStarted?.Invoke();
    }

    public void FinishPlayerTurn()
    {
        NextRound();
        SetEnemyTurn();
        OnPlayerTurnFinished?.Invoke();
        OnEnemyTurnStarted?.Invoke();
    }

    public void NextRound() 
    {
        Round++;
        roundUI.text = "Round: " + Round.ToString();
    }

    public void SetPlayerTurn() 
    {
        IsOngoingRoundEnemy = false;
        IsOngoingRoundPlayer = true;
    }

    public void SetEnemyTurn() 
    {
        IsOngoingRoundEnemy = true;
        IsOngoingRoundPlayer = false;
    }





}
