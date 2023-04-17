using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrintButton : MonoBehaviour
{
    string[] charName = new string[] { "Ryu", "Four Arms", "Heatblast", "Venom" };
    public void OnButtonClick()
    {
        Debug.Log("Hello World\n");
        if (MainCharacter.selectVal == 0)
            Debug.Log(charName[0]);
        else if (MainCharacter.selectVal == 1)
            Debug.Log(charName[1]);
        else if (MainCharacter.selectVal == 2)
            Debug.Log(charName[2]);
        else Debug.Log(charName[3]);
    }
}
