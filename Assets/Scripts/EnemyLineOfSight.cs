using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    public static event Action OnPlayerSpotted;
    [SerializeField] private float _distance;
    [SerializeField] private EventReference _caughtVoiceLines;

    private bool _caugthVoiceLinePlayed;

    private void Update()
    {
        CheckForPlayerInLineOfSight();
    }

    public void CheckForPlayerInLineOfSight()
    {
        CheckRay(transform.right);

        // Angle above
        float angleAbove = 20f; // You can adjust this angle
        Quaternion rotationAbove = Quaternion.Euler(0, 0, angleAbove);
        Vector3 directionAbove = rotationAbove * transform.right;
        CheckRay(directionAbove);

        // Angle below
        float angleBelow = -20f; // You can adjust this angle
        Quaternion rotationBelow = Quaternion.Euler(0, 0, angleBelow);
        Vector3 directionBelow = rotationBelow * transform.right;
        CheckRay(directionBelow);
    }

    private void CheckRay(Vector3 direction)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, _distance);

        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.gameObject.TryGetComponent<Player>(out Player player))
            {
                if (!player.IsInvisible)
                {
                    if (!_caugthVoiceLinePlayed)
                    {
                        _caugthVoiceLinePlayed = true;
                        AudioManager.Instance.PlayOneShot(_caughtVoiceLines, transform.position);
                        OnPlayerSpotted?.Invoke();
                    }
                }
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + direction * _distance, Color.green);
        }
    }

    public void Turn(MoveSteps directionOrder)
    {
        Vector3 direction;

        switch (directionOrder)
        {
            case MoveSteps.Up:
                direction = Vector3.up;
                break;
            case MoveSteps.Down:
                direction = Vector3.down;
                break;
            case MoveSteps.Left:
                direction = Vector3.left;
                break;
            case MoveSteps.Right:
                direction = Vector3.right;
                break;
            default:
                direction = Vector3.right;
                break;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}