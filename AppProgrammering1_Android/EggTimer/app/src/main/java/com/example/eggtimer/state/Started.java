package com.example.eggtimer.state;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.time.LocalTime;

public class Started extends LocalTimerState{

    public Started(EggTimer neweggTimer) {
        super(neweggTimer);
    }

    @Override
    public void startTimer(LocalTime time) {
        System.err.println("Timer is already started");
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void stopTimer() {
        eggTimer.onStop();
        eggTimer.setTimerState(eggTimer.getStopped());
    }
}
