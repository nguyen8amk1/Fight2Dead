using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
using System;
using SocketServer;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public CanvasGroup alertTextContainer;
    public GameObject alertText;
    private float fadeDuration = 3f;
    private GameState globalGameState = GameState.Instance;
    public static int count = 0;

    public string[] UsernameList;
    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        submitButton.onClick.AddListener(OnLoginButtonClick);
    }
    private bool IsValidUsername(string username)
    {
        foreach (string existingUsername in UsernameList)
        {
            if (existingUsername == username)
            {                
                return false;
            }
                  
        }return true;
    }
    private void OnLoginButtonClick()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        // @Test
        // TODO: if the player already register the login form should be automatically filled 
        Debug.Log("Send to server for checking if the info is valid \n (Can go to next scene without login info just for testing)");
        string message = PreGameMessageGenerator.userLoginMessage(username, password);
        ServerCommute.connection.sendToServer(message);

/*
        if (!IsValidUsername(username) || password.Length < 6)
        {
            Debug.Log("Invalid username or password");
            usernameInputField.text = "";
            passwordInputField.text = "";
            alertTextContainer.alpha = 1f;
            alertText.SetActive(true);
            StartCoroutine(FadeOutAlertText());
            return;
        }
*/
        usernameInputField.text = "";
        passwordInputField.text = "";
        Debug.Log("Logged in");
        Debug.Log("Username: " + username);
        Debug.Log("Password: " + password);
    }

    private IEnumerator FadeOutAlertText()
    {
        float startAlpha = alertTextContainer.alpha;
        float elapsedTime = 0f;
        float targetAlpha = 0f; 

        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            alertTextContainer.alpha = newAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        alertTextContainer.alpha = targetAlpha; 
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
        if(globalGameState.loginSuccess)
		{
            Debug.Log("Login success");
			Util.toNextScene();
		} else
		{
            if(count == 1) { 
				Debug.Log("TODO: show failed to login on screen");
				Debug.Log("Invalid username or password");
				usernameInputField.text = "";
				passwordInputField.text = "";
				alertTextContainer.alpha = 1f;
				alertText.SetActive(true);
				StartCoroutine(FadeOutAlertText());
                count = 0;
            }
		}

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
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            submitButton.onClick.Invoke();
        }
    }
}
