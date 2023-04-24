using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFace : MonoBehaviour
{
    public GameObject face0, face1, face2, face3, face4, face5;
    private Vector3 startMaker = new Vector3(17.0f, -5.0f, 0);
    private Vector3 endMaker = new Vector3(5.75f, -5.0f, 0);
    private float timingVar = 0;
    private float speed = 1.0f;
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
        switch (CharacterSelectRight.currentPlayer)
        {
            case 1:                
                switch(CharacterSelectRight.selectVal)
                {
                    case 0:
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
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
                        float c = (float)(timingVar / .5f);
                        face2.transform.position = Vector3.Lerp(startMaker, endMaker, c);                    
                        face2.transform.localScale = new Vector3(3.6f, 8.099999f, 1);                                     
                        break;
                    case 3:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face4);
                        ResetFace(face5);
                        float d = (float)(timingVar / .5f);
                        face3.transform.position = Vector3.Lerp(startMaker, endMaker, d);                        
                        face3.transform.localScale = new Vector3(8.01f, 7.335f, 1);                        
                        break;
                    case 4:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face5);
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
                        float f = (float)(timingVar / .5f);
                        face5.transform.position = Vector3.Lerp(startMaker, endMaker, f);                        
                        face5.transform.localScale = new Vector3(4.5f, 4.5f, 1);                       
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
                switch (CharacterSelectRight.selectVal)
                {
                    case 0:
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face4);
                        ResetFace(face5);
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
                        float c = (float)(timingVar / .5f);
                        face2.transform.position = Vector3.Lerp(startMaker, endMaker, c);
                        face2.transform.localScale = new Vector3(3.6f, 8.099999f, 1);
                        break;
                    case 3:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face4);
                        ResetFace(face5);
                        float d = (float)(timingVar / .5f);
                        face3.transform.position = Vector3.Lerp(startMaker, endMaker, d);
                        face3.transform.localScale = new Vector3(8.01f, 7.335f, 1);
                        break;
                    case 4:
                        ResetFace(face0);
                        ResetFace(face1);
                        ResetFace(face2);
                        ResetFace(face3);
                        ResetFace(face5);
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
                        float f = (float)(timingVar / .5f);
                        face5.transform.position = Vector3.Lerp(startMaker, endMaker, f);
                        face5.transform.localScale = new Vector3(4.5f, 4.5f, 1);
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
