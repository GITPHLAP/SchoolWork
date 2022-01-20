
from pysnmp.entity import engine, config
from pysnmp.carrier.asyncore.dgram import udp
from pysnmp.entity.rfc3413 import ntfrcv
from pysnmp.proto.rfc1902 import ObjectName
import queue
import inspect


# this function prints all received message from trap
def cb_fun(snmp_engine, state_reference, context_engine_id, context_name, var_binds, cb_ctx: queue):
    print("Received new Trap message")
    # print(cb_ctx)
    # cb_ctx.put("test")
    # print(type())
    traps = []
    for v in var_binds:
        # print(v)
        name = v[0]._value
        value = v[1]._value
        tb = TrapValue(TrapVarsToReadable(name), TrapVarsToReadable(value))
        traps.append(tb)


    # print(type(var_binds[2][1]._value))
    # print(var_binds[2][1]._value)
    # test = var_binds[0][0]
    # for i in inspect.getmembers(test):
    #     print(i)
    for trap in traps:
        if (str(trap.name) == "1.3.6.1.6.3.1.1.4.1.0."): #check trap entry 
            findIntSpecificNr(traps)
            if(str(trap.value) =="1.3.6.1.6.3.1.1.5.3."): #"1.3.6.1.6.3.1.1.5.3" for traps where interface is set to down 
                task = MIBTask(findElementValueInList(traps, '1.3.6.1.2.1.2.2.1.2.'), findElementValueInList(traps, '1.3.6.1.4.1.9.2.2.1.1.20.'), 'EthernetStatus')
                print("PEEEEEEEEK")
                # print(var_binds)
                # var_binds['name']
                cb_ctx.put(task)
            elif(str(trap.value) == "1.3.6.1.6.3.1.1.5.4."): #"1.3.6.1.6.3.1.1.5.4" for traps where interface is set to up
                task = MIBTask(findElementValueInList(traps, '1.3.6.1.2.1.2.2.1.2.'), findElementValueInList(traps, '1.3.6.1.4.1.9.2.2.1.1.20.'), 'EthernetStatus')
                print("UPPP")
                cb_ctx.put(task)
        print('%s = %s' % (trap.name, trap.value))  # "'%s = %s' %" text formatting

def findIntSpecificNr(traps):
    for trap in traps:
        if(str(trap.name).startswith("1.3.6.1.2.1.2.2.1.1.")):
            #get the last number in trap entry name
            arr = str(trap.name).split(".")
            intNr = arr[len(arr)-2] # -2 because there is . in the end 
            return intNr

    

def runIt(cb_Ctx, snmpEngine: engine.SnmpEngine):

    TrapAgentAddress = '172.16.4.33'  # Trap listener address (local pc)
    Port = 162  # trap listener port


    config.addTransport(
        snmpEngine,
        udp.domainName,  # + (1,),
        udp.UdpTransport().openServerMode((TrapAgentAddress, Port)))

    # Configure community here
    config.addV1System(snmpEngine, ' ', 'ciscolab')  # (snmpEngine, '?', 'community string')

    print("Agent is listening SNMP Trap on " + TrapAgentAddress + " , Port : " + str(Port))
    print('--------------------------------------------------------------------------')

    ntfrcv.NotificationReceiver(snmpEngine, cb_fun, cbCtx=cb_Ctx)
    # makes a simulate never-ending job for input and output
    snmpEngine.transportDispatcher.jobStarted(1)  # 1 == job id

    try:
        snmpEngine.transportDispatcher.runDispatcher()

    except:
        snmpEngine.transportDispatcher.closeDispatcher()
        raise

def findElementValueInList(list, elementName):
    intNr = findIntSpecificNr(list)
    fullName = f'{elementName}{intNr}.'
    print(f'Fullname:    {fullName}')
    value = ''
    for tr in list:
        if(str(tr.name) == str(fullName)):
            value = str(tr.value)
    if (value == "up"):
        value = 1
    elif (value == "down"):
        value = 0
    elif (value == "administratively down"):
        value = 0
    return value

def TrapVarsToReadable(var):
    # print(var)
    result = ''
    if(type(var) is tuple):
        for t in var:
            result += f'{t}.' #concate tuples to string
    elif (type(var) is int):
        result = var
    else:
        result = str(var, 'utf-8')
    return result

class TrapValue:
  def __init__(self, name, value):
    self.name = name
    self.value = value

class MIBTask:
    def __init__(self, name, value, action):
        self.name = name
        self.value = value
        self.action = action

class MsgTask:
    def __init__(self, msg):
        self.msg = msg

# runIt(queue.Queue(),  engine.SnmpEngine())

