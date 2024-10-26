If you don't want to update your trust store, you can generate a self-signed certificate.
1. Generate a private key and a Certificate Signing Request (CSR):
```
openssl req -newkey rsa:2048 -nodes -keyout key.pem -out csr.pem -subj "/C=AU/ST=Vic/L=Melbourne/O=Mtls Demo Client/CN=mtlsdemo.client"
```

2. Generate a self-signed certificate using the CSR and the private key:
```
openssl x509 -req -days 365 -in csr.pem -signkey key.pem -out cert.pem
```

3. Convert the generated certificate into PKCS12 format:
```
openssl pkcs12 -export -out cert.pfx -inkey key.pem -in cert.pem -passout pass:Password01
```
