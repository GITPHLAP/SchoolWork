
from pysnmp.entity import engine, config
from pysnmp.carrier.asyncore.dgram import udp
from pysnmp.entity.rfc3413 import ntfrcv
import queue


# this function prints all received message from trap
def cb_fun(snmp_engine, state_reference, context_engine_id, context_name, var_binds, cb_ctx: queue):
    print("Received new Trap message")
    print(cb_ctx)
    cb_ctx.put("test")
    for name, val in var_binds:
        print(val)
        if (str(val) == "1.3.6.1.6.3.1.1.5.3"): #"1.3.6.1.6.3.1.1.5.3" for traps where interface is set to down 
            print("PEEEEEEEEK")
        elif(str(val) == "1.3.6.1.6.3.1.1.5.4"): #"1.3.6.1.6.3.1.1.5.4" for traps where interface is set to up
            print("UPPP")
        print('%s = %s' % (name.prettyPrint(), val.prettyPrint()))  # "'%s = %s' %" text formatting

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


# Get FastEthernet number
def getFENum(interface: str ):
    ar = interface.split('/')
    index = len(ar) -1 #its equal to the last index in array
    return ar[index]

