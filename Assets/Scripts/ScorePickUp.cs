using Characters.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopUpSpawner))]
public class ScorePickUp : MonoBehaviour
{
    [SerializeField] private int _addedScore = 1000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            GameManager.Instance.AddScore(_addedScore);
            GetComponent<PopUpSpawner>().SpawnPopUp(_addedScore);
            //todo: play pickup sound
            Destroy(gameObject);
        }
    }
}
