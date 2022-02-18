package com.phlap.roadinfo.model;

import android.location.Location;

import java.security.KeyStore;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

//Only temporary this model is for now used for simulated data
public class SimpleIncident {
    private static DateTimeFormatter format = DateTimeFormatter.ofPattern("dd-MM-yyyy");
    //props
    private Location location;
    private String description;
    private String type;
    private LocalDateTime startOfPeriod;
    private LocalDateTime endOfPeriod;
    private String longDescription;
    private short distance; //short to save space - Dk is not so large

    public SimpleIncident(Location location, String description, String type, LocalDateTime startOfPeriod, LocalDateTime endOfPeriod, String longDescription, short distance) {
        this.location = location;
        this.description = description;
        this.type = type;
        this.startOfPeriod = startOfPeriod;
        this.endOfPeriod = endOfPeriod;
        this.longDescription = longDescription;
        this.distance = distance;

    }

    public Location getLocation() {
        return location;
    }

    public String getDescription() {
        return description;
    }

    public String getType() {
        return type;
    }

    public LocalDateTime getStartOfPeriod() {
        return startOfPeriod;
    }

    public LocalDateTime getEndOfPeriod() {
        return endOfPeriod;
    }

    public String getLongDescription() {
        return longDescription;
    }

    public short getDistance() {
        return distance;
    }


    public String getStartOfPeriodFormatted() {
        if(startOfPeriod == null)
            return null;
        return startOfPeriod.format(format);
    }

    public String getEndOfPeriodFormatted() {
        if(endOfPeriod == null)
            return null;
        return endOfPeriod.format(format);
    }
}
