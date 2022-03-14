CREATE OR REPLACE FUNCTION func_get_situations () 
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
		FROM situations
		LIMIT 5;
END;$$

