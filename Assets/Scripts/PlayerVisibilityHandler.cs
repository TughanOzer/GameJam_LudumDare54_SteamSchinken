using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisibilityHandler : MonoBehaviour
{
    #region Fields and Properties

    private Player _player;
    private SpriteRenderer _spriteRenderer;
    private int _turnStealthStart;
    private int _endStealth;
    [SerializeField] private int _stealthTurnDuration = 3;

    #endregion

    #region Methods

    private void OnEnable()
    {
        //OnTurnEnd += CheckForAbilityEnd;
    }

    private void Start()
    {
        if (Player.Instance != null)
        {
            _player = Player.Instance;
            _spriteRenderer = _player.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TurnPlayerInvisible();
    }

    private void TurnPlayerInvisible()
    {
        if (!_player.IsInvisible)
        {
            //_turnStealthStart = Get current game turn
            //_endStealthStart = _
            _player.SetInvisible();
            _player.ReduceStealthUses();
            _spriteRenderer.DOFade(0.25f, 0.2f);
        }
    }

    private void TurnPlayerVisible()
    {
        if (_player.IsInvisible)
            _spriteRenderer.DOFade(1, 0.2f);
    }

    private void CheckForAbilityEnd()
    {
        //tbd
    }

    #endregion
}
