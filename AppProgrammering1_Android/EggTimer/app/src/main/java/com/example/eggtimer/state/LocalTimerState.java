package com.example.eggtimer.state;

public abstract class LocalTimerState implements TimerState {
    EggTimer eggTimer;
    public LocalTimerState(EggTimer neweggTimer){
        eggTimer = neweggTimer;
    }
}
