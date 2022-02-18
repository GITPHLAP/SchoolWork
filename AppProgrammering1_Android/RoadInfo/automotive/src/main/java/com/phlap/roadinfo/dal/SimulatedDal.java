package com.phlap.roadinfo.dal;

import android.location.Location;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonElement;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;
import com.google.gson.reflect.TypeToken;
import com.phlap.roadinfo.model.SimpleIncident;
import com.phlap.roadinfo.serializer.LocalDateTimeSerializer;

import java.lang.reflect.Type;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class SimulatedDal {

    private static GsonBuilder gsonBuilder = new GsonBuilder();

    public static String getIncidents(Location currentLocation){
        gsonBuilder.registerTypeAdapter(LocalDateTime.class, new LocalDateTimeSerializer());
        Gson gson = gsonBuilder.setPrettyPrinting().create();
        Random random = new Random();
        List<SimpleIncident> dbIncidents = dbIncidents(currentLocation);
        int numberOfIncident = random.nextInt(6-1)+1;

        List<SimpleIncident> resultincidents = new ArrayList<>();

        for (int i = 0; i<numberOfIncident; i++){
            int index = random.nextInt(dbIncidents.size());

            SimpleIncident incident = dbIncidents.get(index);
            resultincidents.add(incident);
            dbIncidents.remove(index);

        }
        Type incidentType = new TypeToken<List<SimpleIncident>>() {}.getType();
//        SimpleIncident[] incidentarr = (SimpleIncident[]) resultincidents.toArray();
        System.out.println("***************************************************");
        System.out.println(gson.toJson(resultincidents, incidentType));
        return gson.toJson(resultincidents, incidentType);
    }

    private static List<SimpleIncident> dbIncidents(Location currentLocation){
        List<SimpleIncident> dbIncidents = new ArrayList<>();

        dbIncidents.add(new SimpleIncident(currentLocation,
                "description1",
                "type1",
                LocalDateTime.now(),
                LocalDateTime.now(),
                "longDescription1111",
                (short) 1));

        dbIncidents.add(new SimpleIncident(currentLocation,
                "description2",
                "type2",
                LocalDateTime.now(),
                LocalDateTime.now(),
                "longDescription2222",
                (short) 2));

        dbIncidents.add(new SimpleIncident(currentLocation,
                "description3",
                "type3",
                LocalDateTime.now(),
                LocalDateTime.now(),
                "longDescription3333",
                (short) 3));

        dbIncidents.add(new SimpleIncident(currentLocation,
                "description4",
                "type4",
                LocalDateTime.now(),
                LocalDateTime.now(),
                "longDescription4444",
                (short) 4));

        dbIncidents.add(new SimpleIncident(currentLocation,
                "description5",
                "type5",
                LocalDateTime.now(),
                LocalDateTime.now(),
                "longDescription5555",
                (short) 5));

        dbIncidents.add(new SimpleIncident(currentLocation,
                "description5",
                "type5",
                LocalDateTime.now(),
                LocalDateTime.now(),
                "longDescription5555",
                (short) 5));


        return dbIncidents;
    }
}
