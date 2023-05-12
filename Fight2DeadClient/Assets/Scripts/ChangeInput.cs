using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
using System;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public CanvasGroup alertTextContainer;
    public GameObject alertText;
    public float fadeDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        submitButton.onClick.AddListener(OnLoginButtonClick);
    }

    private void OnLoginButtonClick()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        while (true)
        {
            if (!IsValidEmail(username) || password.Length < 6)
            {               
                Debug.Log("Invalid username or password");
                usernameInputField.text = "";
                passwordInputField.text = "";
                alertTextContainer.alpha = 1f;
                alertText.SetActive(true);
                StartCoroutine(FadeOutAlertText());
                return;
            }                             
            break;
        }
        Debug.Log("Logged in");
        Debug.Log("Username: " + username);
        Debug.Log("Password: " + password);
    }

    private IEnumerator FadeOutAlertText()
    {
        
        float startAlpha = alertTextContainer.alpha;       
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);          
            alertTextContainer.alpha = newAlpha;           
            elapsedTime += Time.deltaTime;         
            yield return null;
        }

        
        alertTextContainer.alpha = 0f;
        alertText.SetActive(false);
    }

    private bool IsValidEmail(string email)
    {
         string pattern = @"^[a-zA-Z0-9.-_]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

    // Update is called once per frame
    void Update()
    {      
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
                previous.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }else if(Input.GetKeyDown(KeyCode.Return))
        {
            submitButton.onClick.Invoke();         
        }
    }
}