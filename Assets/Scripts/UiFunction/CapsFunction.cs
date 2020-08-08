using UnityEngine;
using UnityEngine.UI;

public class CapsFunction : MonoBehaviour
{
    private InputField inputField;
    
    void Start()
    {
        inputField = gameObject.GetComponent<InputField>();
    }

    
    public void ConvertToCaps()
    {
        inputField.text = inputField.text.ToUpper();
    }
}
