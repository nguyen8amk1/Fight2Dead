using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    public float disappearTime;
    public float appearTime;

    private float disappearTimer = 0f;
    private float appearTimer = 0f;
    public GameObject myObject;
    private bool isBlockVisible = true;

    void Update()
    {
        if (isBlockVisible)
        {
            disappearTimer += Time.deltaTime;
            if (disappearTimer >= disappearTime)
            {
                myObject.SetActive(false);
                isBlockVisible = false;
                disappearTimer = 0f;
            }
        }
        else
        {
            appearTimer += Time.deltaTime;
            if (appearTimer >= appearTime)
            {
                myObject.SetActive(true);
                isBlockVisible = true;
                appearTimer = 0f;
            }
        }
    }
}
