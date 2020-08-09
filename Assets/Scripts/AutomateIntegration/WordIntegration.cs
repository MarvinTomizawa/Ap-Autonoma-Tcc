using Assets.Scripts.Exception;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordIntegration : IntegrationFieldsValidator
{
#pragma warning disable 0649
    [SerializeField] private Text textField;
#pragma warning restore 0649

    public void Enable(string text)
    {
        if (IsNotValid) return;

        gameObject.SetActive(true);
        textField.text = text;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetIsProcessed(bool processed)
    {
        if (IsNotValid) return;

        if (processed)
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    protected override List<(object, string)> FieldsToBeValidated()
        => new List<(object, string)> { (textField, nameof(textField)) };
}
