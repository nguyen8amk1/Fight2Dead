using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFace : MonoBehaviour
{
    public GameObject face0, face1, face2, face3, face4, face5, face6, face7, face8,
        face0_1, face1_1, face2_1, face3_1, face4_1, face5_1, face6_1, face7_1, face8_1;

    private Vector3 startMaker = new Vector3(-17.0f, -5.0f, 0);
    private Vector3 endMaker = new Vector3(-6.15f, -5.0f, 0);
    private Vector3 startMaker_1 = new Vector3(17.0f, -5.0f, 0);
    private Vector3 endMaker_1 = new Vector3(6.3f, -5.0f, 0);
    private float timingVar = 0;
    private float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void ResetFace(GameObject face)
    {
        face.transform.position = startMaker;
    }
    // Update is called once per frame
    void Update()
    {
        switch (CharacterSelect.currentID)
        {
            case 1:
                switch (CharacterSelect.selectVal)
                {
                    case 0:
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
                        ResetFace(face6);
                        ResetFace(face7);
                        ResetFace(face8);
                        float a = (float)(timingVar / .5f);
                        face0.transform.position = Vector3.Lerp(startMaker, endMaker, a);
                        face0.transform.localScale = new Vector3(4.5f, 4.5f, 1);
                        break;
                    case 1:
                        ResetFace(face0);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
                        ResetFace(face6);
                        ResetFace(face7);
                        ResetFace(face8);
                        float b = (float)(timingVar / .5f);
                        face1.transform.position = Vector3.Lerp(startMaker, endMaker, b);
                        face1.transform.localScale = new Vector3(4.59f, 4.59f, 1);
                        break;
                    case 2:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
                        ResetFace(face6);
                        ResetFace(face7);
                        ResetFace(face8);
                        float c = (float)(timingVar / .5f);
                        face2.transform.position = Vector3.Lerp(startMaker, endMaker, c);
                        face2.transform.localScale = new Vector3(0.43f, 0.53f, 1);
                        break;
                    case 3:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face4);
                        ResetFace(face5);
                        ResetFace(face6);
                        ResetFace(face7);
                        ResetFace(face8);
                        float d = (float)(timingVar / .5f);
                        face3.transform.position = Vector3.Lerp(startMaker, endMaker, d);
                        face3.transform.localScale = new Vector3(3.45f, 3.7f, 1);
                        break;
                    case 4:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face5);
                        ResetFace(face6);
                        ResetFace(face7);
                        ResetFace(face8);
                        float e = (float)(timingVar / .5f);
                        face4.transform.position = Vector3.Lerp(startMaker, endMaker, e);
                        face4.transform.localScale = new Vector3(1.755f, 1.6425f, 1);
                        break;
                    case 5:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face6);
                        ResetFace(face7);
                        ResetFace(face8);
                        float f = (float)(timingVar / .5f);
                        face5.transform.position = Vector3.Lerp(startMaker, endMaker, f);
                        face5.transform.localScale = new Vector3(4.5f, 4.5f, 1);
                        break;
                    case 6:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
                        ResetFace(face7);
                        ResetFace(face8);
                        float g = (float)(timingVar / .5f);
                        face6.transform.position = Vector3.Lerp(startMaker, endMaker, g);
                        face6.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                        break;
                    case 7:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
                        ResetFace(face6);
                        ResetFace(face8);
                        float h = (float)(timingVar / .5f);
                        face7.transform.position = Vector3.Lerp(startMaker, endMaker, h);
                        face7.transform.localScale = new Vector3(0.75f, 0.95f, 1);
                        break;
                    case 8:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
                        ResetFace(face6);
                        ResetFace(face7);
                        float k = (float)(timingVar / .5f);
                        face8.transform.position = Vector3.Lerp(startMaker, endMaker, k);
                        face8.transform.localScale = new Vector3(0.85f, 0.73f, 1);
                        break;
                    default:
                        break;
                }
                if (!(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)
                    || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
                    timingVar += Time.deltaTime * speed;
                else timingVar = 0;
                break;

            case 2:
                switch (CharacterSelect.selectVal)
                {
                    case 0:
                        ResetFace(face1_1);
                        ResetFace(face2_1);
                        ResetFace(face3_1);
                        ResetFace(face4_1);
                        ResetFace(face5_1);
                        ResetFace(face6_1);
                        ResetFace(face7_1);
                        ResetFace(face8_1);
                        float a = (float)(timingVar / .5f);
                        face0_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, a);
                        face0_1.transform.localScale = new Vector3(4.5f, 4.5f, 1);
                        break;
                    case 1:
                        ResetFace(face0_1);
                        ResetFace(face2_1);
                        ResetFace(face3_1);
                        ResetFace(face4_1);
                        ResetFace(face5_1);
                        ResetFace(face6_1);
                        ResetFace(face7_1);
                        ResetFace(face8_1);
                        float b = (float)(timingVar / .5f);
                        face1_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, b);
                        face1_1.transform.localScale = new Vector3(4.59f, 4.59f, 1);
                        break;
                    case 2:
                        ResetFace(face0_1);
                        ResetFace(face1_1);
                        ResetFace(face3_1);
                        ResetFace(face4_1);
                        ResetFace(face5_1);
                        ResetFace(face6_1);
                        ResetFace(face7_1);
                        ResetFace(face8_1);
                        float c = (float)(timingVar / .5f);
                        face2_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, c);
                        face2_1.transform.localScale = new Vector3(0.43f, 0.53f, 1);
                        break;
                    case 3:
                        ResetFace(face0_1);
                        ResetFace(face1_1);
                        ResetFace(face2_1);
                        ResetFace(face4_1);
                        ResetFace(face5_1);
                        ResetFace(face6_1);
                        ResetFace(face7_1);
                        ResetFace(face8_1);
                        float d = (float)(timingVar / .5f);
                        face3_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, d);
                        face3_1.transform.localScale = new Vector3(3.45f, 3.7f, 1);
                        break;
                    case 4:
                        ResetFace(face0_1);
                        ResetFace(face1_1);
                        ResetFace(face2_1);
                        ResetFace(face3_1);
                        ResetFace(face5_1);
                        ResetFace(face6_1);
                        ResetFace(face7_1);
                        ResetFace(face8_1);
                        float e = (float)(timingVar / .5f);
                        face4_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, e);
                        face4_1.transform.localScale = new Vector3(1.755f, 1.6425f, 1);
                        break;
                    case 5:
                        ResetFace(face0_1);
                        ResetFace(face1_1);
                        ResetFace(face2_1);
                        ResetFace(face3_1);
                        ResetFace(face4_1);
                        ResetFace(face6_1);
                        ResetFace(face7_1);
                        ResetFace(face8_1);
                        float f = (float)(timingVar / .5f);
                        face5_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, f);
                        face5_1.transform.localScale = new Vector3(4.5f, 4.5f, 1);
                        break;
                    case 6:
                        ResetFace(face0_1);
                        ResetFace(face1_1);
                        ResetFace(face2_1);
                        ResetFace(face3_1);
                        ResetFace(face4_1);
                        ResetFace(face5_1);
                        ResetFace(face7_1);
                        ResetFace(face8_1);
                        float g = (float)(timingVar / .5f);
                        face6_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, g);
                        face6_1.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                        break;
                    case 7:
                        ResetFace(face0_1);
                        ResetFace(face1_1);
                        ResetFace(face2_1);
                        ResetFace(face3_1);
                        ResetFace(face4_1);
                        ResetFace(face5_1);
                        ResetFace(face6_1);
                        ResetFace(face8_1);
                        float h = (float)(timingVar / .5f);
                        face7_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, h);
                        face7_1.transform.localScale = new Vector3(0.75f, 0.95f, 1);
                        break;
                    case 8:
                        ResetFace(face0_1);
                        ResetFace(face1_1);
                        ResetFace(face2_1);
                        ResetFace(face3_1);
                        ResetFace(face4_1);
                        ResetFace(face5_1);
                        ResetFace(face6_1);
                        ResetFace(face7_1);
                        float k = (float)(timingVar / .5f);
                        face8_1.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, k);
                        face8_1.transform.localScale = new Vector3(0.85f, 0.73f, 1);
                        break;
                    default:
                        break;
                }
                if (!(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)
                    || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
                    timingVar += Time.deltaTime * speed;
                else timingVar = 0;
                break;
            default:
                break;
        }
    }
}
