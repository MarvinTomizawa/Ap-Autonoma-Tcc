using UnityEngine;

namespace AutomateIntegration
{
    public class Toggler : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private GameObject[] displayedObjects;
        [SerializeField] private GameObject[] hiddenObjects;
        [SerializeField] private GameObject[] alwaysHideObjects;
        [SerializeField] private bool isHidden;
#pragma warning restore 0649

        private void Start()
        {
            ApplyDisplay();
        }

        private void ApplyDisplay()
        {
            if (isHidden)
            {
                HideObjects();
            }
            else
            {
                DisplayObjects();
            }
        }

        public void DisplayObjects()
        {
            isHidden = false;

            foreach (var item in hiddenObjects)
            {
                item.SetActive(false);
            }

            foreach (var item in displayedObjects)
            {
                item.SetActive(true);
            }

            DisableAlwaysDisabledObjects();
        }

        public void HideObjects()
        {
            isHidden = true;

            foreach (var item in displayedObjects)
            {
                item.SetActive(false);
            }

            foreach (var item in hiddenObjects)
            {
                item.SetActive(true);
            }

            DisableAlwaysDisabledObjects();
        }

        private void DisableAlwaysDisabledObjects()
        {
            foreach (var hiddenObject in alwaysHideObjects)
            {
                hiddenObject.SetActive(false);
            }
        }

        public void Toggle()
        {
            isHidden = !isHidden;
            ApplyDisplay();
        }
    }
}