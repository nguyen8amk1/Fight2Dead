using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SumitCheck : MonoBehaviour
{
    public InputField inputField;

    private void Start()
    {      
        inputField.onEndEdit.AddListener(CheckInputField);
    }

    public void CheckInputField(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            Debug.Log("Input Field null");
        }
        else
        {
            Debug.Log("Content: " + input);
        }
    }
}
