using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuSelectButton : MonoBehaviour
{    
    public void clickedButton()
    {
        string clickedButonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(clickedButonName + " Press");
    }

}
