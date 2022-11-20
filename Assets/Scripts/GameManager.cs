using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;

    public static event Action<GameState> OnStateChanged;

    void Start()
    {
        if(Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        changeState(GameState.GameState);
    }

    public void changeState(GameState state){
        this.state = state;
        switch (state){
            case GameState.GameState:
                HandleGameState();
                break;
            case GameState.UpgradeState:
                HandleUpgradeState();
                break;
            case GameState.PauseState:
                HandlePauseState();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }

        OnStateChanged?.Invoke(state);
    }

    public void HandleGameState(){
        Time.timeScale = 1;
    }

    public void HandleUpgradeState(){
        Time.timeScale = 0;
    }
    public void HandlePauseState(){
        Time.timeScale = 0;
    }
    public void HandleGameOver(){
        Time.timeScale = 0;
    }
}

public enum GameState {
    GameState
    ,UpgradeState,
    PauseState,
    GameOver,
    MainMenu
}