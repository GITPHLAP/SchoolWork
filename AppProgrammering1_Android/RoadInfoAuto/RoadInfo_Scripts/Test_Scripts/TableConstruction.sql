CREATE TABLE test_gencolumn (
	"id" text PRIMARY KEY
)

--Example on how to create generated column as reference
CREATE TABLE test_fk_gencolumn (
	"id" text PRIMARY KEY,
	pid text GENERATED ALWAYS AS (SPLIT_PART("id", '.',1)) STORED,
	CONSTRAINT fk_id
	FOREIGN KEY(pid)
	REFERENCES test_gencolumn(id)
	
);


SELECT * FROM test_gencolumn

SELECT * FROM test_fk_gencolumn

INSERT INTO test_gencolumn
SELECT xmltable.*
FROM xmlTest,
	XMLTABLE('//subroot'
			 PASSING data
			 COLUMNS "id" text PATH '@id'
			);

INSERT INTO test_fk_gencolumn
SELECT xmltable.*
FROM xmlTest,
	XMLTABLE('//element'
			 PASSING data
			 COLUMNS "id" text PATH '@id'
			);




-- SELECT xpath('//root:situationRecord/root:generalPublicComment[root:commentType = "locationDescriptor"]/root:comment/root:values/root:value[@lang = "da-DK"]',
-- 			xml,
-- 			ARRAY[ARRAY['root', 'http://datex2.eu/schema/2/2_0']])
-- FROM xml_doc

--Insert for situationRecords option 2

--INSERT INTO SituationRecords
SELECT 
-- 	cHAR_length(trim(regexp_replace("type", '\s+', ' ', 'g'))) as length
-- 	,cHAR_length("type") as lengthbefore
-- 	,trim(regexp_replace("type", '\s+', ' ', 'g')) as "text"
-- 	,
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
	, xmltable.type
	, xmltable.temporarySpeedLimit
	, xmltable.carParkIdentity
	, xmltable.carParkStatus

FROM xml_doc,
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
                -- roadOrCarriagewayOrLaneManagementType VARCHAR(50) PATH 'root:roadOrCarriagewayOrLaneManagementType',
                -- speedManagementType VARCHAR(50) PATH 'root:speedManagementType',
                temporarySpeedLimit int PATH 'root:temporarySpeedLimit',
                -- generalNetworkManagementType VARCHAR(50) PATH 'root:generalNetworkManagementType',
                -- obstructionType VARCHAR(50) PATH 'root:obstructionType',
                -- reroutingManagementType VARCHAR(50) PATH 'root:reroutingManagementType',
                -- accidentType VARCHAR(50) PATH 'root:accidentType',
                -- abnormalTrafficType VARCHAR(50) PATH 'root:abnormalTrafficType',
                carParkIdentity VARCHAR(50) PATH 'root:carParkIdentity',
                carParkStatus VARCHAR(50) PATH 'root:carParkStatus',
                -- roadMaintenanceType VARCHAR(50) PATH 'root:roadMaintenanceType',
                -- weatherRelatedRoadConditionType VARCHAR(50) PATH 'root:weatherRelatedRoadConditionType',
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
			)
			
-- ORDER BY length DESC
;

