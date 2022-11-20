using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    public GameObject GOUI;
    public TMP_Text bScore;
    public TMP_Text cScore;
    public TMP_Text killScore;

    public void GameEnd(){
        GameManager.Instance.changeState(GameState.GameOver);
        if(!PlayerPrefs.HasKey("BestScore")) PlayerPrefs.SetInt("BestScore", 0);
        int bs = PlayerPrefs.GetInt("BestScore");
        int rs = ScoreManager.score;
        GOUI.SetActive(true);
        if(bs < rs){
            PlayerPrefs.SetInt("BestScore", rs);
        }
        bScore.text = "Best Score: " + Mathf.Max(rs,bs);
        cScore.text = "Score: " + rs;
        killScore.text = "Killed: " + MilestoneSystem.killed;
    }

    public void ToMenu(){
        GameManager.Instance.changeState(GameState.GameState);
        AudioSystem.Instance.StopMusic();
        AudioSystem.Instance.PlayMusic(0);
    }
}
