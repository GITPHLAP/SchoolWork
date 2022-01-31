package com.example.eggtimer;

import android.os.Build;
import android.util.Log;

import androidx.annotation.RequiresApi;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

@RequiresApi(api = Build.VERSION_CODES.O)
public class EggTimer extends Thread {

    private List<EggTimerListener> listeners = new ArrayList<EggTimerListener>();
    public EggTimer() {

    }

    private boolean started = false;
    private DateTimeFormatter dtf = DateTimeFormatter.ofPattern("mm:ss");
    String timerTxt;
    private LocalTime time;

    public void setStarted(boolean _started) {
        started = _started;
    }

    public boolean getStarted(){
        return started;
    }

    public void setTime(LocalTime _time){
        time = _time;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void run() {
        while (started) {

            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            //count down time
            time = time.minusSeconds(1);

            //change text on timerTxt
            timerTxt = time.format(dtf);
            Log.d("TimerTxt", timerTxt);

            //Send notify to all listeners
            for (EggTimerListener l : listeners) {
                l.onCountDown(timerTxt);
            }

            if (time.equals(LocalTime.of(0,0,0))) {
                onStop();
                return;
            }
        }
        //Reset the timer
        onStop();
    }

    public void onStop() {
        started = false;

        //Reset timer
        time = LocalTime.of(0, 0, 0);
        timerTxt = time.format(dtf);

        //Send notify to all listeners
        for (EggTimerListener l : listeners) {
            l.onEggTimerStopped();
        }
}

    public void addListener(EggTimerListener listener){
        listeners.add(listener);
    }

    public void removeListener(EggTimerListener listener){
        listeners.remove(listener);
    }

}
