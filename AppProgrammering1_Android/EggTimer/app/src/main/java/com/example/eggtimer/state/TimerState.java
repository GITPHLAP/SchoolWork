package com.example.eggtimer.state;

import java.time.LocalTime;

public interface TimerState {
    void startTimer(LocalTime time);
    void stopTimer();
}
