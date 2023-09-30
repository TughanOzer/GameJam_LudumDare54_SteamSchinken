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
    public int round = 0;
    public bool ongoingRoundPlayer = true;
    public bool ongoingRoundEnemy = false;

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

    private void FixedUpdate() {

        if (ongoingRoundEnemy ) { 
            turnTextUI.text = "Enemy Turn!"; 
        }
        else if (ongoingRoundPlayer ) {
            turnTextUI.text = "Player Turn!"; 
        }
        else {
            turnTextUI.text = "Error!";
        }
            

    }




    public void RoundCounter() {
            round = round + 1;
            roundUI.text = "Round: " + round.ToString();
    }

    public void SetPlayerTurn(bool state) {
        ongoingRoundEnemy = !state;
        ongoingRoundPlayer = state;
    }
    public void SetEnemyTurn(bool state) {
        Debug.Log("SetEnemyTurn");
        Debug.Log(ongoingRoundEnemy);
        ongoingRoundEnemy = state;
        ongoingRoundPlayer = !state;
        Debug.Log(ongoingRoundEnemy);
    }

    public void FinishEnemyTurn() {
        SetPlayerTurn(true); //Das ist nur vorübergehend!
        SetEnemyTurn(false);
        OnEnemyTurnFinished?.Invoke();
        OnPlayerTurnStarted?.Invoke();
    }

    public void FinishPlayerTurn() {
        RoundCounter();
        SetEnemyTurn(true);
        SetPlayerTurn(false);
        OnPlayerTurnFinished?.Invoke();
        OnEnemyTurnStarted?.Invoke();
    }

}
