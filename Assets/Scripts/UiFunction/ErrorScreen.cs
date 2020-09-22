using System;
using UnityEngine;
using UnityEngine.UI;

public class ErrorScreen : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject errorScreenObject;
    [SerializeField] private Text errorText;
#pragma warning disable 0649
    
    public void ShowError(string errorMessage)
    {
        errorScreenObject.SetActive(true);
        errorText.text = errorMessage;
    }

    internal void ShowError(object tamanhoMaximoPreenchido)
    {
        throw new NotImplementedException();
    }
}
