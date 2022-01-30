package com.example.eggtimer;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

@RequiresApi(api = Build.VERSION_CODES.O)
public class MainActivity extends AppCompatActivity {

    private DateTimeFormatter dtf ;
    private LocalTime time;

    private int selectedEggId;
    private boolean startedTimer;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        dtf  = DateTimeFormatter.ofPattern("mm:ss");
    }

    public void onButtonStartClicked(final View view){
        startedTimer = true;
        //Change onClick method
        view.setOnClickListener(this::onStop);

        //Disable select egg buttons
        findViewById(R.id.softBoiledBtn).setEnabled(false);
        findViewById(R.id.smiling).setEnabled(false);
        findViewById(R.id.hardBoiledBtn).setEnabled(false);

        ((Button)view).setText("Stop");

        final Handler handler = new Handler();
        handler.post(new Runnable() {
            @RequiresApi(api = Build.VERSION_CODES.O)
            @Override
            public void run() {

                //count down time
                time = time.minusSeconds(1);


                //change text on timerTxt
                TextView timerView = findViewById(R.id.timerTxt);
                timerView.setText(time.format(dtf));

                if (time.equals(LocalTime.of(0,0,0)) || !startedTimer) {
                    onStop(view);
                } else {
                    handler.postDelayed(this, 1000);
                }
            }
        });
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

    public void onStop(View v) {
        startedTimer = false;

        //Enable all select buttons
        findViewById(R.id.softBoiledBtn).setEnabled(true);
        findViewById(R.id.smiling).setEnabled(true);
        findViewById(R.id.hardBoiledBtn).setEnabled(true);

        //Reset timer
        time = getBoilTime(selectedEggId);

        //Set timertxt
        TextView timer = findViewById(R.id.timerTxt);
        timer.setText(time.format(dtf));

        //change start button attributes
        v.setEnabled(false);
        ((Button)v).setText("Start");
        v.setOnClickListener(this::onButtonStartClicked);
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

}