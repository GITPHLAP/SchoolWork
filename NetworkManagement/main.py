from netmiko import (
    ConnectHandler,
    NetmikoTimeoutException,
    NetmikoAuthenticationException,
)

cisco_device = {
    "device_type": "cisco_ios",
    "host": "172.16.4.2",
    "username": "admin",
    "password": "Kode1234",
    "secret": "",
}

# create connection to cisco device
net_connect = ConnectHandler(**cisco_device)

#send command
output = net_connect.send_command("show ip int br")

print(output)



# New interface details 
gigabit = {"name": "g0/1",
           "description": "gigabit interface description",
           "ip": "172.16.8.2",
           "netmask": "255.255.0.0"}

# CLI configuration
interface_config = [
    "interface {}".format(gigabit["name"]),
    "description {}".format(gigabit["description"]),
    "ip address {} {}".format(gigabit["ip"], gigabit["netmask"]),
    "no shut"
]

confOutput = net_connect.send_config_set(interface_config)

print(confOutput)