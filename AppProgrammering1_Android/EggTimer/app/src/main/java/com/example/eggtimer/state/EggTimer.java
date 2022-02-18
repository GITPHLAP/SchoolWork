package com.example.eggtimer.state;

import android.os.Build;
import android.util.Log;

import androidx.annotation.RequiresApi;

import com.example.eggtimer.EggTimerListener;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;



/**
 * EggTimer use as a model and simulate the timer in a new Thread
 *
 *
 * @author Philip
 * @see java.lang.Thread
 * @see com.example.eggtimer.EggTimerPresenter
 * @see EggTimerListener
 */
@RequiresApi(api = Build.VERSION_CODES.O)
public class EggTimer extends Thread {
    //TODO: Maybe use integer instead of localTime It cant handle bigger then 23:59:59
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

    /**
     * startTimer call the timerState.startTimer
     * if the state is already started then its return a err message but not throwing
     * else if the state is stopped then it prepare the timer and start the thread
     *
     * @author Philip
     * @see Started
     * @see Stopped
     * @param newTime is LocalTime type
     */
    public void startTimer(LocalTime newTime){
        timerState.startTimer(newTime);
    }

    /**
     * stopTimer call the timerState.stopTimer
     * if the state is already stopped then its return a err message but not throwing
     * else if the state is started then it stop the thread
     *
     * @author Philip
     * @see Started
     * @see Stopped
     */
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

    /**
     * Thread method
     *
     * It wait for a second
     * then set the time to it self minus 1
     * after that the method will notify the listeners with the new timeleft value
     * which is formated mm:ss
     *
     * when the time is equal to 0h 0m 0s then it will stop
     *
     * @author Philip
     * @see Thread
     */
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
                stopTimer();
                return;
            }
        }
        //Reset the timer
        stopTimer();
    }

    void onStart(LocalTime newTime){
        time = newTime;
        running = true;
    }

    /**
     * Stop the timer
     * reset the time and notify listeners the timer is stopped
     *
     * @author Philip
     * @see EggTimerListener
     */
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
