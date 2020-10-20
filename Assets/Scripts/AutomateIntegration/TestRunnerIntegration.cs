using Assets.Scripts.Exception;
using AutomateBase;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class TestRunnerIntegration : IntegrationFieldsValidator
    {
#pragma warning disable 0649
        [SerializeField] private Dropdown actualNodeField;
        [SerializeField] private Dropdown nextWordField;
        [SerializeField] private TestWordAddScript testWordAddScript;
        [SerializeField] private Dropdown[] testRunnerProcessedLetters;
        [SerializeField] private List<TicketIntegration> ticketFields = new List<TicketIntegration>();
        [SerializeField] private List<CommandIntegration> commandsFields = new List<CommandIntegration>();
        [SerializeField] private GameObject testRunnerWindow;
        [SerializeField] private GameObject _successScreen;
#pragma warning restore 0649

        private IndustrySpritesMaps _industrySpritesMaps;
        private string wordToBeProcessed;
        private QueueBehaviour _queueBehaviour;
        private WordProcessor _wordProcessor;
        private ErrorScreen _errorScreen;
        private NodeIntegration ActualNode => _wordProcessor.ActualNode.gameObject.GetComponent<NodeIntegration>();
        
        public void Start()
        {
            _industrySpritesMaps = FindObjectOfType<IndustrySpritesMaps>();
            _errorScreen = FindObjectOfType<ErrorScreen>();
            _wordProcessor = FindObjectOfType<WordProcessor>();
            _queueBehaviour = FindObjectOfType<QueueBehaviour>();
        }

        public void StartTestRunner()
        {
            var processedWord = testWordAddScript.TakeWord();
            if (IsNotValid || string.IsNullOrEmpty(processedWord)) return;

            _wordProcessor.InnitNode();
            ActualNode.Select();
            testRunnerWindow.SetActive(true);
            _queueBehaviour.ResetQueue();

            SetWordToBeProcessed(processedWord);
            UpdateNodeAndNextLetter();

            ShowCurrentTickets();
        }

        public void ProcessNextLetter()
        {
            if (IsNotValid) return;
            string wordToBeProcessed = GetWordToBeProcessed();

            if (wordToBeProcessed.Length == 0)
            {
                _successScreen.SetActive(true);
                return;
            }

            ActualNode.UnSelect();

            var letter = wordToBeProcessed[0];
            var processed = _wordProcessor.ProcessLetter(letter);

            ActualNode.Select();

            if (!processed)
            {
                _errorScreen.ShowError(ExceptionsMessages.NaoFoiPossivelProcessar);
                return;
            }

            SetWordToBeProcessed(wordToBeProcessed.Length >= 1 ? wordToBeProcessed.Substring(1) : "");
            UpdateNodeAndNextLetter();
            ShowCurrentTickets();
        }

        public void StopTestRunner()
        {
            if(IsNotValid) return;
            
            _queueBehaviour.ResetQueue();
            testRunnerWindow.SetActive(false);
        }

        private void SetWordToBeProcessed(string processedWord)
        {
            foreach (var testRunnerLetter in testRunnerProcessedLetters)
            {
                testRunnerLetter.value = 0;
            }

            wordToBeProcessed = processedWord;
            for (int i = 0; i < processedWord.Length; i++)
            {
                testRunnerProcessedLetters[i].value = int.Parse(processedWord[i].ToString());
            }
        }

        private string GetWordToBeProcessed()
        {
            return wordToBeProcessed;
        }

        private void UpdateNodeAndNextLetter()
        {
            actualNodeField.value = _industrySpritesMaps.MapValue[_wordProcessor.ActualNode.NodeName];
            var wordToBeProcessed = GetWordToBeProcessed();

            nextWordField.value = wordToBeProcessed.Length >= 1 ? int.Parse(wordToBeProcessed.Substring(0,1)) : 0;

            foreach (var commandIntegration in commandsFields)
            {
                commandIntegration.Disable();
            }

            var commands = _wordProcessor.ActualNode.Commands;

            for (var i = 0; i < commands.Count; i++)
            {
                var command = commands[i];
                var id = command.Id;
                var processedWordValue = command.ProcessedLetter;
                var poppedTicket = command.PoppedTicket.Letter;
                var pushedTickets = new string(command.PushedTickets.Select(x => x.Letter).ToArray());
                var destinyNode = command.Node.NodeName;
                var actualNodeName = _wordProcessor.ActualNode.NodeName;
                commandsFields[i].SetValues(id, processedWordValue, poppedTicket, pushedTickets, destinyNode, actualNodeName);
            }
        }

        private void ShowCurrentTickets()
        {
            var tickets = _queueBehaviour.GetTicketsAsList();
            foreach (var ticketIntegration in ticketFields)
            {
                ticketIntegration.Disable();
            }

            for (var i = 0; i < tickets.Count; i++)
            {
                ticketFields[i].Enable(tickets[i].Letter);
            }
        }
        
        protected override List<(object, string)> FieldsToBeValidated()
            => new List<(object, string)> {
                (testWordAddScript, nameof(testWordAddScript)),
                (actualNodeField, nameof(actualNodeField)),
                (nextWordField, nameof(nextWordField)),
                (testRunnerWindow, nameof(testRunnerWindow)),
                (testRunnerProcessedLetters, nameof(testRunnerProcessedLetters)),
                (_wordProcessor, nameof(_wordProcessor)),
                (_queueBehaviour, nameof(_queueBehaviour))
            };
    }
}