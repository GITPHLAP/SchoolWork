--CreateTable

--DROP TABLE public.applicationuser;
CREATE TABLE IF NOT EXISTS public.applicationuser
(
    id SERIAL PRIMARY KEY,
    username character varying(256) NOT NULL,
    normalizedusername character varying(256) NOT NULL,
    email character varying(256) ,
    normalizedemail character varying(256) ,
    emailconfirmed bit(1) NOT NULL,
    passwordhash character varying,
	securitystamp varchar(255),
	concurrencystamp varchar(255),
    phonenumber character varying(50) ,
    phonenumberconfirmed bit(1) NOT NULL,
    twofactorenabled bit(1) NOT NULL,
	lockoutend timestamp with time zone,
	lockoutenable bit(1) NOT NULL,
	accessfailedcount int NOT NULL,
    deleted bool DEFAULT false,
    authenticatorkey varchar
);

CREATE TABLE IF NOT EXISTS public.applicationrole
(
    id SERIAL PRIMARY KEY,
    name VARCHAR(256),
    normalizedename VARCHAR(256)
);

CREATE TABLE IF NOT EXISTS public.userlogin
(
    loginprovider varchar(128) not null,
    providerkey varchar(128) not null,
    providerdisplayname varchar,
    userid int not null REFERENCES applicationuser (id), 
    PRIMARY KEY  (loginprovider, providerkey)


)

CREATE TABLE IF NOT EXISTS public.recoverycodes
(
    code varchar not null,
    redeemed bool not null,
    userid int not null REFERENCES applicationuser (id), 
    PRIMARY KEY  (userid, code)
)


--Queries
CREATE OR REPLACE FUNCTION public.func_find_user_by_id(par_userid int)
    RETURNS TABLE(_id int, _username character varying(256), _normalizedusername character varying(256),
	_email character varying(256), _normalizedemail character varying(256),
	_emailconfirmed bool, _passwordhash character varying,
	_securitystamp character varying(255), _concurrencystamp character varying(255) ,
	_phonenumber character varying(50), _phonenumberconfirmed bool ,
	_twofactorenabled bool, _lockoutend timestamp with time zone,
	_lockoutenable bool, _accessfailedcount integer, _deleted bool, _authenticatorkey varchar) 
    LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	RETURN QUERY 
		SELECT id,
			username, 
			normalizedusername, 
			email, 
			normalizedemail, 
			CAST(CAST(emailconfirmed AS INT) AS bool), 
			passwordhash, 
			securitystamp, 
			concurrencystamp, 
			phonenumber, 
			CAST(CAST(phonenumberconfirmed AS INT) AS bool), 
			CAST(CAST(twofactorenabled AS INT) AS bool), 
			lockoutend, 
			CAST(CAST(lockoutenable AS INT) AS bool), 
			accessfailedcount,
			deleted,
            authenticatorkey
		FROM applicationuser
		WHERE id = par_userid AND deleted <> true
		LIMIT 1;
END;
$BODY$;


CREATE OR REPLACE FUNCTION public.func_find_user_by_name(par_name varchar)
    RETURNS TABLE(_id int, _username character varying(256), _normalizedusername character varying(256),
	_email character varying(256), _normalizedemail character varying(256),
	_emailconfirmed bool, _passwordhash character varying,
	_securitystamp character varying(255), _concurrencystamp character varying(255) ,
	_phonenumber character varying(50), _phonenumberconfirmed bool ,
	_twofactorenabled bool, _lockoutend timestamp with time zone,
	_lockoutenable bool, _accessfailedcount integer, _deleted bool, _authenticatorkey varchar) 
    LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	RETURN QUERY 
		SELECT id,
			username, 
			normalizedusername, 
			email, 
			normalizedemail, 
			CAST(CAST(emailconfirmed AS INT) AS bool), 
			passwordhash, 
			securitystamp, 
			concurrencystamp, 
			phonenumber, 
			CAST(CAST(phonenumberconfirmed AS INT) AS bool), 
			CAST(CAST(twofactorenabled AS INT) AS bool), 
			lockoutend, 
			CAST(CAST(lockoutenable AS INT) AS bool), 
			accessfailedcount,
			deleted,
            authenticatorkey
		FROM applicationuser
		WHERE normalizedusername = par_name AND deleted <> true
		LIMIT 1;
END;
$BODY$;



CREATE OR REPLACE FUNCTION public.func_find_user_by_email(par_email varchar)
    RETURNS TABLE(_id int, _username character varying(256), _normalizedusername character varying(256),
	_email character varying(256), _normalizedemail character varying(256),
	_emailconfirmed bool, _passwordhash character varying,
	_securitystamp character varying(255), _concurrencystamp character varying(255) ,
	_phonenumber character varying(50), _phonenumberconfirmed bool ,
	_twofactorenabled bool, _lockoutend timestamp with time zone,
	_lockoutenable bool, _accessfailedcount integer, _deleted bool, _authenticatorkey varchar) 
    LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	RETURN QUERY 
		SELECT id,
			username, 
			normalizedusername, 
			email, 
			normalizedemail, 
			CAST(CAST(emailconfirmed AS INT) AS bool), 
			passwordhash, 
			securitystamp, 
			concurrencystamp, 
			phonenumber, 
			CAST(CAST(phonenumberconfirmed AS INT) AS bool), 
			CAST(CAST(twofactorenabled AS INT) AS bool), 
			lockoutend, 
			CAST(CAST(lockoutenable AS INT) AS bool), 
			accessfailedcount,
			deleted,
            authenticatorkey
		FROM applicationuser
		WHERE normalizedemail = par_email AND deleted <> true
		LIMIT 1;
