using Score;
using System;
using UiFunction;
using UnityEngine;

public class MenuStar : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Level configs")]
    [SerializeField] private int levelIndex;

    [Header("Level components")]
    [SerializeField] private TogglableObject levelStartButton;
    [SerializeField] private TogglableObject[] scoreStars;
    [SerializeField] private GameSaveManager gameSaveManager;
#pragma warning restore 0649

    public void Start()
    {
        ShowLevelStartButton();
        ShowScore();
    }

    private void ShowLevelStartButton()
    {
        if (gameSaveManager.LevelIsUnlocked(levelIndex))
        {
            levelStartButton.Enable();
        }
    }

    public void ShowScore()
    {
        var starCount = gameSaveManager.GetLevelScore(levelIndex);

        for (int i = 0; i < starCount; i++)
        {
            scoreStars[i].Enable();
        }
    }
}
