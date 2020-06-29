using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutomateBase
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        public string NodeName;
        public IList<Command> Commands { get; private set; } = new List<Command>();
        private QueueBehaviour _queueBehaviour;

        void Start()
        {
            _queueBehaviour = FindObjectOfType<QueueBehaviour>();
        }

        public bool ProcessLetter(char letter, out Node nextNode)
        {
            nextNode = this;
            var ticket = _queueBehaviour.GetNextTicket();

            if(ticket is null)
            {
                Debug.Log("A fila está vazia");
                return false;
            }

            var command = Commands.FirstOrDefault(x => x.IsCommandForTicket(ticket) && x.ProcessedLetter == letter);

            if (command is null)
            {
                Debug.Log($"Não possui comando para a letra {letter} e ticket {ticket.Letter}.");
                return false;
            }

            nextNode = command.Node;

            return _queueBehaviour.ProcessItem(command.PoppedTicket, command.PushedTickets);
        }

        public bool AddCommand(char processedWord, char poppedTicket, string pushedTicket, Node node,  int index)
        {
            if (!IsValidCommand(processedWord, poppedTicket))
            {
                return false;
            }

            Commands.Add(new Command(processedWord, poppedTicket, pushedTicket, node, index));

            return true;
        }

        private bool IsValidCommand(char processedWord, char poppedTicket)
        {
            var command = Commands.FirstOrDefault(x => x.ProcessedLetter == processedWord && x.IsCommandForTicket(ticketLetter: poppedTicket));

            if (command != null)
            {
                Debug.LogError($"Já existe um comando para palavra: {processedWord} e ticket: {poppedTicket}");
                return false;
            }

            return true;
        }

        public bool RemoveCommand(Guid id)
        {
            var command = Commands.FirstOrDefault(x => x.Id == id);

            if (command == null) return false;
            
            Commands.Remove(command);
            return true;

        }

        public void ClearCommands()
        {
            Commands.Clear();
        }
    }
}
