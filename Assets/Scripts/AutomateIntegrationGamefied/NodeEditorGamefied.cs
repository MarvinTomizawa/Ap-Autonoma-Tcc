using Assets.Scripts.Exception;
using AutomateIntegration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AutomateIntegrationGamefied
{
    public class NodeEditorGamefied : IntegrationFieldsValidator
    {

#pragma warning disable 0649
        [SerializeField] private Text nodeText;
        [SerializeField] private Dropdown industryField;
        [SerializeField] private Dropdown productField;
        [SerializeField] private Dropdown poppedTicketField;
        [SerializeField] private TicketGameIntegration[] pushedTicketsFields;
        [SerializeField] private CommandIntegration[] commandFields;
        [SerializeField] private GameObject addCommandScreen;
        [SerializeField] private ErrorScreen errorScreen;
#pragma warning restore 0649

        private IndustrySpritesMaps industrySpriteMap;
        private ProductSpriteMap productSpriteMap;
        private const int MaxTicketSize = 13;
        private NodeIntegration _nodeIntegration;

        private void Awake()
        {
            industrySpriteMap = FindObjectOfType<IndustrySpritesMaps>();
            productSpriteMap = FindObjectOfType<ProductSpriteMap>();    
        }

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
            industryField.options = _nodeIntegration.GetNodes().Select(x => new Dropdown.OptionData(x, industrySpriteMap.Map[x])).ToList();
            productField.options = _nodeIntegration.GetProducts().Select(x => new Dropdown.OptionData(productSpriteMap.Map[x])).ToList();
            industryField.value = 0;
            productField.value = 0;

            ClearFields();
            SetTicketsInUi();
        }

        public void AddCommand()
        {
            if (IsNotValid) return;

            if (_nodeIntegration.NodeIndex + 1 > MaxTicketSize)
            {
                errorScreen.ShowError(ExceptionsMessages.TamanhoMaximoPreenchido);
                return;
            }

            var dropdownValue = industryField.value;
            var processedWord = productField.value.ToString()[0];
            var poppedTicket = poppedTicketField.value.ToString()[0];

            string pushedTicket = "";

            foreach (var pushedTicketField in pushedTicketsFields.Where(x => x.IsEnabled))
            {
                pushedTicket += pushedTicketField.Value;
            }

            try
            {
                _nodeIntegration.AddCommand(processedWord, poppedTicket, pushedTicket, dropdownValue);
            }
            catch (System.Exception exception){
                errorScreen.ShowError(exception.Message);
                return;
            } 

            ResetTicketFields();
            SetTicketsInUi();
            addCommandScreen.SetActive(false);
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

        private void ClearFields()
        {
            foreach (var ticket in pushedTicketsFields)
            {
                ticket.Disable();
            }
        }

        private void ResetTicketFields()
        {
            poppedTicketField.value = 0;
            foreach (var ticket in pushedTicketsFields)
            {
                ticket.Reset();
            }
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
                (nodeText, nameof(nodeText)),
                (poppedTicketField, nameof(poppedTicketField)),
                (industryField, nameof(industryField)),
                (errorScreen, nameof(errorScreen)),
                (productField, nameof(productField)),
                (pushedTicketsFields, nameof(pushedTicketsFields)),
                (commandFields, nameof(commandFields)),
                (addCommandScreen, nameof(addCommandScreen)),
                (nodeText, nameof(nodeText))
            };
    }
}
