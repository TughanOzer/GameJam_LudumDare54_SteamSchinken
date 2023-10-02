using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    public static event Action OnPlayerSpotted;
    [SerializeField] private float _distance;

    public void CheckForPlayerInLineOfSight()
    {        
        Physics.Raycast(transform.position, transform.right, out RaycastHit hitInfo, _distance);
       
        if (hitInfo.collider != null)
        { 
            Debug.DrawLine(transform.position, hitInfo.point, Color.red); 

            var player = hitInfo.collider.GetComponent<Player>();

            if (player != null && !player.IsInvisible)
                OnPlayerSpotted?.Invoke();
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right, Color.green);
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