from functools import partial
import tkinter as tk


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

portNr = 20
PortLayoutBuilder(portNr)

#add textbox to layout

tb = tk.Text(window)
tb.insert(tk.INSERT, "Hello")
tb.grid(column=0, columnspan=int(portNr/2), row=5)

window.mainloop()