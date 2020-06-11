using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeEditor : MonoBehaviour
{
    [SerializeField]
    private InputField processedWordField;

    [SerializeField]
    private InputField poppedTicketField;

    [SerializeField]
    private InputField pushedTicketField;

    [SerializeField]
    private Dropdown dropdown;

    [SerializeField]
    private Text nodeText;

    [SerializeField]
    private CommandIntegration[] commandFields = new CommandIntegration[MAX_TICKET_SIZE];

    private const int MAX_TICKET_SIZE = 13;

    private NodeIntegration nodeIntegration;

    public void SelectNode(NodeIntegration nodeSelected)
    {
        nodeIntegration = nodeSelected;
        nodeText.text = $"Nó selecionado: {nodeSelected.GetNodeText()}";
        dropdown.options = nodeIntegration.GetNodes().Select(x => new Dropdown.OptionData(x)).ToList();
        ClearFields();
        SetTicketsInUi();
    }

    void Start()
    {
        ValidateFields();
    }

    public void AddCommand()
    {
        if (NodeIsValid())
        {
            if (nodeIntegration.NodeIndex + 1 > MAX_TICKET_SIZE)
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

            if (nodeIntegration.AddCommand(processedWord, poppedTicket, pushedTicket, dropdownValue)) 
            {
                ClearFields();
                SetTicketsInUi();
            }
        }
    }

    private void ClearFields()
    {
        processedWordField.text = "";
        poppedTicketField.text = "";
        pushedTicketField.text = "";
    }

    public void ClearCommands()
    {
        if (NodeIsValid())
        {
            nodeIntegration.ClearCommands();
            ClearCommandFields();
        }
    }

    public void RemoveCommand(CommandIntegration commandIntegration)
    {
        commandIntegration.Disable();
        nodeIntegration.RemoveCommand(commandIntegration.Id);
        SetTicketsInUi();
    }

    private bool NodeIsValid()
    {
        if (nodeIntegration is null)
        {
            Debug.LogError(BaseException.FieldNotSetted(nameof(nodeIntegration), gameObject.name));
            return false;
        }

        return true;
    }

    private void SetTicketsInUi()
    {
        ClearCommandFields();

        var commands = nodeIntegration.GetCommands();
        foreach (var commandField in commands.Select((command, index) => new { index, command }))
        {
            var command = commandField.command;
            var pushedTicket = command.PushedTickets.Select(x => x.Letter).ToList();
            commandFields[commandField.index]
                .SetValues(command.Id, command.ProcessedLetter, command.PoppedTicket.Letter, string.Concat(pushedTicket), command.Node.NodeName, nodeIntegration.GetNodeName());
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
