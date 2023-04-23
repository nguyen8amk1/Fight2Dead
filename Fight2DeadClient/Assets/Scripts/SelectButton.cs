using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    public GameObject characterSelect;
    private int charVal = 0;
    public void ButtonS()
    {
        if(charVal ==0)
        {
            characterSelect.SetActive(true);
            charVal = 1;
        }
        else
        {
            characterSelect.SetActive(false);
            charVal = 0;
        }       
    }
    

}