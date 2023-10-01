using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisibilityHandler : MonoBehaviour
{
    #region Fields and Properties

    private Player _player;
    private SpriteRenderer _spriteRenderer;
    private int _endStealthRound;
    [SerializeField] private int _stealthTurnDuration = 3;

    #endregion

    #region Methods

    private void OnEnable()
    {
        GameRoundManager.OnPlayerMoved += CheckForAbilityEnd;
    }

    private void OnDisable()
    {
        GameRoundManager.OnPlayerMoved -= CheckForAbilityEnd;
    }

    private void Start()
    {
        if (Player.Instance != null)
        {
            _player = Player.Instance;
            _spriteRenderer = _player.GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TurnPlayerInvisible();
    }

    private void TurnPlayerInvisible()
    {
        if (!_player.IsInvisible && _player.LeftOverStealthUses > 0)
        {
            Debug.Log("Turning Invisible!");
             var turnStealthStart = GameRoundManager.Instance.Round;
            _endStealthRound = turnStealthStart + _stealthTurnDuration;
            _player.SetInvisible();
            _player.ReduceStealthUses();
            _spriteRenderer.DOFade(0.25f, 0.2f);
        }
    }

    private void CheckForAbilityEnd()
    {
        if (_player.IsInvisible && GameRoundManager.Instance.Round >= _endStealthRound)
        {
            Debug.Log("Turning Visible!");
            _player.SetVisible();
            _spriteRenderer.DOFade(1, 0.2f);
        }
    }

    #endregion
}
