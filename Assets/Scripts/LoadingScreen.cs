using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitForLoadingScreen());
    }

    private IEnumerator WaitForLoadingScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(LoadHelper.SceneToBeLoaded.ToString());
    }
}
