using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageChange : MonoBehaviour
{
    public Image Image;
    public Sprite newImageSprite;
    public Sprite oldImageSprite;
    public void ImageTransition()
    {
        if (Image.sprite == oldImageSprite)
        {
            Image.sprite = newImageSprite;
        }
        else Image.sprite = oldImageSprite;
    }

}
