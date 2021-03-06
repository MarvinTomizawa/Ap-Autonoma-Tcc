﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AutomateIntegrationGamefied
{
    internal class TicketGameIntegration : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private Dropdown valueField;
        [SerializeField] private GameObject[] disabledGameObjects;
        [SerializeField] private GameObject[] enabledGameObjects;
#pragma warning restore 0649

        public int Value => int.Parse(valueField.options[valueField.value].text) - 1;
        public bool IsEnabled;

        public void Disable()
        {
            IsEnabled = false;

            AtualizeUi();
        }

        public void Reset()
        {
            valueField.value = 0;
            Disable();
        }

        public void Enable()
        {
            IsEnabled = true;

            AtualizeUi();
        }

        private void AtualizeUi()
        {
            foreach (var enabledItem in enabledGameObjects)
            {
                enabledItem.SetActive(IsEnabled);
            }

            foreach (var disableItem in disabledGameObjects)
            {
                disableItem.SetActive(!IsEnabled);
            }
        }
    }
}