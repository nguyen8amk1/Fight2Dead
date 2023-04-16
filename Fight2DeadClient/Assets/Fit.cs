using UnityEngine;
using UnityEngine.UI;

public class Fit : MonoBehaviour
{
    public Button button;
    public Image image;

    private void Start()
    {
        FitButtonToImageSize();
    }

    private void FitButtonToImageSize()
    {
        RectTransform imageRect = image.GetComponent<RectTransform>();
        float imageWidth = imageRect.rect.width;
        float imageHeight = imageRect.rect.height;

        RectTransform buttonRect = button.GetComponent<RectTransform>();
        buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imageWidth);
        buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageHeight);
    }
}