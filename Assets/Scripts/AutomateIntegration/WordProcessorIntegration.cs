using System.Collections.Generic;
using AutomateBase;
using Exception;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class WordProcessorIntegration : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private InputField wordField;
        [SerializeField] private IList<GameObject> wordFields = new List<GameObject>();
#pragma warning restore 0649

        private readonly IList<Text> _wordTexts = new List<Text>();
        private WordProcessor _wordProcessor;

        public void Start()
        {
            _wordProcessor = FindObjectOfType<WordProcessor>();
            foreach (var field in _wordTexts)
            {
                var textField = field.GetComponent<Text>();
                if(textField is null)
                { 
                    Debug.Log($"Campo de texto não está atribuido para o objeto {field.name}");
                    continue;
                }
                
                _wordTexts.Add(textField);      
            }   
            
            ValidateFields();
        }

        public void AddWord()
        {
            var word = wordField.text;
            _wordProcessor.AddWord(word);
        }

        public void TestCurrentWord()
        {
            var word = wordField.text;
            _wordProcessor.Process(word);
        }

        public void ProcessAll()
        {
            _wordProcessor.ProcessAll();
        }

        public void ResetProcessor()
        {
            _wordProcessor.ResetProcessor();
        }

        private void ValidateFields()
        {
            if (wordFields.Count != _wordTexts.Count)
            {
                Debug.LogError(BaseException.FieldNotInScene(nameof(_wordProcessor)));
            }
            
            if (_wordProcessor is null)
            {
                Debug.LogError(BaseException.FieldNotInScene(nameof(_wordProcessor))); 
            }

            if (wordField is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(wordField), gameObject.name)); 
            }
        }

    }
}