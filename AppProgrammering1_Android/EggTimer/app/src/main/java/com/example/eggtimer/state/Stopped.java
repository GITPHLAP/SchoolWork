package com.example.eggtimer.state;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.time.LocalTime;

public class Stopped extends LocalTimerState{

    public Stopped(EggTimer neweggTimer) {
        super(neweggTimer);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void startTimer(LocalTime time) {
        eggTimer.onStart(time);
        eggTimer.start();

        eggTimer.setTimerState(eggTimer.getStarted());

    }

    @Override
    public void stopTimer() {
        System.err.println("Timer is already stopped");
    }
}
