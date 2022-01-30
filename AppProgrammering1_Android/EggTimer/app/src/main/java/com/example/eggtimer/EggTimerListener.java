package com.example.eggtimer;

public interface EggTimerListener {
    public void onCountDown(long timeLeft);
    public void onEggTimerStopped();
}
