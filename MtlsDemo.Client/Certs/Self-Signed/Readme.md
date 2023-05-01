Apparently, only self-signed client certs will work.
1. Generate a private key and a certificate signing request (CSR):
```
openssl req -newkey rsa:2048 -nodes -keyout key.pem -out csr.pem -subj "/C=AU/ST=Vic/L=Melbourne/O=Mtls Demo Client/CN=mtlsdemo.client"
```

2. Generate a self-signed certificate using the CSR and private key:
```
openssl x509 -req -days 365 -in csr.pem -signkey key.pem -out cert.pem
```

3. Convert the private key and the certificate to a PKCS12 file:
```
openssl pkcs12 -export -out cert.pfx -inkey key.pem -in cert.pem --passout pass:Password01
```
