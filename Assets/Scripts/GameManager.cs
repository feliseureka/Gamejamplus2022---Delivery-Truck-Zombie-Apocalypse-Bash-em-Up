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
        }

        OnStateChanged?.Invoke(state);
    }

    public void HandleGameState(){}

    public void HandleUpgradeState(){
        //
    }
}

public enum GameState {
    GameState
    ,UpgradeState
}