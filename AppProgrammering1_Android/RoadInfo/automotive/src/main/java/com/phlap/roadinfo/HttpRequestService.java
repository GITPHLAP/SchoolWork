package com.phlap.roadinfo;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.util.Log;

import androidx.annotation.Nullable;

import java.util.ArrayList;
import java.util.List;

public class HttpRequestService extends Service {
    private final IBinder binder = new RequestBinder();
    private Object pauseLock = new Object();
    private boolean paused = false;
    private boolean started = false;
    private List<HttpRequestListener> listeners = new ArrayList<>();

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return binder;
    }

    public void start(){
        System.out.println("RequestThread - Started");
        if (!started){
            started = true;
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

                        System.out.println("RequestThread - Running");

                        //TODO: update with the right data
                        for(HttpRequestListener l: listeners) {
                            l.update("Something");
                        }
                    }
                    return;
                }
            }).start();


        }
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
