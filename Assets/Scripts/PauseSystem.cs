using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public OptionsPopUp opManager;
    void Update()
    {
        if(GameManager.Instance.state == GameState.GameState){
            if(Input.GetKeyDown(KeyCode.Escape)){
                GameManager.Instance.changeState(GameState.PauseState);
                opManager.openOptionUI();
            }
        }
    }

    public void closePause(){
        GameManager.Instance.changeState(GameState.GameState);
    }
}
