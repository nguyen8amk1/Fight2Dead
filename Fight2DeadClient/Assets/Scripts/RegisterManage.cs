using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
using SocketServer;

public class RegisterManage : MonoBehaviour
{
    public string[] EmailList;
    public string[] UsernameList;
    EventSystem system;
    public Selectable firstInput;
    public Button regisButton;
    public InputField emailInputField;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public InputField confirmPasswordInputField;
    public CanvasGroup alertTextContainer;
    public GameObject alertText;
    public TMP_Text invalidText;
    public TMP_Text confirmPasswordText;
    public TMP_Text existedEmailText;
    private float fadeDuration = 3f;
    private bool isDuplicate = false;
    // Start is called before the first frame update
    void Start()
    {
        ServerCommute.listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempTCPListening());
        ServerCommute.listenToServerThread.Start();

        system = EventSystem.current;
        regisButton.onClick.AddListener(OnRegisterButtonClick);
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

    private bool IsValidEmailRegister(string email)
    {
        string pattern = @"^[a-zA-Z0-9.-_]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

    private void OnRegisterButtonClick()
    {
        string Email = emailInputField.text;
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string confirmpassword = confirmPasswordInputField.text;

        Debug.Log("TODO: Check the register info for email,username duplication in the database");

        /*
        foreach (string existingUsername in usernameList)
        foreach (string existingEmail in EmailList)
        {
            if (existingEmail == Email)
            {
                Debug.Log("Email has already been used");
                emailInputField.text = "";
                usernameInputField.text = "";
                passwordInputField.text = "";
                confirmPasswordInputField.text = "";
                alertTextContainer.alpha = 1f;
                existedEmailText.gameObject.SetActive(true);
                alertText.SetActive(true);
                StartCoroutine(FadeOutAlertText());
                isDuplicate = true;
                return;
            }
        }
        foreach (string existingUsername in UsernameList)
        {
            if (existingUsername == username)
            {
                Debug.Log("Username has already been used");
                emailInputField.text = "";
                usernameInputField.text = "";
                passwordInputField.text = "";
                confirmPasswordInputField.text = "";
                alertTextContainer.alpha = 1f;
                existedEmailText.gameObject.SetActive(true);
                alertText.SetActive(true);
                StartCoroutine(FadeOutAlertText());
                isDuplicate = true;
                return;
            }
        }
        if (isDuplicate && password != confirmpassword)
        {
            Debug.Log("2 error at the same time");
            emailInputField.text = "";
            usernameInputField.text = "";
            passwordInputField.text = "";
            confirmPasswordInputField.text = "";
            return;
        }    
        if (!IsValidEmailRegister(Email) || password.Length < 6)
        {
            Debug.Log("Invalid Email or password");
            emailInputField.text = "";
            usernameInputField.text = "";
            passwordInputField.text = "";
            confirmPasswordInputField.text = "";
            alertTextContainer.alpha = 1f;
            invalidText.gameObject.SetActive(true);
            alertText.SetActive(true);
            StartCoroutine(FadeOutAlertText());
            return;
        }
        else if (password != confirmpassword)
        {
            Debug.Log("Confirm password does not match");
            confirmPasswordInputField.text = "";
            alertTextContainer.alpha = 1f;
            confirmPasswordText.gameObject.SetActive(true);
            alertText.SetActive(true);
            StartCoroutine(FadeOutAlertText());
            return
        }                  
        */

        Debug.Log("Send to the server for storing");
        string message = PreGameMessageGenerator.userRegistrationMessage(usernameInputField.text, passwordInputField.text);
        ServerCommute.connection.sendToServer(message);

        // TODO: if registration success ->  change to login scene with the info already filled 
        //       else display the error on screen 

        Debug.Log("Registered");
        Debug.Log("Email: " + Email);
        Debug.Log("Username: " + username);
        Debug.Log("Password: " + password);
        emailInputField.text = "";
        passwordInputField.text = "";
        confirmPasswordInputField.text = "";
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
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            regisButton.onClick.Invoke();
        }
    }
}
