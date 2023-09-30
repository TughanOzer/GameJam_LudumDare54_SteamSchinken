using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Fields and Properties

    public static Player Instance { get; private set; }

    public bool IsInvisible { get; private set; }
    public int LeftOverStealthUses { get; private set; } = 3;

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
    }

    public void AddStealthUses(int amount = 1)
    {
        LeftOverStealthUses++;
    }

    public void SetInvisible()
    {
        IsInvisible = true;
    }

    public void SetVisible()
    {
        IsInvisible = false;
    }

    #endregion
}
