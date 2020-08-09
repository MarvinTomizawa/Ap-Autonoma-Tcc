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
        [SerializeField] private NodeIntegration[] nodes;
#pragma warning restore 0649

        private void Start()
        {
            ApplyDisplay();
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

        public void Toggle()
        {
            isHidden = !isHidden;
            ApplyDisplay();
        }

        public void UnSelectNodes()
        {
            foreach (var node in nodes)
            {
                node.UnSelect();
            }
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

        private void DisableAlwaysDisabledObjects()
        {
            foreach (var hiddenObject in alwaysHideObjects)
            {
                hiddenObject.SetActive(false);
            }
        }
    }
}