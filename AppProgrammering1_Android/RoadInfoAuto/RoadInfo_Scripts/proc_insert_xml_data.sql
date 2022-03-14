/**
The procedure is used to translate the xml data into 
2 different tables.

parameter is the xml data

*/

CREATE OR REPLACE PROCEDURE proc_insert_xml_data(xmlData xml)
LANGUAGE plpgsql
AS $$
BEGIN

DROP TABLE IF EXISTS public.xml_data;
CREATE TABLE xml_data AS 
SELECT xmlData AS "xml", NOW() AS lastUpdated ;

--For testing 
INSERT INTO xml_data_history SELECT xmlData AS "xml", NOW() AS lastUpdated;

DELETE FROM situations;
DELETE FROM "situationRecords";


INSERT INTO situations
SELECT xmltable.id
	,xmltable.version
	,xmltable.situationVersionTime
	,trim(regexp_replace(xmltable.locationText, '\s+', ' ', 'g')) as "locationText"
	,trim(regexp_replace(xmltable.eventText, '\s+', ' ', 'g')) as "eventText"
	,trim(regexp_replace(xmltable.regionText, '\s+', ' ', 'g')) as "regionText"
	,trim(regexp_replace(xmltable.nonCodeableInformationText, '\s+', ' ', 'g')) as "nonCodeableInformationText"
	,trim(regexp_replace(xmltable.durationText, '\s+', ' ', 'g')) as "durationText"
	,xmltable.icon
	,xmltable.status
FROM xml_data,
	XMLTABLE(XMLNAMESPACES('http://datex2.eu/schema/2/2_0' AS root,
						    'http://www.w3.org/2001/XMLSchema-instance' AS xsi),
			 '//root:situation'
			 PASSING "xml"
			 COLUMNS "id" VARCHAR(100) PATH '@id',
			 	"version" bigint PATH '@version',
				situationVersionTime timestamp with time zone PATH 'root:situationVersionTime',
				locationText text PATH 'root:situationExtension/root:textualDescriptionsOfSituation/root:textualDescriptions/root:locationText',
				eventText TEXT PATH 'root:situationExtension/root:textualDescriptionsOfSituation/root:textualDescriptions/root:eventText',
				regionText VARCHAR(30) PATH 'root:situationExtension/root:textualDescriptionsOfSituation/root:textualDescriptions/root:regionText',
				nonCodeableInformationText text PATH 'root:situationExtension/root:textualDescriptionsOfSituation/root:textualDescriptions/root:nonCodeableInformationText',
				durationText TEXT PATH 'root:situationExtension/root:textualDescriptionsOfSituation/root:textualDescriptions/root:durationText',
				icon VARCHAR(40) PATH 'root:situationExtension/root:textualDescriptionsOfSituation/root:textualDescriptions/root:icon',
				status VARCHAR(20) PATH 'root:situationExtension/root:textualDescriptionsOfSituation/root:textualDescriptions/root:status'	
			)
;



INSERT INTO public."situationRecords"(
	id, version, situationrecordcreationtime, situationrecordversiontime, validitystatus, startofperiod, endofperiod, generalcomment_description, generalcomment_locationdescriptor, generalcomment_other, latitude, longitude, temporaryspeedlimit, carparkidentity, carparkstatus, publiceventtype, type)
SELECT 
	xmltable.id
	, xmltable.version
	, xmltable.situationRecordCreationTime
	, xmltable.situationRecordVersionTime
	, xmltable.validityStatus
	, xmltable.startOfPeriod
	, xmltable.endOfPeriod
	, trim(regexp_replace(xmltable.generalcomment_description, '\s+', ' ', 'g')) as "generalcomment_description"
	, trim(regexp_replace(xmltable.generalcomment_locationDescriptor, '\s+', ' ', 'g')) as "generalcomment_locationDescriptor"
	, trim(regexp_replace(xmltable.generalcomment_other, '\s+', ' ', 'g')) as "generalcomment_other"
	, xmltable.latitude
	, xmltable.longitude
	, xmltable.temporarySpeedLimit
	, xmltable.carParkIdentity
	, xmltable.carParkStatus
	, xmltable.publicEventType
	, trim(xmltable.type)

FROM xml_data,
	XMLTABLE(XMLNAMESPACES('http://datex2.eu/schema/2/2_0' AS root,
						    'http://www.w3.org/2001/XMLSchema-instance' AS xsi),
			 '//root:situationRecord'
			 PASSING "xml"
			 COLUMNS "id" VARCHAR(100) PATH '@id',
				"version" bigint PATH '@version',
                situationRecordCreationTime timestamp with time zone PATH 'root:situationRecordCreationTime',
                situationRecordVersionTime timestamp with time zone PATH 'root:situationRecordVersionTime',
                validityStatus VARCHAR(50) PATH 'root:validity/root:validityStatus',
                startOfPeriod timestamp with time zone PATH 'root:validity/root:validityTimeSpecification/root:validPeriod/root:startOfPeriod',
                endOfPeriod timestamp with time zone PATH 'root:validity/root:validityTimeSpecification/root:validPeriod/root:endOfPeriod',
                generalcomment_description text PATH 'root:generalPublicComment[root:commentType = "description"]/root:comment/root:values/root:value[@lang = "da-DK"]',
                generalcomment_locationDescriptor text PATH 'root:generalPublicComment[root:commentType = "locationDescriptor"][1]/root:comment/root:values/root:value[@lang = "da-DK"]',
                generalcomment_other text PATH 'root:generalPublicComment[root:commentType = "other"]/root:comment/root:values/root:value[@lang = "da-DK"]',
                latitude double precision PATH 'concat(root:groupOfLocations/root:locationForDisplay/root:latitude, root:groupOfLocations/root:locationContainedInGroup/root:locationForDisplay/root:latitude)',
                longitude double precision PATH 'concat(root:groupOfLocations/root:locationForDisplay/root:longitude, root:groupOfLocations/root:locationContainedInGroup/root:locationForDisplay/root:longitude)',
                temporarySpeedLimit int PATH 'root:temporarySpeedLimit',
                carParkIdentity VARCHAR(50) PATH 'root:carParkIdentity',
                carParkStatus VARCHAR(50) PATH 'root:carParkStatus',
                publicEventType VARCHAR(50) PATH 'root:publicEventType',
			 	
			 	"type" VARCHAR(100) PATH 'concat(
                    root:roadOrCarriagewayOrLaneManagementType,
                    root:speedManagementType,
                    root:generalNetworkManagementType,
                    root:obstructionType,
                    root:reroutingManagementType,
                    root:accidentType,
                    root:abnormalTrafficType,
                    root:roadMaintenanceType,
                    root:weatherRelatedRoadConditionType,
                    root:publicEventType)'
			);
			
 	COMMIT;

END; $$