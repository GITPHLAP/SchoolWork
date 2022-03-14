SELECT xpath('//root:situationRecord',
			 "xml",
			 ARRAY[
				 ARRAY['xsi', 'http://www.w3.org/2001/XMLSchema-instance'],
				 ARRAY['root', 'http://datex2.eu/schema/2/2_0']
			 ])
FROM xml_doc

SELECT xmltable.*
INTO TEMPORARY TABLE jens
FROM xml_doc,
	XMLTABLE(XMLNAMESPACES('http://datex2.eu/schema/2/2_0' AS root,
						    'http://www.w3.org/2001/XMLSchema-instance' AS xsi),
			 '//root:situationRecord'
			 PASSING "xml"
			 COLUMNS "type" text PATH '@xsi:type'
			);

--Get all situationRecord types
SELECT DISTINCT * FROM jens



CREATE TABLE xmlTest AS SELECT
xml $$
<root>
  <subroot id="1">
   <element id="1.1">Hello1.1</element>
   <element id="1.2">Hello1.2</element>
  </subroot>
	<subroot id="2">
   <element id="2.1">Hello2.1</element>
   <element id="2.2">Hello2.2</element>
  </subroot>
    <subroot id="3">
   <element id="3.1">Hello3.1</element>
   <element id="3.2">Hello3.2</element>
  </subroot>
  </root>
$$ AS data;



SELECT xmltable.*
INTO TEMPORARY TABLE test1
FROM xmlTest,
	XMLTABLE('//subroot'
			 PASSING data
			 COLUMNS "id" text PATH '@id'
			);

SELECT xmltable.*
--INTO TEMPORARY TABLE test2
FROM xmlTest,
	XMLTABLE('//element'
			 PASSING data
			 COLUMNS "id" text PATH '@id',
			 	"pId" text PATH 'split_part("@id", ".", 1)'
			);




split_part(@id, ., 1)


SELECT * FROM test1

SELECT * FROM test2


DROP TABLE test1

DROP TABLE test2




CREATE TEMPORARY TABLE xmlTest2 AS SELECT
xml $$
            <situationExtension>
                <textualDescriptionsOfSituation>
                    <textualDescriptions lang="da-DK">
                        <locationText>Hadsundvej</locationText>
                        <eventText>vejarbejde, spærret, kun ét spor farbart</eventText>
                        <regionText>Nordjylland</regionText>
                        <nonCodeableInformationText>Fra Sofievej til Humlebakken ensrettes vejen fra 14/6 til 17/09 på
                            alle sidevej på strækningen vil der være svingning forbudt imod ensretning. Omkørsel via
                            Humlebakken &amp; Sofievej .</nonCodeableInformationText>
                        <durationText>Monday, 14. June til Tuesday, 1. March</durationText>
                        <icon>roadworks</icon>
                        <status>active</status>
                    </textualDescriptions>
                </textualDescriptionsOfSituation>
            </situationExtension>
$$ AS data;



SELECT xmltable.c1,
	xmltable.c2,
	xmltable.c3,
	/** g = instructs the function to remove all alphabets, not just the first one. **/
	trim(regexp_replace(xmltable.c4, '\s+', ' ', 'g')) as c4, 
	xmltable.c5,
	xmltable.c6,
	xmltable.c7
	
FROM xmlTest2,
	XMLTABLE('//situationExtension'
			 PASSING data
			 COLUMNS c1 text PATH 'textualDescriptionsOfSituation/textualDescriptions/locationText',
			 c2 text PATH 'textualDescriptionsOfSituation/textualDescriptions/eventText' ,
			 c3 text PATH 'textualDescriptionsOfSituation/textualDescriptions/regionText' ,
			 c4 text PATH 'textualDescriptionsOfSituation/textualDescriptions/nonCodeableInformationText' ,
			 c5 text PATH 'textualDescriptionsOfSituation/textualDescriptions/durationText' ,
			 c6 text PATH 'textualDescriptionsOfSituation/textualDescriptions/icon' ,
			 c7 text PATH 'textualDescriptionsOfSituation/textualDescriptions/status' 
			);
			
			
			











-- hent comment værdier 
CREATE TEMPORARY TABLE xmlTest3 AS SELECT
xml $$
            <root>
                <generalPublicComment>
                    <comment>
                        <values>
                            <value lang="da-DK">O2 København, Vigerslevvej
                                mellem Vigerslev Allé og Kulbanevej
                                vejarbejde uden for myldretid , spærret, fra 14-02-2022 07:00 til 18-02-2022 15:00
                                Der arbejdes dagligt mellem kl. 07:00 - 15:00 i perioden.</value>
                            <value>O2 København, Vigerslevvej
                                between Vigerslev Allé and Kulbanevej
                                roadworks during off-peak periods, closed, from 02/14/2022 07:00 until 02/18/2022 15:00
                            </value>
                            <value lang="de-DE">O2 Kopenhagen, Vigerslevvej
                                zwischen Vigerslev Allé und Kulbanevej
                                Baustelle in verkehrsarmen Zeiten, gesperrt, von 14.02.2022 07:00 Uhr bis 18.02.2022
                                15:00 Uhr</value>
                        </values>
                    </comment>
                    <commentDateTime>0001-01-01T00:00:00Z</commentDateTime>
                    <commentType>description</commentType>
                </generalPublicComment>
                <generalPublicComment>
                    <comment>
                        <values>
                            <value lang="da-DK">Der arbejdes dagligt mellem kl. 07:00 - 15:00 i perioden.</value>
                        </values>
                    </comment>
                    <commentDateTime>0001-01-01T00:00:00Z</commentDateTime>
                    <commentType>other</commentType>
                </generalPublicComment>
            </root>
$$ AS data;

SELECT xmltable.*
	
FROM xmlTest3,
	XMLTABLE('/root'
			 PASSING data
			 COLUMNS description text PATH '//generalPublicComment[commentType = "description"]/comment/values/value[@lang = "da-DK"]',
--			 "location" text PATH 'textualDescriptionsOfSituation/textualDescriptions/eventText' ,
			 "other" text PATH '//generalPublicComment[commentType = "other"]/comment/values/value[@lang = "da-DK"]'

			);





CREATE TEMPORARY TABLE xmlTest4 AS SELECT
xml $$
<bookstore>

<book>
  <title lang="en">Harry Potter</title>
  <price>29.99</price>
</book>

<book>
  <title lang="en">Learning XML</title>
  <price>39.95</price>
</book>

</bookstore>
$$ AS data;

SELECT xmltable.*
	
FROM xmlTest4,
	XMLTABLE('/bookstore'
			 PASSING data
			 COLUMNS description text PATH '/bookstore/book[price>35.00]'
--			 "location" text PATH 'textualDescriptionsOfSituation/textualDescriptions/eventText' ,
--			 "other" text PATH 'textualDescriptionsOfSituation/textualDescriptions/regionText' ,

			);




			
			