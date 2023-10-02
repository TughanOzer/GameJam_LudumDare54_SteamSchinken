using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Fields and Properties

    public static Player Instance { get; private set; }

    public static event Action OnStealthEnergyLost;
    public static event Action<int> OnStealthEnergyGained;

    [SerializeField] private EventReference _playerVoiceLines;

    public bool IsInvisible { get; private set; }
    public int LeftOverStealthUses { get; private set; } = 3;

    private float _voiceLineTime = 30;

    #endregion

    #region Methods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ReduceStealthUses()
    {
        LeftOverStealthUses--;
        OnStealthEnergyLost?.Invoke();
    }

    public void AddStealthUses(int amount = 1)
    {
        LeftOverStealthUses += amount;
        OnStealthEnergyGained?.Invoke(amount);
    }

    public void SetInvisible()
    {
        IsInvisible = true;
    }

    public void SetVisible()
    {
        IsInvisible = false;
    }

    private void Update()
    {
        if (_voiceLineTime > 0)
            _voiceLineTime -= Time.deltaTime;
        else
            _voiceLineTime = GenerateRandomTime();

        if (_voiceLineTime <= 0)
            AudioManager.Instance.PlayOneShot(_playerVoiceLines, transform.position);
    }

    private float GenerateRandomTime()
    {
        return UnityEngine.Random.Range(20, 40);
    }

    #endregion
}