END;
$BODY$;





CREATE OR REPLACE PROCEDURE public.proc_update_applicationuser(
	IN _id int, _username character varying(256), _normalizedusername character varying(256),
	_email character varying(256), _normalizedemail character varying(256),
	_emailconfirmed bit(1), _passwordhash character varying,
	_securitystamp character varying(255), _concurrencystamp character varying(255) ,
	_phonenumber character varying(50), _phonenumberconfirmed bit(1) ,
	_twofactorenabled bit(1), _lockoutend timestamp with time zone,
	_lockoutenable bit(1), _accessfailedcount integer, _deleted bool, _authenticatorkey varchar)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	
	--TODO: Add logging with ip, username, and timestamp
	
	UPDATE public.applicationuser
		SET username=_username, normalizedusername=_normalizedusername,
			email=_email, normalizedemail=_normalizedemail, emailconfirmed=_emailconfirmed,
			passwordhash=_passwordhash, securitystamp=_securitystamp, 
			concurrencystamp=_concurrencystamp, phonenumber=_phonenumber, 
			phonenumberconfirmed=_phonenumberconfirmed, twofactorenabled=_twofactorenabled,
			lockoutend=_lockoutend, lockoutenable=_lockoutenable, 
			accessfailedcount=_accessfailedcount, deleted=_deleted, authenticatorkey=_authenticatorkey
		WHERE id = _id;
	COMMIT;

END; 
$BODY$;


CREATE OR REPLACE PROCEDURE public.proc_update_recoverycodes(
	IN _id int, _code character varying, _redeemed bool)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	
	--TODO: Add logging with ip, username, and timestamp
	INSERT INTO recoverycodes(userid, code, redeemed)
	VALUES(_id, _code, _redeemed)
	ON CONFLICT (code, userid) DO UPDATE 
		SET redeemed = excluded.redeemed;
	COMMIT;

END; 
$BODY$;


CREATE OR REPLACE FUNCTION public.func_get_recoverycodes(par_userid int)
    RETURNS TABLE(_code varchar,
			_redeemed bool) 
    LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	RETURN QUERY 
		SELECT code,
			redeemed
		FROM recoverycodes
		WHERE userid = par_userid;
END;
$BODY$;



CREATE OR REPLACE PROCEDURE public.proc_insert_applicationuser(
	IN _username character varying(256), _normalizedusername character varying(256),
	_email character varying(256), _normalizedemail character varying(256),
	_emailconfirmed bit(1), _passwordhash character varying,
	_securitystamp character varying(255), _concurrencystamp character varying(255) ,
	_phonenumber character varying(50), _phonenumberconfirmed bit(1) ,
	_twofactorenabled bit(1), _lockoutend timestamp with time zone,
	_lockoutenable bit(1), _accessfailedcount integer, _deleted bool, _authenticatorkey varchar)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	
	--TODO: Add logging with ip, username, and timestamp
	
	INSERT INTO public.applicationuser(username, normalizedusername, email, normalizedemail, 
									   emailconfirmed, passwordhash, securitystamp, concurrencystamp, 
									   phonenumber, phonenumberconfirmed, twofactorenabled, lockoutend, 
									   lockoutenable, accessfailedcount, deleted, authenticatorkey)
	VALUES (_username, _normalizedusername, _email, _normalizedemail, _emailconfirmed, 
			_passwordhash, _securitystamp, _concurrencystamp, _phonenumber, _phonenumberconfirmed, 
			_twofactorenabled, _lockoutend, _lockoutenable, _accessfailedcount, _deleted, _authenticatorkey);
			
	COMMIT;

END; 
$BODY$;



CREATE OR REPLACE PROCEDURE public.proc_delete_applicationuser(
	IN _id int)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	
	--TODO: Add logging with ip, username, and timestamp
	
	UPDATE applicationuser
    SET deleted = true
    WHERE id = _id;
			
	COMMIT;

END; 
$BODY$;


CREATE OR REPLACE FUNCTION public.func_get_userlogins(par_userid int)
    RETURNS TABLE(_loginprovider varchar(128),
			_providerkey varchar(128),
            _providerdisplayname varchar,
            _userid int) 
    LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	RETURN QUERY 
		SELECT loginprovider,
			providerkey,
            providerdisplayname,
            userid
		FROM userlogin
		WHERE userid = par_userid;
END;
$BODY$;









