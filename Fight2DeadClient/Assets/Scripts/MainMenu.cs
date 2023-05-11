using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{    
    public void NewGameDialogYes(string _sceneName)
    {
        Util.toNextScene();
    }
}
