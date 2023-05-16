using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [Header("Switch Mode")]
    public int Mode = 1;
    [Header("1 VS 1 Mode Player ID")]
    public int twoPlayerID = 1;
    [Header("2 VS 2 Mode Player ID")]
    public int fourPlayerID = 1;
    public GameObject char0, char1, char2, char3, char4, char5, char6, char7, char8, char0_1,
        char1_1, char2_1, char3_1, char4_1, char5_1, char6_1, char7_1, char8_1, char0_2, char1_2,
        char2_2, char3_2, char4_2, char5_2, char6_2, char7_2, char8_2, char0_3,
        char1_3, char2_3, char3_3, char4_3, char5_3, char6_3, char7_3, char8_3;
    public GameObject P1, P2, P3, P4;
    public GameObject P1_icon, P1_1_icon, P2_icon, P2_1_icon, P3_icon, P2_2_icon, P4_icon;
    public GameObject pointer0, pointer1, pointer2, pointer3, pointer4, pointer5, pointer6, pointer7,
        pointer8;
    public static int selectVal = 0;
    public static int currentID = 1;

    private Vector3 startMaker = new Vector3(-4.8f, -2.7f, 0);
    private Vector3 endMaker = new Vector3(-7.5f, -2.7f, 0);
    private Vector3 startMaker_1 = new Vector3(4.9f, -2.7f, 0);
    private Vector3 endMaker_1 = new Vector3(7.5f, -2.7f, 0);
    private float timingVar = 0;
    private float timingVar_1 = 0;
    private float timingVarP1 = 0;   
    private float timingVarP3 = 0; 
    private float speed = 3.0f;
    private bool enterHitP1 = false, enterHitP2 = false;
    private int enterCount1 = 0, enterCount2 = 0;
    string[] charName = new string[] { "Cap", "Venom", "Sasori", "Gaara", "Ken", "Ryu",
        "Link","Reborn","Jotaro" };
    private bool P1Log1 = false, P1Log2 = false, P2Log1 = false, P2Log2 = false;
    private bool P1Log = false, P2Log = false, P3Log = false, P4Log = false;
    private bool p1EnterHit = false, p2EnterHit = false, p3EnterHit = false, p4EnterHit = false;
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
        if (Mode == 1)
        {
            P1_icon.SetActive(true);
            P1_1_icon.SetActive(true);
            P2_1_icon.SetActive(true);
            P2_2_icon.SetActive(true);
        }
        if (Mode == 2)
        {
            P1_icon.SetActive(true);
            P2_icon.SetActive(true);
            P3_icon.SetActive(true);
            P4_icon.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (twoPlayerID == 1)
            {
                enterHitP1 = true;
                enterCount1++;
            }
            if (twoPlayerID == 2)
            {
                enterHitP2 = true;
                enterCount2++;
            }
            if (fourPlayerID == 1)
            {                
                p1EnterHit = true;
            }
            if (fourPlayerID == 2)
            {
                p2EnterHit = true;
            }
            if (fourPlayerID == 3)
            {
                p3EnterHit = true;
            }
            if (fourPlayerID == 4)
            {
                p4EnterHit = true;
            }
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
            pointer0.SetActive(true);
        }
        else pointer0.SetActive(false);

        if (selectVal == 1)
        {
            pointer1.SetActive(true);
        }
        else pointer1.SetActive(false);

        if (selectVal == 2)
        {
            pointer2.SetActive(true);
        }
        else pointer2.SetActive(false);

        if (selectVal == 3)
        {
            pointer3.SetActive(true);
        }
        else pointer3.SetActive(false);

        if (selectVal == 4)
        {
            pointer4.SetActive(true);
        }
        else pointer4.SetActive(false);

        if (selectVal == 5)
        {
            pointer5.SetActive(true);
        }
        else pointer5.SetActive(false);

        if (selectVal == 6)
        {
            pointer6.SetActive(true);
        }
        else pointer6.SetActive(false);

        if (selectVal == 7)
        {
            pointer7.SetActive(true);
        }
        else pointer7.SetActive(false);

        if (selectVal == 8)
        {
            pointer8.SetActive(true);
        }
        else pointer8.SetActive(false);

        if(Mode == 1)
        {
            fourPlayerID = 0;
            if (twoPlayerID == 1 && enterCount1 == 0)
            {
                P1.SetActive(true);
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
            }

            if (enterCount1 == 1)
            {
                if (!P1Log1)
                {
                    Debug.Log("P1_char1: " + charName[selectVal]);
                    P1Log1 = true;
                }
                P2.SetActive(true);
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
            }

            if (enterCount1 == 1)
            {
                float t = (float)(timingVar / .5f);
                P1.transform.localScale = new Vector3(3f, 3.5f, 1);
                P1.transform.position = Vector3.Lerp(startMaker, endMaker, t);
                if (t >= 1)
                {
                    enterHitP1 = false;
                }
                timingVar += Time.deltaTime * speed;
            }
            else if (enterCount1 == 2)
            {
                if (!P1Log2)
                {
                    Debug.Log("P1_char2: " + charName[selectVal]);
                    P1Log2 = true;
                }
            }
            else timingVar = 0;

            ////////////////////////////////////////

            if (twoPlayerID == 2 && enterCount2 == 0)
            {
                currentID = 2;
                P3.SetActive(true);
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
            }
            if (enterCount2 == 1)
            {
                if (!P2Log1)
                {
                    Debug.Log("P2_char1: " + charName[selectVal]);
                    P2Log1 = true;
                }
                P4.SetActive(true);
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
            }

            if (enterCount2 == 1)
            {
                float t = (float)(timingVar_1 / .5f);
                P3.transform.localScale = new Vector3(3f, 3.5f, 1);
                P3.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, t);
                if (t >= 1)
                {
                    enterHitP2 = false;
                }
                timingVar_1 += Time.deltaTime * speed;
            }
            else if (enterCount2 == 2)
            {
                if (!P2Log2)
                {
                    Debug.Log("P2_char2: " + charName[selectVal]);
                    P2Log2 = true;
                }
            }
        }
        if (Mode == 2)
        {
            twoPlayerID = 0;
            if(fourPlayerID == 1)
            {
                P1.SetActive(true);
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
            }
            if (p1EnterHit)
            {
                float t = (float)(timingVarP1 / .5f);
                P1.transform.localScale = new Vector3(3f, 3.5f, 1);
                P1.transform.position = Vector3.Lerp(startMaker, endMaker, t);
                if (t >= 1)
                {
                    p1EnterHit = false;
                }
                timingVarP1 += Time.deltaTime * speed;
                if (!P1Log)
                {
                    Debug.Log("P1: " + charName[selectVal]);
                    P1Log = true;
                }
            }            

            if (fourPlayerID == 2)
            {
                P2.SetActive(true);
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
            }
            if(p2EnterHit)
            {
                if (!P2Log)
                {
                    Debug.Log("P2: " + charName[selectVal]);
                    P2Log = true;
                }
            }

            if (fourPlayerID == 3)
            {
                currentID = 2;
                P3.SetActive(true);
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
            }
            if (p3EnterHit)
            {
                float t = (float)(timingVarP3 / .5f);
                P3.transform.localScale = new Vector3(3f, 3.5f, 1);
                P3.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, t);
                if (t >= 1)
                {
                    p3EnterHit = false;
                }
                timingVarP3 += Time.deltaTime * speed;
                if (!P3Log)
                {
                    Debug.Log("P3: " + charName[selectVal]);
                    P3Log = true;
                }
            }

            if(fourPlayerID == 4)
            {
                currentID = 2;
                P4.SetActive(true);
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
            }
            if(p4EnterHit)
            {
                if (!P4Log)
                {
                    Debug.Log("P4: " + charName[selectVal]);
                    P4Log = true;
                }
            }
        }    
    }
}

