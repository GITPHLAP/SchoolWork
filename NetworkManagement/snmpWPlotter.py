import plotille 
import numpy as np
from snmp import get
from pysnmp import hlapi
import time
import datetime

oid = '1.3.6.1.2.1.1.3.0'

xAxis = []
yAxis = []


def convertTicksToSec(ticks):
    seconds = int(ticks)/100
    return seconds

def getDeviceUpTime():
    return get('172.16.4.2', [oid], hlapi.CommunityData('ciscolab'))[oid] #return value
    

def main():
    counter = 0
    while True:
        counter += 1

        upTime = convertTicksToSec(getDeviceUpTime())
        yAxis.append(counter)
        xAxis.append(upTime)


        print(plotille.plot(xAxis, yAxis, height=30, width=60, X_label='seconds', color_mode='byte', lc=25))

        time.sleep(1) #wait 1 second


main()