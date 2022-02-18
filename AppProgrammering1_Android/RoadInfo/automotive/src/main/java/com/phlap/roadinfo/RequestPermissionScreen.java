package com.phlap.roadinfo;

import androidx.annotation.NonNull;
import androidx.car.app.CarContext;
import androidx.car.app.CarToast;
import androidx.car.app.Screen;
import androidx.car.app.model.Action;
import androidx.car.app.model.CarColor;
import androidx.car.app.model.MessageTemplate;
import androidx.car.app.model.OnClickListener;
import androidx.car.app.model.ParkedOnlyOnClickListener;
import androidx.car.app.model.Template;

import java.util.ArrayList;
import java.util.List;

import static android.Manifest.permission.ACCESS_FINE_LOCATION;
import static android.Manifest.permission.ACCESS_COARSE_LOCATION;

public class RequestPermissionScreen extends Screen {

    GrantedCallback grantedCallback;

    protected RequestPermissionScreen(@NonNull CarContext carContext, GrantedCallback callback) {
        super(carContext);
        grantedCallback = callback;
    }

    @NonNull
    @Override
    public Template onGetTemplate() {
        List<String> permissions = new ArrayList<>();
        permissions.add(ACCESS_FINE_LOCATION);
        permissions.add(ACCESS_COARSE_LOCATION);

        String msg = "This app need following permissions";

        OnClickListener listener = ParkedOnlyOnClickListener.create(() ->
                getCarContext().requestPermissions(
                        permissions,
                        (granted, rejected) -> {
                            CarToast.makeText(
                                    getCarContext(),
                                    String.format("Granted: %s   Rejected: %s", granted, rejected),
                                    CarToast.LENGTH_LONG).show();
                            if(!granted.isEmpty()) {
                                grantedCallback.onPermissionGranted();
                                finish(); //pop the screen from screenManager
                            }
                        }));

        Action action = new Action.Builder()
                .setTitle("Grant Access")
                .setBackgroundColor(CarColor.BLUE)
                .setOnClickListener(listener)
                .build();

        return new MessageTemplate.Builder(msg)
                .addAction(action)
                .setHeaderAction(Action.APP_ICON).build();
    }

    /**
     * Callback to notify when permissions is granted
     */
    public interface GrantedCallback {
        void onPermissionGranted();
    }
}
