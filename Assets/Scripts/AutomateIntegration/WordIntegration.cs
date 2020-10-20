using UnityEngine;
using UnityEngine.UI;

public class WordIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Game Components")]
    [SerializeField] private Dropdown[] products;
    [SerializeField] private GameObject correctImage;
    [SerializeField] private GameObject incorrectImage;

    [Header("Component Options")]
    [SerializeField] private bool shouldBeCorrect = true;
#pragma warning restore 0649

    public bool ShouldBeCorrect => shouldBeCorrect;

    private void Start()
    {
        if (correctImage != null && incorrectImage != null)
        {
            correctImage.SetActive(shouldBeCorrect);
            incorrectImage.SetActive(!shouldBeCorrect);
        }
    }

    public string GetWord()
    {
        string word = "";

        foreach (var item in products)
        {
            if (item.value != 0)
            {
                word += item.value.ToString();
            }
        }

        return word;
    }

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
