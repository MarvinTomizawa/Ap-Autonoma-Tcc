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
        [SerializeField] private Dropdown processedWord;
        [SerializeField] private Dropdown poppedTicket;
        [SerializeField] private GameObject[] pushedTicketArray;
        [SerializeField] private Dropdown pushedTicket1;
        [SerializeField] private Dropdown pushedTicket2;
        [SerializeField] private Dropdown pushedTicket3;
        [SerializeField] private Dropdown pushedTicket4;
        [SerializeField] private Dropdown originNode;
        [SerializeField] private Dropdown destinyNode;
#pragma warning restore 0649
        private IndustrySpritesMaps _industrySpritesMaps;

        public void Awake()
        {
            _industrySpritesMaps = FindObjectOfType<IndustrySpritesMaps>();
        }

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
            SetPushedTickets(pushedTicketsValue);
            poppedTicket.value = int.Parse(poppedTicketValue.ToString());
            processedWord.value = int.Parse(processedWordValue.ToString());
            originNode.value = _industrySpritesMaps.MapValue[originNodeValue];
            destinyNode.value = _industrySpritesMaps.MapValue[destinyNodeValue];
        }

        private void SetPushedTickets(string pushedTicketsValue)
        {
            var lenght = pushedTicketsValue.Length;

            foreach (var item in pushedTicketArray)
            {
                item.SetActive(false);
            }

            if (lenght == 0) return;
            pushedTicketArray[0].SetActive(true);
            pushedTicket1.value = int.Parse(pushedTicketsValue[0].ToString());

            if (lenght == 1) return;
            pushedTicketArray[1].SetActive(true);
            pushedTicket2.value = int.Parse(pushedTicketsValue[1].ToString());

            if (lenght == 2) return;
            pushedTicketArray[2].SetActive(true);
            pushedTicket3.value = int.Parse(pushedTicketsValue[2].ToString());

            if (lenght == 3) return;
            pushedTicketArray[3].SetActive(true);
            pushedTicket4.value = int.Parse(pushedTicketsValue[3].ToString());
        }

        protected override List<(object, string)> FieldsToBeValidated()
            => new List<(object, string)> {
                (processedWord, nameof(processedWord)),
                (poppedTicket, nameof(poppedTicket)),
                (pushedTicket1, nameof(pushedTicket1)),
                (pushedTicket2, nameof(pushedTicket2)),
                (pushedTicket3, nameof(pushedTicket3)),
                (pushedTicket4, nameof(pushedTicket4)),
                (originNode, nameof(originNode)),
                (destinyNode, nameof(destinyNode))
            };
    }
}