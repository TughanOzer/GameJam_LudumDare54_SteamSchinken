using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum RoundState { START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class RoundManager2 : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab; //Das hier später zu list und mit for loop durch.

    public Transform playerSpawnLocation;
    public Transform enemySpawnLocation; // Später list

    PlayerController playerCtr;
    Unit enemyUnit;

    [SerializeField] TextMeshProUGUI whoTurnUI;

    public RoundState state;

    void Start()
    {
        state = RoundState.START;
        StartCoroutine(SetupRound());
    }

    IEnumerator SetupRound() {

        whoTurnUI.text = "Preparing Round...";

        GameObject playerGO = Instantiate(playerPrefab, playerSpawnLocation);
        playerCtr = playerGO.GetComponent<PlayerController>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawnLocation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

        state = RoundState.PLAYERTURN;
        OnPlayerWalkTurn();

    }

    IEnumerator PlayerWalkTurn() {
        ////Walk Function
        bool isWalkingPressed = false;
        yield return new WaitUntil(() => isWalkingPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"));
        while (state == RoundState.PLAYERTURN) {

            if (!IsInvoking(nameof(playerCtr.PlayerWalk))) {
                StartCoroutine(playerCtr.PlayerWalk());
            }
        }

        yield return new WaitForSeconds(2f);

    }


    public void OnPlayerWalkTurn() {
        if (state != RoundState.PLAYERTURN) {
            return;
        }
        whoTurnUI.text = "Your Turn";
        RoundCounter();
        StartCoroutine(PlayerWalkTurn());
    }


    public void EnemyTurn() {
        whoTurnUI.text = "Enemy Turn";
    }

    public int round;
    [SerializeField] TextMeshProUGUI roundUI;
    public void RoundCounter() {
        round = round + 1;
        roundUI.text = "Round: " + round.ToString();
    }

}
