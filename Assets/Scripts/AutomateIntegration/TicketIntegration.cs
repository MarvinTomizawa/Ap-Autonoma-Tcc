using System;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class TicketIntegration : MonoBehaviour
    {
        private Text _text;

        public void Awake()
        {
            _text = gameObject.GetComponentInChildren<Text>(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    
        public void Enable(char ticket)
        {
            gameObject.SetActive(true);
            _text.text = Convert.ToString(ticket);
        }
    }
}