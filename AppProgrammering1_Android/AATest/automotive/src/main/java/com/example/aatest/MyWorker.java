package com.example.aatest;

import java.util.ArrayList;
import java.util.List;

public class MyWorker extends Thread{

    private List<MyTestListener> listeners = new ArrayList<>();
    static int counter = 0;

    public void MyWorker() {
        System.out.println("Worker created");
    }
    @Override
    public void run() {
        while(true) {
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            counter++;
//                            System.out.println(counter);

            for (MyTestListener l : listeners) {
                l.refresh(Integer.toString(counter));
            }

        }
    }

    public void addListener(MyTestListener listener){
        listeners.add(listener);
        System.out.println("Screen is add to listener");
    }

}
