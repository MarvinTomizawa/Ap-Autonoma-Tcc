using Assets.Scripts.Exception;
using AutomateBase;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class WordProcessorIntegration : IntegrationFieldsValidator
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
        }

        public void AddWord()
        {
            if (IsNotValid) return;

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
            if (IsNotValid) return;

            for (int i = 0; i < _wordTexts.Count; i++)
            {
                var processed = _wordProcessor.Process(_wordTexts[i]);
                wordFields[i].SetIsProcessed(processed);
            }
        }

        public void ProcessAll()
        {
            if (IsNotValid) return;

            _wordProcessor.ProcessAll();
        }

        public void ResetProcessor()
        {
            if (IsNotValid) return;

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

        protected override List<(object, string)> FieldsToBeValidated()
            => new List<(object, string)> { 
                (_wordProcessor, nameof(_wordProcessor)),
                (wordField, nameof(wordField)) 
            };
    }
}