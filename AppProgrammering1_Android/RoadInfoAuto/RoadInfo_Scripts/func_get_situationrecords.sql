CREATE OR REPLACE FUNCTION func_get_situationrecords (par_situationId VARCHAR(100)) 
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
		WHERE "situationId" = par_situationId;
END;$$
