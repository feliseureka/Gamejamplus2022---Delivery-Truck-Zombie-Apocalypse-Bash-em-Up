using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class waypoint : MonoBehaviour
{
    public Image image;
    public Transform text;
    public Transform target;

    private Transform pl;
    void Awake(){
        pl = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        float minX = image.GetPixelAdjustedRect().width;
        float maxX = Screen.width - minX;
        float minY = image.GetPixelAdjustedRect().height;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        image.transform.position = pos;
        text.position = pos + new Vector2(0, 50);
        text.GetComponent<TMP_Text>().text = Mathf.Floor(Vector3.Distance(target.position, pl.position)) + "";
    }
}
