using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : MonoBehaviour
{
    [SerializeField] private EventReference _energyPickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            Debug.Log("Player Collision!");
            player.AddStealthUses();
            AudioManager.Instance.PlayOneShot(_energyPickUpSound, transform.position);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            Debug.Log("Player Collision!");
            player.AddStealthUses();
            AudioManager.Instance.PlayOneShot(_energyPickUpSound, transform.position);
            Destroy(gameObject);
        }
    }
}
