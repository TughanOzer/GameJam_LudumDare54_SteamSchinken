using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    private Canvas _winCanvas;
    private Image _winImage;

    private void OnEnable()
    {
        GameManager.OnLevelWon += OnLevelWon;
    }

    private void OnDisable()
    {
        GameManager.OnLevelWon -= OnLevelWon;
    }

    private void Start()
    {
        _winCanvas = GetComponent<Canvas>();
        _winImage = GetComponentInChildren<Image>();
        _winCanvas.enabled = false;
    }

    private void OnLevelWon()
    {
        _winCanvas.enabled = true;
        _winImage.DOFade(1, 0.5f);
    }

    private void Update()
    {
        if (Input.anyKeyDown && _winCanvas.enabled)
        {
            AudioManager.Instance.StopLevelMusic();
            AudioManager.Instance.PlayMenuMusic();
            LoadHelper.LoadSceneWithLoadingScreen(SceneName.A_MainMenu);
        }
    }
}
