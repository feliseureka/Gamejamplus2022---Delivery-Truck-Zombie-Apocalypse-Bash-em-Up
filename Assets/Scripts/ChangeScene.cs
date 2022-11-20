using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void moveTo(int i){
        SceneManager.LoadScene(i);
        if(i == 0){
            AudioSystem.Instance.PlayMusic(1);
        }else{
            AudioSystem.Instance.PlayMusic(0);
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
