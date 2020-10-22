using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        LoadLevel("MainMenu");
    }

    public void LoadLevelsScene()
    {
        LoadLevel("LevelScene");
    }

    public void LoadFirstLevel()
    {
        LoadLevel("Level1");
    }

    public void LoadSecondLevel()
    {
        LoadLevel("Level2");
    }

    public void LoadThirdLevel()
    {
        LoadLevel("Level3");
    }

    private void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
