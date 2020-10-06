using Assets.Scripts.Exception;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Dropdown[] products;
#pragma warning restore 0649

    public void Enable(string text)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < text.Length; i++)
        {
            products[i].value = int.Parse(text[i].ToString());
        }
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
