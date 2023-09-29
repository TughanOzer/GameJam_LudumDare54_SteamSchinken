using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string destinationScene = "None") {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(destinationScene);
        
        if(destinationScene == "None" || destinationScene == null || buildIndex == -1) {
            Debug.LogWarning("SceneLoader Error! Please enter a correct scene name or make sure the scene is added in the build settings or disable the SceneLoader script");
        }
        else SceneManager.LoadScene(destinationScene);
    }
}
