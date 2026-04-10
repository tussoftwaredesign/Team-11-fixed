using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenes : MonoBehaviour
{
    [Tooltip("Scene name to load after the cutscene. If empty, loads the next scene in build order.")]
    public string nextSceneName;

    [Tooltip("Time in seconds before the scene automatically changes.")]
    public float autoChangeDelay = 50f;

    private float timer;
    private bool hasChangedScene;

    void Update()
    {
        if (hasChangedScene)
            return;

        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || timer >= autoChangeDelay)
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        hasChangedScene = true;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        int nextSceneIndex = currentScene.buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Cutscenes: No next scene in build settings to load.");
        }
    }
}
