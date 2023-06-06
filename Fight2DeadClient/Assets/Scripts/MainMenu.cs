using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public void NewGameDialogYes(string _sceneName)
    {
        Util.toNextScene();
    }
    void Update()
    {
        if(Input.anyKey)
        {
            startButton.onClick.Invoke();
        }    
    }
}
