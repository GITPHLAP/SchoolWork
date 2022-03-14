--Situation
CREATE TABLE situations(
	"id" VARCHAR(100) PRIMARY KEY,
	"version" bigint,
	situationVersionTime timestamp with time zone,
	locationText VARCHAR(200),
	eventText VARCHAR(200),
	regionText VARCHAR(30),
	nonCodeableInformationText text,
	durationText VARCHAR(100),
	icon VARCHAR(40),
	status VARCHAR(20)	
);





--SituationRecords option 1
CREATE TABLE "situationRecords"(
	"id" VARCHAR(100) PRIMARY KEY,
	"version" bigint,
	"situationId" VARCHAR(100) GENERATED ALWAYS AS (SPLIT_PART("id", '.',1)) STORED,
	
	situationRecordCreationTime timestamp with time zone,
	situationRecordVersionTime timestamp with time zone,
	
	validityStatus VARCHAR(50),
	startOfPeriod timestamp with time zone,
	endOfPeriod timestamp with time zone,
	
	generalcomment_description text,
	generalcomment_locationDescriptor text,
	generalcomment_other text,
	
	latitude double precision,
	longitude double precision,
	
	--specialTypes
	roadOrCarriagewayOrLaneManagementType VARCHAR(50),
	speedManagementType VARCHAR(50),
	temporarySpeedLimit int,
	generalNetworkManagementType VARCHAR(50),
	obstructionType VARCHAR(50),
	reroutingManagementType VARCHAR(50),
	accidentType VARCHAR(50),
	abnormalTrafficType VARCHAR(50),
	carParkIdentity VARCHAR(50),
	carParkStatus VARCHAR(50),
	roadMaintenanceType VARCHAR(50),
	weatherRelatedRoadConditionType VARCHAR(50),
	publicEventType VARCHAR(50)
);




--SituationRecords option 2
CREATE TABLE "situationRecords"(
	"id" VARCHAR(100) PRIMARY KEY,
	"version" bigint,
	"situationId" VARCHAR(100) GENERATED ALWAYS AS (SPLIT_PART("id", '.',1)) STORED,
	
	situationRecordCreationTime timestamp with time zone,
	situationRecordVersionTime timestamp with time zone,
	
	validityStatus VARCHAR(50),
	startOfPeriod timestamp with time zone,
	endOfPeriod timestamp with time zone,
	
	generalcomment_description text,
	generalcomment_locationDescriptor text,
	generalcomment_other text,
	
	latitude double precision,
	longitude double precision,
	temporarySpeedLimit int,
	carParkIdentity VARCHAR(50),
	carParkStatus VARCHAR(50),
	publicEventType VARCHAR(50),
	"type" VARCHAR(100)
);


CREATE TABLE UserActivity(
	userName VARCHAR(100) PRIMARY KEY,
	lastActivity timestamp with time zone
);


CREATE TABLE xml_data_history(
	"xml" xml,
	lastUpdated timestamp with time zone
)