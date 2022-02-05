package com.phlap.roadinfo;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.graphics.drawable.Icon;
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
import androidx.car.app.model.Row;
import androidx.car.app.model.SearchTemplate;
import androidx.car.app.model.Template;
import androidx.lifecycle.DefaultLifecycleObserver;
import androidx.lifecycle.LifecycleOwner;

public class RoadInfoScreen extends Screen implements DefaultLifecycleObserver, HttpRequestListener {

    private final Handler mainHandler = new Handler(Looper.getMainLooper());

    private boolean started = false;
    private boolean bound = false;
    private HttpRequestService requestService;
    protected RoadInfoScreen(@NonNull CarContext carContext) {
        super(carContext);
        getLifecycle().addObserver(this);
        System.out.println("RoadINfoScreen - Initialized");
        System.out.println(bound);

        //bind requestService to the screen to use the services methods
        Intent requestIntent = new Intent(carContext, HttpRequestService.class);
        carContext.bindService(requestIntent, connection, Context.BIND_AUTO_CREATE );
        carContext.startService(requestIntent);

    }

    @NonNull
    @Override
    public Template onGetTemplate() {
        //Create item list for searchTemplate
        //TODO: should create items according to data from vejdirektoratet

        ItemList.Builder itemList = new ItemList.Builder();
        itemList.addItem(new Row.Builder().setTitle("HelloWorld").build());

        return new SearchTemplate.Builder(
                new SearchTemplate.SearchCallback() {}//do nothing
        ).setItemList(itemList.build())
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
    public void onCreate(@NonNull LifecycleOwner owner) {
        DefaultLifecycleObserver.super.onCreate(owner);
        System.out.println("RoadINfoScreen - Created");
        //Waiting for bound service
/*        mainHandler.postDelayed(
                () -> {
                    invalidate();
                },
                1000);*/
    }

    @Override
    public void onStart(@NonNull LifecycleOwner owner) {
        DefaultLifecycleObserver.super.onStart(owner);
        System.out.println("RoadINfoScreen - Started");
        if (!started){
            System.out.println(String.format("RoadInfoScreen bound: %s", bound));
            if (bound){
                requestService.addListeners(this);
                requestService.start();
                started = true;
            }
        }
    }

    @Override
    public void onResume(@NonNull LifecycleOwner owner) {
        System.out.println("RoadINfoScreen - Resumed");
        if (bound) {
            requestService.resume();
        }
//        DefaultLifecycleObserver.super.onResume(owner);
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


    private ServiceConnection connection = new ServiceConnection() {
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
    public void update(String data) {
        //TODO: implement update method
        System.out.println("RoadInfoScreen - Update");
        System.out.println(Thread.activeCount());
        invalidate();
    }
}
