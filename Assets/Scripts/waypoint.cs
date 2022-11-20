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

    private Vector2 LastClamp = new Vector2(0,0);

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
        if(pos.x <= 0 || pos.x >= Screen.width){
            if(pl.position.x > target.position.x){
                LastClamp.x = minX;
            }else if(pl.position.x < target.position.x) LastClamp.x = maxX;
            pos.x = LastClamp.x;
        }
        if(pos.y <= 0 || pos.y >= Screen.height){
            if(pl.position.z > target.position.z){
                LastClamp.y = minY;
            }else LastClamp.y = maxY;
            pos.y = LastClamp.y;
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        image.transform.position = pos;
        text.position = pos + new Vector2(0, 50);
        text.GetComponent<TMP_Text>().text = Mathf.Floor(Vector3.Distance(target.position, pl.position)) + "";
    }
}
