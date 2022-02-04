package com.example.aatest;

import android.content.Intent;
import android.os.Handler;
import android.os.Looper;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.car.app.CarContext;
import androidx.car.app.Screen;
import androidx.car.app.model.Action;
import androidx.car.app.model.ItemList;
import androidx.car.app.model.ListTemplate;
import androidx.car.app.model.Pane;
import androidx.car.app.model.PaneTemplate;
import androidx.car.app.model.Row;
import androidx.car.app.model.SearchTemplate;
import androidx.car.app.model.Template;
import androidx.car.app.model.TemplateWrapper;
import androidx.lifecycle.DefaultLifecycleObserver;
import androidx.lifecycle.LifecycleOwner;


public class HelloScreen extends Screen implements DefaultLifecycleObserver, MyTestListener {

    Pane.Builder paneBuilder = new Pane.Builder().addRow(new Row.Builder().setTitle(text).build());
    PaneTemplate.Builder test = new PaneTemplate.Builder(paneBuilder.build());
    PaneTemplate paneTemplate = test.setHeaderAction(Action.APP_ICON).build();
    TemplateWrapper wrapper;

    /*private final Handler mHandler = new Handler(Looper.getMainLooper());
    static int startcounter = 0;*/
    static String text = "HelloWorld";
    static MyWorker worker = new MyWorker();
//    Pane.Builder paneBuilder = new Pane.Builder();
    public HelloScreen(@NonNull CarContext carContext) {
        super(carContext);
        getLifecycle().addObserver(this);
        System.out.println("Worker will start");
        worker.addListener(this);
        worker.start();
        System.out.println("Worker started");

        wrapper = TemplateWrapper.wrap(paneTemplate);


/*        counter++;
        System.out.println(counter);*/
    }

/*    public HelloScreen(@NonNull CarContext carContext, String test) {
        this(carContext);
        System.out.println(test);
    }*/

/*    @Override
    @SuppressWarnings({"FutureReturnValueIgnored"})
    public void onStart(@NonNull LifecycleOwner owner) {
        // Post a message that finishes loading the template after some time.
        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                while(true){
                    startcounter++;
                    try {
                        Thread.sleep(10000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                    invalidate();

                    System.out.println(startcounter);
                }
            }
        },1000);
    }*/

    @NonNull
    @Override
    public Template onGetTemplate() {
/*        wrapper.setRefresh(true);
        paneBuilder = new Pane.Builder().addRow(new Row.Builder().setTitle(text).build());

        paneBuilder.setLoading(false);

        paneTemplate = new PaneTemplate.Builder(paneBuilder.build())
                .setHeaderAction(Action.APP_ICON).build();

        wrapper.setTemplate(paneTemplate);


        return wrapper.getTemplate();*/

      /*  return new PaneTemplate.Builder(paneBuilder.build())
                .setHeaderAction(Action.APP_ICON)
                .build();*/
        ItemList.Builder itemList = new ItemList.Builder();
        itemList.addItem(new Row.Builder().setTitle(text).build());

        return new SearchTemplate.Builder(
                new SearchTemplate.SearchCallback() {
                    @Override
                    public void onSearchTextChanged(@NonNull String searchText) {
                        refresh("SearchText");
                    }

                    @Override
                    public void onSearchSubmitted(@NonNull String searchText) {
                        refresh("SearchSubmit");
                    }
                }
        ).setItemList(itemList.build())
                .setShowKeyboardByDefault(false)
                .build();
    }

    @Override
    public void refresh(String text) {
        System.out.println(text);
        paneBuilder.setLoading(true);
        this.text = text;
        invalidate();
    }

    @Override
    public void onDestroy(@NonNull LifecycleOwner owner) {
    }
}
