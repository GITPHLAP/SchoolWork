/****
SELECT func_calculate_distance(12.57, 55.68, -118.33, 34.10)
this function calculate distance in km from two point on earth

*/

CREATE OR REPLACE FUNCTION func_calculate_distance (longA double precision, latiA double precision, longB double precision, latiB double precision) 
	RETURNS double precision
	LANGUAGE plpgsql
AS $$
DECLARE 
    distance double precision;
	angelbetweenpoints double precision;
BEGIN
		SELECT (sin(latiA*pi()/180)*
			   sin(latiB*pi()/180)+
			   cos(latiA*pi()/180)*
			   cos(latiB*pi()/180)*
			   cos((longA-longB)*pi()/180))
		INTO angelbetweenpoints;
		
		SELECT (2*
				pi()*
				6370*
				(acos(angelbetweenpoints)*(180/pi())
				 /360))
		INTO distance;
		
		return distance;
END;$$



