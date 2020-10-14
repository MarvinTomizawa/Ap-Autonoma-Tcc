using Assets.Scripts.Exception;
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
        public bool FinishedProcessing => _queueBehaviour.IsEmpty;
        public int CommandCount => Commands.Count;

        private QueueBehaviour _queueBehaviour;

        void Awake()
        {
            _queueBehaviour = FindObjectOfType<QueueBehaviour>();
        }

        public void ResetQueue()
        {
            _queueBehaviour.ResetQueue();
        }

        public bool ProcessLetter(char letter, out Node nextNode)
        {
            nextNode = this;
            var ticket = _queueBehaviour.GetNextTicket();

            if(ticket is null)
            {
                Debug.Log(ExceptionsMessages.FilaVazia);
                return false;
            }

            var command = Commands.FirstOrDefault(x => x.IsCommandForTicket(ticket) && x.ProcessedLetter == letter);

            if (command is null)
            {
                Debug.Log(ExceptionsMessages.SemComandoParaBrinquedoETicket(letter, ticket.Letter));
                return false;
            }

            nextNode = command.Node;

            return _queueBehaviour.ProcessItem(command.PoppedTicket, command.PushedTickets);
        }

        public void AddCommand(char processedWord, char poppedTicket, string pushedTicket, Node node,  int index)
        {
            IsValidCommand(processedWord, poppedTicket);
            Commands.Add(new Command(processedWord, poppedTicket, pushedTicket, node, index));
        }

        private void IsValidCommand(char processedWord, char poppedTicket)
        {
            var command = Commands.FirstOrDefault(x => x.ProcessedLetter == processedWord && x.IsCommandForTicket(ticketLetter: poppedTicket));

            if (command != null)
            {
                throw new System.Exception(ExceptionsMessages.TicketEBrinquedoJaExistente);
            }
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
