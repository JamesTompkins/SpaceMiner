using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{
    [SerializeField][Range(0,1)] float value;
    float fullWidth;
    public RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        fullWidth = GetComponent<RectTransform>().rect.width;
        rect = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void setSpeedBar(float value) {
        float newWidth = fullWidth * (value / MovementController.maxSpeed);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }
}
