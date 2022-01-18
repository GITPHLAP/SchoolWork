from queue import Queue
# from snmpTrapReceiver import runIt
import asyncio
from functools import partial
import tkinter as tk
import time

window = tk.Tk()

window.geometry('1000x1000')

#add textbox to layout

tb = tk.Text(window)
tb.insert(tk.INSERT, "Hello")
tb.pack()

window.mainloop()

async def main():
    queue = Queue()
    task1 = asyncio.create_task(
        workerput(queue)
    ) 
    task2 = asyncio.create_task(
        worker(queue)
    ) 

async def worker(queue):
    test = await queue.get()

    tb.insert(tk.END, test) 

    queue.task_done()

async def workerput(queue):
    while True:
        print("Hit")
        queue.put("test")

        time.sleep(3)

asyncio.run(main())