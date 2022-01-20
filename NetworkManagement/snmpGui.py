from functools import partial
import tkinter as tk
from logging import root
import tkinter as Tkinter
from tkinter import messagebox
import time
import threading
import random
import queue
from snmpTrapReceiver import runIt, MIBTask
from functools import partial
from pysnmp.entity import engine
from cisco_connect import sendCommand, sendConfigSet
from snmp import get
from pysnmp import hlapi


def portBtnCallBack(self):
    if (self['bg'] == 'green'):
        self['bg'] = "red"
        # send config to device
        return
    self['bg'] = "green"
    # send config to device

def PortLayoutBuilder(ports):
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
        frame = tk.Frame(window)
        var = tk.StringVar()
        var.set(f"Interface{i}")

        label = tk.Label(frame, textvariable=var)
        label.pack(side= tk.TOP)

        B = tk.Button(frame, text=f"test{i}")
        B.config(command=partial(portBtnCallBack, B))
        B.pack(side= tk.BOTTOM)

        #Add button to list
        portButtons.append(B)

        frame.grid(column=c, row=r)



window = tk.Tk()
portButtons = []

window.geometry('1000x1000')

interfaces = get('172.16.4.2', ['1.3.6.1.2.1.1.5.0'], hlapi.CommunityData('ciscolab'))
PortLayoutBuilder(interfaces)

#add textbox to layout

tb = tk.Text(window)
tb.insert(tk.INSERT, "Hello")
tb.grid(column=0, columnspan=int(len(interfaces)/2), row=5)

window.mainloop()