using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedScene : MonoBehaviour
{
    int savedSceneIndex = 0;
    void Start()
    {
        
    }

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if(FindObjectsByType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    {
        var activeScene = SceneManager.GetActiveScene();

        savedSceneIndex = activeScene.buildIndex - 1;
        
        if (6 != activeScene.buildIndex && 7 != activeScene.buildIndex)
        {
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }

    public int GetLastScene()
    {
        return PlayerPrefs.GetInt("SavedScene");
    }
}
