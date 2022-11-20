using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void increaseScore(int val){
        score+= val;
        text.text = score + "";
    }
}
