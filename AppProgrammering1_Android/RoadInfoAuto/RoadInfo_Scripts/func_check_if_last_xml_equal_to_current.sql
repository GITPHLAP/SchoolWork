/**
The procedure is used to check if current xml is equal to last 

parameter is the current xml data

*/

CREATE OR REPLACE FUNCTION func_check_if_last_xml_equal_to_current(currentXml xml) RETURNS bool AS
$$
	BEGIN
        IF (SELECT COUNT(1) FROM xml_data WHERE CAST("xml" AS VARCHAR) = CAST(currentXml AS VARCHAR)) > 0
            THEN RETURN TRUE;
            ELSE RETURN FALSE;
        END IF;

	END;
$$ LANGUAGE plpgsql;