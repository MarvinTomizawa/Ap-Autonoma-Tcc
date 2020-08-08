using System.Collections.Generic;
using System.Linq;
using AutomateBase;
using Exception;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class TestRunnerIntegration : MonoBehaviour
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
        private NodeIntegration ActualNode => _wordProcessor.ActualNode.gameObject.GetComponent<NodeIntegration>();
        
        public void Start()
        {
            _wordProcessor = FindObjectOfType<WordProcessor>();
            _queueBehaviour = FindObjectOfType<QueueBehaviour>();
            IsValid();
        }

        public void StartTestRunner()
        {
            if (!IsValid()) return;
            
            _wordProcessor.InnitNode();
            ActualNode.Select();
            testRunnerWindow.SetActive(true);
            
            _queueBehaviour.ResetQueue();
            ActualNode.Select();

            testRunnerProcessedWord.text = processedWordField.text;
            UpdateNodeAndNextLetter();
            
            ShowCurrentTickets();
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

        public void ProcessNextLetter()
        {
            if (!IsValid())
            {
                return;
            }
            
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
                Debug.LogError("Não foi possivel processar a palavra");
                return;
            }
            
            testRunnerProcessedWord.text = wordToBeProcessed.Length >= 1 ? wordToBeProcessed.Substring(1) : "";
            UpdateNodeAndNextLetter();
            ShowCurrentTickets();
        }

        public void StopTestRunner()
        {
            if(!IsValid()) return;
            
            _queueBehaviour.ResetQueue();
            testRunnerWindow.SetActive(false);
        }
        
        private bool IsValid()
        {
            if (processedWordField is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(processedWordField), gameObject.name));
                return false;
            }
            
            if (actualNodeField is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(processedWordField), gameObject.name));
                return false;
            }
            
            if (nextWordField is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(processedWordField), gameObject.name));
                return false;
            }

            if (_wordProcessor is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(_wordProcessor), gameObject.name));
                return false;
            }
            
            if (testRunnerWindow is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(testRunnerWindow), gameObject.name));
                return false;
            }

            if (testRunnerProcessedWord is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(testRunnerProcessedWord), gameObject.name));
                return false;
            }
            
            if (_queueBehaviour is null)
            {
                Debug.LogError(BaseException.FieldNotInScene(nameof(_queueBehaviour)));   
                return false;
            }

            if (ticketFields.Count == 0)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(ticketFields), gameObject.name));
                return false;
            }

            return true;
        }
    }
}