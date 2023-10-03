using UnityEngine.SceneManagement;

internal class LoadHelper
{
    internal static SceneName SceneToBeLoaded { get; private set; } = SceneName.AAA_Finished;
    internal static float LoadDuration { get; private set; } = 1f;
    internal static string CURRENTSCENE_SAVE_PATH { get; } = "CurrentScene";
    internal static bool BladeStalkerLineDone { get; private set; }

    internal static void LoadSceneWithLoadingScreen(SceneName sceneName)
    {
        SceneToBeLoaded = sceneName;
        SceneManager.LoadSceneAsync(SceneName.LoadingScreen.ToString());
    }

    internal static void BladeStalkerLinePlayed()
    {
        BladeStalkerLineDone = true;
    }

    internal static void ResetBladeStalkerLine()
    {
        BladeStalkerLineDone = false;
    }
}

internal enum SceneName
{
    A_MainMenu,
    AAA_Finished,
    LoadingScreen,
    StoryScene,
}

