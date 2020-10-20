using AutomateBase;
using UnityEngine;
using UnityEngine.UI;

public class FinalRunner : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private InputField rightWordsField;
    [SerializeField] private InputField totalWordsField;
    [SerializeField] private WordIntegration[] wordsToBeTested;
    [SerializeField] private WordProcessor wordProcessor;
#pragma warning disable 0649

    public int CorrectQuantity { get; private set; }

    public void Start()
    {
        SetWordsInFinalRunner();
        totalWordsField.text = wordsToBeTested.Length.ToString().PadLeft(2,'0');
    }

    private void SetWordsInFinalRunner()
    {
        foreach (var field in wordsToBeTested)
        {
            wordProcessor.AddWord(field.GetWord());
        }
    }

    public void TestWords()
    {
        CorrectQuantity = 0;

        for (int i = 0; i < wordsToBeTested.Length; i++)
        {
            var processed = wordProcessor.Process(wordsToBeTested[i].GetWord());
            var isRight = processed == wordsToBeTested[i].ShouldBeCorrect;

            wordsToBeTested[i].SetIsProcessed(isRight);

            if (isRight)
            {
                CorrectQuantity++;
            }
        }

        rightWordsField.text = CorrectQuantity.ToString().PadLeft(2,'0');

        wordProcessor.ResetProcessor();
    }
}
