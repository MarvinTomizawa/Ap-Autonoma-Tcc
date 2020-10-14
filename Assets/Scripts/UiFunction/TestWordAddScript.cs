using UnityEngine;
using UnityEngine.UI;

public class TestWordAddScript : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Dropdown[] products;
#pragma warning restore 0649
    public string TakeWord()
    {
        string word = "";

        foreach (var item in products)
        {
            if (item.value != 0)
            {
                word += item.value.ToString();
            }
        }

        ClearFields();
        
        return word;
    }

    private void ClearFields()
    {
        foreach (var item in products)
        {
            item.value = 0;
        }
    }
}
