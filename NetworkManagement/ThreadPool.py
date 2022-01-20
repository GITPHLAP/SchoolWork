from concurrent.futures import ThreadPoolExecutor
from ast import FormattedValue
import asyncio
from asyncio.windows_events import NULL
from logging import root
import tkinter as Tkinter
from tkinter import END, messagebox
import time
import threading
import random
import queue
from tenacity import sleep
from snmpTrapReceiver import runIt, MIBTask, MsgTask
from functools import partial
from pysnmp.entity import engine
# from cisco_connect import sendCommand, sendConfigSet
from snmp import get, getBulk, MIBData, FormattedMibData
from pysnmp import hlapi
import tkinter.scrolledtext as ScollTextBox


class GuiPart:
    def __init__(self, master, queue, endCommand, executor):
        self.portButtons = []

        self.executor = executor

        # master.geometry('1000x800')
        # print(get('172.16.4.2', ['1.3.6.1.2.1.1.5.0'], hlapi.CommunityData('ciscolab')))
        # data = getBulk('172.16.4.2', ['1.3.6.1.2.1.2.2.1.2'], hlapi.CommunityData('ciscolab'), 25) #get first 25 interface descriptions
        # interfaces = FormattedMibData(data)
        portNr = 20
        self.PortLayoutBuilder(portNr)

        #add textbox to layout

        self.tb = ScollTextBox.ScrolledText(master)
        self.tb.insert(Tkinter.INSERT, "Hello")
        self.tb.grid(column=0, columnspan=int(portNr/2), row=5)
        self.queue = queue
        # Set up the GUI
        console = Tkinter.Button(master, text='Done', command=endCommand)
        console.grid(column=0, row=9)
        # Add more GUI stuff here depending on your specific needs
        
        root.protocol("WM_DELETE_WINDOW", endCommand)
    
    # Get FastEthernet number
    def getInterfaceIndex(self, interfaceName: str):
        for btn in self.portButtons:
            if(str(btn['text']) == str(interfaceName)):
                return self.portButtons.index(btn)        
        return NULL

    def translateIntUpValue(self, value):
        print(f'translateIntUpValue:  {type(value)}:  {value}')
        if (value == "up"):
            return 1
        elif (value == "administratively down"):
            return 0
        elif (value == "down"):
            return 0
    
    def translateInterfaceColor(self, status):
        print(f'translateInterfaceColor:  {type(status)}: {status}')
        if(status == 1):
            return "green"
        else:
            return "red"
    
    def getIntStatus(self, interface):
        print(f'getIntStatus:  {type(interface)}:  {interface}')
        # split interface to get last nr
        arr = str(interface).split(".")
        intNr = arr[len(arr)-1]

        oid = f'1.3.6.1.4.1.9.2.2.1.1.20.{intNr}'
        # get status with snmp
        data = get('172.16.4.2', [oid], hlapi.CommunityData('ciscolab'))
        status = FormattedMibData(data).value
        return status

    def getIntStatusAsColor(self, interface):
        status = self.getIntStatus(interface)
        color = self.translateInterfaceColor(self.translateIntUpValue(status))
        return color


    def processIncoming(self):
        """Handle all messages currently in the queue, if any."""
        while self.queue.qsize(  ):
            try:
                msg = self.queue.get(0)
                
                if(type(msg) is MIBTask):
                    print("test")
                    if(str(msg.action) == "EthernetStatus"):
                        # change color on button
                        name = str(msg.name)
                        print(f'Name:    {name}')
                        intIndex = int(self.getInterfaceIndex(name))

                        self.portButtons[intIndex]['bg'] = str(self.translateInterfaceColor(msg.value))
                elif(type(msg) is MsgTask):
                    self.AddPrintTask(msg.msg)

            except queue.Empty:
                pass
    
    def portBtnCallBack(self, btn):
        if (btn['bg'] == 'green'):
            # send action to cisco device
            config = [
                "interface {}".format(btn['text']),
                "shut"
            ]
        else: 
            # send action to cisco device
            config = [
                "interface {}".format(btn['text']),
                "no shut"
            ]
        # task1 = threading.Thread(target=sendConfigSet, args=(config)).start()
        # task1 = asyncio.create_task(sendConfigSet(config))
        self.executor.submit(self.PrintSNMPCommand, ("Test2", self.testcallback))
        # self.AddPrintTask(future.result())
    
    def PrintSNMPCommand(self, config, command):
        result = command(config)
        self.AddPrintTask(result)

    def testcallback(self, config):
        sleep(5)
        return config
    
    def AddPrintTask(self, msg):
        self.executor.submit(self.printToOutputBox, (msg))

    def printToOutputBox(self, msg):
        self.tb.insert(END, f'{str(msg)} \r\n')

    def PortLayoutBuilder(self, ports):
        c = 0
        r = 0
        for i in range(1, ports+1):
        # prepare position for button
            if(i ==1):
                # do nothing
                c = 0
                r = 0
            elif ((i % 2) == 0):
                r = 1
            else:
                c += 1
                r = 0
            
            # Create button inside a frame
            frame = Tkinter.Frame(root)
            var = Tkinter.StringVar()
            var.set(f"Interface{i}")

            label = Tkinter.Label(frame, textvariable=var)
            label.pack(side= Tkinter.TOP)

            B = Tkinter.Button(frame, text=f"test{i}")
            B.config(command=partial(self.portBtnCallBack, B))
            B.pack(side= Tkinter.BOTTOM)

            #Add button to list
            self.portButtons.append(B)

            frame.grid(column=c, row=r)



