using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public void NewGameDialogYes(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    void Update()
    {
        if(Input.anyKey)
        {
            startButton.onClick.Invoke();
        }    
    }
}
