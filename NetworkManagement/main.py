from netmiko import (
    ConnectHandler,
    NetmikoTimeoutException,
    NetmikoAuthenticationException,
)
import logging
logging.basicConfig(filename='test.log', level=logging.DEBUG)
logger = logging.getLogger("netmiko")

cisco_device = {
    "device_type": "cisco_ios",
    "host": "172.16.4.2",
    "username": "admin",
    "password": "Kode1234",
    "secret": "Kode1234",
    "session_log": "output.txt",
    "default_enter": '\r\n',
}

# create connection to cisco device
net_connect = ConnectHandler(**cisco_device)

#send command
output = net_connect.send_command("show ip int br")

print(output)



# New interface details 
gigabit = {"name": "GigabitEthernet0/1",
           "description": "gigabit interface description",
           "ip": "192.168.8.1",
           "netmask": "255.255.255.0"}

# CLI configuration
interface_config = [
    "interface {}".format(gigabit["name"]),
    "description {}".format(gigabit["description"]),
    "ip address {} {}".format(gigabit["ip"], gigabit["netmask"]),
    "no shut"
]
net_connect.enable()
confOutput = net_connect.send_config_set(interface_config)

print(confOutput)

#send command
output3 = net_connect.send_command("show ip int br")

print(output3)