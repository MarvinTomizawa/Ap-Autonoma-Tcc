using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        LoadLevel("MenuScene");
    }

    public void LoadFirstLevel()
    {
        LoadLevel("BaseSceneGamified");
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
