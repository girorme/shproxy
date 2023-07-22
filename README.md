# sh-proxy

> SHProxy is a user-friendly tool that allows you to access a machine within a specific network, even if you are outside it (imagine a job machine with VPN connection :) ). With SHProxy, you can establish a secure SOCKS tunnel through SSH

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

> This tool was built for research purposes, the creator is not responsible for misuse
