using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneSystem : MonoBehaviour
{
    public static int totalkilled = 0;
    public static int killed = 0;
    public Slider slider;
    private static Slider sliderS;
    private int max;

    public int initialMax = 10;

    void Start(){
        killed = 0;
        totalkilled = 0;
        changeMax(initialMax);
        sliderS = slider;
    }

    public void changeMax(int max){
        this.max = max;
        slider.maxValue = this.max;
        killed = 0;
        slider.value = 0;
    }

    public void checkNextLevel(){
        if(slider.value >= this.max){
            changeMax(max*2);
            GameManager.Instance.changeState(GameState.UpgradeState);
        }
    }

    public static void increaseProgress(){
        killed++;
        totalkilled++;
        sliderS.value = killed;
    }
}
