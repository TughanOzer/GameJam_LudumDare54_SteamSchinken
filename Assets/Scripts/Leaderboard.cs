using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    #region Fields and Properties


    [SerializeField] private TextMeshProUGUI _playerNamesGUI;
    [SerializeField] private TextMeshProUGUI _playerScoresGUI;
    [SerializeField] private TMP_InputField _playerNameInput;

    private string _memberID = "20";
    private string _leaderboardKey = "LD_54_Board";
    private int _boardId = 18072;
    private int _score = 1000;
    private int _amountShownScores = 20;
    private string _playerNames = "Names\n";
    private string _playerScores = "Scores\n";

    #endregion

    #region Methods

    private void OnEnable()
    {
        GameManager.OnGameWon += SetScore;
    }

    private void OnDisable()
    {
        GameManager.OnGameWon -= SetScore;
    }

    private void SetScore()
    {
        _score = GameManager.Instance.Score;
    }
   
    #endregion
}
