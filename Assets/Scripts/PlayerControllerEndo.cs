using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlayerControllerEndo : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap _obstacles;
    [SerializeField] private float _jumpPower = 1f;
    [SerializeField] private float _jumpTime = 0.2f;
    private bool _isMoving;
    private bool _isSpotted;
    private Vector3 _startPosition = new();
    private SpriteRenderer _spriteRenderer;


    private void OnEnable()
    {
        EnemyLineOfSight.OnPlayerSpotted += OnPlayerSpotted;
    }

    private void OnDisable()
    {
        EnemyLineOfSight.OnPlayerSpotted -= OnPlayerSpotted;
    }

    private void Start()
    {
        //snaps player to grid
        _startPosition = transform.position;
        Vector3Int cellPosition = _grid.WorldToCell(_startPosition);
        Vector3 newStartPosition = _grid.GetCellCenterWorld(cellPosition);
        transform.position = newStartPosition;
        _startPosition = newStartPosition;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (!_isMoving && !_isSpotted)
        {
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W))
                moveDirection = Vector3.up;
            else if (Input.GetKeyDown(KeyCode.S))
                moveDirection = Vector3.down;
            else if (Input.GetKeyDown(KeyCode.D))
                moveDirection = Vector3.right;
            else if (Input.GetKeyDown(KeyCode.A))
                moveDirection = Vector3.left;

            if (moveDirection != Vector3.zero)
                Move(moveDirection);
        }
    }

    private void Move(Vector3 moveDirection)
    {
        Vector3 newPosition = transform.position + moveDirection;

        // Convert world position to cell position
        Vector3Int cellPosition = _grid.WorldToCell(newPosition);

        //Check if the cell is within the grid boundaries
        if (CheckIfCellInBounds(cellPosition))
        {
            if (!CheckIfCellIsBlocked(cellPosition))
            {
                Vector3 newWorldPosition = _grid.GetCellCenterWorld(cellPosition);
                StartCoroutine(TweenMovement(newWorldPosition));
                GameRoundManager.Instance.RaisePlayerMove();
            }
            else
            {
                Debug.Log("Cell blocked!");
                // play some kind of error sound maybe?
            }
        }
        else
        {
            Debug.Log("Cell not in bounds!");
        }
        Turn(moveDirection);
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
        _isMoving = true;
        transform.DOJump(targetPosition, _jumpPower, 1, _jumpTime);
        yield return new WaitForSeconds(_jumpTime);
        _isMoving = false;
    }

    private void Turn(Vector3 moveSteps)
    {
        if (moveSteps == Vector3.left)
            _spriteRenderer.flipX = true;
        else if (moveSteps == Vector3.right)
            _spriteRenderer.flipX = false;          
    }

    private void OnPlayerSpotted()
    {
        _isSpotted = true;
    }
}
