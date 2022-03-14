CREATE OR REPLACE FUNCTION func_get_closest_situations (currentLongi double precision, currentLati double precision) 
	RETURNS TABLE (
		"s_id" VARCHAR(100),
	    "s_version" bigint,
	    s_situationVersionTime timestamp with time zone,
	    s_locationText VARCHAR(200),
	    s_eventText VARCHAR(200),
	    s_regionText VARCHAR(30),
	    s_nonCodeableInformationText text,
	    s_durationText VARCHAR(100),
	    s_icon VARCHAR(40),
	    s_status VARCHAR(20)
	) 
	LANGUAGE plpgsql
AS $$
BEGIN


	CREATE TEMP TABLE temp_test AS
	SELECT "sr_situationId", sr_distance FROM func_get_closest_situationrecords(currentLongi, currentLati);
	RETURN QUERY 
		SELECT "id",
	        "version",
	        situationVersionTime,
	        locationText,
	        eventText,
	        regionText,
	        nonCodeableInformationText,
	        durationText,
	        icon,
	        status
		FROM situations s
		JOIN temp_test tt ON s."id" = tt."sr_situationId"
		ORDER BY tt.sr_distance
		LIMIT 5;
		
		DROP TABLE temp_test;
END;$$

