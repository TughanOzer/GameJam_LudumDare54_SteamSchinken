using LootLocker.Requests;
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

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(_playerNameInput.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
                FetchTopHighscores();
            }
        });
    }

    private void SubmitHighscore()
    {
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, _score, _boardId, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
            }
            else
                Debug.Log("failed: " + response.Error);
        });
    }

    public void FetchTopHighscores()
    {
        LootLockerSDKManager.GetScoreList(_leaderboardKey, _amountShownScores, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");

                _playerScores = "Scores\n";
                _playerNames = "Names\n";

                //makes an array with all highscores
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    string currentPlayer = "";
                    currentPlayer += members[i].rank + ". ";

                    if (members[i].player.name != "")
                        currentPlayer += members[i].player.name;
                    else
                        currentPlayer += members[i].player.id;

                    _playerScores += members[i].score. + "\n";
                    _playerNames += currentPlayer + "\n";

                    _playerNamesGUI.text = _playerNames;
                    _playerScoresGUI.text = _playerScores;
                }

            }
            else
                Debug.Log("failed: " + response.Error);
        });
    }

    #endregion
}
