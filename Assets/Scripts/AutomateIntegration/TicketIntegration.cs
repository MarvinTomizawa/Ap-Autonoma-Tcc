using System;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class TicketIntegration : MonoBehaviour
    {
        private Dropdown _text;

        public void Awake()
        {
            _text = gameObject.GetComponentInChildren<Dropdown>(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    
        public void Enable(char ticket)
        {
            gameObject.SetActive(true);
            _text.value = int.Parse(ticket.ToString());
        }
    }
}