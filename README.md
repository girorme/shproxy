# shproxy

> SHProxy is a user-friendly tool that allows you to access a machine within a specific network, even if you are outside it (imagine a company machine with VPN connection :) ). With SHProxy, you can establish a secure SOCKS tunnel through SSH

Here's how SHProxy works:
---
<p align="center">
  <img src="https://github.com/girorme/shproxy/assets/54730507/b1a3799f-990e-4321-a508-78a0e88211cf" alt="How it works">
</p>

Abstract
---
- Connect to Remote Machine: First, you connect to a remote machine inside the target network using a VPN. This step allows you to access resources and services that are otherwise restricted to the local network.

- Create a Secure SOCKS Tunnel: Once connected to the remote machine, SHProxy sets up a secure SOCKS tunnel through SSH. The tunnel acts as a secure and encrypted pathway between your machine and the remote network, making sure your data stays safe and confidential.

- HTTP Proxy Over SOCKS: After establishing the SSH connection, SHProxy utilizes the powerful [sthp](https://github.com/KaranGauswami/socks-to-http-proxy) binary to create an HTTP proxy over the SOCKS tunnel. This HTTP proxy enables you to navigate the web and access web services as if you were directly connected to the remote network.

With SHProxy, you can seamlessly work with resources and services within the target network, regardless of your physical location. The tool simplifies the process and ensures your data remains secure throughout the entire connection.

Usage
---
Set the ssh configuration + ports you want to use socks + http proxy in configuration tab:
<p>
  <img src="https://github.com/girorme/shproxy/assets/54730507/312102ad-19b0-40ba-a497-8df14517d4b7" alt="Main Screen">
</p>

In the main screen start the socks proxy, wait and start the http proxy:
<p>
  <img src="https://github.com/girorme/shproxy/assets/54730507/613dbbb5-10a3-4c2d-8f0f-ca66a4c2d6a2" alt="Main Screen">
</p>

Now you need to set `127.0.0.1:socks_port_used_in_config` to socks5 in proxy configuration (browser, other programs) or `127.0.0.1:http_port_used_in_config` to http, in my example:

![image](https://github.com/girorme/shproxy/assets/54730507/ceba324a-a590-443f-a7dc-87aed9bfd0a4)

Download
---
- [Link](https://github.com/girorme/shproxy/releases/tag/1.0.0)


> This tool was built for research purposes, the creator is not responsible for misuse
