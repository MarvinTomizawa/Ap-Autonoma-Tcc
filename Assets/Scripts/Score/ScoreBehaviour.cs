using AutomateBase;
using UiFunction;
using UnityEngine;
using UnityEngine.UI;
namespace Score
{
    public class ScoreBehaviour : MonoBehaviour
    {
#pragma warning disable 0649
        [Header("Score fields")]
        [SerializeField] private GameObject scoreScreen;
        [SerializeField] private TogglableObject star1;
        [SerializeField] private TogglableObject star2;
        [SerializeField] private TogglableObject star3;
        [SerializeField] private InputField rightProductsField;
        [SerializeField] private InputField totalProductsField;
        [SerializeField] private InputField totalCommandsField;
        [SerializeField] private InputField minimumCommandsField;

        [Header("Exercise Values")]
        [SerializeField] private int minimumCommands;
        [SerializeField] private int wordCount;
        [SerializeField] private int levelNumber;

        [Header("Game Fields")]
        [SerializeField] private Node[] nodes;
        [SerializeField] private FinalRunner FinalRunner;
        [SerializeField] private GameSaveManager gameSaveManager;
#pragma warning restore 0649

        public void ShowScore()
        {
            scoreScreen.SetActive(true);

            int commandQuantity = 0;
            foreach (var item in nodes)
            {
                commandQuantity += item.CommandCount;
            }

            var correctQuantity = FinalRunner.CorrectQuantity;

            ShowValuesInFields(correctQuantity, commandQuantity);
            ShowStars(correctQuantity, commandQuantity);
        }

        private void ShowValuesInFields(int correctQuantity, int commandQuantity)
        {
            rightProductsField.text = correctQuantity.ToString();
            totalProductsField.text = wordCount.ToString();
            totalCommandsField.text = commandQuantity.ToString();
            minimumCommandsField.text = minimumCommands.ToString();
        }

        private void ShowStars(int correctWords, int commandQuantity)
        {
            if (correctWords < wordCount / 2)
            {
                return;
            }

            star1.Enable();

            if (correctWords != wordCount)
            {
                gameSaveManager.SaveLevelScore(1, levelNumber);
                return;
            }

            star2.Enable();

            if (commandQuantity != minimumCommands)
            {
                gameSaveManager.SaveLevelScore(2, levelNumber);
                return;
            }

            star3.Enable();
            gameSaveManager.SaveLevelScore(3, levelNumber);
        }
    }
}
