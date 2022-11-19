using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneSystem : MonoBehaviour
{
    public Slider slider;
    private int max;

    public int initialMax = 10;

    void Start(){
        changeMax(initialMax);
    }

    public void changeMax(int max){
        this.max = max;
        slider.maxValue = this.max;
        slider.value = 0;
    }

    public void checkNextLevel(){
        if(slider.value >= this.max){
            changeMax(max*2);
            BroadcastMessage("NextLevel");
        }
    }

    public void increaseProgress(){
        slider.value += 1;
    }
}
