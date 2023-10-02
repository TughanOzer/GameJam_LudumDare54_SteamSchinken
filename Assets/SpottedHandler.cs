using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedHandler : MonoBehaviour
{
    public static SpottedHandler Instance { get; private set; }

    private bool _isLoading = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EnemyLineOfSight.OnPlayerSpotted += OnPlayerSpotted;
    }

    private void OnDisable()
    {
        EnemyLineOfSight.OnPlayerSpotted -= OnPlayerSpotted;
    }

    private void OnPlayerSpotted()
    {
        if (!_isLoading)
            StartCoroutine(LoadWithDelay());
    }

    private IEnumerator LoadWithDelay()
    {
        _isLoading = true;
        yield return new WaitForSeconds(1);
        LoadHelper.LoadSceneWithLoadingScreen(SceneName.AAA_Finished);
    }
}
