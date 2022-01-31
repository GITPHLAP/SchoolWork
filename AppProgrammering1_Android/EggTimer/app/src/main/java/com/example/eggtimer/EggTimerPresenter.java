package com.example.eggtimer;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.time.LocalTime;

@RequiresApi(api = Build.VERSION_CODES.O)
public class EggTimerPresenter implements EggTimerListener {
    private View v;
    EggTimer eggTimer;

    //Constructor
    public EggTimerPresenter(View v){
        this.v = v;
    }
    public void stop() {
        eggTimer.onStop();
    }

    public void start(LocalTime timeToCook) {
        eggTimer = new EggTimer();
        eggTimer.addListener(this);
        eggTimer.setTime(timeToCook);
        eggTimer.setStarted(true);

        eggTimer.start();
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
     * Interface to activity
     */
    public interface View {
        void onCountDown(String timeTxt);
        void onEggTimerStopped();
    }
}
