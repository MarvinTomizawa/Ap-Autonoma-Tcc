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
        [SerializeField] private TestWordAddScript wordInput;
        [SerializeField] private List<WordIntegration> wordsToBeTested = new List<WordIntegration>();
        [SerializeField] private WordProcessor _wordProcessor;
#pragma warning restore 0649

        private readonly IList<string> _wordTexts = new List<string>();
        
        public void Awake()
        {
            _wordProcessor.InnitNode();
        }

        public void AddWord()
        {
            if (IsNotValid) return;

            var word = wordInput.TakeWord();
            if (_wordTexts.Count < wordsToBeTested.Count && !string.IsNullOrEmpty(word))
            {
                _wordTexts.Add(word);
                _wordProcessor.AddWord(word);
                AtualizeWords();
            }
        }

        public void ProcessAllAdded()
        {
            if (IsNotValid) return;

            for (int i = 0; i < _wordTexts.Count; i++)
            {
                var processed = _wordProcessor.Process(_wordTexts[i]);
                wordsToBeTested[i].SetIsProcessed(processed);
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
            _wordTexts.Clear();
            AtualizeWords();
        }

        private void AtualizeWords()
        {
            foreach (var wordField in wordsToBeTested)
            {
                wordField.Disable();
            }

            for (int i = 0; i < _wordTexts.Count; i++)
            {
                wordsToBeTested[i].Enable(_wordTexts[i]);
            }
        }

        protected override List<(object, string)> FieldsToBeValidated()
            => new List<(object, string)> { 
                (_wordProcessor, nameof(_wordProcessor))
            };
    }
}