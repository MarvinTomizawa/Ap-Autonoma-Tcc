using Assets.Scripts.Exception;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class NodeEditor : IntegrationFieldsValidator
    {
#pragma warning disable 0649
        [SerializeField] private InputField processedWordField;
        [SerializeField] private InputField poppedTicketField;
        [SerializeField] private InputField pushedTicketField;
        [SerializeField] private Dropdown dropdown;
        [SerializeField] private Text nodeText;
        [SerializeField] private CommandIntegration[] commandFields;
#pragma warning restore 0649

        private const int MaxTicketSize = 13;

        private NodeIntegration _nodeIntegration;

        public void SelectNode(NodeIntegration nodeSelected)
        {
            if (IsNotValid) return;

            if (_nodeIntegration != null)
            {
                _nodeIntegration.UnSelect();    
            }
            
            _nodeIntegration = nodeSelected;
            _nodeIntegration.Select();
            nodeText.text = $"Nó selecionado: {nodeSelected.GetNodeText()}";
            dropdown.options = _nodeIntegration.GetNodes().Select(x => new Dropdown.OptionData(x)).ToList();
            dropdown.value = 0;
            ClearFields();
            SetTicketsInUi();
        }

        public void AddCommand()
        {
            if (IsNotValid) return;

            if (_nodeIntegration.NodeIndex + 1 > MaxTicketSize)
            {
                Debug.LogError("Tamanho máximo de tickets preenchido.");
                return;
            }

            if (processedWordField.text.Length == 0)
            {
                Debug.LogError("Palavra processada obrigatório.");
                return;
            }

            if (poppedTicketField.text.Length == 0)
            {
                Debug.LogError("Ticket retirado é obrigatório.");
                return;
            }

            var dropdownValue = dropdown.value;
            var processedWord = processedWordField.text[0];
            var poppedTicket = poppedTicketField.text[0];
            var pushedTicket = pushedTicketField.text;

            if (!_nodeIntegration.AddCommand(processedWord, poppedTicket, pushedTicket, dropdownValue)) return;
            
            ClearFields();
            SetTicketsInUi();
        }

        private void ClearFields()
        {
            processedWordField.text = "";
            poppedTicketField.text = "";
            pushedTicketField.text = "";
        }

        public void ClearCommands()
        {
            if (IsNotValid) return;

            _nodeIntegration.ClearCommands();
            ClearCommandFields();
        }

        public void RemoveCommand(CommandIntegration commandIntegration)
        {
            if (IsNotValid) return;

            _nodeIntegration.RemoveCommand(commandIntegration.Id);
            commandIntegration.Disable();
            SetTicketsInUi();
        }

        private void SetTicketsInUi()
        {
            if (IsNotValid) return;

            ClearCommandFields();

            var commands = _nodeIntegration.GetCommands();
            foreach (var commandField in commands.Select((command, index) => new { index, command }))
            {
                var command = commandField.command;
                var pushedTicket = command.PushedTickets.Select(x => x.Letter).ToList();
                commandFields[commandField.index]
                    .SetValues(command.Id, command.ProcessedLetter, command.PoppedTicket.Letter, string.Concat(pushedTicket), command.Node.NodeName, _nodeIntegration.GetNodeName());
            }
        }

        private void ClearCommandFields()
        {
            foreach (var commandField in commandFields)
            {
                commandField.Disable();
            }
        }

        protected override List<(object, string)> FieldsToBeValidated()
            => new List<(object, string)> { 
                (processedWordField, nameof(processedWordField)),
                (poppedTicketField, nameof(poppedTicketField)),
                (pushedTicketField, nameof(pushedTicketField)),
                (nodeText, nameof(nodeText))
            };
    }
}
