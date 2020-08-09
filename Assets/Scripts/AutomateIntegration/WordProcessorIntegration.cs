using System;
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
        [SerializeField] private List<WordIntegration> wordFields = new List<WordIntegration>();
#pragma warning restore 0649

        private readonly IList<string> _wordTexts = new List<string>();
        private WordProcessor _wordProcessor;

        public void Start()
        {
            _wordProcessor = FindObjectOfType<WordProcessor>();
            ValidateFields();
        }

        public void AddWord()
        {
            var word = wordField.text;
            if (_wordTexts.Count < wordFields.Count && !string.IsNullOrEmpty(word))
            {
                _wordTexts.Add(word);
                _wordProcessor.AddWord(word);
                AtualizeWords();
            }

            wordField.text = "";
        }

        public void ProcessAllAdded()
        {
            for (int i = 0; i < _wordTexts.Count; i++)
            {
                var processed = _wordProcessor.Process(_wordTexts[i]);
                wordFields[i].SetIsProcessed(processed);
            }
        }

        public void ProcessAll()
        {
            _wordProcessor.ProcessAll();
        }

        public void ResetProcessor()
        {
            _wordProcessor.ResetProcessor();
            AtualizeWords();
        }

        private void AtualizeWords()
        {
            foreach (var wordField in wordFields)
            {
                wordField.Disable();
            }

            for (int i = 0; i < _wordTexts.Count; i++)
            {
                wordFields[i].Enable(_wordTexts[i]);
            }
        }

        private void ValidateFields()
        {
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