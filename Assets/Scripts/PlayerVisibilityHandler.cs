using DG.Tweening;
using FMODUnity;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisibilityHandler : MonoBehaviour
{
    #region Fields and Properties

    private Player _player;
    private SpriteRenderer _spriteRenderer;
    private int _endStealthRound;
    [SerializeField] private int _stealthTurnDuration = 4;
    [SerializeField] private EventReference _turnVisibleSound;
    [SerializeField] private EventReference _turnInvisibleSound;

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
            AudioManager.Instance.PlayOneShot(_turnInvisibleSound, transform.position);
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
            AudioManager.Instance.PlayOneShot(_turnVisibleSound, transform.position);
            _player.SetVisible();
            _spriteRenderer.DOFade(1, 0.2f);
        }
    }

    #endregion
}
