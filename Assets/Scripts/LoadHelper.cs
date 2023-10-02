using UnityEngine.SceneManagement;

internal class LoadHelper
{
    internal static SceneName SceneToBeLoaded { get; private set; } = SceneName.AAA_Finished;
    internal static float LoadDuration { get; private set; } = 1f;
    internal static string CURRENTSCENE_SAVE_PATH { get; } = "CurrentScene";
    internal static bool CurrentlyBossCombat { get; private set; }

    internal static void LoadSceneWithLoadingScreen(SceneName sceneName)
    {
        SceneToBeLoaded = sceneName;
        SceneManager.LoadSceneAsync(SceneName.LoadingScreen.ToString());
    }
}

internal enum SceneName
{
    A_MainMenu,
    AAA_Finished,
    LoadingScreen,
    StoryScene,
}

