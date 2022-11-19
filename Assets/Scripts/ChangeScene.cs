using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void moveTo(int i){
        SceneManager.LoadScene(i);
    }
}
