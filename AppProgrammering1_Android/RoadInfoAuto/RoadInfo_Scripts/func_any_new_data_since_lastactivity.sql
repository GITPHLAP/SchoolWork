CREATE OR REPLACE FUNCTION func_any_new_data_since_lastactivity(username VARCHAR(100)) RETURNS bool AS
$$
	BEGIN
        IF (SELECT lastActivity FROM UserActivity WHERE userName = username) < (SELECT lastUpdated FROM xml_data WHERE)
            THEN RETURN TRUE;
            ELSE RETURN FALSE;
        END IF;

	END;
$$ LANGUAGE plpgsql;