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
public class MainActivity extends AppCompatActivity implements EggTimerPresenter.View {
    private boolean timerStarted = false;

    //Global buttons
    ImageButton softButton;
    ImageButton smilingButton;
    ImageButton hardButton;
    TextView timerTxtView;
    Button startButton;

    private DateTimeFormatter dtf;
    private LocalTime time;
    EggTimerPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        dtf  = DateTimeFormatter.ofPattern("mm:ss");

        //Instans of view buttons
        softButton = findViewById(R.id.softBoiledBtn);
        smilingButton = findViewById(R.id.smiling);
        hardButton = findViewById(R.id.hardBoiledBtn);
        startButton = findViewById(R.id.startBtn);
        timerTxtView = findViewById(R.id.timerTxt);

        presenter = new EggTimerPresenter(this);
    }

    public void onButtonStartClicked(final View view){

        if(!timerStarted){
            timerStarted = true;
            startButton.setText("Stop");

            presenter.start(time);
        }
        else { // manual stop timer
            presenter.stop();
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
        SelectEggMethod(view.getId());
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    private void SelectEggMethod(int eggId) {
        //set global boil time
        time = getBoilTime(eggId);

        //Set boil time to timer
        timerTxtView.setText(time.format(dtf));

        //Enable startButton
        startButton.setEnabled(true);
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
        timerTxtView.setText(timeTxt);
    }

    /**
     * set global eggtimer prop "started" to false
     * Change the start/stop button text to start
     * Create new eggtimer object
     */
    @Override
    public void onEggTimerStopped() {

        timerStarted = false;
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                startButton.setText("Start");
                startButton.setEnabled(false);

                timerTxtView.setText("00:00");

                //enable egg buttons again
                changeEggButtons();
            }
        });

        //reset the time
        time = LocalTime.of(0, 0, 0);

    }
}