using UnityEngine;

public class Toggler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] displayedObjects;

    [SerializeField]
    private GameObject[] hiddenObjects;

    [SerializeField]
    private bool isHidden = false;


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
    }

    public void Toggle()
    {
        isHidden = !isHidden;
        ApplyDisplay();
    }
}
