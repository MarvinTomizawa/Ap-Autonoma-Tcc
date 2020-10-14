using UnityEngine;

namespace UiFunction
{
    public class TogglableObject : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private GameObject EnabledImage;
        [SerializeField] private GameObject DisabledImage;
        [SerializeField] private bool isEnabled;
#pragma warning restore 0649

        public void Start()
        {
            ShowImage();
        }

        private void ShowImage()
        {
            EnabledImage.SetActive(isEnabled);
            DisabledImage.SetActive(!isEnabled);
        }

        public void Enable()
        {
            isEnabled = true;
            ShowImage();
        }
    }

}