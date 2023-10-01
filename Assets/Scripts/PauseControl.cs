using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static PauseControl Instance { get; private set; }

    public bool GameIsPaused { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
