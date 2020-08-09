using Assets.Scripts.Exception;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class CommandIntegration : IntegrationFieldsValidator
    {
#pragma warning disable 0649
        [SerializeField] private Text processedWord;
        [SerializeField] private Text poppedTicket;
        [SerializeField] private Text pushedTickets;
        [SerializeField] private Text originNode;
        [SerializeField] private Text destinyNode;
#pragma warning restore 0649

        public Guid Id { get; private set; }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void SetValues(Guid id, char processedWordValue, char poppedTicketValue, string pushedTicketsValue, string destinyNodeValue, string originNodeValue)
        {
            if (IsNotValid) return;

            Id = id;
            gameObject.SetActive(true);
            pushedTickets.text = pushedTicketsValue;
            poppedTicket.text = poppedTicketValue.ToString();
            processedWord.text = processedWordValue.ToString();
            originNode.text = originNodeValue;
            destinyNode.text = destinyNodeValue;
        }

        protected override List<(object, string)> FieldsToBeValidated()
            => new List<(object, string)> {
                (processedWord, nameof(processedWord)),
                (poppedTicket, nameof(poppedTicket)),
                (pushedTickets, nameof(pushedTickets)),
                (originNode, nameof(originNode)),
                (destinyNode, nameof(destinyNode))
            };
    }
}