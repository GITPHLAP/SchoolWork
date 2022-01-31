package com.example.eggtimer;

import android.os.Build;

import androidx.annotation.RequiresApi;

import com.example.eggtimer.state.EggTimer;

import java.time.LocalTime;

@RequiresApi(api = Build.VERSION_CODES.O)
public class EggTimerPresenter implements EggTimerListener {
    private final View v;
    EggTimer eggTimer;

    //Constructor
    public EggTimerPresenter(final View v){
        this.v = v;
    }
    public void stop() {
        eggTimer.stopTimer();
    }

    public void start(LocalTime timeToCook) {
        eggTimer = new EggTimer();
        eggTimer.addListener(this);

        eggTimer.startTimer(timeToCook);
    }

    @Override
    public void onCountDown(String timeTxt) {
        v.onCountDown(timeTxt);
    }

    @Override
    public void onEggTimerStopped() {
        v.onEggTimerStopped();
    }

    /**
     * Interface to connect presenter to activity
     */
    public interface View {
        void onCountDown(String timeTxt);
        void onEggTimerStopped();
    }
}
