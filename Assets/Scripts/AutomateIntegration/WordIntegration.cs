using System;
using UnityEngine;
using UnityEngine.UI;

public class WordIntegration : MonoBehaviour
{
#pragma warning disable 0469
    [SerializeField] private Text textField;
#pragma warning restore 0469

    public void Enable(string text)
    {
        gameObject.SetActive(true);
        textField.text = text;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetIsProcessed(bool processed)
    {
        if (processed)
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
    }
}
