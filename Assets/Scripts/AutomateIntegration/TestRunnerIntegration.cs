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
        [SerializeField] private InputField actualNodeField;
        [SerializeField] private InputField nextWordField;
        [SerializeField] private InputField processedWordField;
        [SerializeField] private InputField testRunnerProcessedWord;
        [SerializeField] private List<TicketIntegration> ticketFields = new List<TicketIntegration>();
        [SerializeField] private List<CommandIntegration> commandsFields = new List<CommandIntegration>();
        [SerializeField] private GameObject testRunnerWindow;
        
#pragma warning restore 0649
        
        private QueueBehaviour _queueBehaviour;
        private WordProcessor _wordProcessor;
        private ErrorScreen _errorScreen;
        private NodeIntegration ActualNode => _wordProcessor.ActualNode.gameObject.GetComponent<NodeIntegration>();
        
        public void Start()
        {
            _errorScreen = FindObjectOfType<ErrorScreen>();
            _wordProcessor = FindObjectOfType<WordProcessor>();
            _queueBehaviour = FindObjectOfType<QueueBehaviour>();
        }

        public void StartTestRunner()
        {
            if (IsNotValid || string.IsNullOrEmpty(processedWordField.text)) return;
            
            _wordProcessor.InnitNode();
            ActualNode.Select();
            testRunnerWindow.SetActive(true);
            _queueBehaviour.ResetQueue();

            testRunnerProcessedWord.text = processedWordField.text;
            UpdateNodeAndNextLetter();
            
            ShowCurrentTickets();
        }
        
        public void ProcessNextLetter()
        {
            if (IsNotValid) return;
            
            var wordToBeProcessed = testRunnerProcessedWord.text;
            
            if (wordToBeProcessed.Length == 0)
            {
                Debug.LogWarning("Todas palavras ja foram processadas");
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
            
            testRunnerProcessedWord.text = wordToBeProcessed.Length >= 1 ? wordToBeProcessed.Substring(1) : "";
            UpdateNodeAndNextLetter();
            ShowCurrentTickets();
        }

        public void StopTestRunner()
        {
            if(IsNotValid) return;
            
            _queueBehaviour.ResetQueue();
            testRunnerWindow.SetActive(false);
        }

        private void UpdateNodeAndNextLetter()
        {
            actualNodeField.text = _wordProcessor.ActualNode.NodeName;
            nextWordField.text = testRunnerProcessedWord.text.Length >= 1 ? testRunnerProcessedWord.text.Substring(0,1) : "";

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
                (processedWordField, nameof(processedWordField)),
                (actualNodeField, nameof(actualNodeField)),
                (nextWordField, nameof(nextWordField)),
                (testRunnerWindow, nameof(testRunnerWindow)),
                (testRunnerProcessedWord, nameof(testRunnerProcessedWord)),
                (_wordProcessor, nameof(_wordProcessor)),
                (_queueBehaviour, nameof(_queueBehaviour))
            };
    }
}