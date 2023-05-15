using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextChangeButton : MonoBehaviour
{
    public TMP_Text buttonText;

    private bool isShown = false;

    private void Start()
    {
        
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeText);
    }

    private void ChangeText()
    {
        if (isShown)
        {
            buttonText.text = "Hide";
        }
        else
        {
            buttonText.text = "Show";
        }

        isShown = !isShown;
    }
}
