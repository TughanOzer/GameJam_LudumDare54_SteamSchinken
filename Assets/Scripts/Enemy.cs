using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


public class Enemy : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField] private List<MoveSteps> _moveSteps;
    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap _obstacles;
    [SerializeField] private GameObject _caught;
    private Vector3 _startPosition = new();
    private int _currentIndex = 0;
    private float _jumpPower = 1f;
    private float _jumpTime = 0.2f;
    private EnemyLineOfSight _enemyLineOfSight;
    private SpriteRenderer _visualsRenderer;
    private SpriteRenderer _caughtRenderer;

    #endregion

    #region Methods

    private void OnEnable()
    {
        GameRoundManager.OnPlayerMoved += Move;
        GameManager.OnGameLost += ResetPositionAndIndex;
        EnemyLineOfSight.OnPlayerSpotted += OnPlayerSpotted;
    }

    private void OnDisable()
    {
        GameRoundManager.OnPlayerMoved -= Move;
        GameManager.OnGameLost -= ResetPositionAndIndex;
        EnemyLineOfSight.OnPlayerSpotted -= OnPlayerSpotted;
    }

    private void Start()
    {
        _caughtRenderer = _caught.GetComponent<SpriteRenderer>();
        _visualsRenderer = GetComponentInChildren<SpriteRenderer>();
        _enemyLineOfSight = GetComponentInChildren<EnemyLineOfSight>();
        _startPosition = transform.position;

        //snaps enemy to grid
        Vector3Int cellPosition = _grid.WorldToCell(_startPosition);
        Vector3 newStartPosition = _grid.GetCellCenterWorld(cellPosition);
        transform.position = newStartPosition;
        _startPosition = newStartPosition;
    }

    private void Move()
    {
        MoveSteps nextStep = MoveSteps.None;
        
        if (_moveSteps.Count > 0)
            nextStep = _moveSteps[_currentIndex];
        
        Vector3 moveDirection = Vector3.zero;

        switch (nextStep)
        {
            case MoveSteps.Up:
                moveDirection = Vector3.up;
                break;
            case MoveSteps.Down:
                moveDirection = Vector3.down;
                break;
            case MoveSteps.Left:
                moveDirection = Vector3.left;
                break;
            case MoveSteps.Right:
                moveDirection = Vector3.right;
                break;
        }

        if (moveDirection != Vector3.zero)
        {
            var newPosition = transform.position + moveDirection;

            Vector3Int cellPosition = _grid.WorldToCell(newPosition);

            if (CheckIfCellInBounds(cellPosition))
            {
                if (!CheckIfCellIsBlocked(cellPosition))
                {
                    Vector3 newWorldPosition = _grid.GetCellCenterWorld(cellPosition);
                    StartCoroutine(TweenMovement(newWorldPosition));
                }
                else
                {
                    Debug.Log("Enemy Path blocked!");
                }
            }
            else
            {
                Debug.Log("Cell not in bounds");
            }
        }

        _currentIndex++;

        if (_currentIndex >= _moveSteps.Count)
            _currentIndex = 0;

        //Turns the sprite in the direction of the next step
        if (_moveSteps.Count > 0)
        {
            Turn(_moveSteps[_currentIndex]);
            _enemyLineOfSight.Turn(_moveSteps[_currentIndex]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
            GameManager.Instance.RaiseGameLost();
    }

    private bool CheckIfCellInBounds(Vector3Int cellPosition)
    {
        BoundsInt bounds = _obstacles.cellBounds;
        return bounds.Contains(cellPosition);
    }

    private bool CheckIfCellIsBlocked(Vector3Int cellPosition)
    {
        return _obstacles.HasTile(cellPosition);
    }

    private IEnumerator TweenMovement(Vector3 targetPosition)
    {
        transform.DOJump(targetPosition, _jumpPower, 1, _jumpTime);
        yield return new WaitForSeconds(_jumpTime);
    }

    private void ResetPositionAndIndex()
    {
        transform.position = _startPosition;
        _currentIndex = 0;
    }

    private void Turn(MoveSteps directionOrder)
    {
        switch (directionOrder)
        {
            case MoveSteps.Left:
                _visualsRenderer.flipX = false;
                break;
            case MoveSteps.Right:
                _visualsRenderer.flipX = true;
                break;
        }
    }

    private void OnPlayerSpotted()
    {
        _caughtRenderer.enabled = true;
    }

    #endregion
}

public enum MoveSteps
{
    Up, 
    Down,
    Left,
    Right,
    None,
}
