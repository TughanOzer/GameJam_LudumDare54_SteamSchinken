using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            player.AddStealthUses();
            //todo: play pickup sound;
            Destroy(gameObject);
        }
    }
}
