using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
public class RegisterManage : MonoBehaviour
{
    public string[] usernameList;
    EventSystem system;
    public Selectable firstInput;
    public Button regisButton;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public InputField confirmPasswordInputField;
    public CanvasGroup alertTextContainer;
    public GameObject alertText;
    public TMP_Text invalidText;
    public TMP_Text confirmPasswordText;
    public TMP_Text existedUsernameText;
    private float fadeDuration = 3f;
    private bool isDuplicate = false;
    // Start is called before the first frame update
    void Start()
    {
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
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string confirmpassword = confirmPasswordInputField.text;

        Debug.Log("TODO: Perform the syntax check then pass to the server for storing");

        foreach (string existingUsername in usernameList)
        {
            if (existingUsername == username)
            {
                Debug.Log("Email has already been used");
                usernameInputField.text = "";
                passwordInputField.text = "";
                confirmPasswordInputField.text = "";
                alertTextContainer.alpha = 1f;
                existedUsernameText.gameObject.SetActive(true);
                alertText.SetActive(true);
                StartCoroutine(FadeOutAlertText());
                isDuplicate = true;
                return;
            }
        }
        if (isDuplicate && password != confirmpassword)
        {
            Debug.Log("2 error at the same time");
            usernameInputField.text = "";
            passwordInputField.text = "";
            confirmPasswordInputField.text = "";
            return;
        }    
        if (!IsValidEmailRegister(username) || password.Length < 6)
        {
            Debug.Log("Invalid username or password");
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
            return;
        }                  

        

        Debug.Log("Registered");
        Debug.Log("Username: " + username);
        Debug.Log("Password: " + password);
        usernameInputField.text = "";
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
