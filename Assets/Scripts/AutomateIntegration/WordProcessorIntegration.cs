using UnityEngine;
using UnityEngine.UI;

public class WordProcessorIntegration : MonoBehaviour
{
    [SerializeField]
    private InputField wordField;

    private WordProcessor wordProcessor;

    void Start()
    {
        wordProcessor = FindObjectOfType<WordProcessor>();

        ValidateFields();
    }

    public void AddWord()
    {
        var word = wordField.text;
        wordProcessor.AddWord(word);
    }

    public void TestCurrentWord()
    {
        var word = wordField.text;
        wordProcessor.Process(word);
    }

    public void ProcessAll()
    {
        wordProcessor.ProcessAll();
    }

    public void ResetProcessor()
    {
        wordProcessor.ResetProcessor();
    }

    private void ValidateFields()
    {
        if (wordProcessor is null)
        {
            Debug.LogError(BaseException.FieldNotInScene(nameof(wordProcessor))); 
        }

        if (wordField is null)
        {
            Debug.LogError(BaseException.FieldNotSetted(nameof(wordField), gameObject.name)); 
        }
    }

}
