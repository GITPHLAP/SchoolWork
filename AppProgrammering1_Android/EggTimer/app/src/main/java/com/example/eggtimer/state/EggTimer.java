package com.example.eggtimer.state;

import android.os.Build;
import android.util.Log;

import androidx.annotation.RequiresApi;

import com.example.eggtimer.EggTimerListener;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

@RequiresApi(api = Build.VERSION_CODES.O)
public class EggTimer extends Thread {

    private List<EggTimerListener> listeners = new ArrayList<>();
    public EggTimer() {
        started = new Started(this);
        stopped = new Stopped(this);

        timerState = stopped;
    }

    //State methods and props
    TimerState started;
    TimerState stopped;

    TimerState timerState;

    void setTimerState(TimerState newTimerState){
        timerState = newTimerState;
    }

    public void startTimer(LocalTime newTime){
        timerState.startTimer(newTime);
    }
    public void stopTimer(){
        timerState.stopTimer();
    }

    public TimerState getStarted(){return started;}

    public TimerState getStopped(){ return stopped;}
    //END OF State methods and props

    boolean running = false;
    private DateTimeFormatter dtf = DateTimeFormatter.ofPattern("mm:ss");
    String timerTxt;
    private LocalTime time;

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void run() {
        while (running) {

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

    void onStart(LocalTime newTime){
        time = newTime;
        running = true;
    }

    void onStop() {
        running = false;

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
