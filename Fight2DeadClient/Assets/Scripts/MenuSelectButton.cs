using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuSelectButton : MonoBehaviour
{
    private static GameState globalGameState = GameState.Instance;
    public void clickedButton()
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(clickedButtonName + " Press");
        bool globalPressed = clickedButtonName.Equals("Global");
        bool press1v1 = clickedButtonName.Equals("1VS1");
        if(press1v1)
		{
            Debug.Log("set numplayers to 2");
            globalGameState.numPlayers = 2;
            return;
		}

        bool press2v2 = clickedButtonName.Equals("2VS2");
        if(press2v2)
		{
            Debug.Log("set numplayers to 4");
            globalGameState.numPlayers = 4;
            return;
		}

        if(globalPressed)
		{
            Debug.Log("set online mode to global");
            globalGameState.onlineMode = "GLOBAL";
            return;
		}
        bool lanPressed = clickedButtonName.Equals("LAN");
        if(lanPressed)
		{
            Debug.Log("set online mode to lan");
            globalGameState.onlineMode = "LAN";
            return;
		}

    }

}
