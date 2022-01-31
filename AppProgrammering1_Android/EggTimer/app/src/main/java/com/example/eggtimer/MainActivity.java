package com.example.eggtimer;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

@RequiresApi(api = Build.VERSION_CODES.O)
public class MainActivity extends AppCompatActivity implements EggTimerListener {
    ImageButton softButton;
    ImageButton smilingButton;
    ImageButton hardButton;
    private DateTimeFormatter dtf;
    private LocalTime time;
    EggTimer eggTimer;
    private int selectedEggId;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        dtf  = DateTimeFormatter.ofPattern("mm:ss");
        eggTimer = new EggTimer();
        softButton = findViewById(R.id.softBoiledBtn);
        smilingButton = findViewById(R.id.smiling);
        hardButton = findViewById(R.id.hardBoiledBtn);
    }

    public void onButtonStartClicked(final View view){


        System.err.println(eggTimer.isAlive());
        if(!eggTimer.getStarted()){

            eggTimer.addListener(this);
            eggTimer.setTime(time);
            eggTimer.setStarted(true);
            ((Button)view).setText("Stop");

            eggTimer.start();
        }
        else { // manual stop timer
            eggTimer.onStop();
        }
        changeEggButtons();
    }

    @SuppressLint("NonConstantResourceId")
    private LocalTime getBoilTime(int eggId){
        switch (eggId) {
            case R.id.softBoiledBtn:
                return LocalTime.of(0, 0, 10);

            case R.id.smiling:
                return LocalTime.of(0, 7, 0);

            case R.id.hardBoiledBtn:
                return LocalTime.of(0, 10, 0);

            default:
                throw new RuntimeException("Unknown button id");
        }
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void onButtonEggSelectedClicked(View view) {
        time = getBoilTime(view.getId());
        SelectEggMethod(view.getId(), time);

    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    private void SelectEggMethod(int eggId, LocalTime time) {
        selectedEggId = eggId;

        //Set boil time to timer
        TextView timer = findViewById(R.id.timerTxt);
        timer.setText(time.format(dtf));

        //Enable startButton
        Button startbtn = findViewById(R.id.startBtn);
        startbtn.setEnabled(true);
    }

    private void changeEggButtons() {


        if (hardButton.isEnabled()) {
            hardButton.setEnabled(false);
            hardButton.setAlpha(0.5f);

            smilingButton.setEnabled(false);
            smilingButton.setAlpha(0.5f);

            softButton.setEnabled(false);
            softButton.setAlpha(0.5f);

        } else {
            hardButton.setEnabled(true);
            hardButton.setAlpha(1.0f);

            smilingButton.setEnabled(true);
            smilingButton.setAlpha(1.0f);

            softButton.setEnabled(true);
            softButton.setAlpha(1.0f);
        }

    }

    /**
     * Refresh view with the remaining time
     * @param timeTxt remaining time
     */
    @Override
    public void onCountDown(String timeTxt) {
        TextView timer = findViewById(R.id.timerTxt);

        timer.setText(timeTxt);
    }

    /**
     * set global eggtimer prop "started" to false
     * Change the start/stop button text to start
     * Create new eggtimer object
     */
    @Override
    public void onEggTimerStopped() {
        eggTimer.setStarted(false);

        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Button startBtn = findViewById(R.id.startBtn);
                startBtn.setText("Start");

                TextView timer = findViewById(R.id.timerTxt);
                timer.setText("00:00");

                //enable egg buttons again
                changeEggButtons();
            }
        });

        //reset the time
        time = LocalTime.of(0, 0, 0);

        eggTimer = new EggTimer();


    }
}