class ThreadedClient:
    """
    Launch the main part of the GUI and the worker thread. periodicCall and
    endApplication could reside in the GUI part, but putting them here
    means that you have all the thread controls in a single place.
    """
    def __init__(self, master):
        """
        Start the GUI and the asynchronous threads. We are in the main
        (original) thread of the application, which will later be used by
        the GUI as well. We spawn a new thread for the worker (I/O).
        """

        # Create thread pool to button tasks 

        self.executor = ThreadPoolExecutor(2)

        self.master = master

        self.snmpEngine = engine.SnmpEngine()

        # Create the queue
        self.queue = queue.Queue(  )

        # Set up the GUI part
        self.gui = GuiPart(master, self.queue, self.endApplication, self.executor)

        # Set up the thread to do asynchronous I/O
        # More threads can also be created and used, if necessary
        self.running = 1
        self.thread1 = threading.Thread(target=self.workerThread1)
        self.thread1.start(  )

        # Start the periodic call in the GUI to check if the queue contains
        # anything
        self.periodicCall(  )

    def periodicCall(self):
        """
        Check every 200 ms if there is something new in the queue.
        """
        self.gui.processIncoming(  )
        if not self.running:
            # This is the brutal stop of the system. You may want to do
            # some cleanup before actually shutting it down.

            import sys
            sys.exit(1)
        self.master.after(200, self.periodicCall)

    def workerThread1(self):
        """
        This is where we handle the asynchronous I/O. For example, it may be
        a 'select(  )'. One important thing to remember is that the thread has
        to yield control pretty regularly, by select or otherwise.
        """
        while self.running:
            # To simulate asynchronous I/O, we create a random number at
            # random intervals. Replace the following two lines with the real
            # thing.
            sleep(2)
            self.queue.put(MsgTask("It Works"))
            self.gui.AddPrintTask(self.running)
            # runIt(self.queue, self.snmpEngine)
            test = 1

    def endApplication(self):
        self.running = 0
        self.snmpEngine.transportDispatcher.jobFinished(1)


root = Tkinter.Tk(  )


client = ThreadedClient(root)
root.mainloop()





# if(int(msg.value) == 1):
#                         value = "shutdown"
#                     else:
#                         value = "no shutdown"

#                     # send action to cisco device
#                     config = [
#                         "interface {}".format(msg.name)
#                     ]
#                     sendConfigSet()