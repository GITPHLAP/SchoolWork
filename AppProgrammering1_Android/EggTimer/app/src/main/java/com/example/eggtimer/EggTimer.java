package com.example.eggtimer;

import android.annotation.SuppressLint;
import android.os.Build;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.RequiresApi;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

public class EggTimer extends Thread {

    private Boolean running = false;
    private DateTimeFormatter dtf;
    String timerTxt;
    LocalTime time;

    public void setTimerState(Boolean state) {
        running = state;
    }


    public Boolean isTimerStarted(){
        return running;
    }


    @RequiresApi(api = Build.VERSION_CODES.O)
    public EggTimer(LocalTime _time) {
        dtf  = DateTimeFormatter.ofPattern("mm:ss");
        time = _time;
    }


    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void run() {

        if (running) {
            //count down time
            time = time.minusSeconds(1);

            //change text on timerTxt
            timerTxt = time.format(dtf);
            Log.d("TimerTxt", timerTxt);
            if (time.equals(LocalTime.of(0,0,0))) {
                onStop();
            } else {

            }
        } else {
            onStop();
        }

    }

    public void onStop() {
        running = false;

        //Reset timer
        timerTxt = "00:00";
    }

}
