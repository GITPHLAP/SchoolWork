package com.example.aatest;

import android.app.Service;
import android.content.Intent;

import androidx.annotation.NonNull;
import androidx.car.app.CarAppService;
import androidx.car.app.CarContext;
import androidx.car.app.Screen;
import androidx.car.app.Session;
import androidx.car.app.validation.HostValidator;

public class HelloService extends CarAppService {
    @NonNull
    @Override
    public HostValidator createHostValidator() {
        return HostValidator.ALLOW_ALL_HOSTS_VALIDATOR;
    }

    @NonNull
    @Override
    public Session onCreateSession() {
        System.err.println("***************************************************************************************************************");
        return new Session() {
            @NonNull
            @Override
            public Screen onCreateScreen(@NonNull Intent intent) {
/*                CarContext test =  getCarContext();
                Intent hej = new Intent();
                hej.putExtra("Jens", 12);
                test.setCarAppResult(0, hej);*/
                return new HelloScreen(getCarContext());
            }
        };
    }
}
