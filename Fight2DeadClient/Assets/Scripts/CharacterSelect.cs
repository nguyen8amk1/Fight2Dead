using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject char0, char1, char2, char3, char4, char5, char6, char7, char8, char0_1,
        char1_1, char2_1, char3_1, char4_1, char5_1, char6_1, char7_1, char8_1, char0_2, char1_2,
        char2_2, char3_2, char4_2, char5_2, char6_2, char7_2, char8_2, char0_3,
        char1_3, char2_3, char3_3, char4_3, char5_3, char6_3, char7_3, char8_3;
    public static int selectVal = 0;
    public static int currentPlayer = 1;
    public GameObject characterSelect1, characterSelect2;
    public GameObject playerPosition1, playerPosition2;
    public GameObject pointer;
    private Vector3 startMaker = new Vector3(-4.65f, -2.1f, 0);
    private Vector3 endMaker = new Vector3(-7.5f, -2.1f, 0);
    private Vector3 startMaker_1 = new Vector3(4.65f, -2.1f, 0);
    private Vector3 endMaker_1 = new Vector3(7.5f, -2.1f, 0);
    private float timingVar = 0;
    private bool enterHit = false;
    private int enterCount = 0;
    private float speed = 3.0f;
    string[] charName = new string[] { "Cap", "Venom", "Sasori", "Ishida", "Ken", "Ryu",
        "Link","Reborn","Jotaro" };
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Char0()
    {
        selectVal = 0;
    }
    public void Char1()
    {
        selectVal = 1;
    }
    public void Char2()
    {
        selectVal = 2;
    }
    public void Char3()
    {
        selectVal = 3;
    }
    public void Char4()
    {
        selectVal = 4;
    }
    public void Char5()
    {
        selectVal = 5;
    }
    public void Char6()
    {
        selectVal = 6;
    }
    public void Char7()
    {
        selectVal = 7;
    }
    public void Char8()
    {
        selectVal = 8;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterHit = true;
            enterCount++;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectVal = (selectVal + 1) % 9;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectVal = (selectVal - 1 + 9) % 9;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectVal = (selectVal - 3 + 9) % 9;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectVal = (selectVal + 3) % 9;
        }
        if (selectVal == 0)
        {
            pointer.transform.position = new Vector3(266, 242.6f, 0);
        }
        else if (selectVal == 1)
        {
            pointer.transform.position = new Vector3(345.5f, 242.6f, 0);
        }
        else if (selectVal == 2)
        {
            pointer.transform.position = new Vector3(424.14f, 242.6f, 0);
        }
        else if (selectVal == 3)
        {
            pointer.transform.position = new Vector3(266, 191.6f, 0);
        }
        else if (selectVal == 4)
        {
            pointer.transform.position = new Vector3(345.5f, 191.6f, 0);
        }
        else if (selectVal == 5)
        {
            pointer.transform.position = new Vector3(424.14f, 191.6f, 0);
        }
        else if (selectVal == 6)
        {
            pointer.transform.position = new Vector3(266f, 142.2f, 0);
        }
        else if (selectVal == 7)
        {
            pointer.transform.position = new Vector3(345.5f, 142.2f, 0);
        }
        else if (selectVal == 8)
        {
            pointer.transform.position = new Vector3(424.14f, 142.2f, 0);
        }
        switch (currentPlayer)
        {
            case 1:
                switch (selectVal)
                {
                    case 0:
                        char0.SetActive(true);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        char6.SetActive(false);
                        char7.SetActive(false);
                        char8.SetActive(false);
                        break;
                    case 1:
                        char0.SetActive(false);
                        char1.SetActive(true);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        char6.SetActive(false);
                        char7.SetActive(false);
                        char8.SetActive(false);
                        break;
                    case 2:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(true);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        char6.SetActive(false);
                        char7.SetActive(false);
                        char8.SetActive(false);
                        break;
                    case 3:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(true);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        char6.SetActive(false);
                        char7.SetActive(false);
                        char8.SetActive(false);
                        break;
                    case 4:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(true);
                        char5.SetActive(false);
                        char6.SetActive(false);
                        char7.SetActive(false);
                        char8.SetActive(false);
                        break;
                    case 5:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(true);
                        char6.SetActive(false);
                        char7.SetActive(false);
                        char8.SetActive(false);
                        break;
                    case 6:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        char6.SetActive(true);
                        char7.SetActive(false);
                        char8.SetActive(false);
                        break;
                    case 7:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        char6.SetActive(false);
                        char7.SetActive(true);
                        char8.SetActive(false);
                        break;
                    case 8:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        char6.SetActive(false);
                        char7.SetActive(false);
                        char8.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (selectVal)
                {
                    case 0:
                        char0_1.SetActive(true);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        char6_1.SetActive(false);
                        char7_1.SetActive(false);
                        char8_1.SetActive(false);
                        break;
                    case 1:
                        char0_1.SetActive(false);
                        char1_1.SetActive(true);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        char6_1.SetActive(false);
                        char7_1.SetActive(false);
                        char8_1.SetActive(false);
                        break;
                    case 2:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(true);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        char6_1.SetActive(false);
                        char7_1.SetActive(false);
                        char8_1.SetActive(false);
                        break;
                    case 3:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(true);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        char6_1.SetActive(false);
                        char7_1.SetActive(false);
                        char8_1.SetActive(false);
                        break;
                    case 4:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(true);
                        char5_1.SetActive(false);
                        char6_1.SetActive(false);
                        char7_1.SetActive(false);
                        char8_1.SetActive(false);
                        break;
                    case 5:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(true);
                        char6_1.SetActive(false);
                        char7_1.SetActive(false);
                        char8_1.SetActive(false);
                        break;
                    case 6:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        char6_1.SetActive(true);
                        char7_1.SetActive(false);
                        char8_1.SetActive(false);
                        break;
                    case 7:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        char6_1.SetActive(false);
                        char7_1.SetActive(true);
                        char8_1.SetActive(false);
                        break;
                    case 8:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        char6_1.SetActive(false);
                        char7_1.SetActive(false);
                        char8_1.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (selectVal)
                {
                    case 0:
                        char0_2.SetActive(true);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        char4_2.SetActive(false);
                        char5_2.SetActive(false);
                        char6_2.SetActive(false);
                        char7_2.SetActive(false);
                        char8_2.SetActive(false);
                        break;
                    case 1:
                        char0_2.SetActive(false);
                        char1_2.SetActive(true);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        char4_2.SetActive(false);
                        char5_2.SetActive(false);
                        char6_2.SetActive(false);
                        char7_2.SetActive(false);
                        char8_2.SetActive(false);
                        break;
                    case 2:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(true);
                        char3_2.SetActive(false);
                        char4_2.SetActive(false);
                        char5_2.SetActive(false);
                        char6_2.SetActive(false);
                        char7_2.SetActive(false);
                        char8_2.SetActive(false);
                        break;
                    case 3:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(true);
                        char4_2.SetActive(false);
                        char5_2.SetActive(false);
                        char6_2.SetActive(false);
                        char7_2.SetActive(false);
                        char8_2.SetActive(false);
                        break;
                    case 4:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        char4_2.SetActive(true);
                        char5_2.SetActive(false);
                        char6_2.SetActive(false);
                        char7_2.SetActive(false);
                        char8_2.SetActive(false);
                        break;
                    case 5:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        char4_2.SetActive(false);
                        char5_2.SetActive(true);
                        char6_2.SetActive(false);
                        char7_2.SetActive(false);
                        char8_2.SetActive(false);
                        break;
                    case 6:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        char4_2.SetActive(false);
                        char5_2.SetActive(false);
                        char6_2.SetActive(true);
                        char7_2.SetActive(false);
                        char8_2.SetActive(false);
                        break;
                    case 7:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        char4_2.SetActive(false);
                        char5_2.SetActive(false);
                        char6_2.SetActive(false);
                        char7_2.SetActive(true);
                        char8_2.SetActive(false);
                        break;
                    case 8:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        char4_2.SetActive(false);
                        char5_2.SetActive(false);
                        char6_2.SetActive(false);
                        char7_2.SetActive(false);
                        char8_2.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (selectVal)
                {
                    case 0:
                        char0_3.SetActive(true);
                        char1_3.SetActive(false);
                        char2_3.SetActive(false);
                        char3_3.SetActive(false);
                        char4_3.SetActive(false);
                        char5_3.SetActive(false);
                        char6_3.SetActive(false);
                        char7_3.SetActive(false);
                        char8_3.SetActive(false);
                        break;
                    case 1:
                        char0_3.SetActive(false);
                        char1_3.SetActive(true);
                        char2_3.SetActive(false);
                        char3_3.SetActive(false);
                        char4_3.SetActive(false);
                        char5_3.SetActive(false);
                        char6_3.SetActive(false);
                        char7_3.SetActive(false);
                        char8_3.SetActive(false);
                        break;
                    case 2:
                        char0_3.SetActive(false);
                        char1_3.SetActive(false);
                        char2_3.SetActive(true);
                        char3_3.SetActive(false);
                        char4_3.SetActive(false);
                        char5_3.SetActive(false);
                        char6_3.SetActive(false);
                        char7_3.SetActive(false);
                        char8_3.SetActive(false);
                        break;
                    case 3:
                        char0_3.SetActive(false);
                        char1_3.SetActive(false);
                        char2_3.SetActive(false);
                        char3_3.SetActive(true);
                        char4_3.SetActive(false);
                        char5_3.SetActive(false);
                        char6_3.SetActive(false);
                        char7_3.SetActive(false);
                        char8_3.SetActive(false);
                        break;
                    case 4:
                        char0_3.SetActive(false);
                        char1_3.SetActive(false);
                        char2_3.SetActive(false);
                        char3_3.SetActive(false);
                        char4_3.SetActive(true);
                        char5_3.SetActive(false);
                        char6_3.SetActive(false);
                        char7_3.SetActive(false);
                        char8_3.SetActive(false);
                        break;
                    case 5:
                        char0_3.SetActive(false);
                        char1_3.SetActive(false);
                        char2_3.SetActive(false);
                        char3_3.SetActive(false);
                        char4_3.SetActive(false);
                        char5_3.SetActive(true);
                        char6_3.SetActive(false);
                        char7_3.SetActive(false);
                        char8_3.SetActive(false);
                        break;
                    case 6:
                        char0_3.SetActive(false);
                        char1_3.SetActive(false);
                        char2_3.SetActive(false);
                        char3_3.SetActive(false);
                        char4_3.SetActive(false);
                        char5_3.SetActive(false);
                        char6_3.SetActive(true);
                        char7_3.SetActive(false);
                        char8_3.SetActive(false);
                        break;
                    case 7:
                        char0_3.SetActive(false);
                        char1_3.SetActive(false);
                        char2_3.SetActive(false);
                        char3_3.SetActive(false);
                        char4_3.SetActive(false);
                        char5_3.SetActive(false);
                        char6_3.SetActive(false);
                        char7_3.SetActive(true);
                        char8_3.SetActive(false);
                        break;
                    case 8:
                        char0_3.SetActive(false);
                        char1_3.SetActive(false);
                        char2_3.SetActive(false);
                        char3_3.SetActive(false);
                        char4_3.SetActive(false);
                        char5_3.SetActive(false);
                        char6_3.SetActive(false);
                        char7_3.SetActive(false);
                        char8_3.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        if (enterHit)
        {
            float t = (float)(timingVar / .5f);
            if (enterCount == 1)
            {
                Debug.Log("P1, " + charName[selectVal]);
                currentPlayer = 2;                
                characterSelect1.SetActive(true);
                playerPosition1.transform.localScale = new Vector3(3.5f, 3.5f, 1);
                playerPosition1.transform.position = Vector3.Lerp(startMaker, endMaker, t);
                if (t >= 1)
                {
                    enterHit = false;
                }
            }
            else if (enterCount == 2)
            {
                Debug.Log("P1, " + charName[selectVal]);
                currentPlayer = 3;                
                if (t >= 1)
                {
                    enterHit = false;
                }
            }
            else if (enterCount == 3)
            {
                Debug.Log("P2, " + charName[selectVal]);
                currentPlayer = 4;
                characterSelect2.SetActive(true);
                playerPosition2.transform.localScale = new Vector3(3.5f, 3.5f, 1);
                playerPosition2.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, t);
                if (t >= 1)
                {
                    enterHit = false;
                }
            }
            else if(enterCount ==4)
            {
                Debug.Log("P2, " + charName[selectVal]);
            }    
            timingVar += Time.deltaTime * speed;
        }
        else timingVar = 0;

    }
}
