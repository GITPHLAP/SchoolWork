package com.phlap.roadinfo;

import static com.phlap.roadinfo.dal.SimulatedDal.getIncidents;

import android.Manifest;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Binder;
import android.os.IBinder;
import android.util.Log;

import java.lang.reflect.Type;
import java.nio.charset.StandardCharsets;

import androidx.annotation.Nullable;
import androidx.core.app.ActivityCompat;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.reflect.TypeToken;
import com.phlap.roadinfo.model.SimpleIncident;


import org.json.JSONException;
import org.json.JSONObject;

import java.io.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import com.phlap.roadinfo.dal.SimulatedDal;
import com.phlap.roadinfo.serializer.LocalDateTimeDeserializer;
import com.phlap.roadinfo.serializer.LocalDateTimeSerializer;

import org.json.XML;

public class HttpRequestService extends Service {
    private List<SimpleIncident> lastSimpleIncidents = new ArrayList<>();

    private final IBinder binder = new RequestBinder();
    private final Object pauseLock = new Object();
    private boolean paused = false;
    private boolean started = false;
    private final List<HttpRequestListener> listeners = new ArrayList<>();

    private Location currentLocation;

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return binder;
    }


    private final LocationListener locationListener = new LocationListener() {
        @Override
        public void onLocationChanged(final Location location) {
            currentLocation = location;
        }
    };



/*    private static String convertInputStreamToString(InputStream is) throws IOException {

        ByteArrayOutputStream result = new ByteArrayOutputStream();
        byte[] buffer = new byte[102400];
        int length;
        while ((length = is.read(buffer)) != -1) {
            result.write(buffer, 0, length);
        }

        // Java 1.1
        return result.toString(StandardCharsets.UTF_8.name());

        // Java 10
//         return result.toString(StandardCharsets.UTF_8);
    }*/

    private void beginLocationListener() {
        LocationManager locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            //TODO: Something is wrong
            return;
        }
        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000,
                0, locationListener);
    }

    public void start()  {
        System.out.println("RequestThread - Started");
        if (!started){
            started = true;

            //First start listener
            beginLocationListener();

            new Thread(new Runnable() {
                @Override
                public void run() {
                    while (started) {
                        //the interval for method
                        try {
                            Thread.sleep(1000);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }

                        //Check if thread should pause
                        synchronized (pauseLock) {
                            while(paused){ //Pause thread
                                System.out.println("RequestThread - Paused");
                                try {
                                    pauseLock.wait();
                                } catch (InterruptedException e) {
                                    e.printStackTrace();
                                }
                                System.out.println("RequestThread - Resumed");
                            }
                        }

                        //Before begin the work check if thread is not stopped
                        if (!started){
                            return;
                        }

                        //TODO: Call request
                        List<SimpleIncident> newIncidents = getSimpleIncidents(currentLocation);

                        System.out.println("RequestThread - Running");

                        //TODO: update with the right data
                        if (!newIncidents.equals(lastSimpleIncidents)) {

                            for(HttpRequestListener l: listeners) {
                                l.update(newIncidents);
                            }

                            lastSimpleIncidents = newIncidents;
                        }
                    }
                    return;

                }
            }).start();


        }
    }

/*    public List<SimpleIncident> getIncidentsFromDAL(Location currentLocation){
        return SimulatedDal.getIncidents(currentLocation);
    }*/

    /**
     * Deserialize json incidents to SimpleIncidents
     * @param currentLocation currentlocation which DB use to filter
     * @return
     */
    private List<SimpleIncident> getSimpleIncidents(Location currentLocation){
        List<SimpleIncident> incidents = new ArrayList<>();

        String jsonIncidents = SimulatedDal.getIncidents(currentLocation);

        GsonBuilder gsonBuilder = new GsonBuilder();
        gsonBuilder.registerTypeAdapter(LocalDateTime.class, new LocalDateTimeDeserializer());

        Gson gson = gsonBuilder.setPrettyPrinting().create();

        Type incidentType = new TypeToken<List<SimpleIncident>>() {}.getType();
        incidents = gson.fromJson(jsonIncidents, incidentType);
        return incidents;
    }

    public void pause(){
        synchronized (pauseLock){
            paused = true;
        }
    }

    public void resume() {
        synchronized (pauseLock) {
            paused = false;
            pauseLock.notifyAll();
        }
    }

    public void stop() {
        //if paused then return from paused while
        if (paused) {
            synchronized (pauseLock){
                paused = false;
                pauseLock.notifyAll();
            }
        }
        started = false;
    }

    public void addListeners(HttpRequestListener listener) {
        listeners.add(listener);
    }
    public void removeListener(HttpRequestListener listener) {
        listeners.remove(listener);
    }

    public class RequestBinder extends Binder {
        HttpRequestService getService() {return HttpRequestService.this;}
    }
}
