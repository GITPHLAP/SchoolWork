/**
The procedure is used to update user activity 

parameter is username

*/

CREATE OR REPLACE PROCEDURE proc_update_user_activity(username VARCHAR(100))
LANGUAGE plpgsql
AS $$
BEGIN

	INSERT INTO UserActivity(userName, lastActivity)
	VALUES (username, NOW())
	ON CONFLICT(userName) DO UPDATE 
		SET lastActivity = NOW();

COMMIT;

END; $$