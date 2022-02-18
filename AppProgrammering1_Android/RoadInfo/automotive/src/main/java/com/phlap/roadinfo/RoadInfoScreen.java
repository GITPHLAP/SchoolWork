package com.phlap.roadinfo;

import static androidx.car.app.model.Action.BACK;

import android.Manifest;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.graphics.drawable.Icon;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Handler;
import android.os.IBinder;
import android.os.Looper;
import android.util.Log;

import androidx.annotation.NonNull;
import androidx.car.app.CarContext;
import androidx.car.app.Screen;
import androidx.car.app.model.Action;
import androidx.car.app.model.ActionStrip;
import androidx.car.app.model.CarIcon;
import androidx.car.app.model.ItemList;
import androidx.car.app.model.Pane;
import androidx.car.app.model.PaneTemplate;
import androidx.car.app.model.Row;
import androidx.car.app.model.SearchTemplate;
import androidx.car.app.model.Template;
import androidx.core.app.ActivityCompat;
import androidx.lifecycle.DefaultLifecycleObserver;
import androidx.lifecycle.LifecycleOwner;

import com.phlap.roadinfo.model.SimpleIncident;

import java.util.List;

public class RoadInfoScreen extends Screen implements DefaultLifecycleObserver, HttpRequestListener {

    private final Handler mainHandler = new Handler(Looper.getMainLooper());

    private boolean finishWaiting = false;
    private boolean started = false;
    private boolean bound = false;
    private HttpRequestService requestService;
    private ItemList.Builder itemList;

    protected RoadInfoScreen(@NonNull CarContext carContext) {
        super(carContext);
        getLifecycle().addObserver(this);
        System.out.println("RoadINfoScreen - Initialized");
        System.out.println(bound);

        //bind requestService to the screen to use the services methods
        Intent requestIntent = new Intent(carContext, HttpRequestService.class);
        carContext.bindService(requestIntent, connection, Context.BIND_AUTO_CREATE);
        carContext.startService(requestIntent);
        itemList = new ItemList.Builder();
        itemList.addItem(new Row.Builder().setTitle("HelloWorld").build());

    }

    @Override
    public void onCreate(@NonNull LifecycleOwner owner) {
        System.out.println("OnCreate - started: " + started);
        DefaultLifecycleObserver.super.onCreate(owner);
        System.out.println("RoadINfoScreen - Created");
        //Waiting for bound service
//        while(!bound) {
//        }
    }

    @Override
    public void onStart(@NonNull LifecycleOwner owner) {
        System.out.println("RoadINfoScreen - Started");
        mainHandler.postDelayed(
                () -> {
                    if(bound) {
                        finishWaiting = true;

                        if (!started){
                            System.out.println(String.format("RoadInfoScreen bound: %s", bound));
                            if (bound){
                                requestService.addListeners(this);
                                requestService.start();
                                started = true;
                            }
                        }
                    }

                    invalidate();
                },
                2000);


    }

    @Override
    public void onResume(@NonNull LifecycleOwner owner) {
        System.out.println("RoadINfoScreen - Resumed");
        if (bound) {
            requestService.resume();
        }
//        DefaultLifecycleObserver.super.onResume(owner);
    }

    @NonNull
    @Override
    public Template onGetTemplate() {
        //Create item list for searchTemplate
        //TODO: should create items according to data from vejdirektoratet

        SearchTemplate.Builder searchTemplate = new SearchTemplate.Builder(new SearchTemplate.SearchCallback() {} /* do nothing */ );



        if (!finishWaiting){
            searchTemplate.setLoading(true);
        } else {
            searchTemplate
                    .setItemList(itemList.build());

        }


        return searchTemplate
                .setShowKeyboardByDefault(false)
                .setActionStrip(new ActionStrip.Builder()
                        .addAction(new Action.Builder()
                                .setOnClickListener(() -> getCarContext().finishCarApp())
                                .setIcon(CarIcon.BACK) //TODO: Change to X icon
                                .build())
                        .build())
                .build();
    }


    @Override
    public void onPause(@NonNull LifecycleOwner owner) {
        System.out.println("RoadINfoScreen - Paused");
        if (bound) {
            requestService.pause();
        }
//        DefaultLifecycleObserver.super.onPause(owner);
    }

/*    @Override
    public void onStop(@NonNull LifecycleOwner owner) {
        System.out.println("RoadInfoScreen - Stop requestThread");
    }*/

    @Override
    public void onDestroy(@NonNull LifecycleOwner owner) {
        System.out.println("RoadInfoScreen - Destroy");
        if (bound) {
            requestService.removeListener(this);
            requestService.stop();
        }
//        DefaultLifecycleObserver.super.onDestroy(owner);
    }


    private final ServiceConnection connection = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName componentName, IBinder service) {
            HttpRequestService.RequestBinder binder = (HttpRequestService.RequestBinder) service;
            requestService = binder.getService();
            bound = true;
        }

        @Override
        public void onServiceDisconnected(ComponentName componentName) {
            bound = false;
        }
    };

    @Override
    public void update(List<SimpleIncident> data) {

        itemList = new ItemList.Builder();
        for (SimpleIncident si: data) {
            itemList.addItem(
                new Row.Builder()
    //                .setOnClickListener(() -> getScreenManager().push(new LongIncidentMessageScreen(getCarContext(), longDescription)))
                        .setTitle(si.getDescription())
                        .addText(String.format("Distance: %s  From: %s  To: %s", si.getDistance(), si.getStartOfPeriodFormatted(), si.getEndOfPeriodFormatted()))
                        .build()
            );

        }


        //TODO: implement update method
        System.out.println("RoadInfoScreen - Update");
//        System.out.println(data.toString());
        invalidate();
    }
}
