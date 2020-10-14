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
            wordsToBeTested[i].SetIsProcessed(processed);

            if (processed)
            {
                CorrectQuantity++;
            }
        }

        rightWordsField.text = CorrectQuantity.ToString().PadLeft(2,'0');

        wordProcessor.ResetProcessor();
    }
}
