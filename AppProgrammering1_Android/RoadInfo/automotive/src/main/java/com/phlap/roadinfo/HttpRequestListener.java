package com.phlap.roadinfo;

import com.phlap.roadinfo.model.SimpleIncident;

import java.util.List;

public interface HttpRequestListener {
    void update(List<SimpleIncident> incidents);
}
