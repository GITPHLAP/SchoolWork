import tkinter as Tkinter
from tkinter import messagebox
import time
import threading
import random
import queue
from snmpTrapReceiver import runIt 
from functools import partial
from pysnmp.entity import engine

class GuiPart:
    def __init__(self, master, queue, endCommand):
        self.portButtons = []

        master.geometry('1000x1000')

        portNr = 20
        self.PortLayoutBuilder(portNr)

        #add textbox to layout

        tb = Tkinter.Text(master)
        tb.insert(Tkinter.INSERT, "Hello")
        tb.grid(column=0, columnspan=int(portNr/2), row=5)
        self.queue = queue
        # Set up the GUI
        console = Tkinter.Button(master, text='Done', command=endCommand)
        console.grid(column=0, row=9)
        # Add more GUI stuff here depending on your specific needs

        root.protocol("WM_DELETE_WINDOW", endCommand)

    def processIncoming(self):
        """Handle all messages currently in the queue, if any."""
        while self.queue.qsize(  ):
            try:
                msg = self.queue.get(0)
                # Check contents of message and do whatever is needed. As a
                # simple test, print it (in real life, you would
                # suitably update the GUI's display in a richer fashion).
                print(msg)
            except queue.Empty:
                # just on general principles, although we don't
                # expect this branch to be taken in this case
                pass
    
    def portBtnCallBack(self, btn):
        if (btn['bg'] == 'green'):
            btn['bg'] = "red"
            # send config to device
            return
        btn['bg'] = "green"
        # send config to device

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
        self.master = master

        self.snmpEngine = engine.SnmpEngine()

        # Create the queue
        self.queue = queue.Queue(  )

        # Set up the GUI part
        self.gui = GuiPart(master, self.queue, self.endApplication)

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
            print(self.running)
            runIt(self.queue, self.snmpEngine)

    def endApplication(self):
        self.running = 0
        self.snmpEngine.transportDispatcher.jobFinished(1)


rand = random.Random(  )
root = Tkinter.Tk(  )


client = ThreadedClient(root)
root.mainloop(  )


