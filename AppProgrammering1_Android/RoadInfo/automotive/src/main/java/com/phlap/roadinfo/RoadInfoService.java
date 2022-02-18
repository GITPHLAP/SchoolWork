package com.phlap.roadinfo;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;

import androidx.annotation.NonNull;
import androidx.car.app.CarAppService;
import androidx.car.app.CarContext;
import androidx.car.app.Screen;
import androidx.car.app.ScreenManager;
import androidx.car.app.Session;
import androidx.car.app.validation.HostValidator;

public class RoadInfoService extends CarAppService {
    @NonNull
    @Override
    public HostValidator createHostValidator() {
        return HostValidator.ALLOW_ALL_HOSTS_VALIDATOR;
    }

    @NonNull
    @Override
    public Session onCreateSession() {
        return new Session() {
            @NonNull
            @Override
            public Screen onCreateScreen(@NonNull Intent intent) {

                //Check if permissions is granted
                if((checkSelfPermission(Manifest.permission.ACCESS_FINE_LOCATION)
                        == PackageManager.PERMISSION_GRANTED) &&
                    (checkSelfPermission(Manifest.permission.ACCESS_COARSE_LOCATION)
                            == PackageManager.PERMISSION_GRANTED)){
                    return new RoadInfoScreen(getCarContext());
                }

                //Grant permission with another window
                //First we add the screen we should show when permission is granted
                //To Screen manager.
                //After that we open the permission screen
                ScreenManager screenManager = getCarContext().getCarService(ScreenManager.class);
                screenManager.push(new RoadInfoScreen(getCarContext()));

                return new RequestPermissionScreen(getCarContext(), () ->screenManager.pop());
            }
        };
    }
}
