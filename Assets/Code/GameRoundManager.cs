using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameRoundManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI roundUI;
    public TextMeshProUGUI turnTextUI;
    PlayerController playerController;
    public int round = 0;
    public bool ongoingRoundPlayer = true;
    public bool ongoingRoundEnemy = false;

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
    }

    public void FinishPlayerTurn() {
        RoundCounter();
        SetEnemyTurn(true);
        SetPlayerTurn(false);
    }

}
