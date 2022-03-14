CREATE OR REPLACE FUNCTION func_get_closest_situationrecords (currentLongi double precision, currentLati double precision) 
	RETURNS TABLE (
		"sr_id" VARCHAR(100),
        "sr_version" bigint,
        "sr_situationId" VARCHAR(100),
        sr_situationRecordCreationTime timestamp with time zone,
        sr_situationRecordVersionTime timestamp with time zone,
        sr_validityStatus VARCHAR(50),
        sr_startOfPeriod timestamp with time zone,
        sr_endOfPeriod timestamp with time zone,
        sr_generalcomment_description text,
        sr_generalcomment_locationDescriptor text,
        sr_generalcomment_other text,
        sr_latitude double precision,
        sr_longitude double precision,
        sr_temporarySpeedLimit int,
        sr_carParkIdentity VARCHAR(50),
        sr_carParkStatus VARCHAR(50),
        sr_publicEventType VARCHAR(50),
        "sr_type" VARCHAR(100)	
	) 
	LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY 
		SELECT "id",
            "version",
            "situationId",
            situationRecordCreationTime,
            situationRecordVersionTime,
            validityStatus,
            startOfPeriod,
            endOfPeriod,
            generalcomment_description,
            generalcomment_locationDescriptor,
            generalcomment_other,
            latitude,
            longitude,
            temporarySpeedLimit,
            carParkIdentity,
            carParkStatus,
            publicEventType,
            "type"
		FROM "situationRecords"
		WHERE func_calculate_distance(currentLongi, currentLati, longitude, latitude) <= 10; --distance under or equal to 10 km  
END;$$
