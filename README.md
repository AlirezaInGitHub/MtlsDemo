# mTLS Demo

## Description
This project serves as a demonstration of mutual TLS (mTLS) implementation. Mutual TLS is a security protocol that requires both the client and the server to authenticate each other before establishing a secure communication channel. This ensures a higher level of security compared to traditional TLS, where only the server is authenticated.

## Installation & Run

To run this project locally, follow these steps:

1. `git clone https://github.com/AlirezaInGitHub/MtlsDemo.git)https://github.com/AlirezaInGitHub/MtlsDemo.git`
2. `cd .\MtlsDemo.Client`
3. Follow the instructions in ./Certs/Readme.md
4. Follow the instructions in ./Certs/Self-Signed/Readme.md
5. `dotnet run .\MtlsDemo.Client.csproj`
6. `cd ..`
7. `cd .\MtlsDemo.Server`
8. `dotnet run .\MtlsDemo.Server.csproj`
