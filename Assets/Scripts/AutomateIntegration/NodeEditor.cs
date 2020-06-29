using System.Linq;
using Exception;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class NodeEditor : MonoBehaviour
    {
        [SerializeField]
        private InputField processedWordField = null;

        [SerializeField]
        private InputField poppedTicketField = null;

        [SerializeField]
        private InputField pushedTicketField = null;

        [SerializeField]
        private Dropdown dropdown = null;

        [SerializeField]
        private Text nodeText = null;

        [SerializeField]
        private CommandIntegration[] commandFields = new CommandIntegration[MaxTicketSize];

        private const int MaxTicketSize = 13;

        private NodeIntegration _nodeIntegration;

        public void SelectNode(NodeIntegration nodeSelected)
        {
            _nodeIntegration = nodeSelected;
            nodeText.text = $"Nó selecionado: {nodeSelected.GetNodeText()}";
            dropdown.options = _nodeIntegration.GetNodes().Select(x => new Dropdown.OptionData(x)).ToList();
            dropdown.value = 0;
            ClearFields();
            SetTicketsInUi();
        }

        void Start()
        {
            ValidateFields();
        }

        public void AddCommand()
        {
            if (!NodeIsValid()) return;
            
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
            if (!NodeIsValid()) return;
            
            _nodeIntegration.ClearCommands();
            ClearCommandFields();
        }

        public void RemoveCommand(CommandIntegration commandIntegration)
        {
            commandIntegration.Disable();
            _nodeIntegration.RemoveCommand(commandIntegration.Id);
            SetTicketsInUi();
        }

        private bool NodeIsValid()
        {
            if (!(_nodeIntegration is null)) return true;
            
            Debug.LogError(BaseException.FieldNotSetted(nameof(_nodeIntegration), gameObject.name));
            return false;

        }

        private void SetTicketsInUi()
        {
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

        private void ValidateFields()
        {
            if (processedWordField is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(processedWordField), gameObject.name));
            }

            if (poppedTicketField is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(poppedTicketField), gameObject.name));
            }

            if (pushedTicketField is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(pushedTicketField), gameObject.name));
            }

            if (nodeText is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(nameof(nodeText), gameObject.name));
            }
        }
    }
}
