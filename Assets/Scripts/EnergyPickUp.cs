using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent<Player>(out var player))
        {
            player.AddStealthUses();
            //todo: play pickup sound;
            Destroy(gameObject);
        }
    }
}
