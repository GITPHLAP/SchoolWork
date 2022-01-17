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

output2 = net_connect.send_config_set("int g0/1")
# output2 += net_connect.send_config_set("ip address 1.1.1.1 255.0.0.0")
output2 += net_connect.save_config()

print(output)
print(output2)