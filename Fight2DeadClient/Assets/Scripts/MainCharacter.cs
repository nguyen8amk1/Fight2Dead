using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public GameObject char0, char1, char2, char3;
    public static int selectVal = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (selectVal)
        {
            case 0:
                char0.SetActive(true);
                char1.SetActive(false);
                char2.SetActive(false);
                char3.SetActive(false);
                break;
            case 1:
                char0.SetActive(false);
                char1.SetActive(true);
                char2.SetActive(false);
                char3.SetActive(false);
                break;
            case 2:
                char0.SetActive(false);
                char1.SetActive(false);
                char2.SetActive(true);
                char3.SetActive(false);
                break;
            case 3:
                char0.SetActive(false);
                char1.SetActive(false);
                char2.SetActive(false);
                char3.SetActive(true);
                break;
            default:
                break;

        }
    }
}
