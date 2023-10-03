using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private EventReference _voiceLine;

    private void Start()
    {
        if (!LoadHelper.BladeStalkerLineDone)
        {
            AudioManager.Instance.PlayOneShot(_voiceLine, Vector3.zero);
            LoadHelper.BladeStalkerLinePlayed();
        }
        StartCoroutine(WaitForLoadingScreen());
    }

    private IEnumerator WaitForLoadingScreen()
    {
        yield return new WaitForSeconds(2f);

        if (LoadHelper.BladeStalkerLineDone && LoadHelper.SceneToBeLoaded == SceneName.A_MainMenu)
            LoadHelper.ResetBladeStalkerLine();

        SceneManager.LoadScene(LoadHelper.SceneToBeLoaded.ToString());
    }
}
