package com.example.eggtimer;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

@RequiresApi(api = Build.VERSION_CODES.O)
public class MainActivity extends AppCompatActivity implements EggTimerListener {

    private DateTimeFormatter dtf;
    private LocalTime time;
    EggTimer eggTimer;
    private int selectedEggId;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        dtf  = DateTimeFormatter.ofPattern("mm:ss");

    }

    public void onButtonStartClicked(final View view){

        ((Button)view).setText("Stop");

        eggTimer = new EggTimer(time);

        if(!eggTimer.isTimerStarted()){
            eggTimer.setTimerState(true);
            changeEggButtons();
            eggTimer.start();
        }
        else {
            eggTimer.setTimerState(false);

            ((Button)view).setText("Start");
            changeEggButtons();

        }
    }

    @SuppressLint("NonConstantResourceId")
    private LocalTime getBoilTime(int eggId){
        switch (eggId) {
            case R.id.softBoiledBtn:
                return LocalTime.of(0, 5, 0);

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
        ImageButton soft = findViewById(R.id.softBoiledBtn);
        ImageButton smiling = findViewById(R.id.smiling);
        ImageButton hard = findViewById(R.id.hardBoiledBtn);

        if (hard.isEnabled()) {
            hard.setEnabled(false);
            smiling.setEnabled(false);
            soft.setEnabled(false);
        } else {
            hard.setEnabled(false);
            smiling.setEnabled(false);
            soft.setEnabled(false);
        }

    }

    @Override
    public void onCountDown(long timeLeft) {

    }

    @Override
    public void onEggTimerStopped() {

    }
}