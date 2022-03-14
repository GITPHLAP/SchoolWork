CREATE OR REPLACE FUNCTION func_is_username_valid (_username VARCHAR(100)) 
	RETURNS boolean
	LANGUAGE plpgsql
AS $$
BEGIN
	BEGIN
        IF (SELECT COUNT(1) FROM UserActivity WHERE username = _username) = 0
            THEN RETURN TRUE;
            ELSE RETURN FALSE;
        END IF;

	END;
END;$$

