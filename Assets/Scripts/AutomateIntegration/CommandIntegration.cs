using System;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class CommandIntegration : MonoBehaviour
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
            Id = id;
            gameObject.SetActive(true);
            pushedTickets.text = pushedTicketsValue;
            poppedTicket.text = poppedTicketValue.ToString();
            processedWord.text = processedWordValue.ToString();
            originNode.text = originNodeValue;
            destinyNode.text = destinyNodeValue;
        }
    }
}