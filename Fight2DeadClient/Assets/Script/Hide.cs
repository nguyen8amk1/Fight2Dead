using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private float timer = 0f;
    private bool isBlockVisible = true;
    public float disappearTime;
    public float appearTime;
    public GameObject myObject;
    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isBlockVisible && timer >= disappearTime)
        {
            myObject.SetActive(false);
            isBlockVisible = false;
            timer = 0f;
        }
        // Kiểm tra nếu khối không hiển thị và thời gian đếm đạt đến thời gian xuất hiện lại
        else if (!isBlockVisible && timer >= appearTime)
        {
            // Xuất hiện lại khối
            myObject.SetActive(true);
            isBlockVisible = true;
            // Reset đếm thời gian
            timer = 0f;
        }
    }
}
