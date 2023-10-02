using Characters.UI;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopUpSpawner))]
public class ScorePickUp : MonoBehaviour
{
    [SerializeField] private int _addedScore = 1000;
    [SerializeField] private EventReference _pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            GameManager.Instance.AddScore(_addedScore);
            GetComponent<PopUpSpawner>().SpawnPopUp(_addedScore);
            AudioManager.Instance.PlayOneShot(_pickUpSound, transform.position);
            Destroy(gameObject);
        }
    }
}
