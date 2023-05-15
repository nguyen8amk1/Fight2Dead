using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextChangeButton : MonoBehaviour
{
    public InputField inputField;

    
    private void Start()
    {        
        Button button = GetComponent<Button>();     
        button.onClick.AddListener(ChangeContentType);
    }   
    public void ChangeContentType()
    {
        if (inputField.contentType == InputField.ContentType.Standard)
        {
            inputField.contentType = InputField.ContentType.Password;
        }
        else if (inputField.contentType == InputField.ContentType.Password)
        {
            inputField.contentType = InputField.ContentType.Standard;
        }
    }
}
