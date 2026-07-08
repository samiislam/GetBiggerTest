using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{ 
    const int GAMEOVER_SCENE_INDEX = 3;
    const int MENU_SCENE_INDEX = 0;
    const int LEVELS_SCENE_INDEX = 1;
    const int WINSCREEN_SCENE_INDEX = 4;

    SelectedScene mySelectedScene;

    void Start()
    {
        mySelectedScene = FindAnyObjectByType<SelectedScene>();
    }
    public void LoadGameModeScene()
    {
        StartCoroutine(WaitforSoundGameMode());
    }

    IEnumerator WaitforSoundGameMode()
    {
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene("GameModes");
    }

    public void LoadUpgrades1()
    {
        StartCoroutine(WaitForSoundUpgrades1());
    }
    IEnumerator WaitForSoundUpgrades1()
    {
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene("Upgrades1");
    }
    public void LoadUpgrades2()
    {
        StartCoroutine(WaitForSoundUpgrades2());
    }
    IEnumerator WaitForSoundUpgrades2()
    {
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene("Upgrades2");
    }

    public void LoadRestartScene()
    {
        StartCoroutine(WaitforSoundRestart());
    }

    IEnumerator WaitforSoundRestart()
    {
        int currentSceneIndex = mySelectedScene.GetLastScene();
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitForSoundNextlevel());
    }

    IEnumerator WaitForSoundNextlevel()
    {
        UpgradeManager.SaveUpgradeSettings();
        int currentSceneIndex = mySelectedScene.GetLastScene();
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(WaitForSoundMainMenu());
    }

    IEnumerator WaitForSoundMainMenu()
    {
        UpgradeManager.SaveUpgradeSettings();
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevels()
    {
        StartCoroutine(WaitForSoundLevels());
    }

    IEnumerator WaitForSoundLevels()
    {
        UpgradeManager.SaveUpgradeSettings();
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene("Levels");
    }

    public void LoadPlayerSelectionScene()
    {
        StartCoroutine(WaitForSoundPlayerSelection());
    }

    IEnumerator WaitForSoundPlayerSelection() 
    {
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene("PlayerSelection");
    }

    public void CallLevelLoad(string level)
    {
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(string level) 
    {
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(level);
    }
}
