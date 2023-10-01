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
}