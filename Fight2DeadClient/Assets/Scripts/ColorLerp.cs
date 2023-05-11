using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    public Image image;
    public Color[] s_color;
    public float speed;
    private float changer;

    private int currentIndex = 0;
    private int nextIndex = 1;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = s_color[currentIndex];
    }

    private void Update()
    {
        image.color = Color.Lerp(image.color, s_color[nextIndex], speed * Time.deltaTime);
        changer += speed * Time.deltaTime;

        if (changer >= 1f)
        {
            changer = 0f;
            currentIndex = nextIndex;
            nextIndex = (nextIndex + 1) % s_color.Length;
        }
    }
}
