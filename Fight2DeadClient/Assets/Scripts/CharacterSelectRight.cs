using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject char0, char1, char2, char3, char4, char5, char0_1, char1_1, char2_1, char3_1, char4_1, char5_1;
    public static float selectVal = 0;
    public static int currentPlayer = 1;
    public GameObject characterSelect1;
    public GameObject playerPosition1;
    private Vector3 startMaker = new Vector3(5f, -2.1f, 0);
    private Vector3 endMaker = new Vector3(9f, -2.1f, 0);
    private float timingVar = 0;
    private bool enterHit = false;

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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterHit = true;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectVal = (selectVal + 1) % 6;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectVal = (selectVal - 1 + 6) % 6;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectVal = (selectVal - 3 + 6) % 6;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectVal = (selectVal + 3) % 6;
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
                        break;
                    case 1:
                        char0.SetActive(false);
                        char1.SetActive(true);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        break;
                    case 2:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(true);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        break;
                    case 3:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(true);
                        char4.SetActive(false);
                        char5.SetActive(false);
                        break;
                    case 4:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(true);
                        char5.SetActive(false);
                        break;
                    case 5:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        char4.SetActive(false);
                        char5.SetActive(true);
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
                        break;
                    case 1:
                        char0_1.SetActive(false);
                        char1_1.SetActive(true);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        break;
                    case 2:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(true);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        break;
                    case 3:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(true);
                        char4_1.SetActive(false);
                        char5_1.SetActive(false);
                        break;
                    case 4:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(true);
                        char5_1.SetActive(false);
                        break;
                    case 5:
                        char0_1.SetActive(false);
                        char1_1.SetActive(false);
                        char2_1.SetActive(false);
                        char3_1.SetActive(false);
                        char4_1.SetActive(false);
                        char5_1.SetActive(true);
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
            if (currentPlayer == 1)
            {
                float speed = 3.0f;
                float t = (float)(timingVar / .5f);
                // Debug.Log("timingvar: " + timingVar + "moveDuration: " + moveDuration);          
                currentPlayer = 2;
                selectVal = 0;
                characterSelect1.SetActive(true);
                playerPosition1.transform.localScale = new Vector3(3.5f, 3.5f, 1);
                playerPosition1.transform.position = Vector3.Lerp(startMaker, endMaker, t);
                Debug.Log("Enter Pressed");
                if (t >= 1)
                {
                    enterHit = false;
                }
                timingVar += Time.deltaTime * speed;
            }
            

        }

    }
}
