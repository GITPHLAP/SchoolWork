from cgitb import handler
from netmiko import (
    ConnectHandler,
    NetmikoTimeoutException,
    NetmikoAuthenticationException,
)
from pysnmp import hlapi


def get(target, oids, credentials, port=161, engine=hlapi.SnmpEngine(), context=hlapi.ContextData()):
    handler = hlapi.getCmd(
        engine,
        credentials,
        hlapi.UdpTransportTarget((target, port)),
        context,
        *construct_object_types(oids)
    )
    return fetch(handler, 1)[0]

def construct_object_types(list_of_oids):
    object_types = []
    for oid in list_of_oids:
        object_types.append(hlapi.ObjectType(hlapi.ObjectIdentity(oid)))
    return object_types


def fetch(handler, count):
    result = []
    for i in range(count):
        try:
            error_indication, error_status, error_index, var_binds = next(handler)
            if not error_indication and not error_status:
                items = {}
                for var_bind in var_binds:
                    items[str(var_bind[0])] = cast(var_bind[1])
                result.append(items)
            else:
                raise RuntimeError('Got SNMP error: {0}'.format(error_indication))
        except StopIteration:
            break
    return result

def cast(value):
    try:
        return int(value)
    except (ValueError, TypeError):
        try:
            return float(value)
        except (ValueError, TypeError):
            try:
                return str(value)
            except (ValueError, TypeError):
                pass
    return value

def getBulk(target, oids, credentials, count, port=161, engine=hlapi.SnmpEngine(), context=hlapi.ContextData(), start_from=0):
    handler = hlapi.bulkCmd(
        engine,
        credentials,
        hlapi.UdpTransportTarget((target, port)),
        context,
        start_from, count,
        *construct_object_types(oids)
    )
    return fetch(handler, count)

class MIBData:
  def __init__(self, name, value):
    self.name = name
    self.value = value

def FormattedMibData(data):
    mibData = []
    if(type(data) is not list):
        for int in data:
            mib = MIBData(int, data[int])
            mibData.append(mib)
        return mibData[0]
    for dict in data:
        # print(f'TEST: {type(dict)}')
        for int in dict:
            mib = MIBData(int, dict[int])
            mibData.append(mib)
    return mibData



# getBulk('172.16.4.2', ['1.3.6.1.2.1.2.2.1.2'], hlapi.CommunityData('ciscolab'), 25)
# print(get('172.16.4.2', ['1.3.6.1.2.1.1.5.0'], hlapi.CommunityData('ciscolab')))